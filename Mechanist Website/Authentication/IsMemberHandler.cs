using Mechanist_Website.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mechanist_Website.Authentication
{
    public class IsMemberHandler : AuthorizationHandler<IsMemberRequirement>
    {
        private readonly DataContext _context;

        public IsMemberHandler(DataContext context)
        {
            _context = context;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsMemberRequirement requirement)
        {
            if (_context.Members.Where(m => m.DiscordID.ToString() == context.User.Claims.First().Value.ToString()).ToList().Count > 0)
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
