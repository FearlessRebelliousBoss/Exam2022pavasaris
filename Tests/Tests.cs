using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using SeleniumExtras.WaitHelpers;


namespace WindowsFormsApp1.Tests
{
    [TestClass]
    public class EbayElementTests
    {
        private IWebDriver driver;

        // Izpildās pirms katra testa
        [TestInitialize]
        public void Setup()
        {
            driver = new FirefoxDriver(); // Palaist Firefox pārlūku
            driver.Navigate().GoToUrl("https://www.ebay.com/"); // Atvērt eBay lapu
        }

        [TestMethod]
        public void Test1_field()
        {
            // Pārbauda, vai eksistē elements ar id "gh-ac"
            var element = driver.FindElement(By.Id("gh-ac"));
            Assert.IsNotNull(element, "Elements ar id 'gh-ac' nav atrasts.");

            Console.WriteLine("Tests pabeigts. Nospied Enter, lai turpinātu.");
            Console.ReadLine(); // Gaidīt lietotāja ievadi (pārlūks paliek atvērts)
        }

        [TestMethod]
        public void Test2_search()
        {
            // Pārbauda, vai eksistē elements ar id "gh-btn"
            var element = driver.FindElement(By.Id("gh-btn"));
            Assert.IsNotNull(element, "Elements ar id 'gh-btn' nav atrasts.");

            Console.WriteLine("Tests pabeigts. Nospied Enter, lai turpinātu.");
            Console.ReadLine(); // Gaidīt lietotāja ievadi (pārlūks paliek atvērts)
        }
    }
}
