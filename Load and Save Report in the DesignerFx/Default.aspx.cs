﻿using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System;
using System.Data;
using System.Web;

namespace Load_and_Save_Report_in_the_DesignerFx
{
    public partial class Default : System.Web.UI.Page
    {
        static Default()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonDesignFile_Click(object sender, EventArgs e)
        {
            if (FileReport.PostedFile != null && FileReport.PostedFile.FileName.Length > 0 && FileReport.PostedFile.InputStream != null)
            {
                StiReport report = new StiReport();
                report.Load(FileReport.PostedFile.InputStream);
                StiWebDesignerFx1.Design(report);
            }
        }

        protected void ButtonDesignServer_Click(object sender, EventArgs e)
        {
            if (DropDownListReport.Text != null && DropDownListReport.Text.Length > 0)
            {
                string applicationDirectory = HttpContext.Current.Server.MapPath(string.Empty);
                string reportFileName = applicationDirectory + "\\Reports\\" + DropDownListReport.Text;

                StiReport report = new StiReport();
                report.Load(reportFileName);
                StiWebDesignerFx1.Design(report);
            }
        }

        protected void ButtonDesignNew_Click(object sender, EventArgs e)
        {
            StiReport report = new StiReport();
            StiWebDesignerFx1.Design(report);
        }
        
        protected void StiWebDesignerFx1_SaveReport(object sender, StiSaveReportEventArgs e)
        {
            // Web Designer return StiReport object in the e.Report property
            var reportString = e.Report.SaveToString();

            // You can set the error code which will be displayed by the designer after saving
            // -1: default value, the message is not displayed
            // 0: display 'Report is successfully saved' message
            //e.ErrorCode = 1;
            
            // Also you can set the custom message, it will be displayed after saving
            e.ErrorString = "Your report has been saved.";
        }

        // DataSet for preview report in Web Designer
        protected void StiWebDesignerFx1_PreviewReport(object sender, StiReportDataEventArgs e)
        {
            string applicationDirectory = HttpContext.Current.Server.MapPath(string.Empty);

            DataSet data = new DataSet();
            data.ReadXml(applicationDirectory + "\\Data\\Demo.xml");
            data.ReadXmlSchema(applicationDirectory + "\\Data\\Demo.xsd");

            e.Report.RegData(data);
        }
    }
}