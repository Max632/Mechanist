using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mechanist_Website.Authentication
{
    public class IsAdminRequirement : IAuthorizationRequirement { }
}
