using Microsoft.AspNetCore.Authentication;
using System;

namespace Heimdallr.Authentication
{
    public class DevelopmentAuthenticationOptions : AuthenticationSchemeOptions
    {
        private string _realm;
        
        public DevelopmentAuthenticationOptions()
        {
        }
        
        public string Realm
        {
            get
            {
                return _realm;
            }

            set
            {
                if (!string.IsNullOrEmpty(value) && !IsAscii(value))
                {
                    throw new ArgumentOutOfRangeException("Realm", "Realm must be US ASCII");
                }

                _realm = value;
            }
        }
        
        public bool AllowInsecureProtocol
        {
            get; set;
        }
        
        public new DevelopmentAuthenticationEvents Events { get; set; } = new DevelopmentAuthenticationEvents();


        private bool IsAscii(string input)
        {
            foreach (char c in input)
            {
                if (c < 32 || c >= 127)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
