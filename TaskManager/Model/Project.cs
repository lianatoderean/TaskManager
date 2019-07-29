namespace TaskManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            ProjectMembers = new HashSet<ProjectMember>();
            Tasks = new HashSet<Task>();
        }

        public int projectId { get; set; }

        public int? ownerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dueDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? startDate { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}