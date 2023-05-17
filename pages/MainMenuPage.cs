using Altom.AltDriver;

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
        public AltObject ThemeImage {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/ThemeZone/Image", timeout:10);}
        public AltObject ButtonRight {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/PowerupZone/ButtonRight", timeout:10);}
        public AltObject ButtonLeft {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/PowerupZone/ButtonLeft", timeout:10);}
        public AltObject CharzoneButtonRight {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/CharZone/CharName/CharSelector/ButtonRight", timeout:10);}
        public AltObject PowerUpFirstLeft {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/PowerupZone/Image/Amount", timeout:10);}
        public AltObject ThemeSelectorRight {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/ThemeZone/ThemeSelector/ButtonRight");}
        public AltObject AccessoriesSelectorDown {get => Driver.WaitForObject(By.PATH,"/UICamera/Loadout/AccessoriesSelector/ButtonBottom");}
        //public AltObject ThemeImageName { get => Driver.WaitForObject(By.NAME,"ThemeName").GetText();}
        public AltObject LeaderboardHighScoreName {get => Driver.FindObjectsWhichContain(By.PATH, "/UICamera/Leaderboard/Background/Display/Score/Name")[0];}
        public AltObject AltUnityLogo {get => Driver.FindObject(By.PATH, "/AltTesterPrefab/AltUnityDialog/Icon");}

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

       public void SelectPowerUp()
       {
            PowerUpFirstLeft.Tap();
       }

       public bool ThemeSelectorsDisplayed()
       {
        if (ThemeSelectorRight !=null)
            return true;
        else 
            return false;
       }

       public void ChangeTheme()
       {
        if(ThemeSelectorsDisplayed())
            ThemeSelectorRight.Tap();
        else 
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

      
    

       

      

        

    }
}