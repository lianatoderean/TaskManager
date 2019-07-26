using System;

namespace TaskApp.DTOs
{
    public class ProjectDTO
    {
        public int projectId { get; set; }

        public DateTime? dueDate { get; set; }

        public DateTime? startDate { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string owner { get; set; }
    }
}