using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;

namespace ets_web
{
    public partial class Shift : ets_web.ETSBasePage
    {
        User _bemember = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            _bemember = (User)Session["user"];
            ValidateUserLogin();
            //Shifting();
        }

        protected void Shifting()
        {
           try
            {
                string strFilePath = Server.MapPath( ConfigurationManager.AppSettings["shiftfilepath"].ToString());
                XmlDocument xml = new XmlDocument();
                xml.Load(strFilePath);
               XmlElement parentelement = xml.CreateElement("Shifting");
               XmlElement USERCODE = xml.CreateElement("USERCODE");
                USERCODE.InnerText = _bemember.UserCode.ToString();
                XmlElement USERNAME = xml.CreateElement("USERNAME");
                USERNAME.InnerText = _bemember.FullName;
               XmlElement CHOOSE = xml.CreateElement("CHOOSE");
               if (rbMorning.Checked)
               {
                    CHOOSE.InnerText = rbMorning.Text;
               }
               else
               {
                    CHOOSE.InnerText = rbNoon.Text;
               }
                parentelement.AppendChild(USERCODE);
                parentelement.AppendChild(USERNAME);

                parentelement.AppendChild(CHOOSE);
               xml.DocumentElement.AppendChild(parentelement);
               xml.Save(strFilePath);
            }
           catch (Exception ex)
           {
                ClientScript.RegisterStartupScript(GetType(), "New Rerequirement", "alert('Error Occured. Contact IT Department');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Shifting();
            Response.Redirect("~/Account/DashBoard.aspx");
        }
    }
}