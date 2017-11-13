# xch
A minimal currency converter &amp; viewer written in ASP.NET Core &amp; React

## Prerequisites
#### .NET Core SDK or Visual Studio 2017
Built with v2.0.2, but v1.1.4 should be ok as well
#### npm
Built with 5.5.1

## Local Setup
#### From command line:
```
dotnet run
```
#### From Visual Studio 2017
"ASP.NET and web development" or ".NET Core cross-platform development" workload is required.

## Technology stack

### Server:
- .NET Core 1.1 runtime
- ASP.NET Core 1.1
- xUnit & Moq for unit testing

### Client:
- TypeScript
- Webpack
- React
- [Highcharts](https://github.com/highcharts/highcharts) and [react-highcharts](https://github.com/kirjs/react-highcharts) for data presentation
- A few bootstrap components of [react-bootstrap](https://react-bootstrap.github.io/) flavour
- es6-promise to make stuff work in IE11

## Tested with browsers
- Google Chrome 62
- IE11
