using UU.Lancelot.FileResponder.FormatIO;
using UU.Lancelot.FileResponder.Interfaces;
using UU.Lancelot.FileResponder.Replacers;

// var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();

// var host = builder.Build();
// host.Run();

// TEST //
IReplacer replacer = new PseudoReplacer();
IFormatIO formatIO = new XmlFormatIO();
FileStream templateContent = File.OpenRead("../Examples/template.xml");
FileStream resultContent = File.Create("../Examples/result.xml");
XmlFormatIO XmlFormatIO = new XmlFormatIO();

//formatIO.Format(templateContent, resultContent, replacer);
//Console.WriteLine(result);


//XmlFormatIO.Read(templateContent);


//XmlFormatIO.ReadXml(templateContent, @"C:\Users\marek\Desktop\New Text Document.txt");

// XmlFormatIO.Format(templateContent, resultContent, replacer);

ReplacerMain replacerMain = new ReplacerMain();
string[] placeholders = new string[]
{
    "{{ Random.IntRange(1, 100) }}",                     // Validní: Celé číslo v rozsahu
    "{{ Random.DecimalRange(5.5, 99.99) }}",              // Validní: Desetinné číslo v rozsahu
    "{{ Random.String(20) }}",                            // Validní: Generuje řetězec délky 20
    "{{ Random.Bool() }}",                                // Validní: Generuje bool
    "{{ Math.Add(3, 7) }}",                               // Validní: Sčítání
    "{{ Random.Char() }}",                                // Neimplementované: Generování znaku
    "{{ Random.Bool(1, 0) }}",                            // Neplatné: Bool nebere parametry
    "{{ Math.Subtract(10, 4) }}",                        // Validní: Odečítání
    "{{ Random.IntRange() }}",                            // Neplatné: Bez parametrů
    "{{ Random.NotImplementedMethod() }}",                // Neimplementované: Simulace chyby
    "{{ Math.Divide(10, 2) }}",                           // Validní: Dělení
    "{{ Random.String() }}",                               // Neplatné: Bez parametru
    "{{ Random.IntRange(50, 150) }}",                      // Validní: Celé číslo v jiném rozsahu
    "{{ Random.DecimalRange(-10.5, 10.5) }}",               // Validní: Desetinné číslo v záporném a kladném rozsahu
    "{{ Random.String(15) }}",                              // Validní: Generuje řetězec délky 15
    "{{ Random.Bool() }}",                                  // Validní: Generuje další bool
    "{{ Math.Multiply(4, 5) }}",                            // Validní: Násobení
    "{{ Math.DivideInt(20, 4) }}",                          // Validní: Dělení s celým výsledkem
    "{{ Math.Divide(7, 2) }}",                              // Validní: Dělení s desetiným výsledkem
    "{{ Random.IntRange(-5, 5) }}",                        // Validní: Celé číslo v záporném rozsahu
    "{{ Random.String(-10) }}",                             // Neplatné: Záporná délka řetězce
    "{{ Random.IntRange(100, 50) }}",                       // Neplatné: Min hodnota je větší než max hodnota
    "{{ Math.Subtract(5, 0) }}",                               // Neplatné: Chybějící druhý parametr
    "{{ Random.DecimalRange(0, 10) }}",                      // Validní: Desetinné číslo s nulovým rozsahem
    "{{ Math.Sqrt(-1, 2) }}",                                 // Neplatné: Pokus o odmocninu záporného čísla
    "{{ String.Repeat(\"word\", 5) }}",                      // Validní: Odmocnina kladného čísla
};

int x = 0;

foreach (string placeholder in placeholders)
{
    // Pokusíme se nahradit placeholder a uložit výsledek do proměnné result
    string result = replacerMain.ReplaceValue(placeholder.Trim('{', '{', '}', '}', ' '));

    // Zkontrolujeme, zda je výsledek prázdný
    if (string.IsNullOrEmpty(result))
    {
        Console.WriteLine($"Failed to replace placeholder: {placeholder}");
    }
    else
    {
        System.Console.WriteLine(++x + ". " + result);
    }
}

System.Console.WriteLine("Done");

