using Mechanist_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mechanist_Website.Data
{
    public class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Members.Any())
            {
                return;   // DB has been seeded
            }

            var members = new List<MembersInfo>
            {
                new MembersInfo { DiscordName="Allu#0801", MCName="AlluTheCreator", Name = "Allu", DiscordID=268855990512910336, IsAdmin=true, DateAccepted = DateTime.Now, Retired = false, Twitch="https://www.twitch.tv/alluthecreator"},
                new MembersInfo { DiscordName="Elvar#6660", MCName="Elvar", DiscordID=516373096899411979, IsAdmin=true, DateAccepted = DateTime.Now, Retired = false, Youtube="https://www.youtube.com/channel/UC_a3kJv9CmuFZlQOJ1jVS5w"},
                new MembersInfo { DiscordName="TK#0695", MCName="TK", DiscordID=250378512123428886, IsAdmin=true, DateAccepted = DateTime.Now, Retired = false, Youtube="https://www.youtube.com/channel/UCaLY1yqQtaMs1n5f9WnlDWA"},
                new MembersInfo { DiscordName="Orion#8062", MCName="_0rion", Name="Max", DiscordID=250378512123428886, IsAdmin=true, DateAccepted = DateTime.Now, Retired = false},
                new MembersInfo { DiscordName="Kdender#7327", MCName="Kdender", DiscordID=358645602357215232, IsAdmin=true, DateAccepted = DateTime.Now, Retired = false, Youtube="https://www.youtube.com/Kdender", Twitch = "https://twitch.tv/kdender", Website = "http://kdender.com"},
            };

            members.ForEach(m => context.Members.Add(m));
            context.SaveChanges();
        }
    }
}
