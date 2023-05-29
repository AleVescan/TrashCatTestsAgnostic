namespace alttrashcat_tests_csharp.tests
{

    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Gameplay")]
    public class GamePlayTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        GamePlay gamePlayPage;
        PauseOverlayPage pauseOverlayPage;
        GetAnotherChancePage getAnotherChancePage;
        GameOverScreen gameOverScreen;
        SettingsPage settingsPage;
        StorePage storePage;
        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver();
            mainMenuPage = new MainMenuPage(altDriver);
            mainMenuPage.LoadScene();
            mainMenuPage.PressRun();
            gamePlayPage = new GamePlay(altDriver);
            pauseOverlayPage = new PauseOverlayPage(altDriver);
            getAnotherChancePage = new GetAnotherChancePage(altDriver);
            gameOverScreen = new GameOverScreen(altDriver);
            settingsPage = new SettingsPage(altDriver);
            storePage = new StorePage(altDriver);
        }
        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ABC-1")]
        [AllureOwner("AleV")]
        [AllureDescription("Test to see if the gameplay is started by verifyig specific elements")]

        public void TestGamePlayDisplayedCorrectly()
        {
            Assert.True(gamePlayPage.IsDisplayed());
        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ABC-2")]
        [AllureOwner("AleV")]
        [AllureDescription("Test to see if the gameplay can be paused and resumes correctly afterwards")]
        public void TestGameCanBePausedAndResumed()
        {
            gamePlayPage.PressPause();
            Assert.True(pauseOverlayPage.IsDisplayed());
            pauseOverlayPage.PressResume();
            Assert.True(gamePlayPage.IsDisplayed());
            mainMenuPage.LoadScene();
        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("Test to see if the gameplay can be paused and then stopped")]
        public void TestGameCanBePausedAndStopped()
        {
            gamePlayPage.PressPause();
            pauseOverlayPage.PressMainMenu();
            Assert.True(mainMenuPage.IsDisplayed());
        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("The player can avoid a certain number of obstacles without dying")]
        public void TestAvoidingObstacles()
        {
            gamePlayPage.AvoidObstacles(5);
            Assert.True(gamePlayPage.GetCurrentLife() > 0);
            mainMenuPage.LoadScene();
        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("The player dies if it collides into obstacles")]
        public void TestPlayerDiesWhenObstacleNotAvoided()
        {
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
            mainMenuPage.LoadScene();
        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("AleV")]
        [AllureDescription("Game over screen is displayed after the player dies")]
        public void TestGameOverScreenIsAceesible()
        {
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
            getAnotherChancePage.PressGameOver();
            Assert.True(gameOverScreen.IsDisplayed());
            mainMenuPage.LoadScene();
        }

        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("AleV")]
        [AllureDescription("The player can not select to get another chance when there aren`t enough premim coins")]

        public void TestGetAnotherChangeDisabledWhenNotEnoughCoins()
        {
            gamePlayPage.PressPause();
            pauseOverlayPage.PressMainMenu();
            //delete all data 
            settingsPage.DeleteData();
            mainMenuPage.PressRun();

            //play game until get another chance is displayed
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
            Assert.IsFalse(getAnotherChancePage.GetAnotherChangeObjectState);
            mainMenuPage.LoadScene();
        }

        [Test]
        public void TestThatTrashCatBecomesInvincible()
        {

            gamePlayPage.SetCharacterInvincible("True");
            Thread.Sleep(20000);
            altDriver.WaitForObjectNotBePresent(By.NAME, "GameOver");
            //if this fails, at timeout of 20, it means that the object is displayed, thus exit with a timeout

            gamePlayPage.SetCharacterInvincible("False");
            Thread.Sleep(10000);
            Assert.True(getAnotherChancePage.IsDisplayed());
            mainMenuPage.LoadScene();

        }

        [Test]
        public void TestPremiumButtonColorChangesAsExpectedPerState()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            storePage.PressStoreToAddCoins();
            storePage.CloseStore();
            mainMenuPage.PressRun();
            
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
            Thread.Sleep(1000);
            var initialState = getAnotherChancePage.GetPremiumButtonState();

            var initialButtonColorR = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("r");
            var initialButtonColorG = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("g");
            var initialButtonColorB = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("b");

            var normalColorR = getAnotherChancePage.GetPremiumButtonStateColorRGB("normalColor", "r");
            var normalColorG = getAnotherChancePage.GetPremiumButtonStateColorRGB("normalColor", "g");
            var normalColorB = getAnotherChancePage.GetPremiumButtonStateColorRGB("normalColor", "b");

            Assert.AreEqual(initialButtonColorR, normalColorR);
            Assert.AreEqual(initialButtonColorG, normalColorG);
            Assert.AreEqual(initialButtonColorB, normalColorB);

            // Console.WriteLine("Intial button state code: " + initialState);
            // Console.WriteLine("Intial button color RGB : " + initialButtonColorR + "  " + initialButtonColorG + "  " + initialButtonColorB);
            // Console.WriteLine("Normal color RGB : " + normalColorR + "  " + normalColorG + "  " + normalColorB);

            getAnotherChancePage.PremiumButton.PointerDownFromObject();
            Thread.Sleep(1000);
            object afterPointerDownButtonColorR = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("r");
            object afterPointerDownButtonColorG = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("g");
            object afterPointerDownButtonColorB = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("b");

            var pressedColorR = getAnotherChancePage.GetPremiumButtonStateColorRGB("pressedColor", "r");
            var pressedColorG = getAnotherChancePage.GetPremiumButtonStateColorRGB("pressedColor", "g");
            var pressedColorB = getAnotherChancePage.GetPremiumButtonStateColorRGB("pressedColor", "b");
            var afterPointerDown = getAnotherChancePage.GetPremiumButtonState();

            Assert.AreEqual(afterPointerDownButtonColorR, pressedColorR);
            Assert.AreEqual(afterPointerDownButtonColorG, pressedColorG);
            Assert.AreEqual(afterPointerDownButtonColorB, pressedColorB);

            // Console.WriteLine("Button state after pointer down " + afterPointerDown);
            // Console.WriteLine("Intial button colorRGB : " + afterPointerDownButtonColorR + "  " + afterPointerDownButtonColorG + "  " + afterPointerDownButtonColorB);
            // Console.WriteLine("Pressed color RGB : " + pressedColorR + "  " + pressedColorG + "  " + pressedColorB);

            getAnotherChancePage.PremiumButton.PointerUpFromObject();
            Thread.Sleep(1000);
            var afterPointerUpButtonColorR = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("r");
            var afterPointerUpButtonColorG = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("g");
            var afterPointerUpButtonColorB = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("b");

            var selectedColorR = getAnotherChancePage.GetPremiumButtonStateColorRGB("selectedColor", "r");
            var selectedColorG = getAnotherChancePage.GetPremiumButtonStateColorRGB("selectedColor", "g");
            var selectedColorB = getAnotherChancePage.GetPremiumButtonStateColorRGB("selectedColor", "b");
            var afterPointerUp = getAnotherChancePage.GetPremiumButtonState();

            Assert.AreEqual(afterPointerUpButtonColorR, selectedColorR);
            Assert.AreEqual(afterPointerUpButtonColorG, selectedColorG);
            Assert.AreEqual(afterPointerUpButtonColorB, selectedColorB);

            // Console.WriteLine("Button color after pointer up RGB " + afterPointerUpButtonColorR + "  " + afterPointerUpButtonColorG + "  " + afterPointerUpButtonColorB);
            // Console.WriteLine("Selected color RGB : " + selectedColorR + "  " + selectedColorG + "  " + selectedColorB);
            // Console.WriteLine("Button state after pointer up " + afterPointerUp);
            mainMenuPage.LoadScene();
        }

        [Test]

        public void TestGetWorldPositionTrashCat()
        {
            var Character = gamePlayPage.Character;
            AltVector3 worldPositionCharacter = Character.GetWorldPosition();
            Thread.Sleep(20000);
            AltVector3 worlPositionUpdateObject = Character.UpdateObject().GetWorldPosition();

            Assert.AreNotEqual(worldPositionCharacter.z, worlPositionUpdateObject.z);
            mainMenuPage.LoadScene();
        }

        [Test]
        public void TestGetSCreenPositionTrashCat()
        {

            var Character = altDriver.FindObject(By.NAME, "CharacterSlot");
            AltVector2 screenPositionCharacter = Character.GetScreenPosition();
            Thread.Sleep(5000);
            //altDriver.PressKey(AltKeyCode.LeftArrow);
            gamePlayPage.MoveLeft(gamePlayPage.Character);
            Thread.Sleep(1000);

            AltVector2 screenPositionCharacteraAfterSomeTime = Character.UpdateObject().GetScreenPosition();
            Assert.AreNotEqual(screenPositionCharacter.x, screenPositionCharacteraAfterSomeTime.x);
            mainMenuPage.LoadScene();
        }

        [Test]
        public void TestGetAllComponentsMethod()
        {
            var getAllComponentsList = gamePlayPage.Character.GetAllComponents();
            //For debugging
            // Console.WriteLine("All components are " + getAllComponentsList.Count );
            // Console.WriteLine("First component is " + getAllComponentsList[0].componentName);
            // Console.WriteLine("Second component is " + getAllComponentsList[1].componentName);
            // Console.WriteLine("Third component is " + getAllComponentsList[2].componentName);
            // Console.WriteLine("Fourth component is " + getAllComponentsList[3].componentName);
            // Console.WriteLine("First assembly is " + getAllComponentsList[0].assemblyName);
            // Console.WriteLine("Second assembly is " + getAllComponentsList[1].assemblyName);
            // Console.WriteLine("Third assembly is " + getAllComponentsList[2].assemblyName);
            // Console.WriteLine("Fourth assembly is " + getAllComponentsList[3].assemblyName);

            Assert.AreEqual(getAllComponentsList.Count, 4);
        }

        [Test]
        public void TestGetAllPropertiesMethod()
        {
            AltComponent altComponentForProperties = new AltComponent("UnityEngine.Transform", "UnityEngine.CoreModule");
            var characterPropertiesList = gamePlayPage.Character.GetAllProperties(altComponentForProperties);
            //Console.WriteLine("Property  " + characterPropertiesList[0].name );
            Assert.AreEqual(characterPropertiesList[0].name, "position");
            //Console.WriteLine("Property  " + characterPropertiesList[1].name );
            Assert.AreEqual(characterPropertiesList[1].name, "localPosition");
            //Console.WriteLine("Property  " + characterPropertiesList[2].name );
            Assert.AreEqual(characterPropertiesList[2].name, "eulerAngles");
            //Console.WriteLine("Property  " + characterPropertiesList[3].name );
            Assert.AreEqual(characterPropertiesList[3].name, "localEulerAngles");
            //Console.WriteLine("Property  " + characterPropertiesList[4].name );
            Assert.AreEqual(characterPropertiesList[4].name, "right");
        }

        [Test]
        public void TestGetAllMethodsMethod()
        {
            AltComponent altComponentForMethods = new AltComponent("UnityEngine.Transform", "UnityEngine.CoreModule");
            var characterMethodsList = gamePlayPage.Character.GetAllMethods(altComponentForMethods);
            //Console.WriteLine("Method  " + characterMethodsList[0] );
            Assert.AreEqual(characterMethodsList[0], "UnityEngine.Vector3 get_position()");
            //Console.WriteLine("Method   " + characterMethodsList[1] );
            Assert.AreEqual(characterMethodsList[1], "Void set_position(UnityEngine.Vector3)");
            //Console.WriteLine("Method   " + characterMethodsList[2] );
            Assert.AreEqual(characterMethodsList[2], "UnityEngine.Vector3 get_localPosition()");
            //Console.WriteLine("Method   " + characterMethodsList[3] );
            Assert.AreEqual(characterMethodsList[3], "Void set_localPosition(UnityEngine.Vector3)");
            // Console.WriteLine("Method   " + characterMethodsList[4] );
            Assert.AreEqual(characterMethodsList[4], "UnityEngine.Vector3 GetLocalEulerAngles(UnityEngine.RotationOrder)");
        }

        [Test]
        public void TestGetAllFieldssMethod()
        {
            AltComponent altComponentForFields = new AltComponent("CharacterCollider", "Assembly-CSharp");
            var characterFieldsList = gamePlayPage.Character.GetAllFields(altComponentForFields);
            // Console.WriteLine("Number of Fields  " + characterFieldsList.Count );
            // Console.WriteLine("Field   " + characterFieldsList[0].name );
            // Console.WriteLine("Field   " + characterFieldsList[1].name );
            // Console.WriteLine("Field   " + characterFieldsList[2].name );
            // Console.WriteLine("Field   " + characterFieldsList[3].name );
            // Console.WriteLine("Field   " + characterFieldsList[4].name );
            Assert.AreEqual(characterFieldsList.Count, 7);
        }

        [Test]
        public void TestFindObjectWhichContainsWithCamera()
        {
            var characterName = gamePlayPage.CharacterFoundByWhichContainsWithCamera.name;
            //Console.WriteLine("Character item name" + characterName);
            Assert.AreEqual(characterName, "CharacterSlot");
        }

        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }

        [Test]

        public void TestNumberOfFishbonesIsSame()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.PressRun();
            Thread.Sleep(1000);
            var FishbonesName = altDriver.FindObjects(By.NAME, "Pickup(Clone)");
            var FishbonesPath = altDriver.FindObjects(By.PATH, "//Pickup(Clone)");
            Assert.AreEqual(FishbonesName.Count, FishbonesPath.Count);
            mainMenuPage.LoadScene();
        }

    }
}