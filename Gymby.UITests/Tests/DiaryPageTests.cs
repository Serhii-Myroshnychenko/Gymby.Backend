namespace Gymby.UITests.Tests
{
    public class DiaryPageTests : TestBase
    {
        [Fact]
        public void CreateExerciseInDiary()
        {
            Authorize();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ElementIsClickable(By.LinkText("Щоденник"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".react-calendar__tile:nth-child(11) > abbr"))).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".Diary_calendarBlock__buttonAdd__ypM8w > .Buttons_buttonOrange__mc4Ry"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ExerciseCreationModalProgramsList_exerciseItem__0e2qf:nth-child(2) .ExerciseCreationModalProgramsList_topBlock__G18DO"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ExerciseCreationModalProgramsList_exerciseItem__0e2qf:nth-child(2) .ExerciseCreationModalProgramsList_arrowIcon__sTzzp > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ExerciseCreationModalProgramsList_active__DzNZ9 .SecondDegreeOfNestingExercise_secondDegreeOfNestingExercise__nTERk:nth-child(4) > .SecondDegreeOfNestingExercise_mainBlock__MQvHy"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ExerciseCreationModalWindowTemplate_button__Qo9TQ > .Buttons_buttonGreen__Mbjpl"))).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".Approach_plusButton__5oagv > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_plusButton__5oagv > img"))).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".Approach_editButton__Jvb-- > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(1)"))).Click();
            driver.FindElement(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(1) .ApproachItem_customizableBlock__content__0zhMC:nth-child(1) .Inputs_inputGrey__k3ZMQ")).SendKeys("50");

            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(1) .ApproachItem_customizableBlock__content__0zhMC:nth-child(2) .Inputs_inputGrey__k3ZMQ"))).Click();
            driver.FindElement(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(1) .ApproachItem_customizableBlock__content__0zhMC:nth-child(2) .Inputs_inputGrey__k3ZMQ")).SendKeys("10");

            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(1) > .ApproachItem_customizableBlock__MGwHM"))).Click();
            driver.FindElement(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(1) .ApproachItem_customizableBlock__content__0zhMC:nth-child(3) .Inputs_inputGrey__k3ZMQ")).SendKeys("120");

            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) .ApproachItem_customizableBlock__content__0zhMC:nth-child(1) .Inputs_inputGrey__k3ZMQ"))).Click();
            driver.FindElement(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) .ApproachItem_customizableBlock__content__0zhMC:nth-child(1) .Inputs_inputGrey__k3ZMQ")).SendKeys("60");

            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) > .ApproachItem_customizableBlock__MGwHM"))).Click();
            driver.FindElement(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) .ApproachItem_customizableBlock__content__0zhMC:nth-child(2) .Inputs_inputGrey__k3ZMQ")).SendKeys("8");

            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) > .ApproachItem_customizableBlock__MGwHM"))).Click();
            driver.FindElement(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) .ApproachItem_customizableBlock__content__0zhMC:nth-child(3) .Inputs_inputGrey__k3ZMQ")).SendKeys("120");
            
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_editButton__Jvb-- > img"))).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(1) .ApproachItem_iconsBlock__checkbox__Oixt- > img"))).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) .ApproachItem_iconsBlock__checkbox__Oixt- > img"))).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_editButton__Jvb-- > img"))).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) .ApproachItem_iconsBlock__basket__teeBh > img"))).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_iconsBlock__basket__teeBh > img"))).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_basket__hvwcD > img"))).Click();
        }

        [Fact]
        public void ImportProgramDayToDiary()
        {
            Authorize();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ElementIsClickable(By.LinkText("Щоденник"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".react-calendar__tile:nth-child(11) > abbr"))).Click();

            driver.FindElement(By.CssSelector(".Diary_calendarBlock__buttonAddProgramDay__IzCXH > .Buttons_buttonOrange__mc4Ry")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".ModalWindow_modal__WLLag:nth-child(3) .DiaryModalProgramsList_arrowIcon__sZFvO:nth-child(3) > img:nth-child(1)")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".DiaryModalProgramsList_active__di1RA > .DiaryModalProgramsList_bottomBlock__daysBlock__5172q > .Day_dayBlock__lu0Ux:nth-child(2) > .Day_day__pFlTe")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".ModalWindow_active__sUTnd .Buttons_buttonGreen__Mbjpl")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_approach__IzwMa:nth-child(1) .Approach_editButton__Jvb-- > img"))).Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
        }

        [Fact]
        public void ImportProgramToDiary()
        {
            Authorize();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ElementIsClickable(By.LinkText("Щоденник"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".react-calendar__tile:nth-child(11) > abbr"))).Click();

            driver.FindElement(By.CssSelector(".Diary_calendarBlock__buttonImport__01oSH > .Buttons_buttonOrange__mc4Ry")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".ModalWindow_modal__WLLag:nth-child(5) .DiaryModalProgramsList_arrowIcon__sZFvO:nth-child(3) > img:nth-child(1)")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".DiaryModalWindowTemplate_topBlock__rightBlock__column__1Gj2-:nth-child(2) > .DiaryModalWindowTemplate_topBlock__rightBlock__row__2ua-i:nth-child(1) > input")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".DiaryModalWindowTemplate_topBlock__rightBlock__column__1Gj2-:nth-child(2) > .DiaryModalWindowTemplate_topBlock__rightBlock__row__2ua-i:nth-child(2) > input")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".ModalWindow_active__sUTnd .Buttons_buttonGreen__Mbjpl")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_approach__IzwMa:nth-child(1) .Approach_editButton__Jvb-- > img")).Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".react-calendar__tile:nth-child(12)"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_approach__IzwMa:nth-child(1) .Approach_editButton__Jvb-- > img"))).Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".Approach_basket__hvwcD > img")).Click();
        }
    }
}
