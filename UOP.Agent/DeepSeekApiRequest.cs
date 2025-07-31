using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UOP.Agent
{
	public class DeepSeekApiRequest
	{
		private string ApiKey { get; set; }
		private List<ChatMessage> PreviousChatMessages { get; set; }

		public DeepSeekApiRequest
		(
			string apiKey,
			List<ChatMessage> previousChatMessages
		)
		{
			ApiKey = apiKey;
			PreviousChatMessages = previousChatMessages;
		}

		public async Task<string> GetResponse()
		{
			using (HttpClient httpClient = new HttpClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(
					HttpMethod.Post,
					"https://api.deepseek.com/chat/completions"
				);

				httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
					"Bearer",
					ApiKey
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
					Content = $"Analyze carefully this set of tests groups. Each test group represent a revit api algorith execution. On each execution each step is analysed. Evaluate each execution in particular, and arrive to a conslusion about the overall progress of the development progress.",
				};

				List<ChatMessage> chatMessages = new List<ChatMessage>();
				chatMessages.Add(apiContiguratorMessage);
				chatMessages.AddRange(PreviousChatMessages.ToList());
				deepSeekRequestBodyModel.Messages = chatMessages.ToArray();

				string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(deepSeekRequestBodyModel);

				httpRequestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

				using (HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage))
				{
					string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
					DeepSeekResponseModel deepSeekResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DeepSeekResponseModel>(apiResponse);

					DeepSeekResponseChoiceModel choice = deepSeekResponseModel.Choices.FirstOrDefault();
					return choice.Message.Content;
				}
			}
		}
	}
}
