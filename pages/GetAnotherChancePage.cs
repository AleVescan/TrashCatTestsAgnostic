using Altom.AltDriver;
using Newtonsoft.Json;

namespace alttrashcat_tests_csharp.pages
{
    public class GetAnotherChancePage : BasePage
    {
        public GetAnotherChancePage(AltDriver driver) : base(driver)
        {
        }

        public AltObject GameOverButton { get => Driver.WaitForObject(By.NAME, "GameOver"); }
        public AltObject PremiumButton { get => Driver.WaitForObject(By.NAME, "Premium Button"); }
        public AltObject AvailableCurrency { get => Driver.WaitForObject(By.NAME, "PremiumOwnCount"); }
        public AltObject GetAnotherChanceText {get => Driver.WaitForObject(By.PATH, "/UICamera/Game/DeathPopup/Text");}
        public AltObject PremiumDisplay {get => Driver.FindObject(By.PATH, "/UICamera/Game/DeathPopup/PremiumDisplay");}


        // public AltObject NumberOfPremiumCoins {get => Driver.FindObject(By.PATH, " /UICamera/Game/DeathPopup/PremiumDisplay/CurrencyBG").GetText() ;}

         public bool GetAnotherChangeObjectState{ get => PremiumButton.GetComponentProperty<bool>("UnityEngine.UI.Button", "interactable", "UnityEngine.UI");}


        public bool IsDisplayed()
        {
            if (GameOverButton != null && PremiumButton != null && AvailableCurrency != null)
                return true;
            return false;
        }

      
        public void PressGameOver()
        {
            GameOverButton.Tap();
        }
        public void PressPremiumButton()
        {
            PremiumButton.Tap();
        }

        public int GetPremiumButtonState()
        {
          return  PremiumButton.CallComponentMethod<int>("UnityEngine.UI.Button", "get_currentSelectionState", "UnityEngine.UI", new object[] { } );
        }


        public float GetPremiumButtonCurrentColorRGB(string colorChannel)
        {
            object PremiumCurrentColor = PremiumButton.CallComponentMethod<object>("UnityEngine.CanvasRenderer", "GetColor", "UnityEngine.UIModule", new object[] { });
            string json = JsonConvert.SerializeObject(PremiumCurrentColor);
            dynamic colorData = JsonConvert.DeserializeObject(json);
            float rValue = colorData[colorChannel];
            return rValue;
        }

        

        public float GetPremiumButtonStateColorRGB(string state, string colorStateChannel)
        {
           
            float PremiumButtonStateColorRGB = PremiumButton.GetComponentProperty<float>("UnityEngine.UI.Button", "colors."+state+"."+colorStateChannel, "UnityEngine.UI");
            return PremiumButtonStateColorRGB; 
        }

        

    }
}