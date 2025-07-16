using MyAccount.PageObjects;
using MyAccount.Utils;
using static MyAccount.Utils.ExtendAssert;


namespace MyAccount.TestCases
{
    public class VerifyRequestPayoffQuote : BrowserHelper
    {
        LoginPage loginPage;
        MyAccountSummaryPage accountSummaryPage;
        RequestPayoffQuotePage requestPayoffQuotePage;

        [SetUp]
        public void SetupTest()
        {
            loginPage = new LoginPage(driver);
            accountSummaryPage = new MyAccountSummaryPage(driver);
            requestPayoffQuotePage = new RequestPayoffQuotePage(driver);
        }

        [Test]
        public void RequestPayoffQuoteValidation()
        {
            driver.Navigate().GoToUrl(appUrl);

            // Step 1: Verify Login is successful.
            loginPage.LoginEnterUsernameDetails();

            // Step 2: Verify Acknowledge pop up is closed.
            accountSummaryPage.clickAcknowledgeAndClosePopup();

            // Step 3: Verify Past due pop up is closed.
            accountSummaryPage.closePastDuePopup();

            // Step 4: Verify Account Summary text.
            string accountsummaryText = accountSummaryPage.getAccountSummaryHeaderText()
            Assertion.That(driver, accountsummaryText , Is.EqualTo("Account Summary"),
                "Account Summary displayed text didn't match. Expected : Account Summary but Actual is : " + accountSummaryPage.getAccountSummaryHeaderText(),
                "Account Summary displayed text matched. Expected : Account Summary and Actual is : " + accountSummaryPage.getAccountSummaryHeaderText());

            // Step 5: Click on Request Payoff Quote link.
            requestPayoffQuotePage.ClickRequestPayoffQuote();

            // Step 6: Verify Payoff Quote header.
            string payoffHeaderText = requestPayoffQuotePage.GetRequestPayoffHeaderText();
            Assertion.That(driver, payoffHeaderText, Is.EqualTo("Payoff Quote"),
                "Payoff Quote header text didn't match. Expected : Payoff Quote but Actual is : " + payoffHeaderText,
                "Payoff Quote header text matched. Expected : Payoff Quote and Actual is : " + payoffHeaderText);

            // Step 7: Verify Estimated Payoff Quote text.
            string estimatedPayoffText = requestPayoffQuotePage.GetEstimatedPayoffQuoteText();
            Assertion.That(driver, estimatedPayoffText, Is.EqualTo("Estimated payoff at the time of request quote may vary."),
                "Estimated Payoff Quote text didn't match. Expected : Estimated payoff at the time of request quote may vary. but Actual is : " + estimatedPayoffText,
                "Estimated Payoff Quote text matched. Expected : Estimated payoff at the time of request quote may vary. and Actual is : " + estimatedPayoffText);

            // Step 8: Verify Print button is displayed.
            requestPayoffQuotePage.ValidatePrintButton();

            // Step 9: Verify Logout is successful.
            loginPage.MyAccLogOut();
        }
    }
}