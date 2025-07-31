using Newtonsoft.Json;
using System;

namespace UOP
{
	public class RevitObjectConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return
				typeof(Autodesk.Revit.DB.Element).IsAssignableFrom(objectType) ||
				typeof(Autodesk.Revit.DB.Document).IsAssignableFrom(objectType) ||
				typeof(Autodesk.Revit.DB.Parameter).IsAssignableFrom(objectType);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value is Autodesk.Revit.DB.Element elem)
			{
				writer.WriteValue($"{elem.Name} (Id: {elem.Id})");
				return;
			}

			if (value is Autodesk.Revit.DB.Document doc)
			{
				writer.WriteValue(doc.Title);
				return;
			}

			if (value is Autodesk.Revit.DB.Parameter param)
			{
				string paramValue = param.AsValueString() ?? param.AsString() ?? "No Value";
				writer.WriteValue($"Parameter: {param.Definition.Name} = {paramValue}");
				return;
			}

			writer.WriteValue($"Revit API Object: [{value.GetType().Name}]");
		}

		public override object ReadJson(JsonReader reader, Type o, object v, JsonSerializer s) => throw new NotImplementedException();
	}


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
