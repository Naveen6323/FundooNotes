using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entity
{
    public class User:IUser
    {
        [Key]
        public int ID { get; set; }
        [Column("FirstName",TypeName = "varchar(100)")]
        public string FirstName { get; set; }
        [Column("LastName", TypeName = "varchar(100)")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Column("Email", TypeName = "varchar(100)")]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [Column("Password", TypeName = "varchar(100)")]
        public string Password { get; set; }
    }
}
