using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using SmartVkApi.Base;
using SmartVkApi.Models.RequestModels;

namespace SmartVkApi.Forms.Pages
{
    public class MyProfilePage : Form
    {
        private ITextBox PostTextBox(string text) => ElementFactory.GetTextBox(By.XPath(string.Format("//div[contains (@class, \"wall_post_text\")][contains (text(), \"{0}\")]", text)), "Posted text");
        private ILink AuthorNameLink => ElementFactory.GetLink(By.XPath("//a[@class = \"author\"]"), "Author name");
        private ILabel PostImage(string id) => ElementFactory.GetLabel(By.XPath(string.Format("//div[contains (@class, \"wall_post_cont\")]//a[contains (@href,\"{0}\")]", id)), "Post image mini");
        private ITextBox CommentTextBox(string postText, string commentText) => PostTextBox(postText).FindChildElement<ITextBox>(By.XPath(string.Format("//following::div[@class = \"wall_reply_text\"][contains (text(), \"{0}\")]", commentText)), "Comment");
        private ILink CommentAuthorNameLink(string postText, string commentText) => PostTextBox(postText).FindChildElement<ILink>(By.XPath(string.Format("//following::div[@class = \"wall_reply_text\"][contains (text(), \"{0}\")]//preceding:: div[@class = \"reply_author\"]//a[@class = \"author\"]", commentText)), "Comment author");
        private IButton ShowNextComment(string postText) => PostTextBox(postText).FindChildElement<IButton>(By.XPath("//following:: span[contains(@class, \"js-replies_next_label\")]"), "Show next comment");
        private IButton LikeButton(string postText) => PostTextBox(postText).FindChildElement<IButton>(By.XPath("//following::span[contains(@class, \"_like_button_icon\")]"), "Add like");

        public FullSizeImageForm fullSizeImageForm = new FullSizeImageForm();
        
        public MyProfilePage() : base(By.XPath("//div[@id= \"profile_wall\"]"), "My profile page")
        {
        }

        public string GetPostText(WallPostModel postModel)
        {
            PostTextBox(postModel.message).JsActions.ScrollToTheCenter();
            PostTextBox(postModel.message).State.WaitForEnabled(BaseTest.timeoutForPosts);
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

        public string GetCommentText(WallPostModel postModel, WallCommentModel commentModel)
        {
            ShowNextComment(postModel.message).JsActions.ScrollToTheCenter();
            ShowNextComment(postModel.message).Click();
            CommentTextBox(postModel.message, commentModel.message).State.WaitForEnabled(BaseTest.timeoutForPosts);
            return CommentTextBox(postModel.message, commentModel.message).Text;
        }

        public string GetCommentAuthorLink(WallPostModel postModel, WallCommentModel commentModel)
        {
            return CommentAuthorNameLink(postModel.message, commentModel.message).Href;
        }

        public void AddLike(WallPostModel postModel)
        {
            LikeButton(postModel.message).Click();
        }

        public bool CheckThePostNotExist(WallPostModel postModel)
        {
            return PostTextBox(postModel.message).State.WaitForNotDisplayed(BaseTest.timeoutForPosts);
        }
    }
}
