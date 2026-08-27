# CodePlus.UniversalLogger

A lightweight logging wrapper for .NET applications, built on top of [Serilog](https://serilog.net/).

`CodePlus.UniversalLogger` provides a simple and consistent logging API while keeping the underlying logging implementation centralized and configurable.

## Features

- Simple logging API
- Built on top of Serilog
- Supports `Debug`, `Info`, `Warn`, `Error`, and `Fatal`
- Supports exception logging
- Supports Console logging
- Supports File logging
- Supports asynchronous logging
- Configuration through `appsettings.json`
- ASP.NET Core integration
- Supports .NET Standard 2.0
- Supports .NET 6.0

## Installation

Install the package using the .NET CLI:

```bash
dotnet add package CodePlus.UniversalLogger

## ASP.NET Core Integration

For ASP.NET Core applications, `CodePlus.UniversalLogger` provides an extension method to configure logging during application startup.

### Basic Logging

```csharp
Log.Info("Application started");

Log.Debug("Processing transaction");

Log.Warn("Transaction is taking longer than expected");

Log.Error("Failed to process transaction");

Log.Fatal("Application cannot continue");


### .NET 6+

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseLogHelper();

var app = builder.Build();