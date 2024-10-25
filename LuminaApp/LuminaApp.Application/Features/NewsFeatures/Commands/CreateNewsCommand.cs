using LuminaApp.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NewsFeatures.Commands
{
    public class CreateNewsCommand: IRequest<OperationResult>
    {
        public string Title { get; set; }
        public string NewsPath { get; set; }
        public string AdminId { get; set; }
        public DateTime NewsDate { get; set; }

    }
}

