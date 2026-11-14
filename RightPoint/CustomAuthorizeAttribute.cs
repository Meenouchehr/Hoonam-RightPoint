using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace RightPoint
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        private readonly Models.Access[] allowedroles;
        public CustomAuthorizeAttribute(params Models.Access[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if (RPC.Common.CurrentUser != null)
                if (RPC.Common.CurrentUser.Access != null)
                {
                    foreach (var role in allowedroles)
                    {
                        if ((byte)RPC.Common.CurrentUser.Access == (byte)role)
                        {
                            authorize = true;
                            break;
                        }
                    }
                }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}