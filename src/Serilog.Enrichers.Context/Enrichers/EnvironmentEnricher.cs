// Copyright 2016 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Serilog.Enrichers
{
    using System;
    using Core;
    using Events;

    internal class EnvironmentEnricher : ILogEventEnricher
    {
        private readonly string _environmentVariable;
        private readonly string _propertyName;

        public EnvironmentEnricher(string variableName, string propertyName = null)
        {
            _environmentVariable = variableName;
            _propertyName = propertyName ?? variableName;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var envValue = Environment.GetEnvironmentVariable(_environmentVariable);
            if (!string.IsNullOrWhiteSpace(envValue))
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(_propertyName, envValue));
            }            
        }
    }
}
