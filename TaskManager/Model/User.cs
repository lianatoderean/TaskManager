namespace TaskManager.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ProjectMembers = new HashSet<ProjectMember>();
            Projects = new HashSet<Project>();
            Comments = new HashSet<Comment>();
            UsersTasks = new HashSet<UsersTask>();
        }

        public int userId { get; set; }

        [Required]
        [StringLength(256)]
        public string username { get; set; }

        [StringLength(50)]
        public string firstname { get; set; }

        [StringLength(50)]
        public string lastname { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersTask> UsersTasks { get; set; }
    }
}