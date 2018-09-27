namespace Portal.UICode
{
    public class SiteConfigMgr
    {
        private static readonly SiteConfigSettings _siteConfigSettings = new SiteConfigSettings();

        public static SiteConfigSettings SiteConfigSettings
        {
            get { return _siteConfigSettings; }
        }
    }
}