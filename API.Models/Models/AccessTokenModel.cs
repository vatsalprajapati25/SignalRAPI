﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.Models
{
    public class AccessTokenModel
    {
        public string? Token { get; set; }
        public int ValidityInMin { get; set; }
        public DateTime ExpiresOnUTC { get; set; }
        public long UserId { get; set; }
    }
}
