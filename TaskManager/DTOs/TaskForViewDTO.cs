namespace TaskApp.DTOs
{
    public class TasksForViewDTO
    {
        public int taskId { get; set; }
        public string taskName { get; set; }

        public string description { get; set; }

        public string status { get; set; }

        public string assignedUser { get; set; }
        public int userId { get; set; }
    }
}