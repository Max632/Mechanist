using Mechanist_Website.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mechanist_Website.Authentication
{
    public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        private readonly DataContext _context;

        public IsAdminHandler(DataContext context)
        {
            _context = context;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            if (_context.Members.Where(m => (m.DiscordID.ToString() == context.User.Claims.First().Value.ToString()) && m.IsAdmin == true).ToList().Count > 0)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
