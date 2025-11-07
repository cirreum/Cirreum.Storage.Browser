# Cirreum Storage for Browsers

[![NuGet Version](https://img.shields.io/nuget/v/Cirreum.Storage.Browser.svg?style=flat-square)](https://www.nuget.org/packages/Cirreum.Storage.Browser/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Cirreum.Storage.Browser.svg?style=flat-square)](https://www.nuget.org/packages/Cirreum.Storage.Browser/)
[![GitHub Release](https://img.shields.io/github/v/release/cirreum/Cirreum.Storage.Browser?style=flat-square)](https://github.com/cirreum/Cirreum.Storage.Browser/releases)

Core abstractions and contracts for browser storage in Blazor applications. This package provides the interfaces, event arguments, and configuration types used by storage implementations.

> **Note:** This package contains only the contracts and abstractions.

## What's Included

This package provides the foundational types for building storage implementations:

### Interfaces

- **`IAsyncStorageService`** - Core storage operations interface
- **`ILocalStorageService`** - Marker interface for localStorage implementations
- **`ISessionStorageService`** - Marker interface for sessionStorage implementations
- **`IStorageSerializer`** - Serialization abstraction for custom serializers

### Event Arguments

- **`StorageChangedEventArgs`** - Event data for storage changes (after operation)
- **`StorageChangingEventArgs`** - Event data for storage changes (before operation, cancellable)

### Configuration

- **`StorageOptions`** - Configuration options including `JsonSerializerOptions`

## Installation

```bash
dotnet add package Cirreum.Storage.Browser
```

## Usage

This package is typically referenced by:

1. **Implementation libraries** (like `Cirreum.Blazor.Components`) that provide concrete implementations
2. **Class libraries** that need to work with storage abstractions without depending on a specific implementation
3. **Applications** when using dependency injection with implementation packages

### In a Class Library

Reference this package to work with storage abstractions:

```csharp
using Cirreum.Storage;

public class UserService
{
    private readonly ILocalStorageService _storage;
    
    public UserService(ILocalStorageService storage)
    {
        _storage = storage;
    }
    
    public async Task SaveUserAsync(User user)
    {
        await _storage.SetItemAsync("current-user", user);
    }
}
```

### In an Application

Install both this package and an implementation package (e.g., `Cirreum.Storage.Browser`):

```bash
dotnet add package Cirreum.Storage.Browser
dotnet add package Cirreum.Blazor.Components
```

Then register the services:

```csharp
// Program.cs
using Cirreum.Storage;

builder.Services.AddLocalStorage();
builder.Services.AddSessionStorage();
```

## API Reference

### IAsyncStorageService

Core interface for storage operations:

```csharp
public interface IAsyncStorageService
{
    // Initialization
    ValueTask InitializeAsync();
    
    // Basic Operations
    Task SetItemAsync<T>(string key, T data);
    Task SetItemAsync<T>(string key, T data, JsonTypeInfo<T> typeInfo);
    Task SetItemAsStringAsync(string key, string data);
    Task<T?> GetItemAsync<T>(string key);
    Task<T?> GetItemAsync<T>(string key, JsonTypeInfo<T> typeInfo);
    Task<string?> GetItemAsStringAsync(string key);
    Task RemoveItemAsync(string key);
    Task RemoveItemsAsync(IEnumerable<string> keys);
    Task ClearAsync();
    
    // Key Management
    Task<bool> ContainsKeyAsync(string key);
    Task<IEnumerable<string>> KeysAsync();
    Task<string?> KeyAsync(int index);
    Task<int> LengthAsync();
    
    // Events
    event EventHandler<StorageChangingEventArgs> Changing;
    event EventHandler<StorageChangedEventArgs> Changed;
}
```

### ILocalStorageService

Marker interface for localStorage implementations:

```csharp
public interface ILocalStorageService : IAsyncStorageService { }
```

### ISessionStorageService

Marker interface for sessionStorage implementations:

```csharp
public interface ISessionStorageService : IAsyncStorageService { }
```

### IStorageSerializer

Interface for custom serialization implementations:

```csharp
public interface IStorageSerializer
{
    string Serialize<T>(T obj);
    string Serialize<T>(T obj, JsonTypeInfo<T> typeInfo);
    T? Deserialize<T>(string text);
    T? Deserialize<T>(string text, JsonTypeInfo<T> typeInfo);
}
```

## Event Arguments

### StorageChangedEventArgs

Provides data for the `Changed` event (fired after a storage operation completes):

```csharp
public class StorageChangedEventArgs
{
    public string Key { get; set; }        // The storage key that changed
    public object? OldValue { get; set; }  // Previous value (null if newly created)
    public object? NewValue { get; set; }  // New value (null if removed)
}
```

### StorageChangingEventArgs

Provides data for the `Changing` event (fired before a storage operation executes):

```csharp
public class StorageChangingEventArgs : StorageChangedEventArgs
{
    public bool Cancel { get; set; }  // Set to true to cancel the operation
}
```

**Example usage:**

```csharp
storage.Changing += (sender, e) =>
{
    if (e.Key == "protected-key")
    {
        e.Cancel = true;  // Prevent the change
    }
};

storage.Changed += (sender, e) =>
{
    Console.WriteLine($"Key '{e.Key}' changed from {e.OldValue} to {e.NewValue}");
};
```

## Configuration

### StorageOptions

Configuration options for storage implementations:

```csharp
public class StorageOptions
{
    public JsonSerializerOptions JsonSerializerOptions { get; set; }
}
```

**Configure in your application:**

```csharp
builder.Services.Configure<StorageOptions>(options =>
{
    options.JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
});
```

## Creating Custom Implementations

To create your own storage implementation:

1. Reference this package
2. Implement `IAsyncStorageService` (or `ILocalStorageService`/`ISessionStorageService`)
3. Optionally implement `IStorageSerializer` for custom serialization
4. Register your implementation in the DI container

```csharp
public class CustomStorageService : ILocalStorageService
{
    public ValueTask InitializeAsync() { /* ... */ }
    public Task SetItemAsync<T>(string key, T data) { /* ... */ }
    // ... implement other methods
}

// Register
builder.Services.AddSingleton<ILocalStorageService, CustomStorageService>();
```

---

**Cirreum Foundation Framework** - Layered simplicity for modern .NET