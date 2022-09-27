using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Constants;
using SmartVkApi.Models;

namespace SmartVkApi.Forms
{
    public class SelectLanguageForm : Form
    {
        private IButton SelectLanguageButton(string language) => ElementFactory.GetButton(By.XPath(string.Format("//span[@class = \"language_box_row_label\"]//span[contains (text(), \"{0}\")]", language)), "Language");

        public SelectLanguageForm() : base(By.XPath("//div[@class = \"box_layout\"]"), "Select language form")
        {
        }

        public void SelectLanguage(LocalizedTestDataModel model)
        {
            SelectLanguageButton(model.Language).ClickAndWait();
            SelectLanguageButton(model.Language).State.WaitForNotExist(TimeSpan.FromSeconds(ProjectConstants.TimeoutForElements));
        }
    }
}
