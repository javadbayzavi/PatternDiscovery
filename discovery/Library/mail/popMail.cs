using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Models;
using EAGetMail;
namespace discovery.Library.mail
{
    public class popMail : Email
    {
        private MailClient _client;
        private MailServer _server;
        private List<emailMessage> _emails = new List<emailMessage>();

        public popMail(string _host, string user, string pass, int port) : base(_host, user, pass, port)
        {
            this._client = new MailClient("discoveryPattern");
            
        }
        public override void Connect()
        {
            try
            {
                this._server = new MailServer(this.Host, this.userAccount, this.userPassword, ServerProtocol.Pop3);
                this._server.AuthType = ServerAuthType.AuthLogin;
                this._server.Port = this.Port;
                this._client.Connect(this._server);
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }

        public override void FetchMail()
        {

            if(this._client.Connected)
            {
                if (Directory.Exists(Keys._TEMPDIRECTORY) == false)
                    Directory.CreateDirectory(Keys._TEMPDIRECTORY);


                var infos = new List<MailInfo>(this._client.GetMailInfos());
                foreach (var item in infos)
                {
                    Mail popMail = this._client.GetMail(item);
                    this._emails.Add(new emailMessage()
                    {
                        Subject = popMail.Subject,
                        DateSent = popMail.SentDate,
                        From = popMail.From.Name + "(" + popMail.From.Address + ")"
                    }); 
                }
            }

        }

        public override bool isImported()
        {
            throw new NotImplementedException();
        }

        public override void NormalizeDataSet()
        {
            throw new NotImplementedException();
        }

        public override void ReadDocument()
        {
            throw new NotImplementedException();
        }

        public override void signAsImported()
        {
            throw new NotImplementedException();
        }
    }
}
