using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure
{
	public class Screenshoter
	{
		private readonly IWebDriver webDriver;

		public Screenshoter(IWebDriver webDriver)
		{
			this.webDriver = webDriver;
		}
            
		public void CreateAndSaveScreenshot()
		{
			Console.WriteLine($"Driver is initialized: {webDriver != null}");
			var screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
			var screenshotName = $"{DateTime.Now:yyyyMMdd-HHmmss}-{TestContext.CurrentContext.Test.Name}.png";
			var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, screenshotName);
			screenshot.SaveAsFile(filepath);
            
			Console.WriteLine($"##teamcity[publishArtifacts '{filepath}'] ");
			Console.WriteLine($"##teamcity[testMetadata testName='{TestContext.CurrentContext.Test.Name}' name='Screenshot' type='image' value='{screenshotName}']");
		}
		
		public void SaveTestFailureScreenshot()
		{
			try
			{
				if(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
					CreateAndSaveScreenshot();
			}
			catch(Exception e)
			{
				Console.WriteLine($"Не получилось снять скриншот: {e}'");
			}
		}
	}
}