using discovery.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.mail
{
    public abstract class Email : rawdocument , IMailServiceBroker
    {
        protected string Host;
        protected string userAccount;
        protected string userPassword;
        protected int Port;
        public Email(string _host,string user, string pass, int port)
        {
            this.Host = _host;
            this.userAccount = user;
            this.userPassword = pass;
            this.Port = port;
        }

        public abstract void Connect();
        public abstract void FetchMail();
        public abstract override void NormalizeDataSet();

        //Template method for a generic fethcing data from a mail server
        public override void ReadDocument()
        {
            this.Connect();
            this.FetchMail();
        }

    }
}
