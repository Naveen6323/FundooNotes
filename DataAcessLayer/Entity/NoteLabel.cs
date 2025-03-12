//using DataAcessLayer.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entity
{
    public class NoteLabel : ILabelNotes
    {
        [Required]
        public int LabelId { get; set; }
        [Required]
        public int NoteId { get; set; }

        [ForeignKey("LabelId")]
        public virtual Label Label { get; set; }

        [ForeignKey("NoteId")]
        public virtual UserNotes Note { get; set; }
    }
}
