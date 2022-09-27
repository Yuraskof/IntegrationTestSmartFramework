using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Constants;
using SmartVkApi.Models;
using SmartVkApi.Utilities;

namespace SmartVkApi.Forms.Pages
{
    public class LoginOrRegistrationPage : Form
    {
       
        private ITextBox LoginTextBox => ElementFactory.GetTextBox(By.XPath("//input[@class= \"VkIdForm__input\"]"), "Login");
        private IButton LogInSubmitButton(string signIn) => ElementFactory.GetButton(By.XPath(string.Format("//span[@class= \"FlatButton__content\"][contains (text(), \"{0}\")]", signIn)), "Login submit");
        private IButton SelectLanguageButton(string language) => ElementFactory.GetButton(By.XPath(string.Format("//div[@id= \"content\"]//child::a[contains (text(), \"{0}\")]", language)), "Language");
        
        public static LoginUser loginUser;

        public PasswordForm passwordForm = new PasswordForm();

        public LoginOrRegistrationPage() : base(By.XPath("//div[@id = \"index_login\"] "), "Login or registration page")
        {
            loginUser = JsonUtils.ReadJsonDataFromPath<LoginUser>(ProjectConstants.PathToLoginUser);
        }

        public void SelectLanguage(LocalizedTestDataModel model)
        {
            SelectLanguageButton(model.Language).State.WaitForEnabled();
            SelectLanguageButton(model.Language).Click();
        } 

        private void SetUserLogin(string userName)
        {
            LoginTextBox.State.WaitForNotDisplayed(TimeSpan.FromSeconds(ProjectConstants.TimeoutForElements));
            LoginTextBox.State.WaitForEnabled();
            LoginTextBox.ClearAndType(userName);
        }
        
        private void ClickSignInButton(string signIn)
        {
            LogInSubmitButton(signIn).State.WaitForEnabled();
            LogInSubmitButton(signIn).Click();
        }

        public void PerformLogin(LocalizedTestDataModel model)
        {
            SetUserLogin(loginUser.Login);
            ClickSignInButton(model.SubmitLoginButton);
        }
    }
}