namespace Cirreum.Storage;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;

public interface IAsyncStorageService {

	/// <summary>
	/// Initialize the JS Interop
	/// </summary>
	/// <returns>An awaitable <see cref="ValueTask"/></returns>
	ValueTask InitializeAsync();

	/// <summary>
	/// Clears all data from storage.
	/// </summary>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	Task ClearAsync();

	/// <summary>
	/// Retrieve the specified data from storage as a <typeparamref name="T"/>.
	/// </summary>
	/// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
	/// <returns>The data from the specified <paramref name="key"/> as a <typeparamref name="T"/></returns>
	Task<T?> GetItemAsync<T>(string key);

	/// <summary>
	/// Retrieve the specified data from storage as a <typeparamref name="T"/>.
	/// </summary>
	/// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
	/// <param name="typeInfo">The <see cref="JsonTypeInfo"/> for the <typeparamref name="T"/> type.</param>
	/// <returns>The data from the specified <paramref name="key"/> as a <typeparamref name="T"/></returns>
	Task<T?> GetItemAsync<T>(string key, JsonTypeInfo<T> typeInfo);

	/// <summary>
	/// Retrieve the specified data from storage as a <see cref="string"/>.
	/// </summary>
	/// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
	/// <returns>The data associated with the specified <paramref name="key"/> as a <see cref="string"/></returns>
	Task<string?> GetItemAsStringAsync(string key);

	/// <summary>
	/// Return the name of the key at the specified <paramref name="index"/>.
	/// </summary>
	/// <param name="index"></param>
	/// <returns>The name of the key at the specified <paramref name="index"/></returns>
	Task<string?> KeyAsync(int index);

	/// <summary>
	/// Checks if the <paramref name="key"/> exists in storage, but does not check its value.
	/// </summary>
	/// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
	/// <returns>Boolean indicating if the specified <paramref name="key"/> exists</returns>
	Task<bool> ContainsKeyAsync(string key);

	/// <summary>
	/// The number of items stored in storage.
	/// </summary>
	/// <returns>The number of items stored in storage</returns>
	Task<int> LengthAsync();

	/// <summary>
	/// Get the keys of all items stored in storage.
	/// </summary>
	/// <returns>The keys of all items stored in storage</returns>
	Task<IEnumerable<string>> KeysAsync();

	/// <summary>
	/// Remove the data with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to remove</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	Task RemoveItemAsync(string key);

	/// <summary>
	/// Removes a collection of <paramref name="keys"/>.
	/// </summary>
	/// <param name="keys">A IEnumerable collection of strings specifying the name of the storage slot to remove</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	Task RemoveItemsAsync(IEnumerable<string> keys);

	/// <summary>
	/// Sets or updates the <paramref name="data"/> in storage with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
	/// <param name="data">The data to be saved</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	Task SetItemAsync<T>(string key, T data);

	/// <summary>
	/// Sets or updates the <paramref name="data"/> in storage with the specified <paramref name="key"/>.
	/// </summary>
	/// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
	/// <param name="data">The data to be saved</param>
	/// <param name="typeInfo">The <see cref="JsonTypeInfo"/> for the <typeparamref name="T"/> type.</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	Task SetItemAsync<T>(string key, T data, JsonTypeInfo<T> typeInfo);

	/// <summary>
	/// Sets or updates the <paramref name="data"/> in storage with the specified <paramref name="key"/>. Does not serialize the value before storing.
	/// </summary>
	/// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
	/// <param name="data">The string to be saved</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	Task SetItemAsStringAsync(string key, string data);

	event EventHandler<StorageChangingEventArgs> Changing;
	event EventHandler<StorageChangedEventArgs> Changed;
}

public interface ILocalStorageService : IAsyncStorageService {

}

public interface ISessionStorageService : IAsyncStorageService {

}
