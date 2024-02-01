using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.Config
{
    public class AppSettings
    {
        public string? JWT_Secret { get; set; }
        public string? CustomerKey { get; set; }
        public int JWT_Validity_Mins { get; set; }
        public int PasswordLinkValidityMins { get; set; }
        public string? ErrorSendToEmail { get; set; }
        public string? ApiUrl { get; set; }
    }
}
