using System.IO;

namespace UOP.Common.Documenter
{
	public class WPFDOCUMENTER
	{
		private string DocumentationDirectoryPath { get; set; }
		private string DocumentationDirectoryTestsDirectoryPath { get; set; }
		private string DocumentationDirectoryChatHistoryDirectoryPath { get; set; }
		public Newtonsoft.Json.JsonSerializerSettings SerializerSettings { get; set; }

		public WPFDOCUMENTER
		(
			string documentationDirectoryPath,
			string documentationDirectoryTestsDirectoryPath,
			string documentationDirectoryChatHistoryDirectoryPath
		)
		{
			DocumentationDirectoryPath = documentationDirectoryPath;
			DocumentationDirectoryTestsDirectoryPath = documentationDirectoryTestsDirectoryPath;
			DocumentationDirectoryChatHistoryDirectoryPath = documentationDirectoryChatHistoryDirectoryPath;
;
			SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
			{
				Converters = {
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

		public void DocumentTest(string fileName, dynamic value)
		{
			string filePath = Path.Combine(DocumentationDirectoryTestsDirectoryPath, $"{fileName}.json");

			string jsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(value, SerializerSettings);

			System.IO.File.AppendAllText(
				 filePath,
				 jsonOutput
			);
		}

		public void DocumentChatHistory(string fileName, dynamic value)
		{
			string filePath = Path.Combine(DocumentationDirectoryChatHistoryDirectoryPath, $"{fileName}.json");

			string jsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(value, SerializerSettings);

			System.IO.File.AppendAllText(
				 filePath,
				 jsonOutput
			);
		}
	}
}
