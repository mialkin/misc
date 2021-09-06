using System;

namespace FluentValidationTest
{
    public class Word
    {
        public string Name { get; set; }
        public string Translation { get; set; }
        public int LanguageId { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}