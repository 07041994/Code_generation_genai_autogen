using MyAccount.PageObjects;
using MyAccount.Utils;
using static MyAccount.Utils.ExtendAssert;

namespace MyAccount.TestCases
{
    public class VerifyMyAccountInbox : BrowserHelper
    {
        LoginPage loginPage;
        MyAccountSummaryPage accountSummaryPage;
        MyAccountInboxPage inboxPage;

        [SetUp]
        public void SetupTest()
        {
            loginPage = new LoginPage(driver);
            accountSummaryPage = new MyAccountSummaryPage(driver);
            inboxPage = new MyAccountInboxPage(driver);
        }

        [Test]
        public void InboxValidation()
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
            Assertion.That(driver, accountsummaryText, Is.EqualTo("Account Summary"),
                "Account Summary displayed text didn't match. Expected : Account Summary but Actual is : " + accountSummaryPage.getAccountSummaryHeaderText(),
                "Account Summary displayed text matched. Expected : Account Summary and Actual is : " + accountSummaryPage.getAccountSummaryHeaderText());

            // Step 5: Click on Inbox link.
            inboxPage.ClickInboxLink();

            // Step 6: Verify All Messages text.
            string allMessagesText = inboxPage.GetAllMessagesText();
            Assertion.That(driver, allMessagesText, Is.EqualTo("All Messages"),
                "All Messages text didn't match. Expected : All Messages but Actual is : " + allMessagesText,
                "All Messages text matched. Expected : All Messages and Actual is : " + allMessagesText);

            // Step 7: Click on New Message Box link and verify Contact Customer Service header.
            inboxPage.ClickNewMessageBoxLink();
            string contactHeaderText = inboxPage.GetContactCustomerServiceHeaderText();
            Assertion.That(driver, contactHeaderText, Is.EqualTo("Contact Customer Service"),
                "Contact Customer Service header text didn't match. Expected : Contact Customer Service but Actual is : " + contactHeaderText,
                "Contact Customer Service header text matched. Expected : Contact Customer Service and Actual is : " + contactHeaderText);

            // Step 8: Select Account from dropdown.
            inboxPage.SelectAccountDropdown(1);

            // Step 9: Select Subject from dropdown.
            inboxPage.SelectSubjectDropdown(2);

            // Step 10: Enter message in Message Box.
            inboxPage.EnterMessage("Hi, Raising a complaint regarding payments");

            // Step 11: Click Submit button and verify Thank You message.
            inboxPage.ClickSubmitButton();
            string thankYouMessage = inboxPage.GetThankYouMessageText();
            Assertion.That(driver, thankYouMessage, Is.EqualTo("Thank you for contacting us. Your message will be routed to the appropriate personnel."),
                "Thank You message text didn't match. Expected : Thank you for contacting us. Your message will be routed to the appropriate personnel. but Actual is : " + thankYouMessage,
                "Thank You message text matched. Expected : Thank you for contacting us. Your message will be routed to the appropriate personnel. and Actual is : " + thankYouMessage);

            // Step 12: Click Continue button.
            inboxPage.ClickContinueButton();

            // Step 13: Verify Logout is successful.
            loginPage.MyAccLogOut();
        }
    }
}