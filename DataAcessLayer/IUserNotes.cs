using DataAcessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
    public interface IUserNotes
    {
        int NoteId { get; set; }
        string Title { get; set; }
        string? Description { get; set; }
        string? Color { get; set; }
        bool IsArchieved { get; set; }
        bool IsTrash { get; set; }
        int UserID { get; set; }
        User User { get; set; }
        ICollection<NoteLabel> NoteLabels { get; set; }
    }
}
