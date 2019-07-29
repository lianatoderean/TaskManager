namespace TaskManager.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class UsersTask
    {
        [Key]
        public int userTaskId { get; set; }

        public int taskId { get; set; }

        public int userId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? startDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? endDate { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        [StringLength(1000)]
        public string notes { get; set; }

        public virtual Task Task { get; set; }

        public virtual User User { get; set; }
    }
}