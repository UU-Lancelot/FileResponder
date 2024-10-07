using UU.Lancelot.FileResponder.FormatIO;

XmlFormatIO xmlFormatIO = new XmlFormatIO();

string filePath = @"..\Examples\template.xml";
string resultPath = @"..\Examples\result.xml";

using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
using (FileStream resultStream = new FileStream(resultPath, FileMode.Create))
{
    xmlFormatIO.Format(fileStream, resultStream);
}