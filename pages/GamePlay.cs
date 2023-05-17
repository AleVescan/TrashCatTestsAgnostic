using Altom.AltDriver;
using System;

namespace alttrashcat_tests_csharp.pages
{
    public class GamePlay : BasePage
    {
        public GamePlay(AltDriver driver) : base(driver)
        {
        }
        public AltObject PauseButton { get => Driver.WaitForObject(By.NAME, "pauseButton", timeout: 2); }
        public AltObject Character { get => Driver.WaitForObject(By.NAME, "PlayerPivot"); }
        public AltObject InventoryItem { get => Driver.WaitForObject(By.NAME, "Inventory"); }
        public AltObject PowerUpIcon {get => Driver.WaitForObject(By.NAME, "PowerupIcon");}
        public AltObject RacconMesh {get => Driver.WaitForObject(By.PATH, "/PlayerPivot/CharacterSlot/character(Clone)/RacoonMesh");}
        public AltObject NightLights {get => Driver.WaitForObject(By.NAME, "LightGlows", timeout:10 );}
        public AltObject RacconConstructionGear {get => Driver.WaitForObject(By.PATH, "/PlayerPivot/CharacterSlot/character(Clone)/ConstructionGearMesh");}
        public AltObject CharacterSlot {get => Driver.WaitForObject(By.PATH, "/PlayerPivot/CharacterSlot");}
        public AltObject RunnerMultipler {get => Driver.WaitForObject(By.PATH, "/UICamera/Game/WholeUI/ScoreZone/ScoreLabel/ScoreText/MultiplierText");}
        public AltObject CharacterFoundByWhichContainsWithCamera { get => Driver.FindObjectWhichContains(By.NAME, "Character", By.NAME,  "UICamera" );}
    
      // The following classes are used in order to replace the keystrokes that are not recongized on devices

       public void Jump(AltObject character)
            {
                character.CallComponentMethod<string>("CharacterInputController", "Jump", "Assembly-CSharp", new object[]{});
            }
        public void Slide(AltObject character)
            {
                character.CallComponentMethod<string>("CharacterInputController", "Slide", "Assembly-CSharp", new object[]{});
            }        
        public void MoveRight(AltObject character)
        { 
            character.CallComponentMethod<string>("CharacterInputController", "ChangeLane", "Assembly-CSharp", new string[]{"1"});
        }
        public void MoveLeft(AltObject character)
        { 
            character.CallComponentMethod<string>("CharacterInputController", "ChangeLane", "Assembly-CSharp", new string[]{"-1"});
        }

        // end of clasesses used to replace keystrokes 


        public bool IsDisplayed()
        {
            if (PauseButton != null && Character != null)
            {
                return true;
            }
            return false;
        }

        public bool InventoryItemIsDisplayed()
        {
            if (InventoryItem !=null)
            {
                return true;
            }
            return false;
        }
        public void SelectInventoryIcon()
        {
            InventoryItem.Tap();
        }

        public bool PowerUpIconIsDisplayed()
        {
             if (PowerUpIcon !=null)
            {
                return true;
            }
            return false;
        }

        public bool RacconIsDisplayed()
        {
            if (RacconMesh !=null)
            {
                return true;
            }
            return false;

        }
         public bool NightLightsAreDisplayed()
       {
        if (NightLights != null)
            return true;
        else
            return false;
       }

       public bool ConstructionGearIsDIsplayed()
       {
        if (RacconConstructionGear != null)
            return true;
        else
            return false;
       }

        public void PressPause()
        {
            PauseButton.Tap();
        }
        public int GetCurrentLife()
        {
            return Character.GetComponentProperty<int>("CharacterInputController", "currentLife", "Assembly-CSharp");
        }


        public void SetCharacterInvincible(string state)
        //state can be "True"or"False"
        {
            string[] parameters = new[] {state};
            CharacterSlot.CallComponentMethod<string>("CharacterCollider", "SetInvincibleExplicit", "Assembly-CSharp", parameters) ;
        }

