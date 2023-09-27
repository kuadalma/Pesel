namespace Pesel
{
    public class Osoba
    {
        private string Name;
        private string Surname;
        private string PESEL;
        private string Sex;
        private DateTime BirthDate;
        public Osoba(string imie, string nazwisko, string pesel)
        {
            Name = imie;
            Surname = nazwisko;
            PESEL = pesel;
            Sex = string.Empty;
            EnterData();
        }
        private void EnterData()
        {
            if (!ValidatePesel()) { Console.WriteLine("Numer PESEL jest nieprawidłowy."); return; }
            int rok = int.Parse(PESEL.Substring(0, 2));
            int miesiac = int.Parse(PESEL.Substring(2, 2));
            int dzien = int.Parse(PESEL.Substring(4, 2));
            if (miesiac < 93 && miesiac > 80) { rok += 1800; miesiac -= 80; }
            else if (miesiac < 13 && miesiac > 0) rok += 1900;
            else if (miesiac < 32 && miesiac > 20) { rok += 2000; miesiac -= 20; }
            else if (miesiac < 52 && miesiac > 41) { rok += 2100; miesiac -= 40; }
            else if (miesiac < 72 && miesiac > 61) { rok += 2200; miesiac -= 60; }
            BirthDate = new DateTime(rok, miesiac, dzien);
            Sex = int.Parse(PESEL.Substring(9, 1)) % 2 == 0 ? "Women" : "Men";
            SaveToFile();
        }
        private void SaveToFile()
        {
            try
            {
                File.AppendAllText("data.txt", $"{Name};{Surname};{PESEL};{BirthDate.ToShortDateString()};{Sex}" + Environment.NewLine);
                Console.WriteLine("Saved");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Failed to save ({ex.Message})");
            }
        }
        private bool ValidatePesel()
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };
            int suma = 0;
            if (PESEL.Length != 11) return false;
            for (int i = 0; i < 10; i++)
            {
                if (!char.IsDigit(PESEL[i])) return false;
                suma += int.Parse(PESEL[i].ToString()) * weights[i];
            }
            return ((suma % 10 == 0) ? 0 : 10 - suma % 10) == int.Parse(PESEL[10].ToString());
        }
    }
}