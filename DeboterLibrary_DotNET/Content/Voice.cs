using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeboterLibrary
{
    class Voice
    {
        [JsonProperty("file_id")]
        public string FileId { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("file_size")]
        public long FileSize { get; set; }

    }
}
