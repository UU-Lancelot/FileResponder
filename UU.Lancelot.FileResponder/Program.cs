using UU.Lancelot.FileResponder.FormatIO;
using UU.Lancelot.FileResponder.PlaceholderProcessing;


XmlFormatIO xmlFormatIO = new XmlFormatIO();

string filePath = @"C:\Users\Marek\source\git\FileResponder\Examples\template.xml";
string resultPath = @"C:\Users\Marek\Desktop\test\ahoj.xml";

using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
using (FileStream resultStream = new FileStream(resultPath, FileMode.Create))
{
    xmlFormatIO.Format(fileStream, resultStream);
}