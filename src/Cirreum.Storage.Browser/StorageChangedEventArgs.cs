namespace Cirreum.Storage;

/// <summary>
/// Provides data for storage change events in the browser storage library.
/// </summary>
public class StorageChangedEventArgs {
	/// <summary>
	/// Gets or sets the key of the storage item that changed.
	/// </summary>
	public string Key { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the previous value before the change, or null if the item was newly created.
	/// </summary>
	public object? OldValue { get; set; }

	/// <summary>
	/// Gets or sets the new value after the change, or null if the item was removed.
	/// </summary>
	public object? NewValue { get; set; }
}