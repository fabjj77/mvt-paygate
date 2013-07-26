using System;
using System.Collections.Generic;
using System.Linq;
using BankNet.Core;
using BankNet.Data;
using BankNet.Entity;
using OfficeOpenXml;

namespace Web.Admin.AnVienCard
{
    public partial class Default : System.Web.UI.Page
    {
        private string sSessionList = "List";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime date1;
            DateTime date2;
            if (!Utility.TryDateDMY(fromDate.Text,out date1) || !Utility.TryDateDMY(toDate.Text,out date2))
            {
                Alert.Show("Định dạng ngày tháng sai!");
                return;
            }

            int iStatus = int.Parse(DDL_Status.SelectedValue);
            PayCardFilter(date1, date2, iStatus);
        }

        private void PayCardFilter(DateTime date1, DateTime date2, int status)
        {
            var list = PayCardData.instance.GetListExport(date1, date2, status);
            Contents.DataSource = list;
            Contents.DataBind();
            LtTotal.Text = "Tổng số giao dịch:" + list.Count().ToString();
            Session[sSessionList] = list;
        }

        protected void btnExportAll_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }

        private void ExportExcel()
        {
            var list = (List<PayCardInfo>)Session[sSessionList];
            if (list == null || list.Count() == 0)
            {
                Alert.Show("Không có dữ liệu!");
                return;
            }
            try
            {
                string sdate = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                string fileName ="AnVietCard_" + sdate + ".xlsx";
                var fullpath = Server.MapPath("~/Admin/Export/" + fileName);
                using (var xlPackage = ExcelHelper.OpenFromFile(fullpath))
                {
                    //Note: http://excelpackage.codeplex.com/wikipage?title=Creating%20an%20Excel%20spreadsheet%20from%20scratch&referringTitle=Home

                    const string exportName = "PayCard";
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add(exportName);

                    //Add table headers going cell by cell.
                    worksheet.Cell(1, 1).Value = "STT";
                    worksheet.Cell(1, 2).Value = "ID";
                    worksheet.Cell(1, 3).Value = "Mã hợp đồng";
                    worksheet.Cell(1, 4).Value = "Mã thẻ";
                    worksheet.Cell(1, 5).Value = "Mã giao dịch";
                    worksheet.Cell(1, 6).Value = "Thông báo";
                    worksheet.Cell(1, 7).Value = "Thời gian";

                    worksheet.Column(3).Width = 25;
                    worksheet.Column(4).Width = 30;
                    worksheet.Column(6).Width = 60;
                    worksheet.Column(7).Width = 30;

                    worksheet.Column(3).StyleID = 1;
                    //Fill dòng
                    for (int k = 0; k < list.Count(); k++)
                    {
                        var c = list[k];
                        worksheet.Cell(k + 2, 1).Value = (k + 1).ToString();
                        worksheet.Cell(k + 2, 2).Value = c.ID.ToString();
                        worksheet.Cell(k + 2, 3).Value = c.UserId;
                        worksheet.Cell(k + 2, 4).Value = c.CardId;
                        worksheet.Cell(k + 2, 5).Value = c.ResulId;
                        worksheet.Cell(k + 2, 6).Value = c.Msg;
                        worksheet.Cell(k + 2, 7).Value = string.Format("{0:HH:mm:ss dd/MM/yyyy}", c.CreateDate);
                    }

                    xlPackage.Workbook.Properties.Title = exportName;
                    //xlPackage.Workbook.Properties.Author = "kienthuc.net.vn";
                    //xlPackage.Workbook.Properties.SetCustomPropertyValue("nhuanbut", sdate);

                    xlPackage.Save();

                    //Alert.Show("Export thành công ra tệp " + newFile.FullName.Replace("\\", "/"));
                    Response.Redirect("/Admin/Export/" + fileName);

                }
            }
            catch (Exception theException)
            {
                Alert.Show(theException.Message.Replace("\\", "/"));
            }
        }
    }
}