using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Base;

namespace SmartVkApi.Forms
{
    public class FullSizeImageForm : Form
    {
        private ILabel PostImageFullSize => ElementFactory.GetLabel(By.XPath("//div[@id = \"pv_photo\"]//img[contains (@src, \"album\")]"), "PostImage");
        private IButton CloseFormButton => ElementFactory.GetButton(By.XPath("//div[@class = \"pv_close_btn\"]"), "Close button");
        
        public FullSizeImageForm() : base(By.XPath("//div[@class = \"pv_cont\"]"), "FullSizeImageForm") 
        {
        }

        protected override IDictionary<string, IElement> ElementsForVisualization => new Dictionary<string, IElement>()
        {
            {"PostImage", PostImageFullSize },
        };

        public float CompareImages()
        {
            PostImageFullSize.State.WaitForEnabled(BaseTest.timeoutForElements);
            return Dump.Compare();
        }

        public void CloseForm()
        {
            CloseFormButton.ClickAndWait();
        }
    }
}
