using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform
{
	public class LocationMidleware
	{
		private RequestDelegate next;
		private MessageOptions options;

		public LocationMidleware(RequestDelegate nextDelegate, IOptions<MessageOptions> opts)
		{
			next = nextDelegate;
			options = opts.Value;
		}

		public async Task Invoke(HttpContext context)
		{
			if(context.Request.Path == "/location")
			{
				await context.Response.WriteAsync($"{options.CityName}, {options.Country}");
			}
			else
			{
				await next(context);
			}
		}
	}
}
