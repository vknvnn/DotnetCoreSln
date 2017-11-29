using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SercurityUtility
{
    public class RSAHelper
    {
        #region Private Fields
        private const int KEY_SIZE = 384; // keysize mặc định minimum.
        private const int BLOCK_BYTES = KEY_SIZE / 8; // block byte mặc định minimum 48.
        private bool fOAEP = false;// Mã hóa đối xứng tối ưu mặc định là false
        private RSACryptoServiceProvider rsaProvider = null;
        #endregion

        public bool IsOAEP { get; }
        public int KeySize { get; }
        public int BlockBytes { get; set; }
        #region Constructors

        /// <summary>
        /// số kí tự cho một block khi dung RSA là (((keySize - 384) / 8) + 37) / 2
        /// số kí tự khi sử dụng keySize minimum 384 là: (((384 - 384) / 8) + 37) / 2  = 18 kí tự;
        /// số kí tự khi sử dụng keySize minimum 2048 + 384 là: (((2048) / 8) + 37) / 2  = 146 kí tự;
        /// số kí tự khi sử dụng keysize maximum 16384 là: (((16384 - 384) / 8) + 37) / 2  = 64018 kí tự;
        /// </summary>
        /// <param name="keySize">KeySize >= 384 && KeySize <= 16384 && KeySize % 8 == 0;</param>
        /// <param name="isOAEP">Mã hóa đối xứng tối ưu mặc định là false</param>
        /// 
        public RSAHelper(int keySize = 384, bool isOAEP = false)
        {
            if (keySize < 384 && keySize > 16384 && keySize % 8 != 0)
            {
                throw new ArgumentException("keySize is invaled.");
            }
            IsOAEP = fOAEP;
            KeySize = keySize;
            BlockBytes = keySize / 8;
        }
        #endregion

        #region Private Methods


        private CspParameters GetCspParameters()
        {
            // Create a new key pair on target CSP
            CspParameters cspParams = new CspParameters();
            cspParams.ProviderType = 1; // PROV_RSA_FULL            
            cspParams.KeyNumber = (int)KeyNumber.Signature;
            return cspParams;
        }

        private byte[] Encrypt(string publicKey, byte[] plainTextBytes)
        {
            int maxLength = GetMaxDataLength();
            if (plainTextBytes.Length > maxLength)
                throw new ArgumentException("Maximum data length is " + maxLength);

            if (string.IsNullOrWhiteSpace(publicKey))
                throw new ArgumentException("Key is null or empty");
            CspParameters cspParams = GetCspParameters();
            cspParams.Flags = CspProviderFlags.NoFlags;
            rsaProvider = new RSACryptoServiceProvider(KeySize, cspParams);
            rsaProvider.FromXmlString(publicKey);
            return rsaProvider.Encrypt(plainTextBytes, IsOAEP);
        }

        private string Encrypt(string publicKey, string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentException("Data are empty");
            var plainBytes = Encoding.Unicode.GetBytes(plainText);
            var encryptedBytes = Encrypt(publicKey, plainBytes);
            return Convert.ToBase64String(encryptedBytes);
        }

        private string EncryptUnlimited(string publicKey, string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentException("Data are empty");

            var plainBytes = Encoding.Unicode.GetBytes(plainText);
            int maxLength = GetMaxDataLength();
            if (plainBytes.Length <= maxLength)
            {
                return Encrypt(publicKey, plainText);
            }
            else
            {
                List<byte> listEncryptResult = new List<byte>();
                int lastBlockSize = plainBytes.Length % maxLength;
                var totalBlock = (plainBytes.Length / maxLength) + (lastBlockSize > 0 ? 1 : 0);
                for (int i = 0; i < totalBlock; i++)
                {
                    byte[] plainTextBlock, encryptedBlockBytes;
                    if (i + 1 < totalBlock)
                    {
                        plainTextBlock = new byte[maxLength];
                        Array.Copy(plainBytes, i * maxLength, plainTextBlock, 0, maxLength);
                        encryptedBlockBytes = Encrypt(publicKey, plainTextBlock);
                        listEncryptResult.AddRange(encryptedBlockBytes);
                    }
                    else
                    {
                        plainTextBlock = new byte[lastBlockSize];
                        Array.Copy(plainBytes, i * maxLength, plainTextBlock, 0, lastBlockSize);
                        encryptedBlockBytes = Encrypt(publicKey, plainTextBlock);
                        listEncryptResult.AddRange(encryptedBlockBytes);
                    }
                }
                return Convert.ToBase64String(listEncryptResult.ToArray());
            }
        }

        private byte[] Decrypt(string privateKey, byte[] encryptedBytes)
        {
            if (string.IsNullOrWhiteSpace(privateKey))
                throw new ArgumentException("Key is null or empty");

            CspParameters cspParams = GetCspParameters();
            cspParams.Flags = CspProviderFlags.NoFlags;
            rsaProvider = new RSACryptoServiceProvider(KeySize, cspParams);
            rsaProvider.FromXmlString(privateKey);
            return rsaProvider.Decrypt(encryptedBytes, IsOAEP);
        }
        private string Decrypt(string privateKey, string encryptedText)
        {
            if (string.IsNullOrWhiteSpace(encryptedText))
                throw new ArgumentException("Data are empty");
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var plainBytes = Decrypt(privateKey, encryptedBytes);
            return Encoding.Unicode.GetString(plainBytes);

        }

        private string DecryptUnlimited(string privateKey, string encryptedText)
        {
            if (string.IsNullOrWhiteSpace(encryptedText))
                throw new ArgumentException("Data are empty");

            var encryptedBytes = Convert.FromBase64String(encryptedText);
            if (encryptedBytes.Length <= BlockBytes)
            {
                return Decrypt(privateKey, encryptedText);
            }
            else
            {
                if (encryptedBytes.Length % BlockBytes != 0)
                    throw new Exception("the encryptedText is invaled.");

                List<byte> listDecryptResult = new List<byte>();
                var totalBlock = (encryptedBytes.Length / BlockBytes);
                for (int i = 0; i < totalBlock; i++)
                {
                    byte[] encryptedBlock = new byte[BlockBytes];
                    Array.Copy(encryptedBytes, i * BlockBytes, encryptedBlock, 0, BlockBytes);
                    byte[] plainBlockBytes = Decrypt(privateKey, encryptedBlock);
                    listDecryptResult.AddRange(plainBlockBytes);
                }
                return Encoding.Unicode.GetString(listDecryptResult.ToArray());
            }
        }
        #endregion

        #region Public Methods
        /// <summary>  
        /// Gets the maximum data length for a given key  
        /// </summary>         
        /// <param name="keySize">The RSA key length  
        /// <returns>The maximum allowable data length</returns>  
        public int GetMaxDataLength()
        {
            if (IsOAEP)
                return ((KeySize - 384) / 8) + 7;
            return ((KeySize - 384) / 8) + 37;
        }


        /// <summary>
        /// Generate a new RSA key pair.
        /// </summary>
        /// <param name="publicKey">An XML string containing ONLY THE PUBLIC RSA KEY.</param>
        /// <param name="privateKey">An XML string containing a PUBLIC AND PRIVATE RSA KEY.</param>
        public void GenerateKeys(out string publicKey, out string privateKey)
        {
            try
            {
                CspParameters cspParams = GetCspParameters();
                cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
                rsaProvider = new RSACryptoServiceProvider(KeySize, cspParams);

                // Export public key
                publicKey = rsaProvider.ToXmlString(false);

                // Export private/public key pair 
                privateKey = rsaProvider.ToXmlString(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception generating a new RSA key pair! More info: " + ex.Message);
            }

        }


        /// <summary>
        /// Encrypts data with plainText.
        /// </summary>
        /// <param name="publicKey">An XML string containing the public RSA key.</param>
        /// <param name="plainText">The data to be encrypted.</param>
        /// <param name="isLimited">if isLimited is true, let using GetMaxDataLength to detail.</param>
        /// <returns>The encrypted data.</returns>
        public string Encrypt(string publicKey, string plainText, bool isLimited = true)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentException("Data are empty");
            string encryptedText = "";

            try
            {
                if (isLimited)
                {
                    encryptedText = Encrypt(publicKey, plainText);
                }
                else
                {
                    encryptedText = EncryptUnlimited(publicKey, plainText);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception encrypting file! More info: " + ex.Message);
            }

            return encryptedText;

        }

        /// <summary>
        /// Encrypts data with plainText limited, let using GetMaxDataLength to detail.
        /// </summary>
        /// <param name="privateKey">An XML string containing a public and private RSA key.</param>
        /// <param name="encryptedText">The data to be decrypted.</param>
        /// <param name="isLimited">if isLimited is true, let using GetMaxDataLength to detail.</param>
        /// <returns>The decrypted data, which is the original plain text before encryption.</returns>
        public string Decrypt(string privateKey, string encryptedText, bool isLimited = true)
        {
            if (string.IsNullOrWhiteSpace(encryptedText))
                throw new ArgumentException("Data are empty");

            string plainText = "";

            try
            {
                if (isLimited)
                {
                    plainText = Decrypt(privateKey, encryptedText);
                }
                else
                {
                    plainText = DecryptUnlimited(privateKey, encryptedText);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception decrypting file! More info: " + ex.Message);
            }
            return plainText;

        }

        #endregion
        
    }
    
}
