using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.IO;

namespace AppiumIntegrationTests
{
    [TestFixture]
    public class AppiumTests 
    {
        public static WindowsDriver<WindowsElement> session;

        [SetUp]
        public static void Setup()
        {
            if (session == null)
            {
                string LoyaltyProgramPath = Path.GetFullPath(@"../../../..\LoyaltyProgram\bin\Debug\net6.0-windows\LoyaltyProgram.exe");
                AppiumOptions appCapabilities = new AppiumOptions();
                appCapabilities.AddAdditionalCapability("app", LoyaltyProgramPath);
                
                session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);
                Assert.IsNotNull(session);

                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
            }
        }

        [TearDown]
        public static void TearDown()
        {
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }

        [Test]
        public void WrongLoginTest()
        {
            session.FindElementByAccessibilityId("UsernameText").SendKeys("rafal2");
            session.FindElementByAccessibilityId("PasswordText").SendKeys("rafal2");
            session.FindElementByName("Login").Click();
            Assert.AreEqual("User Not Found!", session.FindElementByName("User Not Found!").Text);
        }

        [Test]
        public void EmptyUsernameTest()
        {
            session.FindElementByAccessibilityId("PasswordText").SendKeys("rafal2");
            session.FindElementByName("Login").Click();
            Assert.AreEqual("Username and Password are required!", session.FindElementByName("Username and Password are required!").Text);
        }

        [Test]
        public void EmptyPasswordTest()
        {
            session.FindElementByAccessibilityId("UsernameText").SendKeys("rafal2");
            session.FindElementByName("Login").Click();
            Assert.AreEqual("Username and Password are required!", session.FindElementByName("Username and Password are required!").Text);
        }

        [Test]
        public void EmptyPasswordAndUsernameTest()
        {
            session.FindElementByName("Login").Click();
            Assert.AreEqual("Username and Password are required!", session.FindElementByName("Username and Password are required!").Text);
        }
    }
}