namespace TaskManager.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Permission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Permission()
        {
            ProjectMembers = new HashSet<ProjectMember>();
        }

        public int permissionId { get; set; }

        [MaxLength(8)]
        public byte[] bytePermission { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }
    }
}