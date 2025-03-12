using DataAcessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
    public interface ILabels
    {
        int LabelId { get; set; }
        string LabelName { get; set; }
        int UserId { get; set; }
        User User { get; set; }
        ICollection<NoteLabel> NoteLabels { get; set; }
    }
}
