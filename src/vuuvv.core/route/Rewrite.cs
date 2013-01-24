using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Linq;
using System.Web;

using vuuvv.utils;

namespace vuuvv.core.route
{
    public class Rewrite : IHttpModule
    {
        public const string ROUTE_CONFIG_NAME = "RouteConfig";
        public static string[] DEFAULTS = {"index.aspx", "default.aspx"};

        public RouteSection Route
        {
            get
            {
                System.Web.Caching.Cache cache = HttpContext.Current.Cache;
                if (cache[ROUTE_CONFIG_NAME] == null)
                {
                    cache.Insert(ROUTE_CONFIG_NAME, ConfigurationManager.GetSection("Route"));
                }
                return (RouteSection)cache[ROUTE_CONFIG_NAME];
            }
        }

        public virtual void Init(HttpApplication app)
        {
            app.AuthenticateRequest += new EventHandler(AuthorizeRequest);
            app.EndRequest += new EventHandler(EndRequest);
        }

        public virtual void Dispose() { }

        protected virtual void AuthorizeRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            Dispatch(app.Context);
        }

        protected virtual void EndRequest(object sender, EventArgs e)
        {
            DBHepler.Close();
        }

        protected void Dispatch(HttpContext context)
        {
            var req = context.Request;
            string path = req.Path;

            if (path.EndsWith(Route.ext))
            {
                var handler = GetHandler(path);
                LoadHandler(handler);
                context.Response.End();
            }
        }

        private string GetSlug(string path, string[] defaults, string ext)
        {
            if (Array.IndexOf(DEFAULTS, path) != -1)
                return "/";
            List<string> tails = new List<string>(DEFAULTS);
            tails.Add(ext);
            return StringUtils.CutTail(path, tails.ToArray());
        }

        private string GetHandler(string path)
        {
            string slug = GetSlug(path.ToLower(), DEFAULTS, Route.ext);
            foreach (RuleElement rule in Route.rules)
            {
                var regex = new Regex(rule.Pattern, RegexOptions.IgnoreCase);
                var m = regex.Match(slug);
                if (m.Success)
                    return rule.Handler;
            }
            throw new HttpException(404, string.Format("Can't Found the path `{0}`", path));
        }

        private void LoadHandler(string handler)
        {
            string[] parts = handler.Split('.');
            string type_name = string.Join(".", parts.Take(parts.Length - 1).ToArray<string>());
            string method_name = parts[parts.Length - 1];
            ClassHelper.StaticCall<object>(type_name, method_name);
        }
    }
}
