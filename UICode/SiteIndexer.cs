using //some namespaces

namespace Portal.UICode
{
    public class SiteIndexer
    {
        private const string EngAppKey = "SiteConfigsEng";
        private const string SpaAppKey = "SiteConfigsSpa";

        public static string GetAppKey(string lang)
        {
            if (lang == "eng")
            {
                return EngAppKey;
            }
            
            return SpaAppKey;
        }

        public static void LoadIntoCache(string siteName)
        {
            HttpContext.Current.Application["SiteConfigsEng"] = GetAllConfigs(siteName, "eng");
            HttpContext.Current.Application["SiteConfigsSpa"] = GetAllConfigs(siteName, "spa");
        }

        public static Dictionary<string, SiteMgrData> GetAllConfigs(string siteName, string language)
        {
            SiteConfigService siteConfigService = new SiteConfigService();
            return siteConfigService.GetSiteMgrDataBySite(siteName, language);
        }

        //CERON 20180927 -- Two HttpApplicationState objects, one for Spa and the other for Eng so both can be accessed
        public SiteMgrData this[string keyName, string lang]
        {
            get
            {
                if (HttpContext.Current.Application[GetAppKey(lang)] != null)
                {
                    Dictionary<string, SiteMgrData> siteDict = (Dictionary<string, SiteMgrData>)HttpContext.Current.Application[GetAppKey(lang)];
                    try
                    {
                        return siteDict[keyName.ToLower()];
                    }
                    catch (KeyNotFoundException e)
                    {
                        //Handle sending email elsewhere
                    }
                }

                return new SiteMgrData();

            }
        }
    }
}