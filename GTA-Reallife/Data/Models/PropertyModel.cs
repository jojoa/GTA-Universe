using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Propertys")]
    public class PropertyModel
    {

        [Key]
        [Column("ID", Order = 1)]
        public int ID { get; set; }

        [ForeignKey("Inv-ID")]
        [Column("Inventory", Order = 2)]
        public InventoryModel Inventory { get; set; }

        [Column("Inv-ID", Order = 3)]
        public int InvID { get; set; }
    }
}
