using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeboterLibrary
{
    class Sticker
    {
        [JsonProperty("file_id")]
        public string FileId { get; set; }

        [JsonProperty("emoji")]
        public string Emoji { get; set; }

        [JsonProperty("file_size")]
        public long FileSize { get; set; }

    }
}
