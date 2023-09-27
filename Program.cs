using Pesel;

Console.WriteLine("Enter Name:");
string imie = Console.ReadLine();
Console.WriteLine("Enter Surame:");
string nazwisko = Console.ReadLine();
Console.WriteLine("Enter PESEL:");
string pesel = Console.ReadLine();

Osoba osoba = new Osoba(imie, nazwisko, pesel);