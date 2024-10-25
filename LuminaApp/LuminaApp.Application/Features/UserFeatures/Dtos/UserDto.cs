using LuminaApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.UserFeatures.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string role { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ParentFK { get; set; }
        public string grade { get; set; }
        public float Note { get; set; }
        public AttendanceType attendanceType { get; set; }
        public string? Observation { get; set; }

    }
}
