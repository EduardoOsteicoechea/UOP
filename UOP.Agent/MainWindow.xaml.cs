using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using UOP.Common.Documenter;
using UOP.Common.Locations;
using UOP.Core;

namespace UOP.Agent
{
	public partial class MainWindow : Window
	{
		#region PROPERTIES

		private string InitializationTime { get; set; } = DateTime.Now.ToString("yyyyMMdd_HHmmss");
		private string DocumentationDirectoryPath { get; set; } = Locations.DocumentationDirectoryPath;
		private string FrameworkErrorsDirectoryPath { get; set; } = Locations.FrameworkErrorsDirectoryPath;
		private string DocumentationChatHistoryDirectoryPathPath { get; set; }
		private WPFDOCUMENTER Documenter { get; set; }


		#endregion PROPERTIES
		public MainWindow()
		{
			InitializeComponent();

			DocumentationChatHistoryDirectoryPathPath = Path.Combine(Locations.ChatHistoryDirectoryPath, InitializationTime, "CHAT");

			CreateRequiredDirectories();

			Documenter = new WPFDOCUMENTER(
				"",
				"",
				DocumentationChatHistoryDirectoryPathPath
			);

			ApiKeyInput.Focus();
		}

		private void CreateRequiredDirectories()
		{
			WRAPPER.ManagedCommand(() =>
			{
				CreateDirectoryIfPossible(DocumentationDirectoryPath);
				CreateDirectoryIfPossible(DocumentationChatHistoryDirectoryPathPath);
				CreateDirectoryIfPossible(FrameworkErrorsDirectoryPath);
			});
		}

		private void CreateDirectoryIfPossible(string path)
		{
			WRAPPER.ManagedCommand(() =>
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			});
		}

		public List<ChatMessage> GetExecutionTestsData()
		{
			var executionsDirectories = Directory.GetDirectories(Locations.DocumentationDirectoryPath);

			var allExecutionsTestsGroups = new List<string>();

			foreach (var executionsDirectory in executionsDirectories)
			{
				var executionsTestsDirectories = Directory.GetDirectories(executionsDirectory).ToList();

				foreach (var executionsTestsDirectory in executionsTestsDirectories)
				{
					var executionsTestsFilesPaths = Directory.GetFiles(executionsTestsDirectory).ToList();

					var executionTestsFullDataString = new StringBuilder();

					foreach (var executionsTestsFilePath in executionsTestsFilesPaths)
					{
						var testContentAsText = File.ReadAllText(executionsTestsFilePath);
						executionTestsFullDataString.AppendLine(testContentAsText);
					}

					allExecutionsTestsGroups.Add(executionTestsFullDataString.ToString());
				}
			}

			var chatMessages = new List<ChatMessage>();

			foreach (var executionsTestsGroup in allExecutionsTestsGroups)
			{
				var a = new ChatMessage()
				{
					Role = "user",
					Content = executionsTestsGroup,
				};

				chatMessages.Add(a);
			}


			return chatMessages;
		}

