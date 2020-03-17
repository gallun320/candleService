using System;

namespace GraphCandleApp.Utils
{
    public class Logger : ILogger
    {
        public Logger()
        {
            var template = "[{Timestamp:HH:mm:ss.fff} {Level: u3}] {Message: lj}{NewLine}";
            var path = $"./Logs/BiasLog_{DateTime.UtcNow.ToString("dd__MMMM  hh_mm_ss")}.log";
        }


        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        private static Logger _logger;

        public static Logger BaseLog { get { 
                if(_logger == null)
                {
                    _logger = new Logger();
                }
                return _logger;
            } 
        }

    }
}