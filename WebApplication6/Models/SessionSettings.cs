using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }

        public static void Remove(this ISession session, string key)
        {
            session.Remove(key);
        }
    }

    public static class SessionSettings
    {
        public static ISession session = AppHttpContext.Current.Session;
        private static SessionContext _sessionContext;
        public static SessionContext sessionContext
        {
            get
            {
                SessionContext context = null;
                if (session.Get<SessionContext>("SessionContext") == default(SessionContext))
                {
                    context = new SessionContext();
                    session.Set<SessionContext>("SessionContext", context);
                    sessionContext = context;
                }
                else
                {
                    context = session.Get<SessionContext>("SessionContext");
                }
                return context; 
            }
            set
            {
                session.Remove("SessionContext");
                session.Set<SessionContext>("SessionContext", value);
            }
        }
    }

    public class SessionContext
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }

    public static class AppHttpContext
    {
        static IServiceProvider services = null;

        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }

        public static HttpContext Current
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }

    }
}
