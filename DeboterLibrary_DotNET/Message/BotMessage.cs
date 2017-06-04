using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeboterLibrary
{
    class BotMessage
    {
        [JsonProperty("bot_token")]
        public string BotToken { get; set; }

        [JsonProperty("messages")]
        public NormalizedMessage[] Messages { get; set; }
    }
}
