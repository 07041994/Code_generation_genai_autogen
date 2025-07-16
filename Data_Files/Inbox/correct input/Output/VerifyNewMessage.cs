using MyAccount.PageObjects;
using MyAccount.Utils;
using OpenQA.Selenium.BiDi.Communication;
using static MyAccount.Utils.ExtendAssert;


//----------Test Steps
// 1. Login to MyAccount
// 2. Close past due popup and Verify Account summary header
// 3. Click on Inbox Link and verify all messages text
// 4. Click on New Message Link
// 5. Verify conatct custmer header
// 6. Fill all details and click submit button
// 7. Verify thank you message
// 8. Click on Continue
// 9. LogOut

namespace MyAccount.TestCases
{
    public class MyAccountInboxVerifyNewMessage:BrowserHelper
    {
        LoginPage loginPage;
        InboxPage inboxPage;
        MyAccountSummaryPage accountSummaryPage;

        [SetUp]
        public void SetupTest()
        {
            loginPage = new LoginPage(driver);
            inboxPage = new InboxPage(driver);
            accountSummaryPage = new MyAccountSummaryPage(driver);
        }

        [Test]
        public void InboxValidation()
        {
            driver.Navigate().GoToUrl(appUrl);
            loginPage.LoginEnterUsernameDetails();
            accountSummaryPage.closePastDuePopup();
            accountSummaryPage.verifyAccountSummaryHeader();

            string inboxMessageText = inboxPage.ValidateNewMsgInInbox();

            Assertion.That(driver, inboxMessageText, Is.EqualTo("All Messages"), 
                "Inbox message didn't match. Expected : All Messages but Actual is : "+ inboxMessageText
                , "Inbox message matched. Expected : All Messages and Actual is : " + inboxMessageText);

            string custServiceHeadText = inboxPage.ValidateContactCustomerServiceHeaderText();

            Assertion.That(driver, custServiceHeadText, Is.EqualTo("Contact Customer Service"),
                "Contact customer service message didn't match. Expected : Contact Customer Service but Actual is : " + custServiceHeadText
                , "Contact customer service message matched. Expected : Contact Customer Service and Actual is : " + custServiceHeadText);

            string thankYouMessageText = inboxPage.ValidateThankYouMessageText();

            Assertion.That(driver, thankYouMessageText, Is.EqualTo("Thank you for contacting us. Your message will be routed to the appropriate personnel."),
                "thank you message didn't match. Expected : Thank you for contacting us. Your message will be routed to the appropriate personnel. but Actual is : " + thankYouMessageText
                , "thank you message matched. Expected : Thank you for contacting us. Your message will be routed to the appropriate personnel. and Actual is : " + thankYouMessageText);
            
            inboxPage.ClickContinueButton();
            
            loginPage.MyAccLogOut();

        }
        
    }
}
