using System.Text;

namespace CleverenceSoftJuniorTest
{
    public class Task1
    {
        private static void Main(string[] args)
        {
            string inputLine = "aaabbcccdde";
            Console.WriteLine(GroupParser(inputLine));
        }

        public static string GroupParser(string inputString)
        {

            StringBuilder resultString = new();

            if (inputString.Length == 0)
                return String.Empty;

            char actualSymb = inputString[0];
            int actualSymbCounter = 1;
            for (int i = 1; i < inputString.Length; i++)
            {
                char tmpSymb = inputString[i];

                if (tmpSymb == actualSymb)
                {
                    actualSymbCounter++;
                }
                else
                {
                    AddNewPart(resultString, actualSymb, actualSymbCounter);
                    actualSymb = tmpSymb;
                    actualSymbCounter = 1;
                }
            }

            AddNewPart(resultString, actualSymb, actualSymbCounter);

            return resultString.ToString();
        }

        private static void AddNewPart(StringBuilder builder, char symb, int counter)
        {
            builder.Append(symb);
            if (counter > 1)
                builder.Append(counter);
        }
    }
}