using //some namespaces

namespace Portal.UICode
{
    public class SiteMgr
    {
        public static void  LoadIntoCache(string siteName)
        {
            SiteIndexer.LoadIntoCache(siteName);
        }

        //IMPORTANT: See SiteIndexer.cs comments
        public static Dictionary<string, SiteMgrData> GetAllEnglishConfigs(string siteName)
        {
            return SiteIndexer.GetAllConfigs(siteName, "eng");
        }

        public static Dictionary<string, SiteMgrData> GetAllSpanishConfigs(string siteName)
        {
            return SiteIndexer.GetAllConfigs(siteName, "spa");
        }

        private static readonly SiteIndexer sSiteSettings = new SiteIndexer();

        public static SiteIndexer SiteSettings
        {
            get { return sSiteSettings; }
        }
       
    }
}