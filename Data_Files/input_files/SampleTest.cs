namespace MyAccount.TestCases
{
    using MyAccount.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static MyAccount.Utils.ExtendAssert;


    public class SampleTest : BrowserHelper
    {

        [Test, Category("SmokeTest"), Category("RegressionTest"), Property("TestCaseID", "TC001")]
        public void SmokeAndRegressionTest()
        {
            driver.Navigate().GoToUrl(appUrl);

            Assertion.That(driver, driver.Title, Is.EqualTo("Google"), 
                "The title of the web page doesn't match. Actual : Google but Expected is : " + driver.Title,
                "The title of the web page matched successfully. Actual : Google but Expected is : " + driver.Title);
        }

        [Test, Category("SmokeTest"), Category("RegressionTest"), Category("P1"), Property("TestCaseID", "TC002")]
        public void SmokeRegressionAndP1Test()
        {
            driver.Navigate().GoToUrl(appUrl);

            Assertion.That(driver, driver.Title, Is.EqualTo("Google"),
                "The title of the web page doesn't match. Actual : Google but Expected is : " + driver.Title,
                "The title of the web page matched successfully. Actual : Google but Expected is : " + driver.Title);
        }

        [Test, Category("RegressionTest"), Property("TestCaseID", "TC003")]
        public void JustRegressionTest()
        {
            driver.Navigate().GoToUrl(appUrl);

            Assertion.That(driver, driver.Title, Is.EqualTo("Google"),
                "The title of the web page doesn't match Actual : Google but Expected is : " + driver.Title,
                "The title of the web page matched successfully. Actual : Google but Expected is : " + driver.Title);

            Assertion.That(driver, driver.Title, !Is.EqualTo("Google"),
                "The title of the web page doesn't match. Actual : Google but Expected is : " + driver.Title,
                "The title of the web page matched successfully. Actual : Google but Expected is : " + driver.Title);
        }
    }
}
