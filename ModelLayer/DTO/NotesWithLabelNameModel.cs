using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class NotesWithLabelNameModel
    {
        public int noteid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<string> labelname { get; set; }
    }
}
