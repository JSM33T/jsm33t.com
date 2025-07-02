using StackExchange.Redis;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using JassWebApi.Entities.Shared;

namespace JassWebApi.Base.Filters
{
    public class PersistAttribute(int minutes) : ActionFilterAttribute
    {
        private readonly int _minutes = minutes;
        private static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var config = context.HttpContext.RequestServices.GetService<JsmtConfig>();
            if (config == null || !config.CacheEnabled) return;

            var db = redis.GetDatabase();
            var key = context.HttpContext.Request.Path.ToString();
            var cached = db.StringGet(key);

            if (cached.HasValue)
            {
                context.Result = new ContentResult { Content = cached, ContentType = "application/json" };
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var config = context.HttpContext.RequestServices.GetService<JsmtConfig>();
            if (config == null || !config.CacheEnabled) return;

            var db = redis.GetDatabase();
            var key = context.HttpContext.Request.Path.ToString();

            if (!db.KeyExists(key))
            {
                if (context.Result is ObjectResult result && result.Value != null)
                {
                    var json = JsonSerializer.Serialize(result.Value);
                    db.StringSet(key, json, TimeSpan.FromMinutes(_minutes));
                }
            }
        }
    }
}
