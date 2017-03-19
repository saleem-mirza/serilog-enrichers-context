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

using System;
using System.Collections.Generic;
using Serilog.Configuration;
using Serilog.Enrichers;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add enrichers for <see cref="System.Environment"/>.
    /// capabilities.
    /// </summary>
    public static class ContextLoggerConfigurationExtension
    {
        /// <summary>
        /// Enrich log events with a specified property.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <param name="keyValue">User provided key value pair to enrich with.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithProperty(this LoggerEnrichmentConfiguration enrichmentConfiguration, KeyValuePair<string, object> keyValue)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));

            if (keyValue.Equals(default(KeyValuePair<string, object>)))
            {
                throw new ArgumentNullException(nameof(keyValue));
            }

            return enrichmentConfiguration.With(new KeyValueEnricher(keyValue));
        }

        /// <summary>
        /// Enrich log events with a Environment variables.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <param name="environmentVariable">Environment variable to enrich with.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithEnvironment(this LoggerEnrichmentConfiguration enrichmentConfiguration, string environmentVariable)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With(new EnvironmentEnricher(environmentVariable));
        }

        /// <summary>
        /// Enrich log events with a machine name.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithMachineName(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));

            return enrichmentConfiguration.With(new KeyValueEnricher(new KeyValuePair<string, object>("MachineName", Environment.MachineName)));
        }

        /// <summary>
        /// Enrich log events with a user name.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithUserName(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));

            return enrichmentConfiguration.With(new KeyValueEnricher(new KeyValuePair<string, object>("UserName", Environment.GetEnvironmentVariable("UserName"))));
        }
    }
}
