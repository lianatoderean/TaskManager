namespace TaskManager.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class aspnet_SchemaVersions
    {
        [Key]
        [Column(Order = 0)]
        public string Feature { get; set; }

        [Key]
        [Column(Order = 1)]
        public string CompatibleSchemaVersion { get; set; }

        public bool IsCurrentVersion { get; set; }
    }
}