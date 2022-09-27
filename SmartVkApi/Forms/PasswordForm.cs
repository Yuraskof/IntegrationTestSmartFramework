using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Constants;
using SmartVkApi.Forms.Pages;
using SmartVkApi.Models;

namespace SmartVkApi.Forms;

public class PasswordForm : Form
{
    private ITextBox PasswordTextBox => ElementFactory.GetTextBox(By.XPath("//input[@class = \"vkc__TextField__input\"][@name = \"password\"]"), "Password");
    private IButton PasswordSubmitButton(string signIn) => ElementFactory.GetButton(By.XPath(string.Format("//span[contains (text(), \"{0}\")]//ancestor:: button", signIn)), "Password submit");
    
    public PasswordForm() : base(By.XPath("//form[@class = \"vkc__EnterPasswordNoUserInfo__content\"]"), "Enter password form")
    {
    }

    private void SetPassword(string userPassword) => PasswordTextBox.ClearAndType(userPassword);
   
    private void ClickContinueButton(string signIn)
    {
        PasswordSubmitButton(signIn).State.WaitForEnabled(TimeSpan.FromSeconds(ProjectConstants.TimeoutForElements));
        PasswordSubmitButton(signIn).Click();
    }

    public void PerformPassword(LocalizedTestDataModel model)
    {
        SetPassword(LoginOrRegistrationPage.loginUser.Password);
        ClickContinueButton(model.SubmitPasswordButton);
    }
}