using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
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
            var loggerDebugFactory = new LoggerFactory();
            loggerDebugFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            loggerDebugFactory.ConfigureNLog("nlog.config");
            ILogger<Program> _logger = loggerDebugFactory.CreateLogger<Program>();
            _logger.LogDebug(20, "Doing hard work! {Action}", "abc");
            var a = 10;
            var b = 0;
            try
            {
                a = a / b;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this is a test");
            }

            var messagesReceived = 0;           
            
            using (var client = new RedisClient("localhost", 6379, "12345^"))
            {
                // redis type string.
                client.Set("student1", new Student
                {
                    Id = 1,
                    Name = "Nghiep",
                    CreatedDate = DateTimeOffset.Now
                });
                // redis type hash.
                client.StoreAsHash<Student>(new Student
                {
                    Id = 4,
                    Name = "Nghiep",
                    CreatedDate = DateTimeOffset.Now
                });

                var fromHash = client.GetFromHash<Student>(1);
                if (fromHash.Id != 0)
                {
                    //Exited in cache
                }

                // redis type list.
                var redis = client.As<Student>();
                var studentKey = string.Format("student:id:{0}", 1);
                var student = new Student { Id = 1, Name = "Nghiep", CreatedDate = DateTimeOffset.Now};
                redis.SetValue(studentKey, student);

                var student1 = client.Get<Student>("student1");

                using (var subscription = client.CreateSubscription())
                {
                    subscription.OnSubscribe = channel =>
                    {
                        _logger.LogDebug(string.Format("Subscribed to '{0}'", channel));
                    };
                    subscription.OnUnSubscribe = channel =>
                    {
                        _logger.LogDebug(string.Format("UnSubscribed from '{0}'", channel));
                    };
                    subscription.OnMessage = (channel, msg) =>
                    {
                        _logger.LogDebug(string.Format("Received '{0}' from channel '{1}'", msg, channel));
                        
                        //As soon as we've received all 5 messages, disconnect by unsubscribing to all channels
                        if (++messagesReceived == PublishMessageCount)
                        {
                            subscription.UnSubscribeFromAllChannels();
                        }
                    };

                    ThreadPool.QueueUserWorkItem(x =>
                    {
                        Thread.Sleep(200);
                        _logger.LogDebug("Begin publishing messages...");

                        using (var redisPublisher = new RedisClient("localhost", 6379, "12345^"))
                        {
                            for (var i = 1; i <= PublishMessageCount; i++)
                            {
                                var message = MessagePrefix + i;
                                _logger.LogDebug(string.Format("Publishing '{0}' to '{1}'", message, ChannelName));
                                redisPublisher.PublishMessage(ChannelName, message);
                                Thread.Sleep(2000);
                            }
                        }
                    });

                    _logger.LogDebug(string.Format("Started Listening On '{0}'", ChannelName));
                    subscription.SubscribeToChannels(ChannelName); //blocking
                }
            }
            Console.ReadLine();
            
        }

        
    }
}
