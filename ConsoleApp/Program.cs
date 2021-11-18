using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var census = new Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)>(){};
            int exit = 0, choice;

            while (1 != exit)
            {

                Console.WriteLine("\nOdaberite akciju:");
                Console.WriteLine("1 - Ispis stanovništva");
                Console.WriteLine("2 - Ispis stanovnika po OIB-u");
                Console.WriteLine("3 - Ispis OIB-a po unosu imena i prezimena te datuma rođenja");
                Console.WriteLine("4 - Unos novog stanovnika");
                Console.WriteLine("5 - Brisanje stanovnika unosom OIB-a");
                Console.WriteLine("6 - Brisanje stanovnika po imenu i prezimenu te datumu rođenja");
                Console.WriteLine("7 - Brisanje svih stanovnika");
                Console.WriteLine("8 - Uređivanje stanovnika");
                Console.WriteLine("9 - Statistika");
                Console.WriteLine("0 - Izlaz iz aplikacije\n");

                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        Print_Population(census);
                        break;
                    case 2:
                        Print_Person_OIB(census);
                        break;
                    case 3:
                        Print_Person_Name_Date(census);
                        break;
                    case 4:
                        New_Entry(census);
                        break;
                    case 5:
                        Delete_OIB(census);
                        break;
                    case 6:
                        Delete_Name_Date(census);
                        break;
                    case 7:
                        census.Clear();
                        Console.WriteLine("Svi stanovnici su obrisani.");
                        break;
                    case 8:
                        Change_Person(census);
                        break;
                    case 9:
                        Statistics(census);
                        break;
                    case 0:
                        exit = 1;
                        break;
                    default:
                        break;
                }
            }
        }

        static string OIB_Length_Check()
        {
            int repeat;
            string findOIB;

            do
            {
                repeat = 0;
                findOIB = (Console.ReadLine());

                if (11 != findOIB.Length)
                {
                    Console.WriteLine("OIB treba biti dug 11 znamenki! Unesite ga opet:");
                    repeat = 1;
                }
            } while (0 != repeat);

            return findOIB;
        }

        static string Name_Space_Check()
        {
            string findName;
            int repeat;

            do
            {
                repeat = 1;
                findName = (Console.ReadLine());

                foreach (var character in findName)
                    if (' ' == character)
                        repeat = 0;

                if (1 == repeat)
                    Console.WriteLine("Pogrešno uneseno ime i prezime! Unesi ga opet: ");

            } while (0 != repeat);

            return findName;
        }

        static DateTime Date_Check()
        {
            int repeat;
            DateTime dateOfBirth;

            do
            {
                repeat = 0;
                dateOfBirth = DateTime.Parse(Console.ReadLine());

                if (dateOfBirth > DateTime.Now)
                {
                    repeat = 1;
                    Console.WriteLine("Unesen je datum koji se još nije dogodio, unesite ispravan datum rođenja: ");
                }

            } while (0 != repeat);

            return dateOfBirth;
        }

        static void Unemployment(Dictionary<string, (string, DateTime)> census)
        {
            int unemployed = 0, employed = 0;

            foreach (var item in census)
            {
                TimeSpan age = DateTime.Now - item.Value.Item2;
                if (age.Days > (23 * 365.25) && age.Days < (65 * 365.25)) employed++;
                else unemployed++;
            }
            double employedRatio = Math.Round((double)employed / census.Count, 4);
            Console.WriteLine("Postotak zaposlenih je " + employedRatio * 100 + "%, a nezaposlenih " + (1 - employedRatio) * 100 + "%.");
        }

        static void Seasons(Dictionary<string, (string, DateTime)> census)
        {
            int winter = 0, spring = 0, summer = 0, autumn = 0;

            //nista ne valja, popravit
            foreach (var item in census)
            {
                var birthDate = (float)item.Value.Item2.Month + item.Value.Item2.Day / 100f;
                if (birthDate >= 12.21 || birthDate < 3.21) winter++;
                else if (birthDate >= 3.21 || birthDate < 6.21) spring++;
                else if (birthDate >= 6.21 || birthDate < 9.23) summer++;
                else if (birthDate >= 9.23 || birthDate < 12.21) autumn++;

            }
        }

        static void Youngest_Person(Dictionary<string, (string, DateTime)> census)
        {//mozda dodat OIB ovdi i kod najstarijih
            var minDays = 200 * 365; // vidit kako bolje
            var YoungestSoFar = ("", DateTime.Now);

            foreach (var item in census)
            {
                TimeSpan age = DateTime.Now - item.Value.Item2;
                if (age.Days < minDays)
                {
                    minDays = age.Days;
                    YoungestSoFar = (item.Value.Item1, item.Value.Item2);
                }
            }

            Console.WriteLine("\nNajmlađi stanovnik je " + YoungestSoFar.Item1 + ", rođen " + YoungestSoFar.Now + ".\n");
        }

        static void Oldest_Person(Dictionary<string, (string, DateTime)> census)
        {
            var maxDays = 0; // vidit kako bolje
            var OldestSoFar = ("", DateTime.Now);

            foreach (var item in census)
            {
                TimeSpan age = DateTime.Now - item.Value.Item2;
                if (age.Days > maxDays)
                {
                    maxDays = age.Days;
                    OldestSoFar = (item.Value.Item1, item.Value.Item2);
                }
            }

            Console.WriteLine("\nNajstariji stanovnik je " + OldestSoFar.Item1 + ", rođen " + OldestSoFar.Now + ".\n");
        }

        static void Average_Age(Dictionary<string, (string, DateTime)> census)
        {
            int daysOldTotal = 0;
            TimeSpan age;

            foreach (var item in census)
            {
                age = DateTime.Now - item.Value.Item2;
                daysOldTotal += age.Days;
            }

            var yearsAvg = Math.Round((double)(daysOldTotal / census.Count) / 365.25, 2);
            Console.WriteLine("Prosjek godina stanovništva je " + 100 * yearsAvg / 100 + " godina.");
        }

        static void Median_Age(Dictionary<string, (string, DateTime)> census)
        {
            List<(string, string, DateTime)> sortedList = Transfer_to_List(census);
            sortedList.Sort((a, b) => a.Item3.CompareTo(b.Item3));
            TimeSpan age = DateTime.Now - sortedList[sortedList.Count / 2].Item3;
            TimeSpan age2 = DateTime.Now - sortedList[(sortedList.Count / 2) - 1].Item3;
            double medianAge;

            if (0 != sortedList.Count % 2)
                medianAge = Math.Round(age.Days / 365.25, 2);
            else
                medianAge = Math.Round((age.Days + age2.Days) / 2 / 365.25, 2);

            Console.WriteLine("Medijan godina stanovništva je " + medianAge + ".");
        }

        static void Print_Population(Dictionary<string, (string, DateTime)> census)
        {
            int exit = 0, subchoice;

            while (0 == exit)
            {
                Console.WriteLine("\n1 - Ispis stanovništva onako kako su spremljeni");
                Console.WriteLine("2 - Ispis stanovništva po datumu rođenja uzlazno");
                Console.WriteLine("3 - Ispis stanovništva po datumu rođenja silazno");
                Console.WriteLine("0 - Povratak na glavni izbornik");

                int.TryParse(Console.ReadLine(), out subchoice);

                switch (subchoice)
                {
                    case 1:
                        Print_Population_Saved(census);
                        break;
                    case 2:
                        Print_Population_Ascending(census);
                        break;
                    case 3:
                        Print_Population_Descending(census);
                        break;
                    case 0:
                        exit = 1;
                        break;
                    default:
                        break;
                }
            }
        }

        static void Print_Population_Saved(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Stanovništvo:");
            Console.WriteLine("OIB:\tIme i prezime:\tDatum rođenja:");
            foreach (var item in census)
            {
                Console.WriteLine(item.Key + "\t" + item.Value.Item1 + "\t" + item.Value.Item2);
            }
        }

        static void Print_Population_Ascending(Dictionary<string, (string, DateTime)> census)
        {
            List<(string, string, DateTime)> sortedList = Transfer_to_List(census);
            sortedList.Sort((a,b) => a.Item3.CompareTo(b.Item3));

            Console.WriteLine("Stanovništvo:");
            Console.WriteLine("OIB:\tIme i prezime:\tDatum rođenja:");
            foreach (var item in sortedList)
                Console.WriteLine(item.Item1 + "\t" + item.Item2 + "\t" + item.Item3);
        }

        static void Print_Population_Descending(Dictionary<string, (string, DateTime)> census)
        {
            List<(string, string, DateTime)> sortedList = Transfer_to_List(census);
            sortedList.Sort((a,b) => b.Item3.CompareTo(a.Item3));

            Console.WriteLine("Stanovništvo:");
            Console.WriteLine("OIB:\tIme i prezime:\tDatum rođenja:");
            foreach (var item in sortedList)
                Console.WriteLine(item.Item1 + "\t" + item.Item2 + "\t" + item.Item3);
        }

        static List<(string, string, DateTime)> Transfer_to_List(Dictionary<string, (string, DateTime)> census)
        {
            List<(string, string, DateTime)> NewList = new List<(string, string, DateTime)>();

            foreach (var item in census)
                NewList.Add((item.Key, item.Value.Item1, item.Value.Item2));

            return NewList;
        }

        static void Print_Person_OIB(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi OIB:");
            string findOIB = OIB_Length_Check();

            foreach (var item in census)
            {
                if (findOIB == item.Key)
                {
                    Console.WriteLine("Osoba s OIB-om " + findOIB + ":");
                    Console.WriteLine("Ime i prezime:\tDatum rođenja:");
                    Console.WriteLine(item.Value.Item1 + "\t" + item.Value.Item2);
                }
                else
                    Console.WriteLine("Ne postoji osoba s tim OIB-om.");
            }
        }

        static void Print_Person_Name_Date(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi ime i prezime: ");
            string findName = Name_Space_Check();

            Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina, sat:minuta:sekunda):");
            DateTime findDate = Date_Check();

            foreach (var item in census)
            {
                if (findName == item.Value.Item1 && findDate == item.Value.Item2)
                {
                    Console.WriteLine("OIB osobe " + findName + " rođene " + findDate + ":");
                    Console.WriteLine(item.Key);
                }
                else if (findName != item.Value.Item1 || findDate != item.Value.Item2)
                    Console.WriteLine("Ne postoji osoba s tim imenom rođena na taj datum.");
            }
        }

        static void New_Entry(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi OIB:");
            string newOIB;
            int repeat;

            do
            {
                repeat = 0;
                newOIB = OIB_Length_Check();

                if (census.ContainsKey(newOIB))
                {
                    Console.WriteLine("Isti OIB već postoji! Unesite ispočetka: ");
                    repeat = 1;
                }
            } while (0 != repeat);

            Console.WriteLine("Unesi ime i prezime:");
            var nameAndSurname = Name_Space_Check();

            Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina, sat:minuta:sekunda):");
            DateTime dateOfBirth = Date_Check();

            census.Add(newOIB, (nameAndSurname, dateOfBirth));
        }

        static void Delete_OIB(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi OIB:");
            string deleteOIB = OIB_Length_Check();
            int repeat = 1;

            if (census.ContainsKey(deleteOIB))
                repeat = 0;

            if (1 == repeat)
                Console.WriteLine("Ne postoji osoba s tim OIB-om.");

            foreach (var item in census)
            {
                if (deleteOIB == item.Key)
                {
                    census.Remove(deleteOIB);
                    Console.WriteLine("Osoba " + item.Value.Item1 + " rođena " + item.Value.Item2 + " je obrisana.");
                }
            }
        }

        static void Delete_Name_Date(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi ime i prezime: ");
            string deleteName = Name_Space_Check();
            string OIBToDelete = "";

            Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina, sat:minuta:sekunda):");
            DateTime deleteDate = Date_Check();

            int sameNameAndDate = 0;

            foreach (var item in census)
            {
                if (deleteName == item.Value.Item1 && deleteDate == item.Value.Item2)
                {
                    sameNameAndDate++;
                    OIBToDelete = item.Key;
                }
            }

            if (0 == sameNameAndDate)
                Console.WriteLine("Ne postoji osoba s tim imenom rođena na taj datum.");

            else if (1 == sameNameAndDate)
            {
                census.Remove(OIBToDelete);
                Console.WriteLine("Osoba " + deleteName + " rođena " + deleteDate + " je obrisana.");
            }

            else if (sameNameAndDate > 1)
            {
                Console.WriteLine("Postoji više ljudi s tim imenom rođeni na taj dan. Njihovi OIB-i su:");
                foreach (var item in census)
                    if (deleteName == item.Value.Item1 && deleteDate == item.Value.Item2)
                        Console.WriteLine(item.Key);

                Console.WriteLine("Upiši OIB osobe koju želiš obrisati:");
                OIBToDelete = (Console.ReadLine());
                census.Remove(OIBToDelete);
                Console.WriteLine("Osoba " + deleteName + " rođena " + deleteDate + " s OIB-om " + OIBToDelete + " je obrisana.");
            }
        }

        static void Change_Person(Dictionary<string, (string, DateTime)> census)
        {
            int subchoice;

            Console.WriteLine("1 - Uredi OIB stanovnika");
            Console.WriteLine("2 - Uredi ime i prezime stanovnika");
            Console.WriteLine("3 - Uredi datum rođenja");

            int.TryParse(Console.ReadLine(), out subchoice);

            switch (subchoice)
            {
                case 1:
                    Change_Person_OIB(census);
                    break;

                case 2:
                    Change_Person_Name(census);
                    break;

                case 3:
                    Change_Person_Date(census);
                    break;
                default:
                    break;
            }
        }

        static void Change_Person_OIB(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi OIB koji želiš promijeniti:");
            string changeOIB = OIB_Length_Check();
            int changeIt = 0;
            string newOIB = "";
            string tempName = "";
            DateTime tempDate = new DateTime();

            if (census.ContainsKey(changeOIB))
                foreach (var item in census)
                    if (changeOIB == item.Key)
                    {
                        Console.WriteLine("Mijenja se OIB osobe " + item.Value.Item1 + " rođene " + item.Value.Item2 + ".");
                        Console.WriteLine("Unesi novi OIB:");
                        newOIB = OIB_Length_Check();
                        tempName = item.Value.Item1;
                        tempDate = item.Value.Item2;
                        changeIt = 1;
                    }

                    else Console.WriteLine("Ne postoji osoba s tim OIB-om.");

            if (1 == changeIt)
            {
                census.Remove(changeOIB);
                census.Add(newOIB, (tempName, tempDate));
                Console.WriteLine("OIB je promijenjen.");
            }
        }

        static void Change_Person_Name(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi OIB stanovnika kojem želiš promijeniti ime i prezime:");
            string changeOIB = OIB_Length_Check();

            if (census.ContainsKey(changeOIB))
            {
                foreach (var item in census)
                    if (changeOIB == item.Key)
                    {
                        Console.WriteLine("Mijenja se ime i prezime osobe " + item.Value.Item1 + " rođene " + item.Value.Item2 + ".");
                        Console.WriteLine("Unesi novo ime:");
                        string newName = Name_Space_Check();
                        census[item.Key] = (newName, item.Value.Item2);
                        Console.WriteLine("Ime je promijenjeno u " + newName + ".");
                    }
            }
            else Console.WriteLine("Ne postoji osoba s tim OIB-om.");
        }

        static void Change_Person_Date(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi OIB stanovnika kojem želiš promijeniti datum rođenja:");
            string changeOIB = OIB_Length_Check();

            if (census.ContainsKey(changeOIB))
            {
                foreach (var item in census)
                    if (changeOIB == item.Key)
                    {
                        Console.WriteLine("Mijenja se datum rođenja osobe " + item.Value.Item1 + " rođene " + item.Value.Item2 + ".");
                        Console.WriteLine("Unesi novi datum rođenja:");
                        DateTime newDate = Date_Check();
                        census[item.Key] = (item.Value.Item1, newDate);
                        Console.WriteLine("Datum rođenja je promijenjen u " + newDate + ".");
                    }
            }
            else Console.WriteLine("Ne postoji osoba s tim OIB-om.");
        }

        static void Statistics(Dictionary<string, (string, DateTime)> census)
        {
            int exit = 0, subchoice;

            while(0 == exit)
            {
                Console.WriteLine("\n1 - Postotak nezaposlenih (od 0 do 23 godine i od 65 do 100 godine) i postotak zaposlenih(od 23 do 65 godine)");
                Console.WriteLine("2 - Ispis najčešćeg imena i koliko ga stanovnika ima");
                Console.WriteLine("3 - Ispis najčešćeg prezimena i koliko ga stanovnika ima");
                Console.WriteLine("4 - Ispis datum na koji je rođen najveći broj ljudi i koji je to datum");
                Console.WriteLine("5 - Ispis broja ljudi rođenih u svakom od godišnjih doba (poredat godišnja doba s obzirom na broj ljudi rođenih u istim)");
                Console.WriteLine("6 - Ispis najmlađeg stanovnika");
                Console.WriteLine("7 - Ispis najstarijeg stanovnika");
                Console.WriteLine("8 - Prosječan broj godina (na 2 decimale)");
                Console.WriteLine("9 - Medijan godina");
                Console.WriteLine("0 - Povratak na glavni izbornik");

                int.TryParse(Console.ReadLine(), out subchoice);

                switch (subchoice)
                {
                    case 1:
                        Unemployment(census);
                        break;
                    case 5:
                        Seasons(census);
                        break;
                    case 6:
                        Youngest_Person(census);
                        break;
                    case 7:
                        Oldest_Person(census);
                        break;
                    case 8:
                        Average_Age(census);
                        break;
                    case 9:
                        Median_Age(census);
                        break;
                    case 0:
                        exit = 1;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
