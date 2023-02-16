using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceTrackingAutomation.Domain.Enums;
using ServiceTrackingAutomation.Domain.Helpers;

namespace ServiceTrackingAutomation.App.Filters
{
	public class AuthFilterAttribute : ActionFilterAttribute
	{
		public AuthFilterAttribute()
		{

		}
		public AuthFilterAttribute(params RoleType[] roles)
		{
			rolesAllowed = roles;
		}
		private readonly RoleType[] rolesAllowed = Array.Empty<RoleType>();
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var authorized = context.HttpContext.IsAuthenticated();
			if(!authorized)
			{
				context.Result = new RedirectResult("/");
			}
			if(rolesAllowed.Length > 0)
			{
				var userRole = (RoleType)context.HttpContext.GetUser().Role;
				if(!rolesAllowed.Any(x => x == userRole))
				{
                    context.Result = new RedirectResult("/");
                }
			}
		}

	}
}
