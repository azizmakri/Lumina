using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Domain.Entities
{
    public class NotificationEntity
    {
        [Key]
        public int NotificationId { get; set; }
        public string Content { get; set; }
        [DefaultValue(false)]
        public Boolean read { get; set; }
        public DateTime Timestamp { get; set; }
        public int newsFk { get; set; }
        public virtual SchoolNews schoolnews { get; set; }
    }
}