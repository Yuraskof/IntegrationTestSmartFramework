using SmartVkApi.Utilities;

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
            LoggerUtils.LogStep(nameof(StringGenerator) + $" \"Generated random text - [{word}]\"");
            return word;
        }
    }
}
