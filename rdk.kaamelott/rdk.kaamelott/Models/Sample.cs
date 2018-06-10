using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace rdk.kaamelott.Models
{
    public class Sample
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("episode")]
        public string Episode { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonIgnore]
        public string ImageCharacter
        {
            get
            {
                return Character.ToLower().Trim()
                    .Replace("é", "e")
                    .Replace("è", "e")
                    .Replace("ê", "e")
                    .Replace("î", "i")
                    .Replace(@"'", "")
                    .Replace(@" ", "")
                    + ".png";

            }
        }

        public override string ToString()
        {
            return Title + " - " + Character + " - " + Episode;
        }
    }
}
