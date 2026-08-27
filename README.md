# Common Logging

A lightweight logging wrapper built on top of [Serilog](https://serilog.net/).

The library provides a simple and consistent logging API for .NET applications,
while keeping the underlying Serilog implementation isolated from application code.

## Features

- Simple logging API
- Built on Serilog
- Supports `Debug`, `Info`, `Warn`, `Error`, and `Fatal`
- Supports exception logging
- Supports contextual logging with `ForContext<T>()`
- Singleton-based logger instance
- Can be shared across multiple .NET applications

## Usage

### Basic Logging

```csharp
Log.Info("Application started");

Log.Debug("Processing transaction");

Log.Warn("Transaction is taking longer than expected");

Log.Error("Failed to process transaction");

Log.Fatal("Application cannot continue");