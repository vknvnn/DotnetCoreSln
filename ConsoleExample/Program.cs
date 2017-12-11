using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace ConsoleExample
{
    public enum Abc
    {
        Abc1 = 1,
        Abc2 = 2,
        Abc3 = 3,
    }
    class Program
    {
        static void Main(string[] args)
        {
            var servicesProvider = BuildDi();
            var runner = servicesProvider.GetRequiredService<Runner>();

            runner.DoAction("Action1");
            Console.WriteLine(Abc.Abc1.ToString());
            Console.WriteLine("Press ANY key to exit");
            Console.ReadLine();
        }


        private static IServiceProvider BuildDi()
        {
            var services = new ServiceCollection();

            //Runner is the custom class
            services.AddTransient<Runner>();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //configure NLog
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            loggerFactory.ConfigureNLog("nlog.config");

            return serviceProvider;
        }
    }


    public class Runner
    {
        private readonly ILogger<Runner> _logger;

        public Runner(ILogger<Runner> logger)
        {
            _logger = logger;
        }

        public void DoAction(string name)
        {
            _logger.LogDebug(20, "Doing hard work! {Action}", name);
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
            
        }


    }
}
