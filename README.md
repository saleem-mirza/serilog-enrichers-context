# Serilog.Enrichers.Context

Enriches Serilog events with information from the environment variables or user provided custom property.

## Getting started
Install [Serilog.Enriches.Context](https://www.nuget.org/packages/Serilog.Enrichers.Context) from NuGet

```PowerShell
Install-Package Serilog.Enriches.Context
```

Configure logger by calling `.Enrich.WithEnvironment(...)`

```C#
var logger = new LoggerConfiguration()
    .ReadFrom.AppSettings()
    .Enrich.WithEnvironment("OS")
    .CreateLogger();
    
logger.Information("This informational message will enrich with OS name");
```
Assuming you are using compatible sink, in this case `Serilog.Enrich.SQLite`, will write log into database as:

| id | Timestamp | Level | Exception | RenderedMessage | Properties |
| --- | --------- | ----- | --------- | --------------- | ---------- |
| 1 | 2016-09-21 20:37:36.7970616+00:00 | Information | | This informational message will enrich with OS name.|{"OS":{"Value":"Windows_NT"}} |
