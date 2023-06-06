namespace Gymby.UITests
{
    public class TestBase : IDisposable
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

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
