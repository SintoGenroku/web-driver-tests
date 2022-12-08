using OpenQA.Selenium;
using WebDriverTests.Helpers;

namespace WebDriverTests.Pages
{
    class SamsungPage : Bases.BasePage
    {
        private const string _homepage = @"https://www.samsung.com/ru";
        private const string _login = "el.mail212@gmail.com";
        private const string _password = "90rUYdfKLUpLYze90";

        private string _inaccessibilityMessageBuffer;

        public int LastUpPositionId { get; set; }
        public DateTime LastUpPositionOpenTime { get; set; }
        public DateTime LastUpPositionCloseTime { get; set; }

        public IWebElement CarouselButton => _wait.Until(_driver => _driver.FindElement(By.Id("home-kv-carousel-slide-1")));
            
        public IWebElement DetailsButton => _wait.Until(_driver => _driver.FindElement(
                By.XPath("/html/body/div[4]/div[4]/div/div/div[2]/div/div[1]/section/div[2]/div[1]/div[2]/div/div[1]/div/a")));
        public IWebElement LogInButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".main-header__btn.btn.btn--black.p-open")));
        public IWebElement GoToMobileButton => _wait.Until(_driver => _driver.FindElement(By.ClassName("welcome__btn")));
        public IWebElement GoToTabletsButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".overlay.a-open.overlay--active > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__wrapper > .btn.btn--black")));
        public IWebElement AllTabletsButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".trading-btn.trading-btn2.trading-btn--up.content_btn_call.content_btn_call2")));

        public IWebElement LoginInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in1")));
        public IWebElement PassordInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in2")));

        public IWebElement ComparingIsUnawailableLabel => _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title")));
        public IWebElement CurrencyLabel => _wait.Until(_driver => _driver.FindElement(By.ClassName("investment_currency")));
        public IWebElement LastPositionDataLabel => _wait.Until(_driver => _driver.FindElement(By.XPath("/html/body/div[5]/div/div[3]/div[5]/div[2]/div[1]/table/tbody/tr[1]/th[2]")));
        public IWebElement LastUpPositionTotalLabel => _wait.Until(_driver => _driver.FindElement(By.Id($"total_{LastUpPositionId}")));
        public IWebElement TotalInput => _wait.Until(_driver => _driver.FindElement(By.Id("tablet")));

        public bool IsInaccessibilityMessageShown => TryToAddToComparingMessage(out _inaccessibilityMessageBuffer) && !string.IsNullOrEmpty(_inaccessibilityMessageBuffer);
        public bool IsPlatformWorking => IsTodayIsWorkDay && IsNowIsWorkTime;
        public bool IsInaccessibilityMessageShownCorrectly => IsInaccessibilityMessageShown ^ IsPlatformWorking;
        public string LastPositionTotal => LastUpPositionTotalLabel.Text;

        public bool IsRedirectedToDetails =>
            _driver.Url == "https://www.samsung.com/ru/smartphones/galaxy-s22-ultra/";

        public SamsungPage(IWebDriver driver) : base(driver)
        {
        }

        public void OpenPage()
        {
            _driver.Url = _homepage;
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        public void ViewCarousel()
        {
            CarouselButton.Click();
        }

        public void ViewDetails()
        {
            DetailsButton.Click();
        }

        public void OpenLoginWindow()
        {
            LogInButton.Click();
        }

        public void InputLogin()
        {
            LoginInput.SendKeys(_login);
        }

        public void InputPasswordAndConfirm()
        {
            PassordInput.SendKeys(_password + Keys.Enter);
        }

        public void GoToTablets()
        {
            GoToMobileButton.Click();
            GoToTabletsButton.Click();
            AllTabletsButton.Click();
        }

        public bool TryToAddToComparingMessage(out string _inaccessibilityMessage)
        {
            try
            {
                _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title > .sgt-active-4-pro > .compare-btn"))).Click();
                _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title > .sgt-s-8 > .compare-btn"))).Click();
                _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title > .sgt-s-8-plus > .compare-btn"))).Click();
                _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title > .sgt-active-4 > .compare-btn"))).Click();
                _inaccessibilityMessage = ComparingIsUnawailableLabel.Text;
                return true;
            }
            catch (OpenQA.Selenium.WebDriverException e)
            {
                _inaccessibilityMessage = string.Empty;
                return false;
            }
        }

        public void AllTablets()
        {
            AllTabletsButton.Click();
        }

        public void InitializeLastUpPositionData()
        {
            var lastUpPositionInfo = LastPositionDataLabel.Text;

            LastUpPositionId = int.Parse(lastUpPositionInfo.Substring(0, 9));
            LastUpPositionOpenTime = DateTime.Parse(lastUpPositionInfo.Substring(11, 19)).TruncateSecond().TruncateMillisecond();
            LastUpPositionCloseTime = DateTime.Parse(lastUpPositionInfo.Substring(32, 19)).TruncateSecond().TruncateMillisecond();
        }
        public void SetTotalValue(int value)
        {
            TotalInput.SendKeys(Keys.Control + "a" + Keys.Control);
            TotalInput.SendKeys(Keys.Delete);
            TotalInput.SendKeys($"{value}");
        }

        private bool IsTodayIsWorkDay => DateTime.Now.DayOfWeek >= (DayOfWeek)1 && DateTime.Now.DayOfWeek <= (DayOfWeek)5;
        private bool IsNowIsWorkTime => DateTime.Now.Hour >= 4 && DateTime.Now.Hour <= 23;
    }
}
