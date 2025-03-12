using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class GetAllNotesWithLabelResponse
    {
        public int notetitle { get; set; }
        public List<string> labelIds { get; set; }
        

    }
}
