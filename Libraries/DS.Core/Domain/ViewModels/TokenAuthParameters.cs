using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Domain.ViewModels
{
    public class TokenAuthParameters
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string refresh_token { get; set; }
    }
}
