using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Platform
{
	public class QueryStringMiddleware
	{
		private RequestDelegate next;

		public QueryStringMiddleware()
		{

		}
		public QueryStringMiddleware(RequestDelegate requestDelegate)
		{
			next = requestDelegate;
		}

		public async Task Invoke(HttpContext context)
		{
			if(context.Request.Method == HttpMethods.Get &&
				context.Request.Query["custom"] == "true")
			{
				await context.Response.WriteAsync("Class-based Middleware \n");
			}
			if(next != null)
			{
				await next(context);
			}
		}
	}
}
