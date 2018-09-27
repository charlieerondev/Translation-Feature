using //some namespaces

namespace Portal {
    public partial class WebMethods : Page
    {
        [WebMethod]
        [ScriptMethod]
        public static string GetTranslation(string input){
            List<TranslationKeys> list = JsonConvert.DeserializeObject<List<TranslationKeys>>(input);
            foreach (TranslationKeys item in list)
            {
                if (item.type == "htmlBlock")
                {
                    item.translation = string.IsNullOrWhiteSpace(SiteMgr.SiteSettings[item.key, item.lang].FieldValue) ?
                        "" : SiteMgr.SiteSettings[item.key, item.lang].FieldValue;
                }
                else
                {
                    item.translation = string.IsNullOrWhiteSpace(SiteMgr.SiteSettings[item.key + "_common", item.lang].FieldValue) ?

                        "" : SiteMgr.SiteSettings[item.key + "_common", item.lang].FieldValue;
                }
            }
            string translations = JsonConvert.SerializeObject(list);
            return translations;
        }
    }
}