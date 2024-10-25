using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Domain.Entities
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public virtual ClassRoom? classRoom { get; set; }
        public int? classRoomFK { get; set; }

    }
}
