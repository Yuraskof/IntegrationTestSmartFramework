using SmartVkApi.Base;

namespace RestApiTask.Utils
{
    public class StringUtils
    {
        public static string StringGenerator(int lettersCount)
        {
            char[] letters = "ABCDEFGHI_JKLMN-OPQRS!TUVWXYZabc,defghigklmnopqrstuvwxyz".ToCharArray();

            Random rand = new Random();

            string word = "";

            for (int j = 1; j <= lettersCount; j++)
            {
                int letter = rand.Next(0, letters.Length - 1);

                word += letters[letter];
            }
            BaseTest.Logger.Info(string.Format("generated random text = {0}", word));
            return word;
        }
    }
}
