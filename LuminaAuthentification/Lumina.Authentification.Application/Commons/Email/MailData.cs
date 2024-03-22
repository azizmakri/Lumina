using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.Commons.Email
{
    public class MailData
    {
        MailData() { }
        public MailData(string emailToId, string emailToName, string emailSubject, string emailBody)
        {
            EmailToId = emailToId;
            EmailToName = emailToName;
            EmailSubject = emailSubject;
            EmailBody = emailBody;
        }
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}
