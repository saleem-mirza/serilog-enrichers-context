# Serilog.Enrichers.Context 

Enriches Serilog events with information from the environment variables or user provided custom property.

## Getting started
Install [Serilog.Enrichers.Context](https://www.nuget.org/packages/Serilog.Enrichers.Context) from NuGet

```PowerShell
Install-Package Serilog.Enrichers.Context
```

### Enriching with environment variables.
Configure logger by calling `.Enrich.WithEnvironment(...)`

####Example
```C#
var logger = new LoggerConfiguration()
    .ReadFrom.AppSettings()
    .Enrich.WithEnvironment("OS")
    .CreateLogger();
    
logger.Information("This informational message will enrich with OS name");
```
When a compatible sink is used, in this case [Serilog.Sinks.AzureDocumentDB](https://www.nuget.org/packages/Serilog.Sinks.AzureDocumentDB), following log message will be emitted to DocumentDb:

```JSON
  {
    "Timestamp": "2016-09-22T07:16:34.0314959-04:00",
    "Level": "Information",
    "MessageTemplate": "This informational message will enrich with OS name",
    "Properties": {
      "OS": "Windows_NT"
    },
    "id": "580bce0b-76d4-f510-60f4-70da00636bc3"
  }
```

### Enriching with user defined property.
Configure logger by calling `.Enrich.WithProperty(KeyValuePair<string, object>)`

`.Enrich.WithProperty(...)` is very useful when multiple applications are logging to centralized store and you want to distinguish logs by some unique property for better discoverability.

####Example

```C#
var logger = new LoggerConfiguration()
    .ReadFrom.AppSettings()
    .Enrich.WithProperty(new KeyValuePair<string, object>("applicationId", "demo"));
    .CreateLogger();
    
logger.Information("This informational message will enrich with custom property");    
```

Assuming **AzureDocumentDB** sink is configured, one should see following log message in Azure DocumentDb collection.

```JSON
  {
    "Timestamp": "2016-09-22T07:33:52.9807951-04:00",
    "Level": "Information",
    "MessageTemplate": "This informational message will enrich with custom property",
    "Properties": {
      "applicationId": "demo"
    },
    "id": "9b672004-4e6b-31a2-3e92-5ea49361c312"
  }
```

### Enriching with user defined function or lambda

Enriching logging with user defined function is now supported. Function accepts LogEvent or object type parameter to act on.
It's upto developer's imagination what to do with functions.

####Example

```C#
var logger = new LoggerConfiguration()
    .ReadFrom.AppSettings()
    .Enrich.WithFunction("f0", x => { 
        return x.ToString().ToUpper(); 
     }, Environment.MachineName)
    .Enrich.WithFunction("f1", () => DateTime.Now.Ticks.ToString())
    .Enrich.WithFunction("f2", 
        e => $"{e.Level.ToString().ToUpper()} - {e.Timestamp.Ticks}"
    )
    .CreateLogger();
    
logger.Information("This informational message will enrich with custom property");    
```

[![Build status](https://ci.appveyor.com/api/projects/status/l81s1m0fd8f1y2v4?svg=true)](https://ci.appveyor.com/project/SaleemMirza/serilog-enrichers-context)

---

Many thanks to the [<img src="resources/jetbrains.svg" width="100px"/>](https://www.jetbrains.com "JetBrains") for donating awesome suite of tools making this project possible.