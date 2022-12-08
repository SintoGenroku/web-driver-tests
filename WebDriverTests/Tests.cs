using NUnit.Framework;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverTests.Helpers;
using WebDriverTests.Pages;

namespace WebDriverTests
{
    [TestFixture]
    class Test
    {
        WebDriver driver;

        [SetUp]
        public void Startup()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(200);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(200);
        }

        [Test]
        public void IsComparingInaccessibilityMessageShownCorrectly_ReturnsTrue()
        {
            var samsungPage = new SamsungPage(driver);
            samsungPage.OpenPage();
            samsungPage.ViewCarousel();
            samsungPage.ViewDetails();
            /*
            samsungPage.OpenLoginWindow();
            samsungPage.InputLogin();
            samsungPage.InputPasswordAndConfirm();
            samsungPage.GoToTablets();
            */

            Assert.IsTrue(samsungPage.IsRedirectedToDetails);
        }

    }
}
