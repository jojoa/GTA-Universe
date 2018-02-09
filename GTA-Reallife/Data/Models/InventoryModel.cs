using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Models
{
    [Table("Inventory")]
    public class InventoryModel
    {
        [Key]
        [Column("id", Order = 1)]
        public int id { get; set; }

        [Column("items", Order = 2)]
        public List<ItemModel> Items { get; set; }

        [Column("owner-a", Order = 3)]
        public List<AccountModel> AccountOwner { get; set; }

        [Column("owner-p", Order = 4)]
        public List<PropertyModel> PropertyOwner { get; set; }

        [Column("owner-v", Order = 5)]
        public List<VehicleModel> VehicleOwner { get; set; }
    }
}
