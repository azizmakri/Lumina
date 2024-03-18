using LuminaApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Domain.Entities
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual User? Parent { get; set; }
        public virtual History? history { get; set; }
        public string? ParentFK { get; set; }
        public int? historyFK { get; set; }
        public virtual ICollection<User>? Students { get; set; }
        public virtual ICollection<Session>? sessions { get; set; }
        public virtual ICollection<Attendance>? Attendances { get; set; }
        public virtual ICollection<ClassRoom>? ClassRooms { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }
        public virtual ICollection<Evaluation>? Evaluations { get; set; }
        public virtual ICollection<Folder>? Folders { get; set; }
    }
}
