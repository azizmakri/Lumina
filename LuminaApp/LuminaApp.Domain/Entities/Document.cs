using System.ComponentModel.DataAnnotations;

namespace LuminaApp.Domain.Entities
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public DateTime Creation_date { get; set; }
        public string DocumentPath { get; set; }
        public int? folderFK { get; set; }
        public virtual Folder? folder { get; set; }
        public ICollection<User>? students { get; set; }
    }
}
