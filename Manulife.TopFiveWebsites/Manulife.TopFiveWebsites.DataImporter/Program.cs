using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;

using Manulife.TopFiveWebsites.Service;

namespace Manulife.TopFiveWebsites.DataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //read config param
                Console.WriteLine("Start reading configuration parameters ...");
                var dataFolderPath = ConfigurationManager.AppSettings["DataFolderPath"];
                var dataFilename = ConfigurationManager.AppSettings["DataFilename"];
                bool deleteAfterImport;
                if (!bool.TryParse(ConfigurationManager.AppSettings["DeleteAfterImport"], out deleteAfterImport))
                    deleteAfterImport = false;

                //ninject construction
                Console.WriteLine("Start constructing dependency injection object map ...");
                IKernel kernel = new StandardKernel();
                kernel.Load(Assembly.GetExecutingAssembly());
                var visitLogService = kernel.Get<IVisitLogService>();

                //check file exist
                var csvFilePath = Path.Combine(dataFolderPath, dataFilename);
                if (!File.Exists(csvFilePath))
                {
                    Console.WriteLine($"File not exists - {csvFilePath}");
                    return;
                }

                //import csv file
                Console.WriteLine("Start importing csv file to database ...");
                using (var reader = File.OpenText(csvFilePath))
                {
                    var count = visitLogService.ImportVisitLogSource(reader);
                    Console.WriteLine($"Completed - imported {count} records");
                }

                //delete csv file
                if(deleteAfterImport)
                {
                    Console.WriteLine("Start deleting csv file ...");
                    File.Delete(csvFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!!ERROR - {ex.Message}");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
