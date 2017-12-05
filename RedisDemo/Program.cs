
using ServiceStack.Redis;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisDemo
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
    class Program
    {
        const int PublishMessageCount = 5;
        const string ChannelName = "SimplePubSubCHANNEL";
        const string MessagePrefix = "MESSAGE ";
        static void Main(string[] args)
        {
            var messagesReceived = 0;           
            
            using (var client = new RedisClient("localhost", 6379, "12345^"))
            {
                
                client.Set("student1", new Student
                {
                    Id = 1,
                    Name = "Nghiep",
                    CreatedDate = DateTimeOffset.Now
                });
                client.StoreAsHash<Student>(new Student
                {
                    Id = 4,
                    Name = "Nghiep",
                    CreatedDate = DateTimeOffset.Now
                });

                var fromHash = client.GetFromHash<Student>(1);
                var student1 = client.Get<Student>("student1");

                using (var subscription = client.CreateSubscription())
                {
                    subscription.OnSubscribe = channel =>
                    {
                        Console.WriteLine("Subscribed to '{0}'", channel);
                    };
                    subscription.OnUnSubscribe = channel =>
                    {
                        Console.WriteLine("UnSubscribed from '{0}'", channel);
                    };
                    subscription.OnMessage = (channel, msg) =>
                    {
                        Console.WriteLine("Received '{0}' from channel '{1}'", msg, channel);
                        
                        //As soon as we've received all 5 messages, disconnect by unsubscribing to all channels
                        if (++messagesReceived == PublishMessageCount)
                        {
                            subscription.UnSubscribeFromAllChannels();
                        }
                    };

                    ThreadPool.QueueUserWorkItem(x =>
                    {
                        Thread.Sleep(200);
                        Console.WriteLine("Begin publishing messages...");

                        using (var redisPublisher = new RedisClient("localhost", 6379, "12345^"))
                        {
                            for (var i = 1; i <= PublishMessageCount; i++)
                            {
                                var message = MessagePrefix + i;
                                Console.WriteLine("Publishing '{0}' to '{1}'", message, ChannelName);
                                redisPublisher.PublishMessage(ChannelName, message);
                                Thread.Sleep(2000);
                            }
                        }
                    });

                    Console.WriteLine("Started Listening On '{0}'", ChannelName);
                    subscription.SubscribeToChannels(ChannelName); //blocking
                }
            }
            Console.ReadLine();
            
        }

        
    }
}
