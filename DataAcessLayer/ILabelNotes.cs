using DataAcessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{

    public interface ILabelNotes
    {
        int LabelId { get; set; }
        int NoteId { get; set; }
        Label Label { get; set; }
        UserNotes Note { get; set; }
    }
}
