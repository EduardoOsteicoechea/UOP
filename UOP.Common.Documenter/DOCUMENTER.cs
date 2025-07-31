using System.IO;

namespace UOP.Common.Documenter
{
	public class DOCUMENTER
	{
		private string DocumentationDirectoryPath { get; set; }
		public Newtonsoft.Json.JsonSerializerSettings SerializerSettings { get; set; }

		public DOCUMENTER
		(
			string documentationDirectoryPath
		)
		{
			DocumentationDirectoryPath = documentationDirectoryPath;
			SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
			{
				Converters = {
						new RevitObjectConverter(),
						new DelegateConverter(),
						new Newtonsoft.Json.Converters.StringEnumConverter(),
					},
				Formatting = Newtonsoft.Json.Formatting.Indented,
			};
		}

		public void Document(string fileName, dynamic value)
		{
			string filePath = Path.Combine(DocumentationDirectoryPath, $"{fileName}.json");

			string jsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(value, SerializerSettings);

			System.IO.File.AppendAllText(
				 filePath,
				 jsonOutput
			);
		}
	}
}
