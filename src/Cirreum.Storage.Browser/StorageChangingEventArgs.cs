namespace Cirreum.Storage;

/// <summary>
/// Provides data for storage changing events that occur before a storage operation is executed.
/// Allows the operation to be cancelled.
/// </summary>
public class StorageChangingEventArgs : StorageChangedEventArgs {
	/// <summary>
	/// Gets or sets a value indicating whether the storage operation should be cancelled.
	/// Set to true to prevent the change from occurring.
	/// </summary>
	public bool Cancel { get; set; }
}