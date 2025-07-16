using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace MyAccount.Utils
{
    public class BrowserHelper
    {
        public IWebDriver driver;
        public string appUrl;
        public string environment;
        public string platform;

        [OneTimeSetUp]
        public void SetupReporting()
        {
            ExtentReporterHelper.SetupReporting();
        }

        [SetUp]
        public void Setup()
        {
            string browser = TestContext.Parameters.Get("browser", "chrome");
            appUrl = TestContext.Parameters.Get("appUrl", "https://www.google.com");
            environment = TestContext.Parameters.Get("environment", "test");
            platform = TestContext.Parameters.Get("platform", "Windows");

            string driverPath = TestContext.CurrentContext.TestDirectory;

            switch (browser?.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver(driverPath);
                    Console.WriteLine("Chrome is the browser selected for this test execution");
                    break;
                case "firefox":
                    driver = new FirefoxDriver(driverPath);
                    break;
                case "edge":
                    driver = new EdgeDriver(driverPath);
                    break;
                default:
                    driver = new ChromeDriver(driverPath);
                    break;
            }
            //ExtentReporterHelper.SetupReporting(driver);
            driver.Manage().Window.Maximize();
            ExtentReporterHelper.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : TestContext.CurrentContext.Result.StackTrace;

            ExtentReporterHelper.LogTestResult(status, stacktrace);
            ExtentReporterHelper.Flush();
        }
    }
}
