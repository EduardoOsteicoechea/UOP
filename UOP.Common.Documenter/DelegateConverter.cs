using Newtonsoft.Json;
using System;

namespace UOP.Common.Documenter
{
	internal class DelegateConverter : Newtonsoft.Json.JsonConverter
	{
		public override bool CanConvert(System.Type objectType)
		{
			return typeof(System.Linq.Expressions.Expression).IsAssignableFrom(objectType);
		}

		public override void WriteJson
		(
			Newtonsoft.Json.JsonWriter writer,
			System.Object value,
			Newtonsoft.Json.JsonSerializer serializer
		)
		{
			if (value is System.Linq.Expressions.Expression exp)
			{
				writer.WriteValue(exp.ToString());
			}
			else
			{
				writer.WriteNull();
			}
		}

		public override object ReadJson
		(
			Newtonsoft.Json.JsonReader reader,
			System.Type o,
			System.Object v,
			Newtonsoft.Json.JsonSerializer s
		) => throw new NotImplementedException();
	}
}
