using System;
using System.Net;

namespace SipClient.Models
{
    public class SipClientAccount
    {
        public string Name { get; set; }
        public Uri Registrar { get; set; }
        public string User { get; set; }
        public string AuthenticationUser { get; set; }
        public string Password { get; set; } // TODO dont save password purely in string
        public int Timeout { get; set; } = 3600;

        public SipClientAccount()
        {
            
        }

        public SipClientAccount(string username, string password, Uri registrar)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("Username must be valid username."); // TODO username check?
            if (registrar == null)
                throw new ArgumentNullException("Invalid registrar uri.");

            Name = username;
            User = username;
            AuthenticationUser = username;
            Password = password;
            Registrar = registrar;
        }
    }
}