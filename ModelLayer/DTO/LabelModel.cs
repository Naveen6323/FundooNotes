using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class LabelModel
    {
        [Required(ErrorMessage = "Label Name cannot be empty")]
        public string LabelName { get; set; }
        //[Required(ErrorMessage = "Label noteID cannot be empty")]
        //public int NoteId { get; set; }
        //[Required(ErrorMessage = "Label UserID cannot be empty")]
        //public int UserId { get; set; }
    }
}
