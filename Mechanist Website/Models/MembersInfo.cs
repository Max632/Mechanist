using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mechanist_Website.Models
{
    public class MembersInfo
    {
        [Key]
        public int MemberID { get; set; }

        public long DiscordID { get; set; }
        public bool IsAdmin { get; set; }

        public string DiscordName { get; set; }
        public string MCName { get; set; }
        public string Name { get; set; }

        public DateTime DateAccepted { get; set; }

        public string Twitch { get; set; }
        public string Youtube { get; set; }
        public string Website { get; set; }

        public bool Retired { get; set; }
    }
}
