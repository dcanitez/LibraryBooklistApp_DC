using System;

namespace LibraryAutomation_DC.Classes
{
    public class Book
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsSale { get; set; }
    }
}