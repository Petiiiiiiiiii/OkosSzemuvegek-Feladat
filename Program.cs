using CA231201;

static void Beolvasas(List<OkosSzemuveg> lista)
{
    StreamReader reader = new(@"..\..\..\src\okosszemüvegek.txt");
    _ = reader.ReadLine();
    while (!reader.EndOfStream) lista.Add(new OkosSzemuveg(reader.ReadLine()));
    reader.Close();
}
static List<OkosSzemuveg> Feladat7(List<OkosSzemuveg> lista) 
{
    return lista
        .Where(sz => sz.KameraFelbontas >= 12 && sz.ProcesszorTeljesitmeny == 2)
        .ToList();
}
static List<OkosSzemuveg> Feladat8(List<OkosSzemuveg> lista) 
{
    return lista
        .Where(sz => sz.Uzemido > lista.Average(sz => sz.Uzemido))
        .ToList();
}
static Dictionary<string,double> Feladat10(List<OkosSzemuveg> lista) 
{
    return lista
        .Where(sz => sz.Tarhely < 100)
        .ToDictionary(sz => sz.Sorszam.ToString(), sz => sz.InchToCm());
}
static List<string> Feladat11(List<OkosSzemuveg> lista) 
{
    List<string> szenzorok = lista
        .SelectMany(sz => sz.Szenzorok, (s, szenzor) => new { Sorszam = s.Sorszam, Szenzor = szenzor })
        .DistinctBy(s => s.Szenzor)
        .Select(s => s.Szenzor)
        .ToList();

    for (int i = 0; i < szenzorok.Count; i++)
    {
        if (szenzorok[i] == "gyroscope")
        {
            szenzorok.RemoveAt(i);
        }
        if (szenzorok[i] == "accelerometer")
        {
            szenzorok[i] = "gyorsulásmérő";
        }
    }
    return szenzorok
        .Order()
        .ToList();
}
static List<OkosSzemuveg> Feladat12(List<OkosSzemuveg> lista) 
{
    return lista
        .Where(sz => sz.Tarhely >= 1024)
        .ToList();
}
static void Kiiras(List<OkosSzemuveg> lista) 
{
    StreamWriter writer = new(@"..\..\..\src\feladat13.txt");
    var feladat13 = lista
        .Where(sz => sz.Szenzorok.Count >= 3)
        .ToList();

    feladat13.ForEach(sz => writer.WriteLine(sz));
    writer.Close();
}

List<OkosSzemuveg> szemuvegek = new();

try
{
    Beolvasas(szemuvegek);
}
catch
{
    Console.WriteLine("Hiba a beolvasás során!");
}

//6.feladat
szemuvegek.ForEach(x => Console.WriteLine(x));

//7.feladat
List<OkosSzemuveg> feladat7 = Feladat7(szemuvegek);
Console.WriteLine($"7. feladat: \n\t{feladat7.Count} db \n");

//8.feladat
Console.WriteLine($"8. feladat: \n\t{Feladat8(szemuvegek).Count} db és az átlagos üzemidő: {szemuvegek.Average(sz => sz.Uzemido)} \n");
Feladat8(szemuvegek).ForEach(x => Console.WriteLine(x));

//9.feladat (kész a kért módon)

//10. feladat
Console.WriteLine("\n10. feladat:");
Dictionary<string, double> feladat10 = Feladat10(szemuvegek);
foreach (var szemuveg in Feladat10(szemuvegek))
{
    Console.WriteLine($"\tSorszama: {szemuveg.Key}\tMérete: {szemuveg.Value} cm");
}

//11. feladat
Console.WriteLine("\n11. feladat:");
Feladat11(szemuvegek).ForEach(x => Console.WriteLine($"\t{x}"));

//12. feladat
Console.WriteLine("\n12. feladat:\n");
try
{
    Feladat12(szemuvegek).ForEach(x => Console.WriteLine(x));
}
catch
{
    Console.WriteLine("\tNincs egy ilyen se!");
}

//13. feladat
Console.WriteLine("\n13. feladat:");
try
{
    Kiiras(szemuvegek);
    Console.WriteLine("\tSikeres kiírás!");
}
catch
{
    Console.WriteLine("\tHiba történt a kiírás során!");
}

Console.ReadKey();

