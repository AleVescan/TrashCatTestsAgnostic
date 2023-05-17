using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using System;
using System.Threading;
using NUnit.Framework;
using System.Threading.Tasks;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;


namespace alttrashcat_tests_csharp.tests
{


    [TestFixture]
    [AllureNUnit]
    public class MainMenuTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        StorePage storePage;
        GamePlay gamePlay;
        SettingsPage settingsPage;
        GetAnotherChancePage getAnotherChancePage;

        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver(port: 13000);
            mainMenuPage = new MainMenuPage(altDriver);
            gamePlay= new GamePlay(altDriver);
            settingsPage = new SettingsPage(altDriver);
            storePage = new StorePage(altDriver);
            getAnotherChancePage = new GetAnotherChancePage(altDriver);
             mainMenuPage.LoadScene();
        }

     
        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("Verifies that the main menu is correctly displayed")]
        public void TestMainMenuPageLoadedCorrectly()
        {
            Assert.True(mainMenuPage.IsDisplayed());
        }

        public SettingsPage GetSettingsPage()
        {
            return settingsPage;
        }

        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("Resets the game data")]
        public void TestDeleteData()
        
        {
            mainMenuPage.LoadScene();
            settingsPage.DeleteData();
            mainMenuPage.PressStore();
            Assert.True(storePage.CountersReset());
        }

        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("Test that the player can buy a magnet and use it in gameplay")]
        public void TestMagnetIsUsedInGameplay()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            bool buttonState = storePage.BuyButtonsAreEnabled();
            if(buttonState == true)
                storePage.BuyMagnet();
            else
                {storePage.PressStore();
                 storePage.PressCharactersTab();
                 storePage.ReloadItems();
                 storePage.BuyMagnet();
                }

            storePage.CloseStore();

            mainMenuPage.MovePowerUpLeft();
            mainMenuPage.PressRun();
            gamePlay.SelectInventoryIcon();
            Assert.IsTrue(gamePlay.PowerUpIconIsDisplayed());
        }
        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("Test that the player can buy a life power-up and use it in gameplay")]
         public void TestThatLifePowerUpAddsALife()
         {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            bool buttonState = storePage.BuyButtonsAreEnabled();
            if(buttonState == true)
                storePage.BuyLife();
            else
                {storePage.PressStore();
                 storePage.PressCharactersTab();
                 storePage.ReloadItems();
                 storePage.BuyLife();
                }
            storePage.CloseStore();
            mainMenuPage.MovePowerUpLeft();
            mainMenuPage.PressRun();

            while (gamePlay.GetCurrentLife() > 1)
                {Thread.Sleep(5);}
            gamePlay.SelectInventoryIcon();       
            Assert.AreEqual(gamePlay.GetCurrentLife(), 2);
 
         }

         [Test]

         public void TestLeaderBoardNameHighScoreChanges()
         {
            mainMenuPage.LoadScene();
            mainMenuPage.SelectLeaderBoard();
            mainMenuPage.SetHighScoreName();
            Assert.AreEqual(mainMenuPage.LeaderboardHighScoreName.GetText(), "HighScore");
            
         }

        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("The player can buy and use the raccoon character")]

         public void TestTheUserCanPlayWithRaccoon()
         {
            mainMenuPage.LoadScene();
            settingsPage.DeleteData();
            mainMenuPage.PressStore();
            storePage.PressStore();
            storePage.PressCharactersTab();
            storePage.BuyRubbishRaccon();
            storePage.CloseStore();
            mainMenuPage.ChangeCharacter();
            mainMenuPage.PressRun();
            Thread.Sleep(20);
            Assert.IsTrue(gamePlay.RacconIsDisplayed());
         }

        [Test]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureOwner("AleV")]
        [AllureDescription("The player can buy and accesorise the character")]
         public void TestThatTheCharacterCanWearAccessories()
         {

            mainMenuPage.LoadScene();
            settingsPage.DeleteData();
            mainMenuPage.PressStore();
            storePage.PressStore();
            storePage.PressAccessoriesTab();
            storePage.BuyAccessoryItems();
            storePage.CloseStore();
           // mainMenuPage.ChangeCharacter();
            mainMenuPage.ChangeAccessory();
            mainMenuPage.PressRun();
            Thread.Sleep(10);
            Assert.IsTrue(gamePlay.ConstructionGearIsDIsplayed());

        





         }

        [Test]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureOwner("AleV")]
        [AllureDescription("The player can change the theme of the game")]
         public void TestNightTimeThemeisApplied()
         {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            bool buttonState = storePage.BuyButtonsAreEnabled();
            if(buttonState == true)
                {
                storePage.OpenThemes();
                storePage.BuyNightTheme();
                }
            else
                {storePage.PressStore();
                storePage.OpenThemes();
                storePage.BuyNightTheme();
                }
         
            storePage.CloseStore();
            Thread.Sleep(100);
            mainMenuPage.ChangeTheme();
            Thread.Sleep(100);
            mainMenuPage.PressRun();
            Assert.IsTrue(gamePlay.NightLightsAreDisplayed());

         }

        

        [TestCase("MasterSlider")]
        [TestCase("MusicSlider")]
        [TestCase("MasterSFXSlider")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("The player can modify the sound settings")]
         public void SliderValuesChangeAsExpected(string sliderName)
         {
            //SliderName can be one of the three values : Master, Music,SFX
            mainMenuPage.LoadScene();
            mainMenuPage.PressSettings();
            settingsPage.MoveSliderToStart(sliderName);

           float initialSliderValue= settingsPage.GetSliderValue(sliderName);
            //slide handle
            settingsPage.MoveSlider(sliderName);

            float finalSliderValue = settingsPage.GetSliderValue(sliderName);


            Console.WriteLine ("intial value is " + initialSliderValue);
           Assert.AreNotSame(initialSliderValue, finalSliderValue);
        }
        [Test]
        public void TestGetParent()
        { 
            mainMenuPage.LoadScene();
            Thread.Sleep(100);
            var altObject = altDriver.FindObject( By.NAME, "ThemeZone", By.NAME, "UICamera");
            var altObjectParent = altObject.GetParent();
            Assert.AreEqual("Loadout", altObjectParent.name);
        }

        [Test]

        public void TestGetAppScreenSize()
        {
            mainMenuPage.LoadScene();
            altDriver.CallStaticMethod<string>("UnityEngine.Screen", "SetResolution", "UnityEngine.CoreModule", new string[] { "375", "667", "true" }, new string[] { "System.Int32", "System.Int32", "System.Boolean" });
            var screensize = altDriver.GetApplicationScreenSize();
            Console.WriteLine("screensize resolution X " + screensize.x + " screensize resolution Y " + screensize.y);
            Assert.AreEqual(screensize.x, 375);
            Assert.AreEqual(screensize.y, 667);
            
        }

        // [Test] This fails due to SetStaticPropertyIssue 

        // public void TestSetStaticCameraCount()
        // {
        //     var numberOfCameras = altDriver.GetStaticProperty<int>("UnityEngine.Camera", "allCamerasCount", "UnityEngine.CoreModule" );
        //     Console.WriteLine("Number of cameras is "+ numberOfCameras);

        //     var firstCamera = altDriver.GetStaticProperty<object>("UnityEngine.Camera", "allCameras", "UnityEngine.CoreModule");
        //     altDriver.SetStaticProperty("UnityEngine.Camera", "allCameras", "UnityEngine.CoreModule", firstCamera);

        //     altDriver.SetStaticProperty("UnityEngine.Camera", "allCamerasCount", "UnityEngine.CoreModule", 5);
        //    var updatedNumberOfCameras = altDriver.GetStaticProperty<int>("UnityEngine.Camera", "allCamerasCount", "UnityEngine.CoreModule" );
        //      Console.WriteLine("Updated number of cameras is "+ updatedNumberOfCameras);

        // }

        [Test]

        public void TestGetTimeScaleinGame()
        {
             var timeScaleFromGame = altDriver.GetTimeScale();
             //Console.WriteLine ("timescale is " + timeScaleFromGame);
             altDriver.SetTimeScale(0.1f);
             mainMenuPage.PressRun();
             Thread.Sleep(1000);
             Assert.AreEqual(0.1f,altDriver.GetTimeScale() );
            Thread.Sleep(1000);
             altDriver.SetTimeScale(1);

        }

        [Test]

     

        

           [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }





    }
}