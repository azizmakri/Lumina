﻿using System.ComponentModel.DataAnnotations;

namespace LuminaApp.Domain.Entities
{
    public class ClassRoom
    {
        [Key]
        public int ClassroomId { get; set; }
        public string ClassroomName { get; set; }
        public virtual User? employee { get; set; }
        public string? employeeFk { get; set; }
        public ICollection<Equipment>? equipments { get; set; }
    }
}