        public void SetRunnerMultiplier()
        {
            
           // RunnerMultipler.SetText("x10");
      
            RunnerMultipler.SetComponentProperty("UnityEngine.UI.Text", "text", "x 10", "UnityEngine.UI" );

        }

        // public void WaitForComponentPropertyBoxColliderEnabled()
        // {
        //     var propertyBoxColliderEnabled =  Driver.WaitForComponentProperty<string>( "UnityEngine.BoxCollider" , "enabled", <string> "True", "UnityEngine.PhysicsModule");
        // }

      
        public void AvoidObstacles(int numberOfObstacles)
        {
            var character = Character;
            bool movedLeft = false;
            bool movedRight = false;
            for (int i = 0; i < numberOfObstacles; i++)
            {
                var allObstacles = Driver.FindObjectsWhichContain(By.NAME, "Obstacle");
                allObstacles.Sort((x, y) => x.worldZ.CompareTo(y.worldZ));
                allObstacles.RemoveAll(obs => obs.worldZ < character.worldZ);
                var obstacle = allObstacles[0];

                System.Console.WriteLine("Obstacle: " + obstacle.name + ", z:" + obstacle.worldZ + ", x:" + obstacle.worldX);
                System.Console.WriteLine("Next: " + allObstacles[1].name + ", z:" + allObstacles[1].worldZ + ", x:" + allObstacles[1].worldX);

                while (obstacle.worldZ - character.worldZ > 5)
                {
                    obstacle = Driver.FindObject(By.ID, obstacle.id.ToString());
                    character = Driver.FindObject(By.NAME, "PlayerPivot");
                }
                if (obstacle.name.Contains("ObstacleHighBarrier"))
                {
                    //Driver.PressKey(AltKeyCode.DownArrow);
                    Slide(character);
                }
                else
                if (obstacle.name.Contains("ObstacleLowBarrier") || obstacle.name.Contains("Rat"))
                {

                   // Driver.PressKey(AltKeyCode.UpArrow, 0, 0);
                   Jump(character);
                }
                else
                {
                    if (obstacle.worldZ == allObstacles[1].worldZ)
                    {
                        if (obstacle.worldX == character.worldX)
                        {
                            if (allObstacles[1].worldX == -1.5f)
                            {
                                //Driver.PressKey(AltKeyCode.RightArrow, 0, 0);
                                MoveRight(character);
                                movedRight = true;
                            }
                            else
                            {
                                //Driver.PressKey(AltKeyCode.LeftArrow, 0, 0);
                                MoveLeft(character);
                                movedLeft = true;
                            }
                        }
                        else
                        {
                            if (allObstacles[1].worldX == character.worldX)
                            {
                                if (obstacle.worldX == -1.5f)
                                {
                                    //Driver.PressKey(AltKeyCode.RightArrow, 0, 0);
                                    MoveRight(character);
                                    movedRight = true;
                                }
                                else
                                {
                                    //Driver.PressKey(AltKeyCode.LeftArrow, 0, 0);
                                    MoveLeft(character);
                                    movedLeft = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (obstacle.worldX == character.worldX)
                        {
                            //Driver.PressKey(AltKeyCode.RightArrow, 0, 0);
                            MoveRight(character);
                            movedRight = true;
                        }
                    }
                }
                while (character.worldZ - 3 < obstacle.worldZ && character.worldX < 99)
                {
                    obstacle = Driver.FindObject(By.ID, obstacle.id.ToString());
                    character = Driver.FindObject(By.NAME, "PlayerPivot");
                }
                if (movedRight)
                {
                   // Driver.PressKey(AltKeyCode.LeftArrow, 0, 0);
                   MoveLeft(character);
                    movedRight = false;
                }
                if (movedLeft)
                {
                    //Driver.PressKey(AltKeyCode.RightArrow, 0, 0);
                    MoveRight(character);
                    movedRight = false;
                }
            }
            character.CallComponentMethod<string>("CharacterInputController", "CheatInvincible",  "Assembly-CSharp", new string[]{"false"});

        }
    }
}