using System.Runtime.CompilerServices;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests;
public class LocationTests
{

    // Ok so when the code compiles I will write some tests:-)
    // Due to pc 'upgrade' I'm working on vscode on linux which is not my normal dev environment (usually windows/visual studio) 
    // so everything is a tiny bit harder for me whilst learning new ways of working.
    // If I had my normal dev environment I'd be fixing everything in a flash.
    // and yes I know selenium is not normal unit tests, Ditto on the dev environment
    // don't have the right click create tests thing to hand in vscode

    [Test]
    [Category("Selenium")]
    public void LastLocationRetrieved()
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:7019/user/1");
        //Assert.That(driver.Title, Is.EqualTo("todo"));
        driver.Quit();

    }

 
   
}
