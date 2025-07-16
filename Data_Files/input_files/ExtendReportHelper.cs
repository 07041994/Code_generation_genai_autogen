using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Runtime.InteropServices;

namespace MyAccount.Utils
{
    public static class ExtentReporterHelper
    {
        private static ExtentReports extent;
        private static ExtentTest test;

        public static void SetupReporting(string reportPath = "Spark.html")
        {
            var sparkReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);
            extent.AddSystemInfo("Operating System", GetOperatingSystemInfo());
            extent.AddSystemInfo("Browser", TestContext.Parameters.Get("browser", "chrome"));
        }

        public static void CreateTest(string testName)
        {
            test = extent.CreateTest(testName);
        }

        public static void LogTestResult(NUnit.Framework.Interfaces.TestStatus status, string stacktrace = "")
        {
            Status logstatus;

            switch (status)
            {
                case NUnit.Framework.Interfaces.TestStatus.Failed:
                    logstatus = Status.Fail;
                    test.Log(logstatus, "Test ended with " + logstatus + " – " + stacktrace);
                    break;
                case NUnit.Framework.Interfaces.TestStatus.Skipped:
                    logstatus = Status.Skip;
                    test.Log(logstatus, "Test ended with " + logstatus);
                    break;
                default:
                    logstatus = Status.Pass;
                    test.Log(logstatus, "Test ended with " + logstatus);
                    break;
            }
        }

        public static string CaptureScreenshot(IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string screenshotPath = $"Screenshots\\screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string screenshotDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "Screenshots");
            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }

        public static string FormatBold(string message)
        {
            return $"<b>{message}</b>";
        }

        public static void LogCustomAssertSuccess(string message, string screenshotPath)
        {
            test.Log(Status.Pass, message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
        }

        public static void LogCustomAssertFailure(string message, string screenshotPath)
        {
            test.Log(Status.Fail, FormatBold(message), MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
        }

        private static string GetOperatingSystemInfo()
        {
            return RuntimeInformation.OSDescription;
        }

        public static void Flush()
        {
            extent.Flush();
        }
    }
}
