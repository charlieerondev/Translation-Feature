using //some namespace

namespace Portal.UICode
{
    public abstract class SiteConfig
    {
        public abstract string GetAppKey();
        public abstract Dictionary<string, string> GetSiteConfigs(string siteName);

        public void LoadIntoCache(string siteName)
        {
            string AppKey = GetAppKey();
            HttpContext.Current.Application[AppKey] = GetSiteConfigs(siteName);
        }

        public string this[string keyName]
        {
            get
            {
                string AppKey = GetAppKey();
                if (HttpContext.Current.Application[AppKey] != null)
                {
                    Dictionary<string, string> AppConfigDictionary = null;
                    AppConfigDictionary = (Dictionary<string, string>) HttpContext.Current.Application[AppKey];
                    return AppConfigDictionary[keyName.ToLower()];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public Dictionary<string, string> GetAll()
        {
           
                string AppKey = GetAppKey();
                if (HttpContext.Current.Application[AppKey] != null)
                {
                    Dictionary<string, string> AppConfigDictionary = null;
                    AppConfigDictionary = (Dictionary<string, string>)HttpContext.Current.Application[AppKey];
                    return AppConfigDictionary;
                }
            return null;
        }
    }
}