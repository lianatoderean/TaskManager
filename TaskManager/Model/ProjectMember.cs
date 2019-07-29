namespace TaskManager.Model
{
    using System.ComponentModel.DataAnnotations;

    public partial class ProjectMember
    {
        [Key]
        public int projectMembersId { get; set; }

        public int userId { get; set; }

        public int projectId { get; set; }

        public int permissionId { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual Project Project { get; set; }

        public virtual User User { get; set; }
    }
}