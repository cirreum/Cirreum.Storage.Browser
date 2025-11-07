namespace Cirreum.Storage;

using System.Text.Json.Serialization.Metadata;

/// <summary>
/// The Serializer for use with <see cref="IAsyncStorageService"/>.
/// </summary>
public interface IStorageSerializer {

	/// <summary>
	/// Serialize the specified object to Json text.
	/// </summary>
	/// <typeparam name="T">The Type of object being serialized.</typeparam>
	/// <param name="obj">The object to serialize.</param>
	/// <returns>The Json text.</returns>
	string Serialize<T>(T obj);

	/// <summary>
	/// Serialize the specified object to Json text.
	/// </summary>
	/// <typeparam name="T">The Type of object being serialized.</typeparam>
	/// <param name="obj">The object to serialize.</param>
	/// <param name="typeInfo">The <see cref="JsonTypeInfo"/> for the <typeparamref name="T"/> type.</param>
	/// <returns>The Json text.</returns>
	string Serialize<T>(T obj, JsonTypeInfo<T> typeInfo);

	/// <summary>
	/// Deserialize the specified string into the requested &lt;T&gt; type
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> to deserialize to.</typeparam>
	/// <param name="text">The Json text to deserialize.</param>
	/// <returns>The deserialized object if deserialization was successful; otherwise null.</returns>
	T? Deserialize<T>(string text);

	/// <summary>
	/// Deserialize the specified string into the requested &lt;T&gt; type
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> to deserialize to.</typeparam>
	/// <param name="text">The Json text to deserialize.</param>
	/// <param name="typeInfo">The <see cref="JsonTypeInfo"/> for the <typeparamref name="T"/> type.</param>
	/// <returns>The deserialized object if deserialization was successful; otherwise null.</returns>
	T? Deserialize<T>(string text, JsonTypeInfo<T> typeInfo);

}