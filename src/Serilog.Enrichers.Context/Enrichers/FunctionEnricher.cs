using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers
{
    internal class FunctionEnricher : ILogEventEnricher
    {
        private readonly string _key;
        private readonly Func<object, string> _f1;
        private readonly Func<string> _f0;
        private readonly Func<LogEvent, string> _f2;
        private readonly object _parameter;

        public FunctionEnricher(string key, Func<string> func)
        {
            _key = key;
            _f0  = func;
        }

        public FunctionEnricher(string key, Func<object, string> func, object parameter)
        {
            _key       = key;
            _f1        = func;
            _parameter = parameter;
        }

        public FunctionEnricher(string key, Func<LogEvent, string> func)
        {
            _key       = key;
            _f2        = func;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            try {
                var result = string.Empty;

                if (_f0 != null) {
                    result = _f0();
                }
                else if (_f1 != null) {
                    result = _f1(_parameter);
                }
                else if (_f2 != null) {
                    result = _f2(logEvent);
                }

                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(_key, result));
            }
            catch (Exception ex) {
                Debugging.SelfLog.WriteLine(ex.Message);
            }
        }
    }
}
