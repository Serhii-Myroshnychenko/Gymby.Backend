namespace Gymby.UITests.Tests
{
    public class ProfilePageTests : TestBase
    {
        [Fact]
        public void ProfileDataFill()
        {
            Authorize();

            var cabinetIcon = driver.FindElement(By.CssSelector(".NavbarMain_header__iconCabinet__00KDg > img"));
            cabinetIcon.Click();

            var fileInputElement1 = driver.FindElement(By.CssSelector(".PersonalData_avatarBlock__input__iDQkv"));
            Thread.Sleep(TimeSpan.FromSeconds(2));
            fileInputElement1.SendKeys(AvatarFilePath);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".Buttons_buttonGreen__Mbjpl"))).Click();

            var input1 = driver.FindElement(By.CssSelector(".PersonalData_inputData__row__VLGhF:nth-child(1) > .PersonalData_inputData__item__Up418:nth-child(1) > .Inputs_inputGrey__k3ZMQ"));
            input1.Clear();
            input1.SendKeys("Thomas");
            Thread.Sleep(TimeSpan.FromSeconds(1));

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
            Thread.Sleep(TimeSpan.FromSeconds(1));

            var fileInputElement = driver.FindElement(By.CssSelector(".AddingCardPhotos_addingCard__input__gf0xu"));
            fileInputElement.SendKeys(PhotoFilePath1);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".Buttons_buttonGreen__Mbjpl"))).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            fileInputElement.SendKeys(PhotoFilePath2);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".Buttons_buttonGreen__Mbjpl"))).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            fileInputElement.SendKeys(PhotoFilePath3);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".Buttons_buttonGreen__Mbjpl"))).Click();

            driver.FindElement(By.LinkText("Переглянути особистий профіль")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 360);");
            wait.Until(ElementIsClickable(By.CssSelector(".CarouselProfile_carouselProfile__arrow_right__etwjp > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".CarouselProfile_carouselProfile__arrow_right__etwjp > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".CarouselProfile_carouselProfile__arrow__GBe-K:nth-child(1) > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".CarouselProfile_carouselProfile__arrow__GBe-K:nth-child(1) > img"))).Click();
        }

        [Fact]
        public void GetProfileInUserList()
        {
            Authorize();

            var cabinetIcon = driver.FindElement(By.CssSelector(".NavbarMain_header__iconCabinet__00KDg > img"));
            cabinetIcon.Click();

            wait.Until(ElementIsClickable(By.LinkText("Пошук людей"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Search_navBlock__option_line__0qJVB"))).Click();
            driver.FindElement(By.CssSelector(".Inputs_inputGreySearch__nYP2q")).Click();
            driver.FindElement(By.CssSelector(".Inputs_inputGreySearch__nYP2q")).SendKeys("kerol");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".SearchItem_infoBlock__username__2B0Yx")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.CssSelector(".Buttons_buttonExit__s\\+qhp > span")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Inputs_inputGreySearch__nYP2q")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.CssSelector(".Inputs_inputGreySearch__nYP2q")).SendKeys("sophia");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".SearchItem_infoBlock__name__hi4K7")).Click();
        }
    }
}