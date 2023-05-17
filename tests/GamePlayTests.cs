using System;
using System.Threading;
using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using NUnit.Framework;
using Allure;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;
using Newtonsoft.Json;
using System.Drawing;

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
            storePage= new StorePage(altDriver);

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
            
         }

         [Test]

         public void TestPremiumButtonColorChangesAsExpectedPerState()
         {
            mainMenuPage.LoadScene();
            mainMenuPage.PressStore();
            storePage.PressStore();
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

            Assert.AreEqual(initialButtonColorR,normalColorR ); 
            Assert.AreEqual(initialButtonColorG,normalColorG ); 
            Assert.AreEqual(initialButtonColorB,normalColorB ); 


            Console.WriteLine("Intial button state code: " + initialState);
            Console.WriteLine("Intial button color RGB : " + initialButtonColorR+ "  "+ initialButtonColorG + "  "+ initialButtonColorB );
            Console.WriteLine("Normal color RGB : " + normalColorR+ "  "+ normalColorG + "  "+ normalColorB );


            getAnotherChancePage.PremiumButton.PointerDownFromObject();
            Thread.Sleep(1000);
            object afterPointerDownButtonColorR = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("r"); 
            object afterPointerDownButtonColorG = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("g"); 
            object afterPointerDownButtonColorB = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("b"); 

            var pressedColorR = getAnotherChancePage.GetPremiumButtonStateColorRGB("pressedColor", "r");
            var pressedColorG = getAnotherChancePage.GetPremiumButtonStateColorRGB("pressedColor", "g");
            var pressedColorB = getAnotherChancePage.GetPremiumButtonStateColorRGB("pressedColor", "b"); 
            var afterPointerDown = getAnotherChancePage.GetPremiumButtonState();

            Assert.AreEqual(afterPointerDownButtonColorR ,pressedColorR ); 
            Assert.AreEqual(afterPointerDownButtonColorG ,pressedColorG ); 
            Assert.AreEqual(afterPointerDownButtonColorB ,pressedColorB );

            Console.WriteLine("Button state after pointer down " + afterPointerDown);
            Console.WriteLine("Intial button colorRGB : " + afterPointerDownButtonColorR+ "  "+ afterPointerDownButtonColorG + "  "+ afterPointerDownButtonColorB );
            Console.WriteLine("Pressed color RGB : " + pressedColorR+ "  "+ pressedColorG + "  "+ pressedColorB );
            
            getAnotherChancePage.PremiumButton.PointerUpFromObject();
            Thread.Sleep(1000);
            var afterPointerUpButtonColorR = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("r");
            var afterPointerUpButtonColorG = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("g");
            var afterPointerUpButtonColorB = getAnotherChancePage.GetPremiumButtonCurrentColorRGB("b");

            var selectedColorR = getAnotherChancePage.GetPremiumButtonStateColorRGB("selectedColor", "r");
            var selectedColorG = getAnotherChancePage.GetPremiumButtonStateColorRGB("selectedColor", "g");
            var selectedColorB = getAnotherChancePage.GetPremiumButtonStateColorRGB("selectedColor", "b");
            var afterPointerUp = getAnotherChancePage.GetPremiumButtonState();

            Assert.AreEqual(afterPointerUpButtonColorR ,selectedColorR ); 
            Assert.AreEqual(afterPointerUpButtonColorG ,selectedColorG ); 
            Assert.AreEqual(afterPointerUpButtonColorB ,selectedColorB ); 
            
            Console.WriteLine("Button color after pointer up RGB " + afterPointerUpButtonColorR+ "  "+ afterPointerUpButtonColorG + "  "+ afterPointerUpButtonColorB);
            Console.WriteLine("Selected color RGB : " + selectedColorR+ "  "+ selectedColorG + "  "+ selectedColorB );
            Console.WriteLine("Button state after pointer up " + afterPointerUp);



         }

         [Test]

         public void TestDistanceModifieddWithinGame()
         {
            gamePlayPage.SetCharacterInvincible("True");
            Thread.Sleep(20000);
            // var RunnerMultiplier= altDriver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/ScoreZone/ScoreLabel/ScoreText/MultiplierText");
            // var InitialRunnerMultiplier=RunnerMultiplier.GetText();
            // RunnerMultiplier.SetText("x 5");
            // var SetRunnerMultiplierText = altDriver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/ScoreZone/ScoreLabel/ScoreText/MultiplierText").GetText();
            // Assert.AreEqual("x 5", SetRunnerMultiplierText);

            var Distance = altDriver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/DistanceZone/DistanceText");
            var intialDistance = Distance.GetText();
            var finalDistance=  Distance.SetText("3000m");
             //Assert.AreEqual(Distance.GetText(), "3000m");
    
        // //     gamePlayPage.SetRunnerMultiplier();

        //     Assert.AreEqual(RunnerMultiplier.GetText(), "x 5");
        //    // Assert.AreNotEqual(initialRunnerMultiplier, finalRunnerMultiplier);

            //   gamePlayPage.SetRunnerMultiplier();
            // Thread.Sleep(20000);


         }

         [Test]

         public void TestGetWorldPositionTrashCat()
         {

            var Character = gamePlayPage.Character; 
            AltVector3 worldPositionCharacter = Character.GetWorldPosition();

            Console.WriteLine("World position of character X axis " + worldPositionCharacter.x);
            Console.WriteLine("World position of character Y axis " + worldPositionCharacter.y);
            Console.WriteLine("World position of character Z axis " + worldPositionCharacter.z);

            Thread.Sleep(20000);
             AltVector3 worldPositionCharacterAfterSomeTime = Character.GetWorldPosition();
            AltVector3 worlPositionUpdateObject = Character.UpdateObject().GetWorldPosition();
           

            Console.WriteLine("World position of character after some time X axis " + worldPositionCharacterAfterSomeTime.x);
            Console.WriteLine("World position of character after some time Y axis " + worldPositionCharacterAfterSomeTime.y);
            Console.WriteLine("World position of character after some time Z axis " + worldPositionCharacterAfterSomeTime.z);

            Console.WriteLine("World position of UpdateObject X axis " + worlPositionUpdateObject.x);
            Console.WriteLine("World position of UpdateObject Y axis " + worlPositionUpdateObject.y);
            Console.WriteLine("World position of UpdateObject  Z axis " + worlPositionUpdateObject.z);



            Assert.AreNotEqual(worldPositionCharacter.z, worlPositionUpdateObject.z);
         }

        [Test]
         public void TestGetSCreenPositionTrashCat()
         {

            var Character = altDriver.FindObject(By.NAME, "CharacterSlot");
            AltVector2 screenPositionCharacter = Character.GetScreenPosition();

            Console.WriteLine("Screen position of character X axis " + screenPositionCharacter.x);
            Console.WriteLine("Screen position of character Y axis " + screenPositionCharacter.y);

            Thread.Sleep(5000);
            //altDriver.PressKey(AltKeyCode.LeftArrow);
            gamePlayPage.MoveLeft(gamePlayPage.Character);
            Thread.Sleep(1000);

            AltVector2 screenPositionCharacteraAfterSomeTime = Character.UpdateObject().GetScreenPosition();

            Console.WriteLine("Screen position of character after some time X axis " + screenPositionCharacteraAfterSomeTime.x);
            Console.WriteLine("Screen position of character after some time Y axis " + screenPositionCharacteraAfterSomeTime.y);
            Assert.AreNotEqual(screenPositionCharacter.x, screenPositionCharacteraAfterSomeTime.x);
         }

         [Test]

         public void TestGetAllComponentsMethod()
         {

            Console.WriteLine("All components are " + gamePlayPage.Character.GetAllComponents().Count );
            Console.WriteLine("First component is " + gamePlayPage.Character.GetAllComponents()[0].componentName);
            Console.WriteLine("Second component is " + gamePlayPage.Character.GetAllComponents()[1].componentName);
            Console.WriteLine("Third component is " + gamePlayPage.Character.GetAllComponents()[2].componentName);
            Console.WriteLine("Fourth component is " + gamePlayPage.Character.GetAllComponents()[3].componentName);
            Console.WriteLine("First assembly is " + gamePlayPage.Character.GetAllComponents()[0].assemblyName);
            Console.WriteLine("Second assembly is " + gamePlayPage.Character.GetAllComponents()[1].assemblyName);
            Console.WriteLine("Third assembly is " + gamePlayPage.Character.GetAllComponents()[2].assemblyName);
            Console.WriteLine("Fourth assembly is " + gamePlayPage.Character.GetAllComponents()[3].assemblyName);

            Assert.AreEqual(gamePlayPage.Character.GetAllComponents().Count , 4);  
         }

        [Test]
         public void TestGetAllPropertiesMethod()
         {
            AltComponent altComponentForProperties= new AltComponent("UnityEngine.Transform","UnityEngine.CoreModule");

            Console.WriteLine("Property  " + gamePlayPage.Character.GetAllProperties(altComponentForProperties)[0].name );
            Assert.AreEqual(gamePlayPage.Character.GetAllProperties(altComponentForProperties)[0].name, "position" );

            Console.WriteLine("Property  " + gamePlayPage.Character.GetAllProperties(altComponentForProperties)[1].name );
            Assert.AreEqual(gamePlayPage.Character.GetAllProperties(altComponentForProperties)[1].name, "localPosition" );

            Console.WriteLine("Property  " + gamePlayPage.Character.GetAllProperties(altComponentForProperties)[2].name );
            Assert.AreEqual(gamePlayPage.Character.GetAllProperties(altComponentForProperties)[2].name, "eulerAngles" );

            Console.WriteLine("Property  " + gamePlayPage.Character.GetAllProperties(altComponentForProperties)[3].name );
            Assert.AreEqual(gamePlayPage.Character.GetAllProperties(altComponentForProperties)[3].name, "localEulerAngles" );

            Console.WriteLine("Property  " + gamePlayPage.Character.GetAllProperties(altComponentForProperties)[4].name );
             Assert.AreEqual(gamePlayPage.Character.GetAllProperties(altComponentForProperties)[4].name, "right" );


         }
        
        [Test]
         public void TestGetAllMethodsMethod()
         {
            AltComponent altComponentForMethods= new AltComponent("UnityEngine.Transform","UnityEngine.CoreModule");

            Console.WriteLine("Method  " + gamePlayPage.Character.GetAllMethods(altComponentForMethods)[0] );
            Assert.AreEqual(gamePlayPage.Character.GetAllMethods(altComponentForMethods)[0], "UnityEngine.Vector3 get_position()" );

            Console.WriteLine("Method   " + gamePlayPage.Character.GetAllMethods(altComponentForMethods)[1] );
            Assert.AreEqual(gamePlayPage.Character.GetAllMethods(altComponentForMethods)[1], "Void set_position(UnityEngine.Vector3)" );

            Console.WriteLine("Method   " + gamePlayPage.Character.GetAllMethods(altComponentForMethods)[2] );
            Assert.AreEqual(gamePlayPage.Character.GetAllMethods(altComponentForMethods)[2], "UnityEngine.Vector3 get_localPosition()" );

            Console.WriteLine("Method   " + gamePlayPage.Character.GetAllMethods(altComponentForMethods)[3] );
            Assert.AreEqual(gamePlayPage.Character.GetAllMethods(altComponentForMethods)[3], "Void set_localPosition(UnityEngine.Vector3)" );

            Console.WriteLine("Method   " + gamePlayPage.Character.GetAllMethods(altComponentForMethods)[4] );
            Assert.AreEqual(gamePlayPage.Character.GetAllMethods(altComponentForMethods)[4], "UnityEngine.Vector3 GetLocalEulerAngles(UnityEngine.RotationOrder)" );
         }

         [Test]
         public void TestGetAllFieldssMethod()
         {
            AltComponent altComponentForFields= new AltComponent("CharacterCollider","Assembly-CSharp");

            Console.WriteLine("Number of Fields  " + gamePlayPage.Character.GetAllFields(altComponentForFields).Count );
            Console.WriteLine("Field   " + gamePlayPage.Character.GetAllFields(altComponentForFields)[0].name );
            Console.WriteLine("Field   " + gamePlayPage.Character.GetAllFields(altComponentForFields)[1].name );
            Console.WriteLine("Field   " + gamePlayPage.Character.GetAllFields(altComponentForFields)[2].name );
            Console.WriteLine("Field   " + gamePlayPage.Character.GetAllFields(altComponentForFields)[3].name );
            Console.WriteLine("Field   " + gamePlayPage.Character.GetAllFields(altComponentForFields)[4].name );

            Assert.AreEqual(gamePlayPage.Character.GetAllFields(altComponentForFields).Count, 7);
         }
        

      

         [Test]
         public void TestFindObjectWhichContainsWithCamera()
         {
           var characterName =  gamePlayPage.CharacterFoundByWhichContainsWithCamera.name;
           Console.WriteLine("Character item name" + characterName);

           Assert.AreEqual(characterName, "CharacterSlot" );

         }

         

        

       


        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}