using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("BankAccounts")]
    class BankModel
    {
        [Key]
        [Column("id", Order = 1)]
        public int ID { get; set; }
    }
}
