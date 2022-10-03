using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Base;
using SmartVkApi.Constants;
using SmartVkApi.Models;
using SmartVkApi.Utilities;

namespace SmartVkApi.Forms.Pages
{
    public class LoginOrRegistrationPage : Form
    {
        private ITextBox LoginTextBox => ElementFactory.GetTextBox(By.XPath("//input[@class= \"VkIdForm__input\"]"), "Login");
        private IButton LogInSubmitButton => ElementFactory.GetButton(By.XPath("//form[@class = \"VkIdForm__form\"]//button[contains(@class,\"signInButton\")]"), "Login submit");
        private IButton SelectLanguageButton(string language) => ElementFactory.GetButton(By.XPath(string.Format("//div[@id= \"content\"]//child::a[contains (text(), \"{0}\")]", language)), "Language");
        
        public static LoginUser loginUser;
        public PasswordForm passwordForm = new PasswordForm();

        public LoginOrRegistrationPage() : base(By.XPath("//div[@id = \"index_login\"] "), "Login or registration page")
        {
            loginUser = JsonUtils.ReadJsonDataFromPath<LoginUser>(FileConstants.PathToLoginUser);
        }

        private void SelectLanguage(LocalizedTestDataModel model)
        {
            SelectLanguageButton(model.Language).State.WaitForEnabled();
            SelectLanguageButton(model.Language).Click();
        } 

        private void SetUserLogin(string userName)
        {
            LoginTextBox.State.WaitForNotDisplayed(BaseTest.timeoutForElements);
            LoginTextBox.State.WaitForEnabled();
            LoginTextBox.ClearAndType(userName);
        }
        
        private void ClickSignInButton()
        {
            LogInSubmitButton.State.WaitForEnabled();
            LogInSubmitButton.Click();
        }

        public void PerformAuthorisation(LocalizedTestDataModel model)
        {
            SelectLanguage(model);
            SetUserLogin(loginUser.Login);
            ClickSignInButton();
        }
    }
}