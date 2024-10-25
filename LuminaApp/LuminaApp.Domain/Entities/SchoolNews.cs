using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Domain.Entities
{
    public class SchoolNews
    {
        [Key]
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string SchoolFk { get; set; }
        public string NewsPath { get; set; }
        public DateTime NewsDate { get; set; }
        public virtual User School { get; set; }
        public virtual NotificationEntity notification { get; set; }
    }
}
