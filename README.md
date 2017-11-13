# xch
A minimal currency converter &amp; viewer written in ASP.NET Core &amp; React

## Prerequisites
#### .NET Core SDK
Built with v2.0.2, but v1.1.4 should be ok as well
#### npm
Built with 5.5.1
#### optional: Visual Studio 2017
With "ASP.NET and web development" or ".NET Core cross-platform development" workload

## Local Setup
```
dotnet run
```

## Technology stack

### Server:
- .NET Core 1.1 runtime
- ASP.NET Core 1.1 for server side web
- xUnit & Moq for unit testing

### Client:
- TypeScript
- Webpack
- React
- [Highcharts](https://github.com/highcharts/highcharts) and [react-highcharts](https://github.com/kirjs/react-highcharts) for data presentation
- es6-promise to make stuff work in IE11
