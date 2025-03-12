//using DataAcessLayer.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entity
{
    public class UserNotes : IUserNotes
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        [Column("Note", TypeName = "varchar(100)")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Column("Description", TypeName = "varchar(500)")]
        public string? Description { get; set; }

        public string? Color { get; set; }
        public bool IsArchieved { get; set; }
        public bool IsTrash { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<NoteLabel> NoteLabels { get; set; } = new List<NoteLabel>();
    }
}
