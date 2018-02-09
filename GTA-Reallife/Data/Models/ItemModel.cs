using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Models
{

    [Table("Items")]
    public class ItemModel
    {
        [Key]
        [Column("id", Order = 1)]
        public int ID { get; set; }

        [Column("Name", Order = 2, TypeName = "char"), StringLength(255)]
        public string ItemName { get; set; }

        [Column("Count", Order = 3)]
        public int ItemCount { get; set; }

        [ForeignKey("InvID")]
        [Column("Invent")]
        public InventoryModel Inventory { get; set; }

        [Column("InvID")]
        public int InvID { get; set; }

        

    }
}
