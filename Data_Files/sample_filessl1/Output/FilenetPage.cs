using OpenQA.Selenium;
using MyAccount.Utils;

namespace MyAccount.PageObjects
{
    public class FilenetPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public FilenetPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement AcquisitionByAccountNumber => driver.FindElement(By.XPath("//*[@id='dijit__TreeNode_3_label']"));
        public IWebElement AccountNumber => driver.FindElement(By.XPath("//*[@id='ecm_widget_search_SearchForm_3_ecm.widget.SearchCriterian_0']"));
        public IWebElement SearchButton => driver.FindElement(By.XPath("//*[@id='dijit_form_Button_13_label']"));
        public IWebElement PDFDocument => driver.FindElement(By.XPath("img[alt='application/pdf']"));

        public void EnterAccountNumber(string accountNumber)
        {
            genericHelper.sendKeys(AccountNumber, accountNumber, "Account Number Field");
        }

        public void ClickSearchButton()
        {
            genericHelper.clickOn(SearchButton, "Search Button");
        }
    }
}