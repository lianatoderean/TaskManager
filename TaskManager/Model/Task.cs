namespace TaskManager.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            Comments = new HashSet<Comment>();
            UsersTasks = new HashSet<UsersTask>();
        }

        public int taskId { get; set; }

        public int projectId { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public virtual Project Project { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersTask> UsersTasks { get; set; }
    }
}