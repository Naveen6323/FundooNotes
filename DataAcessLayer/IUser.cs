using DataAcessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
    public interface IUser
    {
        int ID { get; set; }
        string? FirstName { get; set; }
        string? LastName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        ICollection<UserNotes> Notes { get; set; }
    }
}
