﻿// Copyright 2016 Serilog Contributors
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
using System.Runtime.InteropServices;
using Serilog.Configuration;
using Serilog.Enrichers;
using Serilog.Events;

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
        /// Enrich log events with a Function.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <param name="key">unique to represent value returned by function</param>
        /// <param name="func">User provided function to execute</param>
        /// <returns>Configuration object allowing method chaining.</returns>

        public static LoggerConfiguration WithFunction(
            this LoggerEnrichmentConfiguration enrichmentConfiguration,
            string key,
            Func<LogEvent, string> func)
        {
            return enrichmentConfiguration.With(new FunctionEnricher(key, func));
        }

        /// <summary>
        /// Enrich log events with a Function.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <param name="key">unique to represent value returned by function</param>
        /// <param name="func">User provided function to execute</param>
        /// <returns>Configuration object allowing method chaining.</returns>

        public static LoggerConfiguration WithFunction(
            this LoggerEnrichmentConfiguration enrichmentConfiguration,
            string key,
            Func<string> func)
        {
            return enrichmentConfiguration.With(new FunctionEnricher(key, func));
        }

        
        /// <summary>
        /// Enrich log events with a Function.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <param name="key">unique to represent value returned by function</param>
        /// <param name="func">User provided function to execute</param>
        /// <param name="parameter">Parameter to pass to user defined function</param>
        /// <returns>Configuration object allowing method chaining.</returns>

        public static LoggerConfiguration WithFunction(
            this LoggerEnrichmentConfiguration enrichmentConfiguration,
            string key,
            Func<object, string> func,
            object parameter)
        {
            return enrichmentConfiguration.With(new FunctionEnricher(key, func, parameter));
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

            var machineName = "COMPUTERNAME";

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                machineName = "HOSTNAME";
            }

            return enrichmentConfiguration.With(new KeyValueEnricher(new KeyValuePair<string, object>("MachineName", Environment.GetEnvironmentVariable(machineName))));
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
