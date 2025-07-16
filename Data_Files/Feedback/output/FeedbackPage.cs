using log4net;
using MyAccount.PageObjects;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccount.PageObjects
{
    public class FeedbackPage(IWebDriver driver)
    {
        
        public IWebElement FeedbackTab => driver.FindElement(By.Id("feedback-tab"));
        public IWebElement FeedbackAndSupportHeaderCC => driver.FindElement(By.XPath("//div[@class='entry-content']/img"));
        public IWebElement FeedbackAndSupportHeaderScusa => driver.FindElement(By.XPath("//div[@class='entry-content']/img"));
        public IWebElement ContentScusaRadioBtn => driver.FindElement(By.XPath("//div[@id='wpforms-38047-field_1-container']//li[3]"));
        public IWebElement ContentCCRadioBtn => driver.FindElement(By.XPath("//div[@id='wpforms-59688-field_1-container']//li[3]"));
        public IWebElement DesignScusaRadioBtn => driver.FindElement(By.XPath("//div[@id='wpforms-38047-field_2-container']//li[3]"));
        public IWebElement DesignCCRadioBtn => driver.FindElement(By.XPath("//div[@id='wpforms-59688-field_2-container']//li[3]"));
        public IWebElement UsabilityScusaRadioBtn => driver.FindElement(By.XPath("//div[@id='wpforms-38047-field_3-container']//li[3]"));
        public IWebElement UsabilityCCRadioBtn => driver.FindElement(By.XPath("//div[@id='wpforms-59688-field_3-container']//li[3]"));
        public IWebElement OverallScusaRadioBtn => driver.FindElement(By.XPath("//div[@id='wpforms-38047-field_4-container']//li[3]"));
        public IWebElement OverallCCRadioBtn => driver.FindElement(By.XPath("//div[@id='wpforms-59688-field_4-container']//li[3]"));
        public IWebElement ReasonForVisitScusaDropdown => driver.FindElement(By.Id("wpforms-38047-field_5"));
        public IWebElement ReasonForVisitCCDropdown => driver.FindElement(By.Id("wpforms-59688-field_5"));
        public IWebElement DidYouAccomplishScusaRadioBtn => driver.FindElement(By.Id("wpforms-38047-field_6_1"));
        public IWebElement DidYouAccomplishCCRadioBtn => driver.FindElement(By.Id("wpforms-59688-field_6_1"));
        public IWebElement HowEasyOrDifficultWasItScusaRadioBtn => driver.FindElement(By.Id("wpforms-38047-field_7_2"));
        public IWebElement HowEasyOrDifficultWasItCCRadioBtn => driver.FindElement(By.Id("wpforms-59688-field_7_7"));
        public IWebElement IWoulUseThisWebsiteInFutureScusaRadioBtn => driver.FindElement(By.Id("wpforms-38047-field_8_3"));
        public IWebElement IWoulUseThisWebsiteInFutureCCRadioBtn => driver.FindElement(By.Id("wpforms-59688-field_8_7"));
        public IWebElement SubmitScusaButton => driver.FindElement(By.Id("wpforms-submit-38047"));
        public IWebElement SubmitCCButton => driver.FindElement(By.Id("wpforms-submit-59688"));
        public IWebElement ThankYouMessageScusaText => driver.FindElement(By.XPath("//div[@id='wpforms-confirmation-38047']/p/strong"));
        public IWebElement ThankYouMessageCCText => driver.FindElement(By.XPath("//div[@id='wpforms-confirmation-59688']/p/strong"));

        GenericHelper genericHelper = new GenericHelper(driver);
        string Brand = "Chrystel Capital";
        string title;
        string newTabHandle;

        public void NavigateToFeedbackPage()
        {
            genericHelper.clickOn(FeedbackTab, "Feedback Tab");

            newTabHandle = driver.WindowHandles.Last();
            driver.SwitchTo().Window(newTabHandle);

            title = driver.Title;
            Console.WriteLine("Title of the page is "+title);
        }

        public string FillDetailsAndSubmitOnFeedbackPage()
        {
            if (Brand.ToString() == "Scusa")
            {
                genericHelper.clickOn(ContentScusaRadioBtn, "Content Radio Button");
                genericHelper.clickOn(DesignScusaRadioBtn, "Design Radio Button");
                genericHelper.clickOn(UsabilityScusaRadioBtn, "Usability Radio Button");
                genericHelper.clickOn(OverallScusaRadioBtn, "Overall Radio Button");
                genericHelper.selectValueFromDropdown(ReasonForVisitScusaDropdown, 1);
                genericHelper.clickOn(DidYouAccomplishScusaRadioBtn, "Did You Accomplish Radio Button");
                genericHelper.clickOn(HowEasyOrDifficultWasItScusaRadioBtn, "Easy or Difficult Radio Button");
                genericHelper.clickOn(IWoulUseThisWebsiteInFutureScusaRadioBtn, "Would You Use This In Future Radio Button");
                genericHelper.clickOn(SubmitScusaButton, "Submit Button");
                return ThankYouMessageScusaText.Text;
            }
            else
            {
                genericHelper.clickOn(ContentCCRadioBtn, "Content Radio Button");
                genericHelper.clickOn(DesignCCRadioBtn, "Design Radio Button");
                genericHelper.clickOn(UsabilityCCRadioBtn, "Usability Radio Button");
                genericHelper.clickOn(OverallCCRadioBtn, "Overall Radio Button");
                genericHelper.selectValueFromDropdown(ReasonForVisitCCDropdown, 1);
                genericHelper.clickOn(DidYouAccomplishCCRadioBtn, "Did You Accomplish Radio Button");
                genericHelper.clickOn(HowEasyOrDifficultWasItCCRadioBtn, "Easy or Difficult Radio Button");
                genericHelper.clickOn(IWoulUseThisWebsiteInFutureCCRadioBtn, "Would You Use This In Future Radio Button");
                genericHelper.clickOn(SubmitCCButton, "Submit Button");
                
                return ThankYouMessageCCText.Text;
            }
        }



    }

}
