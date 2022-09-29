using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Constants;

namespace SmartVkApi.Forms
{
    public class FullSizeImageForm : Form
    {
        private ILabel PostImageFullSize => ElementFactory.GetLabel(By.XPath("//div[@id = \"pv_photo\"]//img[contains (@src, \"album\")]"), "Post image");

        private IButton CloseFormButton => ElementFactory.GetButton(By.XPath("//div[@class = \"pv_close_btn\"]"), "Close button");
        
        public FullSizeImageForm() : base(By.XPath("//div[@class = \"pv_cont\"]"), "Full size image form") 
        {
        }

        protected override IDictionary<string, IElement> ElementsForVisualization => new Dictionary<string, IElement>()
        {
            {"Post image", PostImageFullSize },
        };

        public float CompareImages()
        {
            PostImageFullSize.State.WaitForEnabled(TimeSpan.FromSeconds(ProjectConstants.TimeoutForElements));
            return Dump.Compare();
        }

        public void CloseForm()
        {
            CloseFormButton.ClickAndWait();
        }
    }
}
