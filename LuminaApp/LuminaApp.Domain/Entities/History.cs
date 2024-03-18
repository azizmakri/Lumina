using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Domain.Entities
{
    public class History
    {
        [Key]
        public int HistoryId { get; set; }
        public DateTime HistoryDate { get; set; }
        public virtual User? user { get; set; }
    }
}
