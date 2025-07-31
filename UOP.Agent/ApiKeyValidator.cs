using Newtonsoft.Json;
using System.Net.Http;

namespace UOP.Agent
{
	public class DeepSeekBalanceInfo
	{
		public string Currency { get; set; }
		public string TotalBalance { get; set; }
		public string GrantedBalance { get; set; }
		public string ToppedUpBalance { get; set; }
	}

	public class DeepSeekBalanceResponse
	{
		[JsonProperty("is_available")]
		public bool IsAvailable { get; set; }

		[JsonProperty("balance_infos")]
		public DeepSeekBalanceInfo[] BalanceInfos { get; set; }
	}
	public class ApiKeyValidator
	{
		public bool IsValid { get; set; }
		private string ProvidedKey { get; set; }
		public ApiKeyValidator(string providedKey)
		{
			ProvidedKey = providedKey;
		}
		public async Task<bool> Validate()
		{
			using (HttpClient httpClient = new HttpClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(
					HttpMethod.Get,
					"https://api.deepseek.com/user/balance"
				);

				httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
					"Bearer",
					ProvidedKey
				);

				httpRequestMessage.Headers.Accept.Add(
					new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
				);

				using (HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage))
				{
					string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();

					DeepSeekBalanceResponse deepSeekResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DeepSeekBalanceResponse>(apiResponse);

					if (deepSeekResponse.IsAvailable)
					{
						IsValid = deepSeekResponse.IsAvailable;
						return IsValid;
					}

					return false;
				}
			}
		}
	}
}

