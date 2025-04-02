using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class GetAllNotesWithLabelResponse
    {
        public int NoteId { get; set; }
        public string  title { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public bool isTrash { get; set; }
        public bool isArchieved { get; set; }
        public List<NoteLabelDto> Labels { get; set; } 

    }
}
