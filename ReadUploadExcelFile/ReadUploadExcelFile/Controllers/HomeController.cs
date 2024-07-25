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
                string rootPath = Server.MapPath("~/App_Data/Uploads"); // Ensure this directory exists
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
                            db.SaveChanges();
                            i++;
                        }
                        
                    }

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
        
        public ActionResult AddStudent()
        {
            return RedirectToAction("Index");
        }
    }
}