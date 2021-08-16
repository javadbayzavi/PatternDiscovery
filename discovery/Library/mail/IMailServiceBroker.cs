using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.mail
{
    //This interface define necessary functions to connect to a mail service and fetch email content
    public interface IMailServiceBroker
    {
        //Connect to a remote mail server
        void Connect();
        
        //FetchMail from Mail Server
        void FetchMail();
    }
}
