namespace Gymby.UITests
{
    public class TestBase : IDisposable
    {
        protected const string BaseUrl = "https://gymby-web.azurewebsites.net/";
        protected const string DemoEmail = "demonstration1@gmail.com";
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
