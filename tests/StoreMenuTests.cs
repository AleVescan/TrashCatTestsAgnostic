
namespace alttrashcat_tests_csharp.tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Store")]

    public class StoreMenuTests
    {
        AltDriver altDriver;
        StorePage storePage;
        MainMenuPage mainMenuPage;
        SettingsPage settingsPage;
        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver(port: 13000);
            storePage = new StorePage(altDriver);
            storePage.LoadScene();
            mainMenuPage = new MainMenuPage(altDriver);
            settingsPage = new SettingsPage(altDriver);
        }
        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("The store page is displayed as expected")]
        public void TestStoreMenuPageLoadedCorrectly()
        {
            Assert.True(storePage.StoreIsDisplayed());
        }

        [Test]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureOwner("AleV")]
        [AllureDescription("Pressing the store, testing helper method, increases the coins")]
        public void TestPressingStoreIncreasesCoins()
        {
            string initialPremiumCoinsValue = storePage.PremiumCounter.GetText(); 
            string initialCoinsValue = storePage.CoinsCounter.GetText();
            storePage.PressStoreToAddCoins();
            string updatedPremiumCoinsValue = storePage.PremiumCounter.GetText();  
            string updatedCoinsValue = storePage.CoinsCounter.GetText();

            Assert.Less(initialCoinsValue, updatedCoinsValue);
            Assert.Less(initialPremiumCoinsValue, updatedPremiumCoinsValue);
        }

        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("The buy buttons are active only if there are enough coins available")]
        public void TestBuyButtonsBecomeActiveOnlyWhenEnoughCoins()
        {
           mainMenuPage.LoadScene();
           settingsPage.DeleteData();
           mainMenuPage.PressStore();          
           Assert.IsFalse(storePage.BuyButtonsState());
           storePage.PressStoreToAddCoins(); 
           Thread.Sleep(1000);
           storePage.PressCharactersTab();
           storePage.ReloadItems();
           Thread.Sleep(1000);
        
           Assert.IsTrue(storePage.BuyButtonsState());
        }

        [Test]
        public void TestBuyMagnetCanBeSetInteractableWithoutEnoughCoins()
        {
            mainMenuPage.LoadScene();
            settingsPage.DeleteData();
            mainMenuPage.PressStore();
            Assert.IsFalse(storePage.BuyButtonsState());
            Thread.Sleep(1000);
            storePage.EnableButtonObject(storePage.FirstBuyButtonInTab);
            Thread.Sleep(1000);
            Assert.IsTrue(storePage.EnableButtonObject(storePage.FirstBuyButtonInTab));
        }

        [Test]

        public void TestThatPremiumButtonAtCoordinatesIsFound()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            Assert.AreEqual(storePage.PremiumButtonAtCoordinates.GetText(), "+");
        }

        [Test]
        [Ignore ("This test will fail because Pointer Enter and Exit do not work as expected and change the tab color")]
        public void TestCharactersTabChangesColorPointEnterExit()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            object normalCharactersTabColor = storePage.GetColorOfObject(storePage.CharactersTab);
            storePage.CharactersTab.PointerEnterObject();
            Thread.Sleep(1000);
            object hoverCharactersTabColor = storePage.GetColorOfObject(storePage.CharactersTab);
            storePage.CharactersTab.PointerExitObject();
            Thread.Sleep(1000);
            object afterCharactersTabColor = storePage.GetColorOfObject(storePage.CharactersTab);

            Assert.AreNotEqual(normalCharactersTabColor, hoverCharactersTabColor);
            Assert.AreNotEqual(hoverCharactersTabColor, afterCharactersTabColor);
            Assert.AreNotEqual(normalCharactersTabColor, afterCharactersTabColor);
        }

        [Test]

        public void TestKeyPreferancesInStoreMenu()
        {
            Assert.Multiple(()=>
            {              
            altDriver.DeletePlayerPref();
            altDriver.SetKeyPlayerPref("test", "TestString");
            var stringVar = altDriver.GetStringKeyPlayerPref("test");
            Assert.AreEqual(stringVar, "TestString");

            altDriver.SetKeyPlayerPref("test", 1);
            var intVar = altDriver.GetIntKeyPlayerPref("test");
            Assert.AreEqual(intVar, 1);

            altDriver.SetKeyPlayerPref("test", 1.0f);
            var floatVar = altDriver.GetFloatKeyPlayerPref("test");
            Assert.AreEqual(floatVar, 1.0f);

            altDriver.DeleteKeyPlayerPref("test");
            });
        }
        [Test]
        public void TestPlayerPrefsWithStaticMethod()
        {
             altDriver.CallStaticMethod<string>("UnityEngine.PlayerPrefs", "SetInt", "UnityEngine.CoreModule", new[] { "Test", "1" });
             //int a = altDriver.CallStaticMethod<int>("UnityEngine.PlayerPrefs", "GetInt", "UnityEngine.CoreModule", new[] { "Test", "2" });
             int a = altDriver.GetIntKeyPlayerPref("Test");
             Assert.AreEqual(1, a);
        }

        [Test]

        public void TestGetStaticPropertyBrightness()
        {
         float brightness = altDriver.GetStaticProperty<float>("UnityEngine.Screen", "brightness","UnityEngine.CoreModule" ); 
         Console.WriteLine("Brightness is " + brightness); 
         Assert.AreEqual(brightness, 1);
        }

        [Test] 
        [Ignore("Test that fails due to SetStaticProperty issue")]
        public void TestSetStaticPropertyScreenBrightness()

        {   
            float brightness = altDriver.GetStaticProperty<float>("UnityEngine.Screen", "brightness","UnityEngine.CoreModule" ); 
            object expectedValue = 0.1f; 
            altDriver.SetStaticProperty("UnityEngine.Screen", "brightness", "UnityEngine.CoreModule", expectedValue);
            float brightnessUpdated = altDriver.GetStaticProperty<float>("UnityEngine.Screen", "brightness","UnityEngine.CoreModule" ); 
            Console.WriteLine ("brightness is " + brightness );

        }

        [Test] 
        [Ignore("Test that fails because of inccorect PointerEnterObject use")]
        public void TestPointerEnterAndExit()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            Thread.Sleep(100);
            storePage.ItemsTab.PointerEnterObject();
           Assert.IsTrue( storePage.IsPointerOnObject());

        }


         [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }

        
    }
}