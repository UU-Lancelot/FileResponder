using System.Text.RegularExpressions;

namespace UU.Lancelot.FileResponder.Classes;
//cte obsah a zavola IReplacer
public class FindPlaceholdersXml
{
    public void Read(string fileContent, string placeholder = "{{")
    {
        Regex redex = new Regex(@"\{\{(.*?)\}\}");
        MatchCollection matches = redex.Matches(fileContent);

        foreach (Match match in matches)
        {
            Console.WriteLine("Found placeholder: " + match.Groups[1].Value);
            //Replacer
        }
    }
}