using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace SmartVkApi.Forms.Pages
{
    public class MyProfilePage : Form
    {
        //div[@id = "page_wall_posts"]//div[contains (@class, "wall_post_text")] 
        public MyProfilePage() : base(By.XPath("//div[@id= \"profile_wall\"]"), "My profile page")
        {
        }
        //    if (!AqualityServices.ConditionalWait.WaitFor(() => {
        //                return ProductLabel(productName).State.IsDisplayed;
        //            },
        //            TimeSpan.FromSeconds(ProjectConstants.Timeout), TimeSpan.FromSeconds(ProjectConstants.PollingInterval)))
        //    {
        //        NextItemsButton.ClickAndWait();
        //    }
    }
}