		private async void ApiKeyValidateButton_Click(object sender, RoutedEventArgs e)
		{
			var previousMessages = GetExecutionTestsData();

			ValidatingApiKeyProgressBar.Visibility = Visibility.Visible;

			var apiResponse = "";
			var apiResponseContent = "";

			using (HttpClient httpClient = new HttpClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(
					HttpMethod.Post,
					"https://api.deepseek.com/chat/completions"
				);

				httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
					"Bearer",
					ApiKeyInput.Text
				);

				httpRequestMessage.Headers.Accept.Add(
					new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
				);

				DeepSeekRequestBodyModel deepSeekRequestBodyModel = new DeepSeekRequestBodyModel();
				deepSeekRequestBodyModel.Model = "deepseek-chat";
				deepSeekRequestBodyModel.Stream = false;

				ChatMessage apiContiguratorMessage = new ChatMessage()
				{
					Role = "system",
					Content = @$"Analyze carefully this set of tests groups. Each test group represent a revit api algorith execution. On each execution each step is analysed. Evaluate each execution in particular, and arrive to a conslusion about the overall progress of the development progress.
					
					EXAMPLE INPUT: 
					Which is the highest mountain in the world? Mount Everest.

					EXAMPLE JSON OUTPUT:
					{{
						 ""test_group_final_suggestion_or_conclusion"": ""The core algorithm for regulatory analysis is complete and functioning correctly based on these test results."",
						 ""test_group_overview"": ""The test group represents a sequence of Revit API method executions that successfully collect and analyze building elements (reference planes, levels, and rooms) to determine parking requirements based on Uruguayan building regulations (USAB 2)."",
						 ""successful_tests_number"": ""24"",
						 ""failed_tests_number"": ""0"",
						 ""step_by_step_description_of_the_test_workflow"": [
						  ""Reference Plane Collection & Filtering: Successfully collected 81 reference planes (001-007); Identified specific regulatory planes (L.I.B, L.D.P 1, L.D.P 2, L.D.P F)."",
						  ""Level Analysis: Collected 8 building levels (008-010); Identified 1 basement level (Subsuelo) (011); Confirmed basement existence (012)."",
						  ""Room Analysis: Collected 266 rooms (013-015); Identified parameter for room classification (016); Found 1 car parking space (017,019); Found 1 bicycle parking space (018,020)."",
						  ""Intersection Calculations: Calculated 4 intersection points between regulatory planes (021-024); All intersection coordinates were successfully computed.""
						 ],
						 ""suggestions"": [
						  ""Adding negative test cases"",
						  ""Optimizing performance for large models"",
						  ""Enhancing parameter validation"",
						  ""Potentially expanding the room classification system""
						 ],
						 ""clever_insights"": ""The 'MC_Tipo de Local/Área Descubierta' parameter appears unset in many rooms, which could be validated earlier to prevent potential issues or inconsistencies downstream. The large number of rooms collected (266) suggests that performance optimization for room collection and analysis might become critical for larger models.""
					}}
					",
				};

				List<ChatMessage> chatMessages = new List<ChatMessage>();
				chatMessages.Add(apiContiguratorMessage);
				chatMessages.Add(previousMessages.ToList().LastOrDefault());
				//chatMessages.AddRange(previousMessages.ToList());
				deepSeekRequestBodyModel.Messages = chatMessages.ToArray();

				string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(deepSeekRequestBodyModel);

				httpRequestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

				using (HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage))
				{
					apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
					DeepSeekResponseModel deepSeekResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DeepSeekResponseModel>(apiResponse);

					DeepSeekResponseChoiceModel choice = deepSeekResponseModel.Choices.FirstOrDefault();
					apiResponseContent = choice.Message.Content;
				}
			}

			var cleansedApiResponse = (apiResponseContent.Replace("```json\n", ""));
			cleansedApiResponse = (cleansedApiResponse.Replace("\n```", ""));
			cleansedApiResponse = (cleansedApiResponse.Replace("\\", ""));

			var deserializedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RevitApiAnalysis>(cleansedApiResponse);

			Documenter.DocumentChatHistory("ChatHistory", cleansedApiResponse);

			UpdateUI(deserializedResponse);

			ValidatingApiKeyProgressBar.Visibility = Visibility.Collapsed;
		}

		public void UpdateUI(RevitApiAnalysis content)
		{
			var a = new TestAnalysisControl(content);
			TestsAnalysisTray.Children.Add(a);

			//FlowDocument doc = new FlowDocument();
			//doc.ColumnWidth = Double.NaN;
			//doc.FontSize = 14;
			//doc.FontFamily = new FontFamily("Segoe UI");

			//// Title Section
			//Section titleSection = new Section();
			//Paragraph titleParagraph = new Paragraph(new Run("Revit API Analysis Report"));
			//titleParagraph.FontWeight = FontWeights.Bold;
			//titleParagraph.FontSize = 20;
			//titleParagraph.TextAlignment = TextAlignment.Center;
			//titleParagraph.Margin = new Thickness(0, 0, 0, 10);
			//titleSection.Blocks.Add(titleParagraph);
			//doc.Blocks.Add(titleSection);

			//// Overview Section
			//Section overviewSection = new Section();
			//Paragraph overviewParagraph = new Paragraph();
			//overviewParagraph.Inlines.Add(new Bold(new Run("Overview: ")));
			//overviewParagraph.Inlines.Add(new Run(content.test_group_overview));
			//overviewSection.Blocks.Add(overviewParagraph);
			//doc.Blocks.Add(overviewSection);

			//// Conclusion Section
			//Section conclusionSection = new Section();
			//Paragraph conclusionParagraph = new Paragraph();
			//conclusionParagraph.Inlines.Add(new Bold(new Run("Final Suggestion/Conclusion: ")));
			//conclusionParagraph.Inlines.Add(new Run(content.test_group_final_suggestion_or_conclusion));
			//conclusionSection.Blocks.Add(conclusionParagraph);
			//doc.Blocks.Add(conclusionSection);

			//// Test Results Summary Section
			//Section summarySection = new Section();
			//Paragraph summaryParagraph = new Paragraph();
			//summaryParagraph.Inlines.Add(new Bold(new Run("Test Results Summary:")));
			//summaryParagraph.Inlines.Add(new LineBreak());
			//summaryParagraph.Inlines.Add(new Run($"Successful Tests: {content.successful_tests_number}"));
			//summaryParagraph.Inlines.Add(new LineBreak());
			//summaryParagraph.Inlines.Add(new Run($"Failed Tests: {content.failed_tests_number}"));
			//summarySection.Blocks.Add(summaryParagraph);
			//doc.Blocks.Add(summarySection);

			//// Step-by-Step Workflow Section
			//Section workflowSection = new Section();
			//Paragraph workflowTitle = new Paragraph(new Bold(new Run("Step-by-Step Test Workflow:")));
			//workflowTitle.FontSize = 16;
			//workflowTitle.Margin = new Thickness(0, 10, 0, 5);
			//workflowSection.Blocks.Add(workflowTitle);

			//List workflowList = new List();
			//workflowList.MarkerStyle = TextMarkerStyle.Disc;
			//workflowList.Margin = new Thickness(20, 0, 0, 0);
			//foreach (string step in content.step_by_step_description_of_the_test_workflow)
			//{
			//	ListItem item = new ListItem(new Paragraph(new Run(step)));
			//	workflowList.ListItems.Add(item);
			//}
			//workflowSection.Blocks.Add(workflowList);
			//doc.Blocks.Add(workflowSection);

			//// Suggestions Section
			//Section suggestionsSection = new Section();
			//Paragraph suggestionsTitle = new Paragraph(new Bold(new Run("Suggestions for Improvement:")));
			//suggestionsTitle.FontSize = 16;
			//suggestionsTitle.Margin = new Thickness(0, 10, 0, 5);
			//suggestionsSection.Blocks.Add(suggestionsTitle);

			//List suggestionsList = new List();
			//suggestionsList.MarkerStyle = TextMarkerStyle.Disc;
			//suggestionsList.Margin = new Thickness(20, 0, 0, 0);
			//foreach (string suggestion in content.suggestions)
			//{
			//	ListItem item = new ListItem(new Paragraph(new Run(suggestion)));
			//	suggestionsList.ListItems.Add(item);
			//}
			//suggestionsSection.Blocks.Add(suggestionsList);
			//doc.Blocks.Add(suggestionsSection);

			//// Clever Insights Section
			//Section insightsSection = new Section();
			//Paragraph insightsTitle = new Paragraph(new Bold(new Run("Clever Insights:")));
			//insightsTitle.FontSize = 16;
			//insightsTitle.Margin = new Thickness(0, 10, 0, 5);
			//insightsSection.Blocks.Add(insightsTitle);

			//Paragraph insightsParagraph = new Paragraph(new Run(content.clever_insights));
			//insightsSection.Blocks.Add(insightsParagraph);
			//doc.Blocks.Add(insightsSection);

			//MainTrayChatTray.Document = doc;
		}
	}
}