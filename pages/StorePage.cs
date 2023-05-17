using Altom.AltDriver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace alttrashcat_tests_csharp.pages
{
    public class StorePage : BasePage
    {
        public StorePage(AltDriver driver) : base(driver)
        {
        }
        public void LoadScene()
        {
            Driver.LoadScene("Shop");
        }


        public AltObject StoreTitle { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/StoreTitle"); }
        public AltObject CloseButton { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Button");}
        public AltObject ItemsTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Item");}
        public AltObject CharactersTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Character");}
        public AltObject AccessoriesTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Accesories");}
        public AltObject ThemesTab { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/TabsSwitch/Themes");}
        public AltObject BuyButton { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/ItemsList/Container/ItemEntry(Clone)/NamePriceButtonZone/PriceButtonZone/BuyButton");}
        public AltObject PremiumPlusButton { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Premium/Button");}
        public AltObject CoinImage { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Coin/Image");}
        public AltObject PremiumCoinImage { get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Premium/Image");}

        public string componentName = "UnityEngine.UI.Button"; 
        public string assemblyName = "UnityEngine.UI";
        public string propertyName = "interactable";

       // public List<> allBuyButtons = new List<>(Driver.FindObjectsWhichContain(By.NAME, "BuyButton"));
        public List<AltObject> allBuyButtons { get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton");}
         public AltObject BuyMagnetButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[0] ;}
         public AltObject BuyMultiplierButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[1] ;}
         public AltObject BuyInvincibleButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[2] ;}
         public AltObject BuyLifeButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[3] ;}
        public AltObject BuyNightimeButton { get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[1];}
        public AltObject PremiumCounter {get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Premium/PremiumCounter");}
        public string PremiumCoinsValue { get =>PremiumCounter.GetText(); }
        public AltObject CoinsCounter {get => Driver.WaitForObject(By.PATH,"/Canvas/Background/Coin/CoinsCounter");}
        public string CoinsCounterValue { get =>CoinsCounter.GetText(); }
        public AltObject BuyRaccoon {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[1] ;}

        public AltObject BuySafetyButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[0] ;}
        public AltObject BuyPartyHatButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[1] ;}
        public AltObject BuySmartHatButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[2] ;}
        public AltObject BuyRacoonSafetyButton {get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[3] ;}
        public AltObject SardineImage {get => Driver.FindObject(By.PATH, "/Canvas/Background/Premium/Image");}
        public AltObject PremiumButtonAtCoordinates {get=> Driver.FindObjectAtCoordinates(new AltVector2(SardineImage.x - 46, SardineImage.y));}
        public AltObject EventSystemStore {get => Driver.FindObject(By.NAME, "EventSystem");}

       

        public bool BuyButtonsAreEnabled()
        {
        
            var BuyMagnetEnabled = BuyMagnetButton.GetComponentProperty<string>(componentName, propertyName, assemblyName);
            var BuyMultiplierEnabled = BuyMultiplierButton.GetComponentProperty<string>(componentName, propertyName, assemblyName);
            var BuyInvincibleEnabled = BuyInvincibleButton.GetComponentProperty<string>(componentName, propertyName, assemblyName);
            var BuyLifeEnabled = BuyLifeButton.GetComponentProperty<string>(componentName, propertyName, assemblyName);
           
        
            if (BuyMagnetEnabled == "true" && BuyInvincibleEnabled=="true" && BuyMultiplierEnabled=="true" && BuyLifeEnabled=="true")
                return true;
            else 
                return false; 

        }

        public bool BuyMagnetButtonIsEnabled()
        {
            var BuyMagnetEnabled = BuyMagnetButton.GetComponentProperty<string>(componentName, propertyName, assemblyName);
             if (BuyMagnetEnabled == "true")
             return true;
            else 
                return false; 
        }

        public void BuyMagnet()
        {
            BuyMagnetButton.Tap();
        }

        public void BuyLife()
        {
            BuyLifeButton.Tap();
        }
         public bool StoreIsDisplayed()
        {
            if (StoreTitle != null && CloseButton != null && ItemsTab != null && CharactersTab != null && AccessoriesTab != null && ThemesTab != null && BuyButton != null && PremiumPlusButton !=null && CoinImage !=null && PremiumCoinImage !=null)
                return true;
            return false;   
        }
        public void OpenThemes()
        {
            ThemesTab.Tap();
        }
        public bool CountersReset()
        {
            if (CoinsCounterValue == "0" && PremiumCoinsValue == "0")
                return true;
            return false;
        }
        public void PressStore()
        {
            StoreTitle.Tap();
        }
        public void ReloadItems()
        {
            ItemsTab.Tap();
        }
        public void PressCharactersTab()
        {
            CharactersTab.Tap();
        }
        public void BuyRubbishRaccon()
        {
            CharactersTab.Tap();
            BuyRaccoon.Tap();
        }
        public void CloseStore()
        {
            CloseButton.Tap();
        }
        public void BuyNightTheme()
        {
            BuyNightimeButton.Tap();
        }

        public void PressAccessoriesTab()
        {
            AccessoriesTab.Tap();
        }
        public void BuyAccessoryItems()
        {
           BuySafetyButton.Tap();
           BuyPartyHatButton.Tap();
           BuySmartHatButton.Tap();
           BuyRacoonSafetyButton.Tap();
            
        }

        public void EnableMagnetBuyButton()
        {
            BuyMagnetButton.SetComponentProperty("UnityEngine.UI.Button", "interactable", "True", "UnityEngine.UI");
        }

        public void CharactersTabPointerEnterExitStateColors()
        {
            object normalCharactersTabColor= CharactersTab.CallComponentMethod<object>("UnityEngine.CanvasRenderer", "GetColor", "UnityEngine.UIModule", new object[] { });
            Console.WriteLine(" Normal button color " +  normalCharactersTabColor);
            CharactersTab.PointerEnterObject();
            Thread.Sleep(1000);

            object hoverCharactersTabColor= CharactersTab.CallComponentMethod<object>("UnityEngine.CanvasRenderer", "GetColor", "UnityEngine.UIModule", new object[] { });
            Console.WriteLine(" Hover button color " +  hoverCharactersTabColor);

            CharactersTab.PointerExitObject();
            Thread.Sleep(1000);
            object afterCharactersTabColor = CharactersTab.CallComponentMethod<object>("UnityEngine.CanvasRenderer", "GetColor", "UnityEngine.UIModule", new object[] { });
             Console.WriteLine(" After exit button color " +  hoverCharactersTabColor);

        }

         public bool IsPointerOnObject()
       {
        bool data = Driver.CallStaticMethod<bool>("UnityEngine.EventSystems.StandaloneInputModule", "IsPointerOverGameObject", "UnityEngine.UI", new object[] { });
        return data;
       }



    }
}

    