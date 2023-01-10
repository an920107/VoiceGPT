using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VoiceGPT {
    class ChatGPT {

        public static bool succeed { get; private set; }
        public static string responseText { get; private set; }

        public static async Task Post(string text) {

            succeed = false;

            try {

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", "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");

                var dict = new Dictionary<string, object>();
                dict.Add("model", "text-davinci-003");
                dict.Add("prompt", text);
                dict.Add("max_tokens", 2048);
                dict.Add("temperature", 0);
                dict.Add("top_p", 1);

                HttpContent content = new StringContent(JsonConvert.SerializeObject(dict));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync(
                    "https://api.openai.com/v1/completions", content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                responseText = Regex.Match(responseBody, "\"text\":\".*?\"").ToString();
                responseText = responseText.Substring(12, responseText.Length - 13);
                responseText = Regex.Replace(responseText, "(\\\\n)+", "\n");

            } catch (Exception exception) {
                succeed = false;
                responseText = exception.Message;
            }
        }
    }
}
