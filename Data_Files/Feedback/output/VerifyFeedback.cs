using MyAccount.PageObjects;
using MyAccount.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyAccount.Utils.ExtendAssert;

namespace MyAccount.TestCases
{
    public class VerifyFeedback:BrowserHelper
    {
        FeedbackPage fpage;
        LoginPage lpage;

        [SetUp]
        public void SetupTest()
        {
            lpage = new LoginPage(driver);
            fpage = new FeedbackPage(driver);
        }
        [Test]
        public void FeedbackValidation()
        {
            driver.Navigate().GoToUrl(appUrl);

            fpage.NavigateToFeedbackPage();

            string ThankyouMessageText = fpage.FillDetailsAndSubmitOnFeedbackPage();
            Assertion.That(driver, ThankyouMessageText, Is.EqualTo("Thank you very much for your time and valuable feedback."),
                "Thank you message didn't match. Expected : Thank you very much for your time and valuable feedback. but Actual is : " + ThankyouMessageText
                , "Thank you message matched. Expected : Thank you very much for your time and valuable feedback. and Actual is : " + ThankyouMessageText);

            
        }
    }
}
