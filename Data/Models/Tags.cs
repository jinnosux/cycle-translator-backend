using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Translator.Data.Models
{
    
    public class Tags
    {
        public Guid Id{get; set;}        // x
        public string Name {get; set;}   // key za vrednost specific jezika
        [JsonIgnore]
        public virtual List<Translations> Translations {get; set;}
    }
}

