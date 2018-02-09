using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Models
{
    [Table("Inventorys")]
    public class InventoryModel
    {
        [Key]
        [Column("id", Order = 1)]
        public int id { get; set; }

        [Column("items", Order = 2)]
        public List<ItemModel> Items { get; set; }
    }
}
