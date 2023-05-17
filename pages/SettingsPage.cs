using Altom.AltDriver;

namespace alttrashcat_tests_csharp.pages
{
    public class SettingsPage : BasePage
    {
        public SettingsPage(AltDriver driver) : base(driver)
        {
        }
        public void LoadScene()
        {
            Driver.LoadScene("Main");
        }

    public AltObject SettingsButton { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingButton", timeout: 10); }
    public AltObject SettingsPopUp { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background", timeout: 10 );}

    public AltObject DeleteDataButton { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background/DeleteData", timeout: 10 );  }
    public AltObject ConfirmationPopUp { get=> Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/ConfirmPopup/Image", timeout: 10);}

    public AltObject ConfirmYesButton { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/ConfirmPopup/Image/YESButton", timeout: 10);}
    public AltObject ClosePopUpButton { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background/CloseButton");}
 

    // public AltObject MasterSlider { get => Driver.WaitForObject(By.NAME, "MasterSlider"); }
    // public AltObject MusicSlider { get => Driver.WaitForObject(By.NAME, "MusicSlider"); }
    // public AltObject MasterSFXSlider { get => Driver.WaitForObject(By.NAME, "MasterSFXSlider"); }

    
    
    public void PressSettings ()
    {
        SettingsButton.Tap();
    }
        
     public bool PopUpisDisplayed()
    {
            if (SettingsPopUp != null && DeleteDataButton != null)
                return true;
            return false;
    }

    public void PressDeleteData()
    {
        DeleteDataButton.Tap();
    }

    public bool ConfirmationPopUpisDisplayed()
    {
        if (ConfirmationPopUp != null && ConfirmYesButton != null)
                return true;
            return false;
    }

    public void PressYesDeleteData()
    {
        ConfirmYesButton.Tap();
    }

    public void PressClosePopUp()
    {
        ClosePopUpButton.Tap();
    }

    public void DeleteData()
    {
        SettingsButton.Tap();
        DeleteDataButton.Tap();
        ConfirmYesButton.Tap();
        ClosePopUpButton.Tap();
    }

    //public int GetSliderValue(string sliderName)
    public float GetSliderValue(string sliderName)

    {
       var  slider = Driver.WaitForObject(By.NAME, sliderName); 
       //var sliderHandleMaster = Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background/MasterSlider/Handle Slide Area/Handle");
       float getSliderValue = slider.GetComponentProperty<float>("UnityEngine.UI.Slider", "value", "UnityEngine.UI");
       return getSliderValue;
    }

    public void MoveSlider(string sliderName)
    {
       // var  slider = Driver.WaitForObject(By.NAME, sliderName); 
        var sliderHandle = Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background/"+sliderName+"/Handle Slide Area/Handle");
        Driver.Swipe(new AltVector2(sliderHandle.x, sliderHandle.y), new AltVector2(sliderHandle.x+10, sliderHandle.y));
    }

     public void MoveSliderToStart(string sliderName)
    {
       // var  slider = Driver.WaitForObject(By.NAME, sliderName); 
        var sliderHandle = Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background/"+sliderName+"/Handle Slide Area/Handle");
        Driver.Swipe(new AltVector2(sliderHandle.x, sliderHandle.y), new AltVector2(sliderHandle.x-1000, sliderHandle.y));
    }






    }}