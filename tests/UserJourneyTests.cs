

namespace alttrashcat_tests_csharp.tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("UserJourneys")]


    public class UserJourneyTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        GamePlay gamePlay;
        PauseOverlayPage pauseOverlayPage;
        GetAnotherChancePage getAnotherChancePage;
        GameOverScreen gameOverScreen;
        SettingsPage settingsPage;
        StartPage startPage;
        StorePage storePage;


        [SetUp]
        public void Setup()
        {

            altDriver = new AltDriver();
            mainMenuPage = new MainMenuPage(altDriver);
            gamePlay = new GamePlay(altDriver);
            pauseOverlayPage = new PauseOverlayPage(altDriver);
            getAnotherChancePage = new GetAnotherChancePage(altDriver);
            gameOverScreen = new GameOverScreen(altDriver);
            settingsPage = new SettingsPage(altDriver);
            startPage = new StartPage(altDriver);
            storePage = new StorePage(altDriver);
            mainMenuPage.LoadScene();

        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("An user journey in which the player starts the game, pauses it, resumes playing until it dies and the game over screen is displayed")]

        public void UserJourneyPlayandPause()
        {
            Assert.Multiple(() =>
       {
           //User opens the game
           mainMenuPage.PressRun();
           Assert.True(gamePlay.IsDisplayed());
           gamePlay.AvoidObstacles(5);
           Assert.True(gamePlay.GetCurrentLife() > 0);
           //user pauses the game 
           gamePlay.PressPause();
           Assert.True(pauseOverlayPage.IsDisplayed());
           pauseOverlayPage.PressResume();
           Assert.True(gamePlay.IsDisplayed());
           float timeout = 20;
           while (timeout > 0)
           {
               try
               {
                   getAnotherChancePage.IsDisplayed();
                   break;
               }
               catch (Exception)
               {
                   timeout -= 1;
               }
           }
           //user dies and game over screen is displayed
           getAnotherChancePage.PressGameOver();
           Assert.True(gameOverScreen.IsDisplayed());
       });
            mainMenuPage.LoadScene();
        }




        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("An user journey in which the game data is reset and after adding coins the user buys store items and uses them in gameplay")]
        public void UserJourneyBuyItems()
        {
            Assert.Multiple(() =>
        {

            //delete current game data
            settingsPage.DeleteData();
            mainMenuPage.PressStore();
            // verify if buttons are disabled when no money
            Assert.IsFalse(storePage.BuyButtonsState());
            storePage.PressStoreToAddCoins();
            Thread.Sleep(1000);
            storePage.PressCharactersTab();
            storePage.ReloadItems();
            Thread.Sleep(1000);
            //get coins by pressing Store and verify buttons get enabled 
            Assert.IsTrue(storePage.BuyButtonsState());

            //buy magnet and night theme
            storePage.BuyMagnet();
            storePage.OpenThemes();
            storePage.BuyNightTheme();
            storePage.CloseStore();
            mainMenuPage.MovePowerUpLeft();
            Assert.NotNull(mainMenuPage.ThemeSelectorRight);
            mainMenuPage.ChangeTheme();
            Thread.Sleep(100);
            //verify bought items are available in game
            mainMenuPage.PressRun();
            Assert.NotNull(gamePlay.InventoryItem);

            Assert.NotNull(gamePlay.NightLights);
            gamePlay.SelectInventoryIcon();
            Assert.NotNull(gamePlay.PowerUpIcon);
            mainMenuPage.LoadScene();
        });
        }

        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("An user journey in which the player has the most chances of revival by using a life power-up and a second chance")]
        public void UserJourneyReviveAndGetASecondChance()
        {
            Assert.Multiple(() =>
            {
            settingsPage.DeleteData();
            mainMenuPage.PressStore();
            // verify if buttons are disabled when no money
            Assert.IsFalse(storePage.BuyButtonsState());
            storePage.PressStoreToAddCoins();
            Thread.Sleep(1000);
            storePage.PressCharactersTab();
            storePage.ReloadItems();
            Thread.Sleep(1000);
            //get coins by pressing Store and verify buttons get enabled 
            Assert.IsTrue(storePage.BuyButtonsState());
            storePage.BuyLife();
            storePage.CloseStore();
            mainMenuPage.MovePowerUpLeft();
            mainMenuPage.PressRun();

            while (gamePlay.GetCurrentLife() > 1)
            { Thread.Sleep(5); }

            gamePlay.SelectInventoryIcon();
            Assert.AreEqual(gamePlay.GetCurrentLife(), 2);
            float timeout = 20;
            while (timeout > 0)
            {
                try
                {
                    getAnotherChancePage.IsDisplayed();
                    break;
                }
                catch (Exception)
                {
                    timeout -= 1;
                }
            }
            Assert.True(getAnotherChancePage.IsDisplayed());
            getAnotherChancePage.PressPremiumButton();
            while (timeout > 0)
            {
                try
                {
                    gameOverScreen.IsDisplayed();
                    break;
                }
                catch (Exception)
                {
                    timeout -= 1;
                }
            }

            Assert.True(gameOverScreen.IsDisplayed());
            mainMenuPage.LoadScene();
            });
        }

        [Test]

        public void TestTheNumberOfAllEnabledElementsFromDifferentPagesIsDifferent()
        {

            var mainMenuPageEnabledElements = altDriver.GetAllElements(enabled: true);
            mainMenuPage.PressRun();
            Thread.Sleep(1000);
            var gamePlayPageEnabledElements = altDriver.GetAllElements(enabled: true);

            Assert.AreNotEqual(mainMenuPageEnabledElements.Count, gamePlayPageEnabledElements.Count);
            mainMenuPage.LoadScene();

        }

        [Test]

        public void TestTheNumberOfAllDisabledElementsFromDifferentPagesIsDifferent()
        {
            mainMenuPage.LoadScene();
            var mainMenuPageDisabledElements = altDriver.GetAllElements(enabled: false);
            mainMenuPage.PressRun();
            Thread.Sleep(1000);
            var gamePlayPageDisabledElements = altDriver.GetAllElements(enabled: false);

            Assert.AreNotEqual(mainMenuPageDisabledElements.Count, gamePlayPageDisabledElements.Count);
            mainMenuPage.LoadScene();

        }

        
        [Test]
        public void TestMethodsThatHandleScenes()
        {
            System.Collections.Generic.List<string> loadedSceneNames = altDriver.GetAllLoadedScenes();
            // Console.WriteLine("Numer of loaded scenes " + loadedSceneNames.Count);
            // Console.WriteLine("Name of loaded scenes " + loadedSceneNames[0]);
            Assert.AreEqual(loadedSceneNames[0], "Main");
            Assert.AreEqual("Main", altDriver.GetCurrentScene());

            mainMenuPage.PressStore();

            System.Collections.Generic.List<string> loadedSceneNamesAfterStore = altDriver.GetAllLoadedScenes();
            // Console.WriteLine("Numer of loaded scenes " + loadedSceneNamesAfterStore.Count);
            // Console.WriteLine("Name of loaded scenes " + loadedSceneNamesAfterStore[0]);
            // Console.WriteLine("Name of loaded scenes " + loadedSceneNamesAfterStore[1]);
            // Console.WriteLine("Name of current scene " + altDriver.GetCurrentScene());

            altDriver.UnloadScene("Main");
           // Console.WriteLine("Name of loaded scenes after unloading Main " + altDriver.GetAllLoadedScenes()[0]);
            Assert.AreEqual("Shop", altDriver.GetCurrentScene());

            altDriver.LoadScene("Shop", true);
            //Console.WriteLine("Name of loaded scenes after loading shop " + altDriver.GetAllLoadedScenes()[0]);
            Assert.AreEqual("Shop", altDriver.GetCurrentScene());

            Assert.AreEqual(altDriver.GetAllLoadedScenes()[0], "Shop");
            mainMenuPage.LoadScene();
        }

        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }

    }

}