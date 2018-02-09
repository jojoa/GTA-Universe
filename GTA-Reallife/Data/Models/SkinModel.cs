using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Skins")]
    public class SkinModel
    {
        [Key]
        [Column("id", Order = 1)]
        public int id { get; set; }

        [ForeignKey("AccountID")]
        [Column("Account", Order = 2)]
        public AccountModel Account { get; set; } 

        [Column("AccountID", Order = 3)]
        public int AccountID { get; set; }
    }
}
