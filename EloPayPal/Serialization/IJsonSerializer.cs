using System;
using System.Collections.Generic;

namespace EloPayPal.Serialization
{
	/// <summary>
	/// This interface must be implemented in order to provide a Json serializer to EloPayPal.
	/// </summary>
	/// <remarks>The serialization and deserialization methods need not worry about dates, as EloPayPal converts every single date 
	/// to strings; these methods, however, need to consider IDictionary%lt;string, object%gt; as an object whose keys are parameters 
	/// and whose values are values of these parameters, serializing these as if they were objects.</remarks>
	public interface IJsonSerializer
	{
		string Serialize(object obj);
		T Deserialize<T>(string jsonObj);
	}
}