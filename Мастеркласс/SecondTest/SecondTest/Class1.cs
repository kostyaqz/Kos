using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SecondTest
{
	[TestFixture]
    public class Class1
		
    {
	    private IWebDriver driver;

	    [SetUp]

	    public void SetUp()

	    {
			driver = new ChromeDriver();
	    }

	    [Test]

	    public void SecondTest()
	    {
			driver.Navigate().GoToUrl("https://google.ru");
			driver.FindElement(By.Name("q")).SendKeys("Selenium");
			driver.FindElement(By.Name("btnK")).Click();
	    }


    }
}
