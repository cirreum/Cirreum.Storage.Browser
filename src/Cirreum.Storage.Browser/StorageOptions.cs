namespace Cirreum.Storage;

using System.Text.Json;

/// <summary>
/// Contains options related to Storage.
/// </summary>
public class StorageOptions {
	/// <summary>
	/// Gets or sets the json serializer options.
	/// </summary>
	/// <value>
	/// The json serializer options.
	/// </value>
	/// <remarks>
	/// Defaults to <see cref="JsonSerializerOptions.Default"/>.
	/// </remarks>
	public JsonSerializerOptions JsonSerializerOptions { get; set; } = JsonSerializerOptions.Default;
}