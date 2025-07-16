using ClosedXML.Excel;
using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class MaturityModificationPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;
        DataBaseHelper dbHelper = new DataBaseHelper();

        public MaturityModificationPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }
        public IWebElement MaturityDateHeader => driver.FindElement(By.XPath("//h4[@class='h4']/strong"));
        public IWebElement Message1 => driver.FindElement(By.XPath("(//span[@data-bind='hidden: ReachedMaturityDate']/strong)[1]"));
        public IWebElement Message2 => driver.FindElement(By.XPath("(//span[@data-bind='hidden: ReachedMaturityDate']/strong)[2]"));
        public IWebElement AcknowledgeButton => driver.FindElement(By.XPath("//button[@class='btn-block']"));
        public IWebElement MatmodHeader => driver.FindElement(By.XPath("//h4[@class='h4']/strong[text()='Maturity Date']"));
        public IWebElement ClickHereLink => driver.FindElement(By.XPath("//a[contains(@data-bind, 'goToAutomateMatMod')]"));
        public IWebElement AccountMaturityHeader => driver.FindElement(By.XPath("//h3[text()='Account Maturity Modification']"));
        public IWebElement VisitFaqLink => driver.FindElement(By.XPath("//div[@class='col-xs-12 info-faq']//a"));
        public IWebElement HelpAndSupportCC => driver.FindElement(By.XPath("//h1[@data-testid='welcome-title']"));
        public IWebElement HelpAndSupportScusa => driver.FindElement(By.XPath("//h1[contains(text(),'Santander Help and Support')]"));
        public IWebElement RequestMatMod => driver.FindElement(By.XPath("//*[@id='display-mat-mod-card']/div/div[4]/div/div/div/div[2]/a/div"));
        public IWebElement ContinueButton => driver.FindElement(By.XPath("//*[@id='request-mat-mod-data']/div/div[7]/div[1]/div[2]/div/button"));
        public IWebElement AgreementDocument => driver.FindElement(By.XPath("//*[@id='request-mat-mod-agreement']/div/div[2]/div/span[1]/a"));
        public IWebElement ConsentButton => driver.FindElement(By.Id("consent-mat-mod"));
        public IWebElement SubmitButton => driver.FindElement(By.XPath("//*[@id='request-mat-mod-agreement']/div/div[3]/div/div/button[1]"));
        public IWebElement CancelButton => driver.FindElement(By.XPath("//button[@class='small btn-primary submit-button secondary' and contains(text(),'Cancel')]"));
        public IWebElement ConsentCheckbox => driver.FindElement(By.Id("consent-mat-mod"));
        public IWebElement AgreementAcknowledgement => driver.FindElement(By.XPath("//*[@id='request-mat-mod-consent-confirmation']/div/div[2]/div/span[1]/strong"));
