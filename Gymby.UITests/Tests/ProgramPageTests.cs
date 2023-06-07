namespace Gymby.UITests.Tests
{
    public class ProgramPageTests : TestBase
    {
        [Fact]
        public void AuthorizationAndProgramDataFill()
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
            var cabinetIcon = driver.FindElement(By.CssSelector(".NavbarMain_header__iconCabinet__00KDg > img"));
            cabinetIcon.Click();
        }
    }
}
