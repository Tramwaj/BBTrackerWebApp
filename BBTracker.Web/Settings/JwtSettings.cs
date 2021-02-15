using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBTracker.Web.Settings
{
    public class JwtSettings
    {
        public string ValidIssuer { get; set; } = "https://localhost:44347/";
        public string ValidAudience { get; set; } = "https://localhost:44347/";
        public string Secret { get; set; } = "1983646546546546465643kihu";
        public int LifetimeInSeconds { get; set; } = 3600;
    }
}
