using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System;

namespace CrystalReportPdf.Api
{
    public static class AppUtils
    {
        public static ReportDocument GetReport(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                // If the file name is null or empty, throw an argument exception
                throw new ArgumentException("File name is required");
            }
            // Load the report
            ReportDocument report = new ReportDocument();

            // Combine the template path
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConstants.TemplatesFolder, fileName);

            // Check if the template file exists
            if (!File.Exists(filePath))
            {
                // Return a not found, throw an exception
                throw new FileNotFoundException("Report template not found", filePath);
            }

            // Load the report template
            report.Load(filePath);

            // Return the report
            return report;
        }
    }
}