using System.Text.RegularExpressions;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using SmartVkApi.Constants;
using SmartVkApi.Models;

namespace SmartVkApi.Utilities
{
    public static class MailClient
    {
        private static readonly ImapConfig imapConfig = FileReader.ReadJsonData<ImapConfig>(ProjectConstants.PathToImapConfig);
        private static Logger Logger => AqualityServices.Get<Logger>();

        private static bool CheckLinkInMessage(IMailFolder inbox, string productName)
        {
            Logger.Info("Update messages");
            inbox.Check();

            if (inbox.Count > 0)
            {
                Logger.Info("Got messages");
                var message = inbox.GetMessage(inbox.Count - 1);
                var subject = inbox.GetMessage(inbox.Count - 1).Subject;

                if (subject.Contains(productName))
                {
                    Logger.Info(string.Format("Subject contains {0}", productName));
                    var textBody = inbox.GetMessage(inbox.Count - 1).TextBody;

                    Match match = Regex.Match(textBody, @"https:.+Download");

                    if (match.Success)
                    {
                        Logger.Info(string.Format("Message contains download link"));
                        return true;
                    }
                }
                Logger.Info(string.Format("Subject doesn't contain {0}", productName));
            }
            return false;
        }

        public static bool CheckMessage(string productName)
        {
            using (var client = new ImapClient())
            {
                Logger.Info("Connected to mail server");
                client.Connect(imapConfig.host, Convert.ToInt32(imapConfig.port), SecureSocketOptions.SslOnConnect);

                imapConfig.Email = TestContext.Parameters["emailAdress"];
                imapConfig.MailPassword = TestContext.Parameters["mailPassword"];

                client.Authenticate(imapConfig.Email, imapConfig.MailPassword);

                Logger.Info("Opend inbox folder");
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                if (AqualityServices.ConditionalWait.WaitFor(() => {
                            return CheckLinkInMessage(inbox, productName);
                },
                        TimeSpan.FromSeconds(ProjectConstants.TimeoutForServer), TimeSpan.FromSeconds(ProjectConstants.PollingIntervalForServer)))
                {
                    Logger.Info("Disconnected from the mail server");
                    client.Disconnect(true);
                    return true;
                }

                Logger.Info("Disconnected from the mail server");
                client.Disconnect(true);
                return false;
            }
        }
    }
}
