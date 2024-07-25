using ClosedXML.Excel;
using ExcelDataReader;
using ReadUploadExcelFile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ReadUploadExcelFile.Controllers
{
    public class HomeController : Controller
    {
        StudentEntities db = new StudentEntities();
        public ActionResult Index()
        {
            var students = db.Students.ToList();
            return View(students);
        }
        public async Task<ActionResult> UploadExcel(HttpPostedFileBase fileUpload)
        {
            List<string> sheetNames = new List<string>();
            if (fileUpload == null || fileUpload.ContentLength == 0)
            {
                ViewBag.Message = "File is empty!";
                return View();
            }

            if (fileUpload.ContentType == "application/vnd.ms-excel" || fileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(fileUpload.FileName);
                string rootPath = Server.MapPath("~/App_Data/Uploads"); 
                string filePath = Path.Combine(rootPath, fileName);

                try
                {
                    if (!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    fileUpload.SaveAs(filePath);

                    DataTableCollection tables = ReadFromExcel(filePath, ref sheetNames);

                    foreach (DataTable dt in tables)
                    {
                        var i = 0;
                        foreach (DataRow dr in dt.Rows) 
                        {
                            
                            var student = new Student();
                            student.FirstName = dt.Rows[i][0].ToString();
                            student.LastName = dt.Rows[i][1].ToString();
                            student.MiddleName = dt.Rows[i][2].ToString();
                            student.City = dt.Rows[i][3].ToString();
                            student.State = dt.Rows[i][4].ToString();
                            student.Country = dt.Rows[i][5].ToString();
                            student.StreetAddress = dt.Rows[i][6].ToString();
                            student.Email = dt.Rows[i][7].ToString();
                            student.PhoneNo = dt.Rows[i][8].ToString();
                            student.ParentName = dt.Rows[i][9].ToString();
                            student.ParentNumber = dt.Rows[i][10].ToString();
                            db.Students.Add(student);
                            i++;
                        }
                        
                    }
                    db.SaveChanges();

                    ViewBag.SheetNames = sheetNames;
                    ViewBag.Message = "File uploaded successfully!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error processing the file: " + ex.Message;
                }
                finally
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }
            else
            {
                ViewBag.Message = "Invalid file type!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExportToExcel()
        {
            var students = db.Students.ToList();
            var stream = CreateExcelFile(students);

            string excelName = $"Students-{Guid.NewGuid()}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        private DataTableCollection ReadFromExcel(string filePath, ref List<string> sheetNames)
        {
            DataTableCollection tableCollection = null;
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                    });

                    tableCollection = result.Tables;

                    foreach (DataTable table in tableCollection)
                    {
                        sheetNames.Add(table.TableName);
                    }
                }
            }
            return tableCollection;
        }

        public MemoryStream CreateExcelFile(List<Student> students)
        {
            var workbook = new XLWorkbook();
           
            IXLWorksheet worksheet = workbook.Worksheets.Add("Students");
            
            worksheet.Cell(1, 1).Value = "ID"; 
            worksheet.Cell(1, 2).Value = "FirstName"; 
            worksheet.Cell(1, 3).Value = "LastName"; 
            worksheet.Cell(1, 4).Value = "MiddleName";
            worksheet.Cell(1, 5).Value = "City"; 
            worksheet.Cell(1, 6).Value = "Country";
            worksheet.Cell(1, 6).Value = "StreetAddress";
            worksheet.Cell(1, 6).Value = "Email";
            worksheet.Cell(1, 6).Value = "PhoneNo";
            worksheet.Cell(1, 6).Value = "ParentName";
            worksheet.Cell(1, 6).Value = "ParentNumber";

            int row = 2;

            foreach (var std in students)
            {
                worksheet.Cell(row, 1).Value = std.ID;
                worksheet.Cell(row, 2).Value = std.FirstName;
                worksheet.Cell(row, 3).Value = std.LastName;
                worksheet.Cell(row, 4).Value = std.MiddleName;
                worksheet.Cell(row, 5).Value = std.City;
                worksheet.Cell(row, 6).Value = std.StreetAddress;
                worksheet.Cell(row, 6).Value = std.Email;
                worksheet.Cell(row, 6).Value = std.PhoneNo;
                worksheet.Cell(row, 6).Value = std.ParentName;
                worksheet.Cell(row, 6).Value = std.ParentNumber;
                row++; 
            }
            
            var stream = new MemoryStream();
            
            workbook.SaveAs(stream);
            
            stream.Position = 0;
            return stream;
        }

    }
}