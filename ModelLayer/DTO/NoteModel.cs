using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTO
{
    public class CreateNoteModel
    {
        [Required(ErrorMessage ="title cannot be empty")]
        public string? Title { get; set; }
        [DefaultValue("")]
        public string? Description { get; set; }
        [DefaultValue("")]
        public string? Color { get; set; }
        [DefaultValue(false)]
        public bool IsArchieved { get; set; }
        [DefaultValue(false)]
        public bool IsTrash { get; set; } 

    }
}
