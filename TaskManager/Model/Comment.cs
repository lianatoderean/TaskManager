namespace TaskManager.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Comment
    {
        public int commentId { get; set; }

        public int? taskId { get; set; }

        public int? userId { get; set; }

        [StringLength(50)]
        public string title { get; set; }

        [Column("comment")]
        [StringLength(500)]
        public string comment { get; set; }

        public DateTime? postDate { get; set; }

        public virtual Task Task { get; set; }

        public virtual User User { get; set; }
    }
}