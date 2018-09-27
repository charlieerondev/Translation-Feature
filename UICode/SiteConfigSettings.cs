using //some namespaces

namespace Portal.UICode
{
    public class SiteConfigSettings : SiteConfig
    {
        private const string AppKey = "SiteConfigSettings_CA6E3F82-A1AD-49C4-87AA-073E83CFF122";

        public override string GetAppKey()
        {
            return AppKey;
        }

        public override Dictionary<string, string> GetSiteConfigs(string siteName)
        {
            SiteConfigSettingManager siteConfigSettingManager = new SiteConfigSettingManager();
            return siteConfigSettingManager.GetSiteConfigValues(siteName);
        }
    }
}