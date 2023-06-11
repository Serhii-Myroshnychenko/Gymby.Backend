namespace Gymby.UITests.Tests
{
    public class ProgramPageTests : TestBase
    {
        [Fact]
        public void ProgramDataFill()
        {
            Authorize();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ElementIsClickable(By.LinkText("Програми"))).Click();

            wait.Until(ElementIsClickable(By.LinkText("Особисті"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Buttons_buttonGreen__Mbjpl:nth-child(1)"))).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".ProgramsCard_card__text__yXmSg"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Buttons_buttonGreen__Mbjpl"))).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".SelectSimple_select__IV4IC:nth-child(1)"))).Click();
            wait.Until(ElementIsClickable(By.XPath("//option[. = 'початковий']"))).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".SelectSimple_select__IV4IC:nth-child(2)"))).Click();
            wait.Until(ElementIsClickable(By.XPath("//option[. = 'набір маси']"))).Click();

            var titleInput = wait.Until(ElementIsClickable(By.CssSelector(".ProgramsProgramDescription_program__titleEdit__vbVd7 > .Inputs_inputGrey__k3ZMQ")));
            titleInput.Clear();
            titleInput.SendKeys("Program test");

            var descInput = wait.Until(ElementIsClickable(By.CssSelector(".Textarea_textareaGrey__5EBUw")));
            descInput.Clear();
            descInput.SendKeys("Program desc desc desc");

            driver.FindElement(By.CssSelector(".ProgramsProgramLeftPanelList_text__rZgIg > .Inputs_inputGrey__k3ZMQ")).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".Buttons_buttonBlue__zZOac"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ExerciseCreationModalProgramsList_exerciseItem__0e2qf:nth-child(2) .ExerciseCreationModalProgramsList_arrowIcon__sTzzp > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ExerciseCreationModalProgramsList_active__DzNZ9 .SecondDegreeOfNestingExercise_secondDegreeOfNestingExercise__nTERk:nth-child(3) > .SecondDegreeOfNestingExercise_mainBlock__MQvHy"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ExerciseCreationModalWindowTemplate_button__Qo9TQ > .Buttons_buttonGreen__Mbjpl"))).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".Approach_plusButton__5oagv > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_plusButton__5oagv > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_editButton__Jvb-- > img"))).Click();

            var approach1Input1 = wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(1) .ApproachItem_customizableBlock__content__0zhMC:nth-child(2) .Inputs_inputGrey__k3ZMQ")));
            approach1Input1.Clear();
            approach1Input1.SendKeys("20");

            var approach1Input2 = wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(1) .ApproachItem_customizableBlock__content__0zhMC:nth-child(3) .Inputs_inputGrey__k3ZMQ")));
            approach1Input2.Clear();
            approach1Input2.SendKeys("90");

            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) > .ApproachItem_customizableBlock__MGwHM"))).Click();
            var approach2Input1 = wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) .ApproachItem_customizableBlock__content__0zhMC:nth-child(2) .Inputs_inputGrey__k3ZMQ")));
            approach2Input1.Clear();
            approach2Input1.SendKeys("20");

            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) > .ApproachItem_customizableBlock__MGwHM"))).Click();
            var approach2Input2 = wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) .ApproachItem_customizableBlock__content__0zhMC:nth-child(3) .Inputs_inputGrey__k3ZMQ")));
            approach2Input2.Clear();
            approach2Input2.SendKeys("90");
            
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_editButton__Jvb-- > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_editButton__Jvb-- > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_approachItem__WOvch:nth-child(2) .ApproachItem_iconsBlock__basket__teeBh > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ApproachItem_iconsBlock__basket__teeBh > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_editButton__Jvb-- > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_editButton__Jvb-- > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Approach_basket__hvwcD > img"))).Click();

            wait.Until(ElementIsClickable(By.CssSelector(".ProgramsProgramLeftPanelList_navigation__title__image__0FErF > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ProgramsProgramLeftPanelList_text__rZgIg > .Inputs_inputGrey__k3ZMQ"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ProgramsProgramLeftPanelList_item__XiSPw:nth-child(3) > .ProgramsProgramLeftPanelList_basketIcon__31rFS > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector("div:nth-child(2) > .Buttons_buttonGreen__Mbjpl"))).Click();

            wait.Until(ElementIsClickable(By.LinkText("Програми"))).Click();
            wait.Until(ElementIsClickable(By.LinkText("Особисті"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".ProgramsCard_card__closeIcon__tPNfd > img"))).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".Buttons_buttonGreen__Mbjpl:nth-child(2)"))).Click();
        }
    }
}
