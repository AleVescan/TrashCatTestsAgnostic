


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
        public AltObject StoreTitle { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/StoreTitle"); }
        public AltObject CloseButton { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/Button"); }
        public AltObject ItemsTab { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/TabsSwitch/Item"); }
        public AltObject CharactersTab { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/TabsSwitch/Character"); }
        public AltObject AccessoriesTab { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/TabsSwitch/Accesories"); }
        public AltObject ThemesTab { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/TabsSwitch/Themes"); }
        public AltObject BuyButton { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/ItemsList/Container/ItemEntry(Clone)/NamePriceButtonZone/PriceButtonZone/BuyButton"); }
        public AltObject PremiumPlusButton { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/Premium/Button"); }
        public AltObject CoinImage { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/Coin/Image"); }
        public AltObject PremiumCoinImage { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/Premium/Image"); }
        public List<AltObject> allBuyButtons { get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton"); }
        public AltObject FirstBuyButtonInTab { get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[0]; }
        public AltObject SecondBuyButtonInTab { get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[1]; }
        public AltObject ThirdBuyButtonInTab { get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[2]; }
        public AltObject FourthBuyButtonInTab { get => Driver.FindObjectsWhichContain(By.NAME, "BuyButton")[3]; }
        public AltObject PremiumCounter { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/Premium/PremiumCounter"); }
        public string PremiumCoinsValue { get => PremiumCounter.GetText(); }
        public AltObject CoinsCounter { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/Coin/CoinsCounter"); }
        public string CoinsCounterValue { get => CoinsCounter.GetText(); }
        public AltObject SardineImage { get => Driver.FindObject(By.PATH, "/Canvas/Background/Premium/Image"); }
        public AltObject PremiumButtonAtCoordinates { get => Driver.FindObjectAtCoordinates(new AltVector2(SardineImage.x - 46, SardineImage.y)); }
        public bool EnableButtonObject(AltObject button)
        {
            button.SetComponentProperty("UnityEngine.UI.Button", "interactable", "True", "UnityEngine.UI");
            return button.GetComponentProperty<bool>("UnityEngine.UI.Button", "interactable", "UnityEngine.UI");
        }
        public bool ButtonObjectState(AltObject button)
        {
            return button.GetComponentProperty<bool>("UnityEngine.UI.Button", "interactable", "UnityEngine.UI");
        }
        public bool BuyButtonsState()
        {
            bool BuyMagnetState = ButtonObjectState(FirstBuyButtonInTab);
            bool BuyMultiplierState = ButtonObjectState(SecondBuyButtonInTab);
            bool BuyInvincibleState = ButtonObjectState(ThirdBuyButtonInTab);
            bool BuyLifeState = ButtonObjectState(FourthBuyButtonInTab);

            if (BuyMagnetState && BuyInvincibleState && BuyMultiplierState && BuyLifeState)
                return true;
            else
                return false;
        }
        public void BuyMagnet()
        {
            FirstBuyButtonInTab.Tap();
        }
        public void BuyLife()
        {
            FourthBuyButtonInTab.Tap();
        }
        public bool StoreIsDisplayed()
        {
            if (StoreTitle != null && CloseButton != null && ItemsTab != null && CharactersTab != null && AccessoriesTab != null && ThemesTab != null && BuyButton != null && PremiumPlusButton != null && CoinImage != null && PremiumCoinImage != null)
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
        public void PressStoreToAddCoins()
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
            SecondBuyButtonInTab.Tap();
        }
        public void CloseStore()
        {
            CloseButton.Tap();
        }
        public void BuyNightTheme()
        {
            SecondBuyButtonInTab.Tap();
        }
        public void PressAccessoriesTab()
        {
            AccessoriesTab.Tap();
        }
        public void BuyAccessoryItems()
        {
            FirstBuyButtonInTab.Tap();
            SecondBuyButtonInTab.Tap();
            ThirdBuyButtonInTab.Tap();
            FourthBuyButtonInTab.Tap();
        }
        public object GetColorOfObject(AltObject colouredObject)
        {
            return colouredObject.CallComponentMethod<object>("UnityEngine.CanvasRenderer", "GetColor", "UnityEngine.UIModule", new object[] { });
        }
        public bool IsPointerOnObject()
        {
            bool data = Driver.CallStaticMethod<bool>("UnityEngine.EventSystems.StandaloneInputModule", "IsPointerOverGameObject", "UnityEngine.UI", new object[] { });
            return data;
        }
    }
}

