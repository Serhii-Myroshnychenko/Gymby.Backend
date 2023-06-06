namespace Gymby.UITests.Tests
{
    public class ProfilePageTests : TestBase
    {
        private const string BaseUrl = "https://gymby-web.azurewebsites.net/";
        private const string DemoEmail = "demonstration1@gmail.com";
        private const string DemoPassword = "TestUser123";
        private const string AvatarFilePath = "D:\\Project\\Gymby.Backend\\Gymby.UITests\\Data\\ProfilePage\\tommy3.jpg";
        private const string PhotoFilePath1 = "D:\\Project\\Gymby.Backend\\Gymby.UITests\\Data\\ProfilePage\\tommy1.jpg";
        private const string PhotoFilePath2 = "D:\\Project\\Gymby.Backend\\Gymby.UITests\\Data\\ProfilePage\\tommy2.jpg";
        private const string PhotoFilePath3 = "D:\\Project\\Gymby.Backend\\Gymby.UITests\\Data\\ProfilePage\\tommy4.jpeg";

        [Fact]
        public void AuthorizationAndProfileDataFill()
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

            var fileInputElement1 = driver.FindElement(By.CssSelector(".PersonalData_avatarBlock__input__iDQkv"));
            Thread.Sleep(TimeSpan.FromSeconds(2));
            fileInputElement1.SendKeys(AvatarFilePath);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Buttons_buttonGreen__Mbjpl")).Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));
            var input1 = driver.FindElement(By.CssSelector(".PersonalData_inputData__row__VLGhF:nth-child(1) > .PersonalData_inputData__item__Up418:nth-child(1) > .Inputs_inputGrey__k3ZMQ"));
            input1.Clear();
            input1.SendKeys("Thomas");

            var input2 = driver.FindElement(By.CssSelector(".PersonalData_inputData__row__VLGhF:nth-child(1) > .PersonalData_inputData__item__Up418:nth-child(2) > .Inputs_inputGrey__k3ZMQ"));
            input2.Clear();
            input2.SendKeys("Shelby");

            Thread.Sleep(TimeSpan.FromSeconds(1));
            var input3 = driver.FindElement(By.CssSelector(".Textarea_textareaGrey__5EBUw"));
            input3.Clear();
            input3.SendKeys("I'm Thomas Shelby, leader of the Peaky Blinders. Don't mistake my calm demeanor for weakness because beneath it lies a cunning mind and an unwavering determination. I've clawed my way up from the grimy streets of Birmingham to become a force to be reckoned with in the criminal underworld.");

            Thread.Sleep(TimeSpan.FromSeconds(1));
            var input4 = driver.FindElement(By.CssSelector(".PersonalData_socialMedia__block__u3PYb:nth-child(1) > .Inputs_inputGrey__k3ZMQ"));
            input4.Clear();
            input4.SendKeys("thomas.shelby");

            Thread.Sleep(TimeSpan.FromSeconds(1));
            var input5 = driver.FindElement(By.CssSelector(".PersonalData_socialMedia__block__u3PYb:nth-child(2) > .Inputs_inputGrey__k3ZMQ"));
            input5.Clear();
            input5.SendKeys("@shelby.thomas");

            var fileInputElement = driver.FindElement(By.CssSelector(".AddingCardPhotos_addingCard__input__gf0xu"));
            fileInputElement.SendKeys(PhotoFilePath1);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Buttons_buttonGreen__Mbjpl")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(4));

            fileInputElement.SendKeys(PhotoFilePath2);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Buttons_buttonGreen__Mbjpl")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            fileInputElement.SendKeys(PhotoFilePath3);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Buttons_buttonGreen__Mbjpl")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            driver.FindElement(By.LinkText("Переглянути особистий профіль")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(4));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 360);");
            driver.FindElement(By.CssSelector(".CarouselProfile_carouselProfile__arrow_right__etwjp > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.CssSelector(".CarouselProfile_carouselProfile__arrow_right__etwjp > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.CssSelector(".CarouselProfile_carouselProfile__arrow__GBe-K:nth-child(1) > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.CssSelector(".CarouselProfile_carouselProfile__arrow__GBe-K:nth-child(1) > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            driver.FindElement(By.CssSelector(".Buttons_buttonExit__s\\+qhp > span")).Click();
            driver.FindElement(By.LinkText("Заміри тіла")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(3));
            driver.FindElement(By.CssSelector(".Buttons_buttonGreen__Mbjpl")).Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.CssSelector(".MeasurementsItem_editButton__x7IUe > img")).Click();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).Click();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).Clear();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).SendKeys("10");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.CssSelector(".MeasurementsItem_applyButton__PzQWG > img")).Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.CssSelector(".MeasurementsItem_editButton__x7IUe > img")).Click();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).Click();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).Clear();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).SendKeys("115");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.CssSelector(".MeasurementsItem_applyButton__PzQWG > img")).Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".MeasurementsItem_editButton__x7IUe > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".MeasurementsItem_deleteButton__iuuTP > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }
    }
}