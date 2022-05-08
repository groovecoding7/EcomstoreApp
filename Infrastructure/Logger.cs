using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.RepositoriesContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class Logger
    {

        private static IDictionary<String,ILogger> _loggers;
        private static IServiceCollection _services;

        public static void Initialize(IServiceCollection services)
        {
            _services = services;
        }
        public static ILogger GetLogger(String category)
        {
            if (_services == null)
            {
                throw new ArgumentNullException(nameof(_services));
            }

            if (_loggers == null)
            {
                _loggers = new Dictionary<string, ILogger>();
            }

            if (!_loggers.ContainsKey(category))
            {
                using (ServiceProvider serviceProvider = _services.BuildServiceProvider())
                {
                    ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                    _loggers.Add(category, loggerFactory.CreateLogger(category));
                }

            }
            return _loggers[category];
        }

    }

}
