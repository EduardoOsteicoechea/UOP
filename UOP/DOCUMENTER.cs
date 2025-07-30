using System.IO;

namespace UOP
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
			WRAPPER.ManagedCommand(() =>
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
			});
		}

		public void Document(string fileName, dynamic value)
		{
			WRAPPER.ManagedCommand(() =>
			{
				string filePath = Path.Combine(DocumentationDirectoryPath, $"{fileName}.json");

				string jsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(value, SerializerSettings);

				System.IO.File.AppendAllText(
					 filePath,
					 jsonOutput
				);
			});
		}
	}
}
