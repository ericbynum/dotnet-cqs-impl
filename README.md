# CQS Implementation in .NET Web API
This is an idea I'm playing with that resolves a command/query at runtime to make debugging easier while reducing complexity that you get with a typical mediator implementation. The command/query is a single class rather than having a separate handler. It is more like a single-responsibility service class, if anything. The CQS commands/queries can be constructor-injected or resolved from the DI container at runtime. 

This project is the default web api template that gives you an example to get a weather forecast. It has been refactored to apply this CQS technique.

## Command/Query interfaces should extend a simple marker interface to make DI registration automatic.
```csharp
interface IGetWeatherForecastQuery : IQuery
```

## In startup, register all commands/queries in your assembly:
```csharp
services.AddCqs();
```

## Resolve at runtime and execute:
```csharp
var query = _cqsResolver.ResolveQuery<IGetWeatherForecastQuery>();
WeatherForecast[] forecasts = query.Execute(daysToForecast: 5);
```