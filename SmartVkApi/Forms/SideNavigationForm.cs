using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Constants;
using SmartVkApi.Models;

namespace SmartVkApi.Forms
{
    public class SideNavigationForm : Form
    {
        private IButton MyPageButton(string myPage) => ElementFactory.GetButton(By.XPath(string.Format("//nav[@class = \"side_bar_nav\"]//span[contains (text(), \"{0}\")]", myPage)), "Side navigation form");
        private IButton MoreButton => ElementFactory.GetButton(By.XPath("//a[@id = \"left_menu_more\"]"), "More button");
        private IButton ChangeLanguageButton => ElementFactory.GetButton(By.XPath("//a[contains (@onclick,  \"language\")]"), "Change language button");

        
        public SideNavigationForm() : base(By.XPath("//nav[@class = \"side_bar_nav\"]"), "Side navigation form")
        {
        }

        public bool GoToMyProfilePage(LocalizedTestDataModel model)
        {
            FormElement.State.WaitForEnabled(TimeSpan.FromSeconds(ProjectConstants.TimeoutForElements));

            if (MyPageButton(model.MyProfile).State.IsExist)
            {
                MyPageButton(model.MyProfile).State.WaitForEnabled();
                MyPageButton(model.MyProfile).Click();
                return true;
            }
            OpenLanguageSelectForm();
            return false;
        }

        private void OpenLanguageSelectForm()
        {
            MoreButton.JsActions.HoverMouse();
            ChangeLanguageButton.State.WaitForEnabled(TimeSpan.FromSeconds(ProjectConstants.TimeoutForElements)); 
            ChangeLanguageButton.Click();
        }
    }
}