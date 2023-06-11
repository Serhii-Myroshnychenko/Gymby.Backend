namespace Gymby.UITests
{
    public class TestBase : IDisposable
    {
        protected const string BaseUrl = "https://gymby-web.azurewebsites.net/";
        protected const string DemoEmail = "thomas.shelby@gmail.com";
        protected const string DemoPassword = "TestUser123";
        protected const string AvatarFilePath = "D:\\Project\\Gymby.Backend\\Gymby.UITests\\Data\\ProfilePage\\tommy3.jpg";
        protected const string PhotoFilePath1 = "D:\\Project\\Gymby.Backend\\Gymby.UITests\\Data\\ProfilePage\\tommy1.jpg";
        protected const string PhotoFilePath2 = "D:\\Project\\Gymby.Backend\\Gymby.UITests\\Data\\ProfilePage\\tommy2.jpg";
        protected const string PhotoFilePath3 = "D:\\Project\\Gymby.Backend\\Gymby.UITests\\Data\\ProfilePage\\tommy4.jpeg";

        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected IWebElement element;

        public TestBase()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void Authorize()
        {
            driver.Navigate().GoToUrl(BaseUrl);
            Thread.Sleep(TimeSpan.FromSeconds(4));

            var loginButton = driver.FindElement(By.CssSelector(".NavbarLanding_header__buttonsBody__WSWTd > .Buttons_buttonOrange__mc4Ry"));
            loginButton.Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            var emailField = driver.FindElement(By.Id("Email"));
            emailField.SendKeys(DemoEmail);

            var passwordField = driver.FindElement(By.Id("Password"));
            passwordField.SendKeys(DemoPassword);

            var loginSubmitButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            loginSubmitButton.Click();

            WaitForUrl(BaseUrl);
            Assert.True(driver.Url.Contains(BaseUrl), "Авторизация не выполнена");

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        public static Func<IWebDriver, IWebElement> ElementIsClickable(By locator)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return (element != null && element.Displayed && element.Enabled) ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        protected void WaitForUrl(string url)
        {
            wait.Until(d => d.Url.Contains(url));
        }
    }
}
