
using co_ordinates;
using OfficeOpenXml;
using RestSharp;
using Newtonsoft.Json;
using APIResultData;
using co_ordinates.Pages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System.Web.Helpers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace FileReader
{
    public class ExcelFileReader : Controller
    {
        static Singleton singleton = Singleton.Instance;
        //private readonly IHubContext<ProgressHub> _hubContext;


        [HttpGet]
        public string MessageCount()
        {
            string outputData = "fffff";

            return outputData;
        }


        public static void ReadExcelFile(string filePath, string TemplateID)
        {
            string APIKey = "api-B3F9F7F4E02C11ED9CBCF23C91BBF4A0";
            singleton.count = 0;
            singleton.countstr = "Processing records..";
            singleton.totalcount = 0;
            List<Addresses> AddressesList = new List<Addresses>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))            {
                // Get the first worksheet                
                var worksheet = package.Workbook.Worksheets[0];                
                var count = 0;
                int GetLastUsedRow(ExcelWorksheet sheet)
                {
                    if (sheet.Dimension == null) { return 0; } // In case of a blank sheet
                    var row = sheet.Dimension.End.Row;
                    while (row >= 1)
                    {
                        var range = sheet.Cells[row, 1, row, sheet.Dimension.End.Column];
                        if (range.Any(c => !string.IsNullOrEmpty(c.Text)))
                        {
                            break;
                        }
                        row--;
                    }
                    return row;
                }
                var totalItems = (GetLastUsedRow(worksheet)) - 1;
                singleton.totalcount = totalItems;
                // Loop through all rows in the worksheet
                for (int row = 2; row <= totalItems + 1; row++)
                {
                    // Loop through all columns in the row
                    //for (int col = 1; col <= 2; col++)                  
                    count++;
                    UploadModel model = new UploadModel();
                    singleton.count = count;
                    model.LoadingData = "Processing record " + count + " of " + totalItems;
                    singleton.countstr = "Processing record " + count + " of " + totalItems ;
                    // Get the cell value at the current row and column
                    var cellValue1 = worksheet.Cells[row, 1].Value;
                    if (cellValue1 != null && cellValue1 != "")
                    {

                        //var cellValue2 = worksheet.Cells[row, 2].Value;

                        // Do something with the cell value (e.g. print it to the console)
                        Addresses addresses = new Addresses();
                        addresses.address = cellValue1.ToString();
                        AddressesList.Add(addresses);

                        //Get Email Address and send

                        var client = new RestClient("https://api.smtp2go.com/v3/email/send");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        string body = "{\r\n  \"api_key\": \"" + APIKey + "\",\r\n  \"to\": [\r\n    \"" + addresses.address + "\"\r\n  ],\r\n  \"sender\": \"Build it <no-reply@buildit.co.za>\",\r\n  \"template_id\": \"" + TemplateID + "\",\r\n  \"template_data\": {},\r\n  \"custom_headers\": [\r\n    {\r\n      \"header\": \"Reply-To\",\r\n      \"value\": \"Customer Queries <michelle.dickson@spar.co.za>\"\r\n    }\r\n  ]\r\n}";
                        request.AddParameter("application/json", body, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        Console.WriteLine(response.Content);
                        worksheet.Cells[row, 2].Value = response.Content;
                    }
                    else
                    {
                        continue;
                    }
                    
                }
                package.Save();
                singleton.countstr = "Completed record " + count + " of " + totalItems;
                Thread.Sleep(1500);
            }            
        }
    }
}

