namespace Gymby.UITests.Tests
{
    public class MeasurementPageTests : TestBase
    {
        [Fact]
        public void MeasurementDataFill()
        {
            Authorize();

            driver.FindElement(By.LinkText("Заміри тіла")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.CssSelector(".Buttons_buttonGreen__Mbjpl")).Click();
            driver.FindElement(By.CssSelector(".Buttons_buttonGreen__Mbjpl")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            driver.FindElement(By.CssSelector(".MeasurementsItem_measurementsItem__acqep:nth-child(1) .MeasurementsItem_editButton__x7IUe > img")).Click();
            driver.FindElement(By.CssSelector(".MeasurementsItem_fullDate__oyD2z > input")).Click();
            driver.FindElement(By.CssSelector(".MeasurementsItem_fullDate__oyD2z > input")).Clear();
            driver.FindElement(By.CssSelector(".MeasurementsItem_fullDate__oyD2z > input")).SendKeys("08.06.2023");
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).Click();
            driver.FindElement(By.CssSelector(".MeasurementsItem_fullDate__oyD2z > input")).Clear();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).SendKeys("10");
            driver.FindElement(By.CssSelector(".MeasurementsItem_applyButton__PzQWG > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.CssSelector(".MeasurementsItem_measurementsItem__acqep:nth-child(2) .MeasurementsItem_editButton__x7IUe > img")).Click();
            driver.FindElement(By.CssSelector(".MeasurementsItem_fullDate__oyD2z > input")).Click();
            driver.FindElement(By.CssSelector(".MeasurementsItem_fullDate__oyD2z > input")).Clear();
            driver.FindElement(By.CssSelector(".MeasurementsItem_fullDate__oyD2z > input")).SendKeys("09.06.2023");
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).Click();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).Clear();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).SendKeys("15");
            driver.FindElement(By.CssSelector(".MeasurementsItem_applyButton__PzQWG > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.CssSelector(".MeasurementsItem_measurementsItem__acqep:nth-child(2) .MeasurementsItem_editButton__x7IUe > img")).Click();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).Click();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).Clear();
            driver.FindElement(By.CssSelector(".Inputs_inputGrey__k3ZMQ")).SendKeys("30");
            driver.FindElement(By.CssSelector(".MeasurementsItem_applyButton__PzQWG > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.CssSelector(".MeasurementsItem_measurementsItem__acqep:nth-child(1) .MeasurementsItem_editButton__x7IUe > img")).Click();
            driver.FindElement(By.CssSelector(".MeasurementsItem_deleteButton__iuuTP > img")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.CssSelector(".MeasurementsItem_editButton__x7IUe > img")).Click();
            driver.FindElement(By.CssSelector(".MeasurementsItem_deleteButton__iuuTP > img")).Click();
        }

        [Fact]
        public void MeasurementCheckData()
        {
            Authorize();

            driver.FindElement(By.LinkText("Заміри тіла")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.LinkText("Заміри тіла")).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__HFulB"))).Click();
            Assert.Equal("Рука", wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Text);

            driver.FindElement(By.LinkText("Стегно")).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__HFulB"))).Click();
            Assert.Equal("Стегно", wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Text);

            driver.FindElement(By.LinkText("Талія")).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__HFulB"))).Click();
            Assert.Equal("Талія", wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Text);

            driver.FindElement(By.LinkText("Груди")).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Click();
            Assert.Equal("Груди", wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Text);

            driver.FindElement(By.LinkText("Предпліччя")).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Click();
            Assert.Equal("Предпліччя", wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Text);

            driver.FindElement(By.LinkText("Плечі")).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Click();
            Assert.Equal("Плечі", wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Text);

            driver.FindElement(By.LinkText("Шия")).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Click();
            Assert.Equal("Шия", wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Text);

            driver.FindElement(By.LinkText("Відсоток жиру")).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Click();
            Assert.Equal("Відсоток жиру", wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Text);

            driver.FindElement(By.LinkText("Вага")).Click();
            wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Click();
            Assert.Equal("Вага", wait.Until(ElementIsClickable(By.CssSelector(".MainItem_navBlock__title__3sqko"))).Text);
        }
    }
}