//        public IWebElement MaturityDateHeader => driver.FindElement(By.XPath("//h4[@class='h4']/strong[text()='Maturity Date']"));
        public IWebElement AccountMaturityModificationHeader => driver.FindElement(By.XPath("//h3[text()='Account Maturity Modification']"));
        public IWebElement RequestMaturityModificationLink => driver.FindElement(By.XPath("//h4[text()='Request a Maturity Modification']"));
        public IWebElement AcknowledgementBtn => driver.FindElement(By.XPath("//*[@id='submit-mat-mod-acknowledgement']/div/div/button"));

        public IWebElement PayoffRemainingBalance => driver.FindElement(By.XPath("//h4[text()='Payoff your remaining balance']"));
        public int matModHeaderCount => driver.FindElements(By.XPath("//h4[@class='h4']/strong[text()='Maturity Date']")).Count;
        
        public IWebElement SplashPageHeader => driver.FindElement(By.XPath("//h4[@class='h4']/strong[text()='Maturity Date']"));
 //       public IWebElement AcknowledgeButton => driver.FindElement(By.XPath("//*[@id='submit-mat-mod-acknowledgement']/div/div/button"));
        public IWebElement MaturityMessage => driver.FindElement(By.XPath("(//div//span//strong[contains(text(),'Your account ')])[1]"));

        public IWebElement MaturityModHeader => driver.FindElement(By.XPath("//h3[text()='Account Maturity Modification']"));
        public IWebElement MaturityDate => driver.FindElement(By.XPath("(//div[@id='display-mat-mod-card']//span[@data-bind='text: MaturityDate'])[2]"));

        public IWebElement MMMessage => driver.FindElement(By.XPath("//p[contains(text(),'Your account')]"));

        public string GetMMMessageText()
        {
            genericHelper.waitForElement(MMMessage);
            return MMMessage.Text;
        }

        public void ValidateClickHereLinkDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(ClickHereLink, "Click Here Link");
        }

        public void ValidateAcknowledgeButtonDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(AcknowledgeButton, "Acknowledge Button");
        }
   /*     public string GetMaturityModHeaderText()
        {
            genericHelper.waitForElement(MaturityModHeader);
            return MaturityModHeader.Text;
        }
   */   

        public DateTime GetMaturityDate()
        {
            genericHelper.waitForElement(MaturityDate);
            string dateText = MaturityDate.Text;
            return DateTime.Parse(dateText);
           
        }
        public void ClickAcknowledgeButton()
        {
            genericHelper.clickOn(AcknowledgeButton, "Acknowledge Button");
        }

        public string GetSplashPageHeaderText()
        {
            genericHelper.waitForElement(SplashPageHeader);
            return SplashPageHeader.Text;
        }

        public string GetMaturityMessageText()
        {
            genericHelper.waitForElement(MaturityMessage);
            return MaturityMessage.Text;
        }
        public void ClickClickHereLink()
        {
            genericHelper.clickOn(ClickHereLink, "Click Here Link");
        }

        public string GetMatmodHeaderText()
        {
            genericHelper.waitForElement(MatmodHeader);
            return MatmodHeader.Text;
        }
        public string GetMaturityDateHeaderText()
        {
            genericHelper.waitForElement(MaturityDateHeader);
            return MaturityDateHeader.Text;
        }

        public string GetMessage1Text()
        {
            genericHelper.waitForElement(Message1);
            return Message1.Text;
        }

        public string GetMessage2Text()
        {
            genericHelper.waitForElement(Message2);
            return Message2.Text;
        }

        public void ValidateAcknowledgeButtonPresence()
        {
            genericHelper.ValidateElementPresentOrNot(AcknowledgeButton, "Acknowledge Button");
        }

        public string GetAccountMaturityHeaderText()
        {
            Thread.Sleep(2000);
            genericHelper.waitForElement(AccountMaturityHeader);
            return AccountMaturityHeader.Text;
        }
        public void ClickAcknowledgementButton() 
        {
            genericHelper.clickOn(AcknowledgementBtn, "Acknowledgement Button");
        }

        public void ClickVisitFaqLink()
        {
            genericHelper.clickOn(VisitFaqLink, "Visit FAQ Link");
        }

        public string GetAccountMaturityModificationHeaderText()
        {
            genericHelper.waitForElement(AccountMaturityModificationHeader);
            return AccountMaturityModificationHeader.Text;
        }

        public void ClickRequestMaturityModificationLink()
        {
            genericHelper.clickOn(RequestMaturityModificationLink, "Request Maturity Modification Link");
        }

        public void ClickPayoffRemainingBalance()
        {
            genericHelper.clickOn(PayoffRemainingBalance, "Payoff Remaining Balance");
        }

        public string GetHelpAndSupportLinkCC()
        {
            genericHelper.waitForElement(HelpAndSupportCC);
            return HelpAndSupportCC.Text;
        }

        public void ClickRequestMatMod()
        {
            genericHelper.clickOn(RequestMatMod, "Request Maturity Modification");
        }
        public void ClickConsentCheckbox()
        {
            genericHelper.clickOn(ConsentCheckbox, "Consent Checkbox");
        }
        public void ClickContinueButton()
        {
            genericHelper.clickOn(ContinueButton, "Continue Button");
        }

        public void ClickAgreementDocument()
        {
            genericHelper.clickOn(AgreementDocument, "Agreement Document");
        }

        public void ClickConsentButton()
        {
            genericHelper.clickOn(ConsentButton, "Consent Button");
        }

        public void ClickCancelButton()
        {
            genericHelper.clickOn(CancelButton, "Cancel Button");
        }
        

        public void ClickSubmitButton()
        {
            genericHelper.clickOn(SubmitButton, "Submit Button");
        }

        public string GetAgreementAcknowledgementText()
        {
            genericHelper.waitForElement(AgreementAcknowledgement);
            genericHelper.waitForElementToBeVisible(AgreementAcknowledgement, "We have now executed the modification agreement.");
            Thread.Sleep(3000);
            return AgreementAcknowledgement.Text;
        }
        public bool IsSubmitButtonDisabled()
        {
            genericHelper.waitForElement(SubmitButton);
            return !SubmitButton.Enabled;
        }
        public string GetHelpAndSupportLinkScusa()
        {
            genericHelper.waitForElement(HelpAndSupportScusa);
            return HelpAndSupportScusa.Text;
        }

        public void UpdateCategoryIdOfAccountTo1(string AccountId)
        {
            string squery = $@"use Servicing update MatureMod.QualificationDaily set CategoryID=3 where Accountid = '{AccountId}' (select Accountid,CategoryID from  MatureMod.QualificationDaily where Accountid='{AccountId}')";

            Dictionary<string, object> objResults = new Dictionary<string, object>();
            objResults = dbHelper.fetchValuesFromDB(squery);

            if(objResults.Count > 0)
            {
                Console.WriteLine("Category id updated to 3");
            }

        }
    }
}