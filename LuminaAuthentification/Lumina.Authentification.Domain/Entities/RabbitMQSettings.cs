﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Domain.Entities
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }
    }
}
