using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Vehicles")]
    public class VehicleModel
    {
        [Key]
        [Column("ID", Order = 1)]
        public int ID { get; set; }

        [ForeignKey("Inv-ID")]
        [Column("Inventory", Order = 2)]
        public InventoryModel Inventory { get; set; }

        [Column("Inv-ID", Order = 3)]
        public int InvID { get; set; }

        [ForeignKey("Acc-ID")]
        [Column("Account", Order = 4)]
        public AccountModel Account { get; set; }

        [Column("Acc-ID", Order = 5)]
        public int AccID { get; set; }
    }
}
