# Serilog.Enrichers.Context

Enriches Serilog events with information from the environment variables or user provided custom property.

## Getting started
Install [Serilog.Enriches.Context](https://www.nuget.org/packages/Serilog.Enrichers.Context) from NuGet

```PowerShell
Install-Package Serilog.Enriches.Context
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
