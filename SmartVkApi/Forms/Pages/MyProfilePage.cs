﻿using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Constants;
using SmartVkApi.Models;

namespace SmartVkApi.Forms.Pages
{
    public class MyProfilePage : Form
    {
        private ITextBox PostTextBox(string text) => ElementFactory.GetTextBox(By.XPath(string.Format("//div[contains (@class, \"wall_post_text\")][contains (text(), \"{0}\")]", text)), "Posted text");
        private ILink AuthorNameLink => ElementFactory.GetLink(By.XPath("//a[@class = \"author\"]"), "Author name");

        private ILabel PostImage(string id) => ElementFactory.GetLabel(By.XPath(string.Format("//div[contains (@class, \"wall_post_cont\")]//a[contains (@href,\"{0}\")]", id)), "Post image mini");

        public FullSizeImageForm fullSizeImageForm = new FullSizeImageForm();
        
        public MyProfilePage() : base(By.XPath("//div[@id= \"profile_wall\"]"), "My profile page")
        {
        }

        public string GetPostText(WallPostModel postModel)
        {
            PostTextBox(postModel.message).State.WaitForEnabled(TimeSpan.FromSeconds(ProjectConstants.TimeoutForElements));
            return PostTextBox(postModel.message).Text;
        }

        public string GetAuthorLink()
        {
            return AuthorNameLink.Href;
        }

        public void OpenFullSizeImage(string id)
        {
            PostImage(id).ClickAndWait();
        }
    }
}
