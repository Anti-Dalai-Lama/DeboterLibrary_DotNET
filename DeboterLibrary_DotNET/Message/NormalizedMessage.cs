using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeboterLibrary
{
    class NormalizedMessage : BaseMessage
    {
        [JsonProperty("photo")]
        public Photo Photo { get; set; }

        public NormalizedMessage(Message message)
        {
            MessageId = message.MessageId;
            From = message.From;
            Chat = message.Chat;
            Date = message.Date;
            Text = message.Text;
            Audio = message.Audio;
            Document = message.Document;
            Sticker = message.Sticker;
            Video = message.Video;
            Voice = message.Voice;
            Location = message.Location;
            Venue = message.Venue;
            Contact = message.Contact;
            Caption = message.Caption;

            Photo = (message.Photo == null) ? null : message.Photo.ToList().Aggregate((ph1, ph2) => ph1.Height.CompareTo(ph2.Height) > 0 ? ph1 : ph2);
        }
    }
}
