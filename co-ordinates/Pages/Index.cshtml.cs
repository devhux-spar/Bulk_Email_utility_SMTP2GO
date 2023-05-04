using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;
using FileReader;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace co_ordinates.Pages
{
    public class UploadModel : PageModel
    {
        public string LoadingData = "";
        public void OnGet()
        {
        }

        public ActionResult GetCount()
        {

            var data = new Dictionary<string, int>
            {
                {"count", 2}
            };
            return Page();
        }

            public async Task<IActionResult> OnPostAsync(IFormCollection form)
        {
            var file = form.Files.FirstOrDefault();
            if (file != null && file.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                var templateID = form["TemplateID"];

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //Read the file
                ExcelFileReader.ReadExcelFile(filePath, templateID);


                //Provide link for user to download

                return File(System.IO.File.ReadAllBytes(filePath), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sent Emails.xlsx");

            }

            return Page();
        }
    }
}
