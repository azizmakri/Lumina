using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NewsFeatures.Dtos
{
    public class NewsDTO
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string SchoolFk { get; set; }
        public string NewsPath { get; set; }
        public DateTime NewsDate { get; set; }
    }
}
