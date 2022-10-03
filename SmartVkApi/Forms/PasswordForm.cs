using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Base;
using SmartVkApi.Forms.Pages;

namespace SmartVkApi.Forms;

public class PasswordForm : Form
{
    private ITextBox PasswordTextBox => ElementFactory.GetTextBox(By.XPath("//input[@class = \"vkc__TextField__input\"][@name = \"password\"]"), "Password");
    private IButton PasswordSubmitButton => ElementFactory.GetButton(By.XPath("//form[@class=\"vkc__EnterPasswordNoUserInfo__content\"]//span[@class=\"vkuiButton__in\"]"), "Password submit");
    
    public PasswordForm() : base(By.XPath("//form[@class = \"vkc__EnterPasswordNoUserInfo__content\"]"), "Enter password form")
    {
    }

    private void SetPassword(string userPassword) => PasswordTextBox.ClearAndType(userPassword);
   
    private void ClickContinueButton()
    {
        PasswordSubmitButton.State.WaitForEnabled(BaseTest.timeoutForElements);
        PasswordSubmitButton.Click();
    }

    public void PerformPassword()
    {
        SetPassword(LoginOrRegistrationPage.loginUser.Password);
        ClickContinueButton();
    }
}