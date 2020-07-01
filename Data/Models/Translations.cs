using System;

namespace Translator.Data.Models
{
    
    public class Translations
    {
        public Guid Id{get; set;}             // x
        public string Word {get; set;}        // bukvalna rec
        public Tags Tag{get; set;}
        public Guid TagId{get; set;}           
        public Languages Languages{get; set;}
        public Guid LanguagesId{get; set;} // Language ID
    }
}
