string sciezkaDoPliku = @"C:\Users\karol\Desktop\test.txt";
string[] katakana_tablica = File.ReadAllLines(sciezkaDoPliku);
int signLength = 64;
List<string> katakana64 = new List<string>();
string longString = "";
foreach (string sign in katakana_tablica)
{
    if (!string.IsNullOrWhiteSpace(sign))
    {
        longString += sign;
    }
}

for (int i = 0; i < longString.Length; i += signLength)
{
    string fragment64 = longString.Substring(i, signLength);
    katakana64.Add(fragment64);
}

foreach (string sign in katakana64)
{
    Console.WriteLine(sign);
}
