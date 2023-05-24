
namespace alttrashcat_tests_csharp.tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("StartPage")]

    public class StartPageTests
    {
        private AltDriver altDriver;
        private MainMenuPage mainMenuPage;
        private StartPage startPage;
        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver();
            startPage = new StartPage(altDriver);
            startPage.Load();
            mainMenuPage = new MainMenuPage(altDriver);

        }
        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("The start page is loaded correectly with all elements")]
        public void TestStartPageLoadedCorrectly()
        {
            Assert.True(startPage.IsDisplayed());
        }
        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("The start page is displayed when opening the application")]
        public void TestStartButtonLoadMainMenu()
        {
            startPage.PressStart();
            Assert.True(mainMenuPage.IsDisplayed());
        }

        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}