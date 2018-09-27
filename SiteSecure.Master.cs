using //some namespaces

namespace Portal
{
    public partial class SiteSecure : MasterPage
    {
        protected string lang;

        protected void Page_Init(object sender, EventArgs e)
        {
            CacheControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lang = Session["lang"].ToString();
                }
                catch
                {
                    Session["lang"] = "eng";
                    lang = "eng";
                }
        SetHtmlBlocks();
            }

            Page.LoadComplete += Page_LoadComplete;
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "langScript",
                "lang = '" + Session["lang"] + "';", true);
        }

#region SITE CONFIG
        private void SetHtmlBlocks()
        {
            //CERON 20180927 -- get last 2 or 3 parts of URL. Ex: "members/index.aspx"
            string[] s = Request.Url.Segments;
            string path;
            try
            {
                if (s.Length >= 3 && s[s.Length - 3].ToLower().Contains(/*PART OF URL*/) && !s[s.Length - (s.Length - 1)].ToLower().Contains(/*PART OF URL*/))
                {
                    return;
                }
            }
            catch
            {
                return;
            }

            if (s[s.Length - 2].ToLower().Contains("secure"))
            {
                path = s[s.Length - 3].ToLower() + s[s.Length - 2].ToLower() + s[s.Length - 1].ToLower();
            }
            else
            {
                path = s[s.Length - 2].ToLower() + s[s.Length - 1].ToLower();
            }

            try
            {
                SetMasterLanguageControls(path);
                SetLanguageControls(path);
            }
            catch { }
        }
        private void SetMasterLanguageControls(string path)
        {
            string ctlPath = path.Split('/').Last().Split('.')[0];

            foreach (Control ctl in from ctlContent in Page.Controls.OfType<MasterPage>()
                .SelectMany(ctlMaster => ctlMaster.Controls.Cast<Control>()).OfType<HtmlForm>()
                                    from Control ctlChild in ctlContent.Controls
                                    from Control ctl in ctlChild.Controls
                                    select ctl)
            {
                if (ctl.ID != null)
                {
                    try
                    {
                        SetControlValues(ctl, ctlPath);
                    }
                    catch (KeyNotFoundException)
                    {
                        //Key not found, do nothing
                    }
                    catch (Exception)
                    {
                        //Send Email
                    }
                }
            }
        }
        private void SetLanguageControls(string path)
        {
            string ctlPath = path.Split('/').Last().Split('.')[0];
            foreach (Control ctl in
                        from ctlContent in
                            Page.Controls.OfType<MasterPage>()
                                .SelectMany(ctlMaster => ctlMaster.Controls.Cast<Control>())
                                .OfType<HtmlForm>()
                                .SelectMany(ctlForm => ctlForm.Controls.Cast<Control>())
                                .OfType<ContentPlaceHolder>()
                        from Control ctlChild in ctlContent.Controls
                        from Control ctl in ctlChild.Controls
                        select ctl)
            {
                if (ctl.ID != null)
                {
                    try
                    {
                        if (ctl.HasControls())
                        {
                            foreach (Control child in ctl.Controls)
                            {
                                SetControlValues(ctl, ctlPath);
                                SetControlValues(child, ctlPath);
                            }
                        }
                        else
                        {
                            SetControlValues(ctl, ctlPath);
                        }
                    }
                    catch (KeyNotFoundException)
                    {
                        //do nothing
                    }
                    catch (Exception)
                    {
                        //Send email
                    }
                }

            }
        }
        private void SetControlValues(Control ctl, string ctlPath)
        {
            if (!string.IsNullOrWhiteSpace(SiteMgr.SiteSettings[ctl.ID.ToLower() + '_' + ctlPath, lang].FieldValue))
            {
                if (ctl is CheckBoxList || ctl is DropDownList || ctl is RadioButtonList)
                {
                    if (ctl is DropDownList)
                    {
                        var ddl = (DropDownList)ctl;
                        foreach (ListItem item in ddl.Items)
                        {
                            string val;
                            val = !string.IsNullOrWhiteSpace(SiteMgr.SiteSettings[item.Text.ToLower() + '_' + ctlPath, lang].FieldValue) ?
                                SiteMgr.SiteSettings[item.Text.ToLower() + "_" + ctlPath, lang].FieldValue :
                                SiteMgr.SiteSettings[item.Text.ToLower() + "_common", lang].FieldValue;
                            if (!string.IsNullOrWhiteSpace(val))
                            {
                                item.Text = val;
                            } 
                        }
                    }
                    else if (ctl is RadioButtonList)
                    {
                        var ddl = (RadioButtonList)ctl;
                        foreach (ListItem item in ddl.Items)
                        {
                            string val;
                            val = !string.IsNullOrWhiteSpace(SiteMgr.SiteSettings[item.Text.ToLower() + '_' + ctlPath, lang].FieldValue) ?
                                SiteMgr.SiteSettings[item.Text.ToLower() + "_" + ctlPath, lang].FieldValue :
                                SiteMgr.SiteSettings[item.Text.ToLower() + "_common", lang].FieldValue;
                            if (!string.IsNullOrWhiteSpace(val))
                            {
                                item.Text = val;
                            }
                        }
                    }
                    else //ctl is a CheckBoxList
                    {
                        var list = (CheckBoxList)ctl;
                        foreach (ListItem item in list.Items)
                        {
                            string val;
                            val = !string.IsNullOrWhiteSpace(SiteMgr.SiteSettings[item.Text.ToLower() + '_' + ctlPath, lang].FieldValue) ?
                                SiteMgr.SiteSettings[item.Text.ToLower() + "_" + ctlPath, lang].FieldValue :
                                SiteMgr.SiteSettings[item.Text.ToLower() + "_common", lang].FieldValue;
                            if (!string.IsNullOrWhiteSpace(val))
                            {
                                item.Text = val;
                            }
                        }
                    }
                }
            }
        }

        protected void CacheControl()
        {
            try
            {
                string[] arr = ConfigMgr.AppSettings["pagesnocache"].Split(','); //CERON 20180927 -- A comma-delimited string of pages that contain sensitive information
                foreach (string page in arr)
                {
                    if (Request.Url.ToString().ToLower().Contains(page.ToLower()) && !string.IsNullOrWhiteSpace(page))
                    {
                        Response.AddHeader("Cache-Control", "max-age=0,no-cache,no-store,must-revalidate");
                        Response.AddHeader("Pragma", "no-cache");
                        Response.AddHeader("Expires", "Tue, 01 Jan 1970 00:00:00 GMT");
                    }
                } 
            }
            catch { }
        }
    }
}