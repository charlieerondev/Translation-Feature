using //some namespace

namespace Portal
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //CERON 20180927 -- Other code that runs on application startup
            SiteMgr.LoadIntoCache(ConfigurationManager.AppSettings["sitename"]);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            //CERON 20180927 -- Register routes
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["lang"] = "eng";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //CERON 20180927 -- Do stuff
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //CERON 20180927 -- Do stuff
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //CERON 20180927 -- Do stuff
        }
    }
}