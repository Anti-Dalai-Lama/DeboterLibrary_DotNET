using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeboterLibrary
{
    public class Bot
    {
        private string BotToken;
        private string apiAddress = "http://localhost:14989";

        public Bot(string token)
        {
            BotToken = token;
        }

        public HttpStatusCode SendUpdatesToDeboterAPI(string telegramUpdates)
        {
            Regex rx = new Regex(@"\\[uU][0-9a-fA-F]{4}");
            telegramUpdates = rx.Replace(telegramUpdates, 
                match => ((char)Int32.Parse(match.Value.Substring(2), NumberStyles.HexNumber)).ToString());

            Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(telegramUpdates);
            Response response = JsonConvert.DeserializeObject<Response>(obj.ToString());

            if(response.Result != null)
            {
                List<NormalizedMessage> messages = new List<NormalizedMessage>();
                response.Result.ToList().ForEach(update => messages.Add(new NormalizedMessage(update.Message)));

                BotMessage bm = new BotMessage() { BotToken = BotToken, Messages = messages.ToArray() };

                HttpWebRequest answer = (HttpWebRequest)WebRequest.Create(apiAddress + "/sendmessages");
                answer.ContentType = "application/json";
                answer.Method = "POST";

                using (var streamWriter = new StreamWriter(answer.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(bm);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                return ((HttpWebResponse)answer.GetResponse()).StatusCode;
            }
            else
            {
                return HttpStatusCode.NoContent;
            }
        }

        public HttpStatusCode SendMessageToDeboterAPI(string telegramMessage)
        {
            Regex rx = new Regex(@"\\[uU][0-9a-fA-F]{4}");
            telegramMessage = rx.Replace(telegramMessage,
                match => ((char)Int32.Parse(match.Value.Substring(2), NumberStyles.HexNumber)).ToString());

            Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(telegramMessage);
            Message message = JsonConvert.DeserializeObject<Message>(obj.GetValue("result").ToString());
            message.From.UserId = 0;
            message.From.FirstName = null;
            message.From.LastName = null;
            message.From.Username = "bot";

            if (message.MessageId != 0)
            {
                List<NormalizedMessage> messages = new List<NormalizedMessage>();
                messages.Add(new NormalizedMessage(message));

                BotMessage bm = new BotMessage() { BotToken = BotToken, Messages = messages.ToArray() };

                HttpWebRequest answer = (HttpWebRequest)WebRequest.Create(apiAddress + "/sendmessages");
                answer.ContentType = "application/json";
                answer.Method = "POST";

                using (var streamWriter = new StreamWriter(answer.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(bm);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                return ((HttpWebResponse)answer.GetResponse()).StatusCode;
            }
            else
            {
                return HttpStatusCode.NoContent;
            }
        }
    }
}
