using //some namespace

namespace Portal
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
		        //CERON 20180927 -- some stuff unrelated to the translation feature happens in here
            }
        }
    }
}