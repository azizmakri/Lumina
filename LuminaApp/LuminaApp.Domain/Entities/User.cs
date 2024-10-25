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
        public string? ParentFK { get; set; }
        public int? historyFK { get; set; }
        public int? gradeFK { get; set; }
        public int? subjectFK { get; set; }
        public virtual User? Parent { get; set; }
        public virtual History? history { get; set; }
        public virtual Grade? grade { get; set; }
        public virtual ICollection<Subject>? subjects { get; set; }
        public virtual ICollection<User>? Students { get; set; }
        public virtual ICollection<SchoolNews>? News { get; set; }
        public virtual ICollection<Attendance>? Attendances { get; set; }
        public virtual ICollection<ClassRoom>? ClassRooms { get; set; }
        public virtual ICollection<DocumentEntity>? teacherDocuments { get; set; }
        public virtual ICollection<Evaluation>? evaluations { get; set; }
        public virtual ICollection<DocumentEntity>? studentDocuments { get; set; }
        public virtual ICollection<Folder>? Folders { get; set; }
    }
}
