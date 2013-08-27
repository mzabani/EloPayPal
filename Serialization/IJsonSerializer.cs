using System;

namespace EloPayPal.Serialization
{
	/// <summary>
	/// This interface must be implemented in order to provide a Json serializer to EloPayPal.
	/// </summary>
	public interface IJsonSerializer
	{
		string Serialize(object obj);
		T Deserialize<T>(string jsonObj);
	}
}