using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;

namespace vuuvv.core.route
{
    class Dispatcher
    {
        public const string ROUTE_CONFIG_NAME = "RouteConfig";

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
    }
}
