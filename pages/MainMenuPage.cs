namespace alttrashcat_tests_csharp.pages
{
    public class MainMenuPage : BasePage
    {
        public MainMenuPage(AltDriver driver) : base(driver)
        {
        }       
        public void LoadScene()
        {
            Driver.LoadScene("Main");
        }
        public AltObject StoreButton { get => Driver.WaitForObject(By.NAME, "StoreButton", timeout: 10); }
        public AltObject LeaderBoardButton { get => Driver.WaitForObject(By.NAME, "OpenLeaderboard", timeout: 10); }
        public AltObject SettingsButton { get => Driver.WaitForObject(By.NAME, "SettingButton", timeout: 10); }
        public AltObject MissionButton { get => Driver.WaitForObject(By.NAME, "MissionButton", timeout: 10); }
        public AltObject RunButton { get => Driver.WaitForObject(By.NAME, "StartButton", timeout: 10); }
        public AltObject CharacterName { get => Driver.WaitForObject(By.NAME, "CharName", timeout: 10); }
        public AltObject ThemeName { get => Driver.WaitForObject(By.NAME, "ThemeName", timeout: 10); }
        public AltObject ThemeZoneCamera { get => Driver.FindObject(By.NAME, "ThemeZone", By.NAME, "UICamera"); }
        public AltObject ThemeImage {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/ThemeZone/Image", timeout:10);}
        public AltObject ButtonLeft {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/PowerupZone/ButtonLeft", timeout:10);}
        public AltObject CharzoneButtonRight {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/CharZone/CharName/CharSelector/ButtonRight", timeout:10);}
        public AltObject ThemeSelectorRight {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/ThemeZone/ThemeSelector/ButtonRight");}
        public AltObject AccessoriesSelectorDown {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/AccessoriesSelector/ButtonBottom");}
        public AltObject LeaderboardHighScoreName {get => Driver.FindObjectsWhichContain(By.PATH, "/UICamera/Leaderboard/Background/Display/Score/Name")[0];}
        public bool IsDisplayed()
        {
            if (StoreButton != null && LeaderBoardButton != null && SettingsButton != null && MissionButton != null && RunButton != null && CharacterName != null && ThemeName != null && ThemeImage !=null)
                return true;
            return false;
        }
        public void PressRun()
        {
            RunButton.Tap();
        }
        public void PressSettings()
        {
            SettingsButton.Tap();
        }
        public void PressStore()
        {
            StoreButton.Tap();
        }
       public void MovePowerUpLeft()
       {
            ButtonLeft.Tap();
       }
    // The following method seems to no longer be used in any tests, so I will comment it
    //    public void SelectPowerUp()
    //    {
    //         PowerUpFirstLeft.Tap();
    //    }
       public void ChangeTheme()
       {
            ThemeSelectorRight.Tap();    
       }
       public void ChangeCharacter()
       {
        CharzoneButtonRight.Tap();
       }
       public void ChangeAccessory()
       {
        AccessoriesSelectorDown.Tap();
       }
       public void SelectLeaderBoard()
       {
        LeaderBoardButton.Tap();
       }
       public void SetHighScoreName()
       {
        LeaderboardHighScoreName.SetText("HighScore");
       }
        public void SetResolution(string x, string y, string fullscreen)
        {
        Driver.CallStaticMethod<string>("UnityEngine.Screen", "SetResolution", "UnityEngine.CoreModule", new string[] { x, y, fullscreen }, new string[] { "System.Int32", "System.Int32", "System.Boolean" });
        }

      
    

       

      

        

    }
}