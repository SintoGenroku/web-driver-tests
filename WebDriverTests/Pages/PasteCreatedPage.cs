using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverTests.Pages
{
    public class PasteCreatedPage : Bases.BasePage
    {
        public IWebElement RawPasteDataText => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".post-view > textarea")));
        public IWebElement HighlightLanguage => _wait.Until(_driver => _driver.FindElement(By.CssSelector("div.top-buttons > div.left > a")));
        public IWebElement ExpirationTime => _wait.Until(_driver => _driver.FindElement(By.CssSelector("div.info-bottom > div.expire")));
        public IWebElement Title => _wait.Until(_driver => _driver.FindElement(By.CssSelector("div.info-bar > div.info-top > h1")));

        public PasteCreatedPage(IWebDriver driver) : base(driver)
        {
            _wait.Until(_driver => _driver.FindElement(By.ClassName("user-icon")));
        }
    }
}
