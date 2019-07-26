using System;

namespace TaskApp.DTOs
{
    public class CommentDTO
    {
        public string title { get; set; }

        public string comment { get; set; }

        public DateTime? postDate { get; set; }

        public string username { get; set; }
    }
}