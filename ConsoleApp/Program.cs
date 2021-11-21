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

            census.Add("44444444444", ("Ante Antimon", new DateTime(1980, 11, 1)));
            census.Add("22222222222", ("Ivan Horvat", new DateTime(2007, 10, 3)));
            census.Add("33333333333", ("Ivana Horvat", new DateTime(1940, 10, 10)));
            census.Add("33333333334", ("Šime Šimić", new DateTime(2007, 1, 3)));
            census.Add("54644944448", ("Ivan Ivanović", new DateTime(1990, 11, 4)));
            census.Add("44444944448", ("Petra Horvat", new DateTime(1995, 11, 11)));
            census.Add("44444644445", ("Jan Mlakar", new DateTime(1980, 8, 31)));
            census.Add("94444644445", ("N'Golo Kanté", new DateTime(1980, 8, 31)));
            census.Add("44444744445", ("Enver Hoxha", new DateTime(1950, 10, 3)));
            census.Add("44444844445", ("Ivan Horvat", new DateTime(2007, 10, 3)));
            census.Add("74444944445", ("Björk Guðdóttir", new DateTime(1995, 8, 7)));
            census.Add("14444944448", ("José Quiñones", new DateTime(1985, 4, 7)));
            census.Add("54444944448", ("Ryūnosuke Shōji", new DateTime(1985, 4, 7)));
            census.Add("44444944548", ("D'Angelo Danté", new DateTime(1985, 4, 7)));

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

                int.TryParse(Console.ReadLine().Trim(), out choice);

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
                        Delete_Everyone(census);
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

        static string OIB_Check()
        {
            int repeat;
            string findOIB;

            do
            {
                repeat = 0;
                findOIB = (Console.ReadLine().Trim());

                if (11 != findOIB.Length)
                {
                    Console.WriteLine("OIB treba biti dug 11 znamenki! Unesite ga opet:");
                    repeat = 1;
                }

                else if(!OIB_Digits_Check(findOIB))
                {
                    Console.WriteLine("OIB treba sadržavati samo brojeve! Unesite ga opet:");
                    repeat = 1;
                }

            } while (0 != repeat);

            return findOIB;
        }

        static bool OIB_Digits_Check(string OIB)
        {
            foreach (var item in OIB)
                if (item < '0' || item > '9')
                    return false;

            return true;
        }

        static string Name_Check()
        {
            string findName;
            int repeat;

            do
            {
                repeat = 0;
                findName = (Console.ReadLine()).Trim();

                if (!Name_Letters_Check(findName)) { 
                    Console.WriteLine("Ime mora sadržavati isključivo latinična slova i razmake! Unesite ga ispočetka:");
                    repeat = 1;
                }

            } while (0 != repeat);

            return findName;
        }

        static bool Name_Letters_Check(string name)
        {
            int oneWord = 0;

            foreach (var item in name)
                if (!char.IsLetter(item) && item != ' ' && item != '\'')
                    return false;

            foreach (var item in name)
                if (' ' == item)
                    oneWord = 1;

            if (0 == oneWord) return false;

            return true;
        }

        static DateTime Date_Check()
        {
            int repeat;
            DateTime dateOfBirth;

            do
            {
                repeat = 0;
                dateOfBirth = DateTime.Parse(Console.ReadLine().Trim());

                if (dateOfBirth > DateTime.Now)
                {
                    repeat = 1;
                    Console.WriteLine("Unesen je datum koji se još nije dogodio, unesite ispravan datum rođenja: ");
                }

            } while (0 != repeat);

            return dateOfBirth.Date;
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

                int.TryParse(Console.ReadLine().Trim(), out subchoice);

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
            TimeSpan age;

            Console.WriteLine("Stanovništvo:");
            Console.WriteLine("OIB:\t\tIme i prezime:\t\tDatum rođenja:");
            foreach (var item in census)
            {
                age = DateTime.Now - item.Value.Item2;
                if (age.Days > (23 * 365.25) && age.Days < (65 * 365.25))
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (age.Days < (23 * 365.25) || age.Days > (65 * 365.25))
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(item.Key + "\t" + item.Value.Item1 + "\t\t" + item.Value.Item2.ToString("dd.MM.yyyy."));
                Console.ResetColor();
            }
        }

        static void Print_Population_Ascending(Dictionary<string, (string, DateTime)> census)
        {
            TimeSpan age;

            List<(string, string, DateTime)> sortedList = Transfer_to_List(census);
            sortedList.Sort((a, b) => a.Item3.CompareTo(b.Item3));

            Console.WriteLine("Stanovništvo:");
            Console.WriteLine("OIB:\t\tIme i prezime:\t\tDatum rođenja:");
            foreach (var item in sortedList)
            {
                age = DateTime.Now - item.Item3;
                if (age.Days > (23 * 365.25) && age.Days < (65 * 365.25)) Console.ForegroundColor = ConsoleColor.Green;
                else if (age.Days < (23 * 365.25) || age.Days > (65 * 365.25)) Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(item.Item1 + "\t" + item.Item2 + "\t\t" + item.Item3.ToString("dd.MM.yyyy."));
                Console.ResetColor();
            }
        }

        static void Print_Population_Descending(Dictionary<string, (string, DateTime)> census)
        {
            TimeSpan age;

            List<(string, string, DateTime)> sortedList = Transfer_to_List(census);
            sortedList.Sort((a, b) => b.Item3.CompareTo(a.Item3));

            Console.WriteLine("Stanovništvo:");
            Console.WriteLine("OIB:\t\tIme i prezime:\t\tDatum rođenja:");
            foreach (var item in sortedList)
            {
                age = DateTime.Now - item.Item3;
                if (age.Days > (23 * 365.25) && age.Days < (65 * 365.25)) Console.ForegroundColor = ConsoleColor.Green;
                else if (age.Days < (23 * 365.25) || age.Days > (65 * 365.25)) Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(item.Item1 + "\t" + item.Item2 + "\t\t" + item.Item3.ToString("dd.MM.yyyy."));
                Console.ResetColor();
            }
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
            string findOIB = OIB_Check();
            int foundIt = 0;

            foreach (var item in census)
            {
                if (findOIB == item.Key)
                {
                    Console.WriteLine("Stanovnik s OIB-om " + findOIB + ":");
                    Console.WriteLine("Ime i prezime:\tDatum rođenja:");
                    Console.WriteLine(item.Value.Item1 + "\t" + item.Value.Item2.ToString("dd.MM.yyyy."));
                    foundIt++;
                }
            }
            if(0 == foundIt)
                Console.WriteLine("Ne postoji stanovnik s tim OIB-om.");
        }

        static void Print_Person_Name_Date(Dictionary<string, (string, DateTime)> census)
        {
            int foundIt = 0;
            Console.WriteLine("Unesi ime i prezime: ");
            string findName = Name_Check();

            Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina):");
            DateTime findDate = Date_Check();

            foreach (var item in census)
            {
                if (findName == item.Value.Item1 && findDate == item.Value.Item2)
                {
                    Console.WriteLine("OIB osobe " + findName + " rođene " + findDate.ToString("dd.MM.yyyy.") + ":");
                    Console.WriteLine(item.Key);
                    foundIt++;
                }
            }
           if (0 == foundIt)
                Console.WriteLine("Ne postoji stanovnik s tim imenom rođen na taj datum.");
        }

        static void New_Entry(Dictionary<string, (string, DateTime)> census)
        {
            int correct = 0;
            while (0 == correct)
            {
                Console.WriteLine("Jeste li sigurni da želite unijeti novog stanovnika?");
                Console.WriteLine("(da/ne)");
                var confirmation = Console.ReadLine().Trim();

                if ("da" == confirmation)
                {
                    correct = 1;
                    Console.WriteLine("Unesi OIB:");
                    string newOIB;
                    int repeat;

                    do
                    {
                        repeat = 0;
                        newOIB = OIB_Check();

                        if (census.ContainsKey(newOIB))
                        {
                            Console.WriteLine("Isti OIB već postoji! Unesite ispočetka: ");
                            repeat = 1;
                        }
                    } while (0 != repeat);

                    Console.WriteLine("Unesi ime i prezime:");
                    var nameAndSurname = Name_Check();

                    Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina):");
                    DateTime dateOfBirth = Date_Check();

                    census.Add(newOIB, (nameAndSurname, dateOfBirth));
                    Console.WriteLine("Uspješno unesen stanovnik " + nameAndSurname + " s OIB-om " + newOIB + " rođen " + dateOfBirth.ToString("dd.MM.yyyy."));
                }
                else if ("ne" == confirmation)
                {
                    correct = 1;
                    Console.WriteLine("Povratak na glavni izbornik.");
                }
                else
                    Console.WriteLine("Nepravilan unos, molimo ponovite (unesite 'da' ili 'ne'):");
            }
        }


        static void Delete_OIB(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi OIB:");
            string deleteOIB = OIB_Check();
            string confirmation;
            int repeat = 1, correct = 0;

            if (census.ContainsKey(deleteOIB))
                repeat = 0;

            if (1 == repeat)
                Console.WriteLine("Ne postoji stanovnik s tim OIB-om.");

            foreach (var item in census)
            {
                if (deleteOIB == item.Key)
                {
                    while (0 == correct)
                    {
                        Console.WriteLine("Jeste li sigurni da želite obrisati stanovnika " + item.Value.Item1 + " rođenog " + item.Value.Item2.ToString("dd.MM.yyyy.") + "?");
                        Console.WriteLine("(da/ne)");
                        confirmation = Console.ReadLine().Trim();
                        if ("da" == confirmation)
                        {
                            correct = 1;
                            census.Remove(deleteOIB);
                            Console.WriteLine("Stanovnik " + item.Value.Item1 + " rođen " + item.Value.Item2.ToString("dd.MM.yyyy.") + " je obrisan.");
                        }
                        else if ("ne" == confirmation)
                        {
                            correct = 1;
                            Console.WriteLine("Povratak na glavni izbornik.");
                        }
                        else
                            Console.WriteLine("Nepravilan unos, molimo ponovite (unesite 'da' ili 'ne'):");
                    }
                }
            }
        }

        static void Delete_Name_Date(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi ime i prezime: ");
            string deleteName = Name_Check();
            string OIBToDelete = "";
            string confirmation;
            int correct = 0;

            Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina):");
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
                Console.WriteLine("Ne postoji stanovnik s tim imenom rođena na taj datum.");

            else if (1 == sameNameAndDate)
            {
                while (0 == correct)
                {
                    Console.WriteLine("Jeste li sigurni da želite obrisati stanovnika " + deleteName + " rođenog " + deleteDate.ToString("dd.MM.yyyy.") + "?");
                    Console.WriteLine("(da/ne)");
                    confirmation = Console.ReadLine().Trim();
                    if ("da" == confirmation)
                    {
                        correct = 1;
                        census.Remove(OIBToDelete);
                        Console.WriteLine("Stanovnik " + deleteName + " rođen " + deleteDate.ToString("dd.MM.yyyy.") + " je obrisan.");
                    }
                    else if ("ne" == confirmation)
                    {
                        correct = 1;
                        Console.WriteLine("Povratak na glavni izbornik.");
                    }
                    else
                        Console.WriteLine("Nepravilan unos, molimo ponovite (unesite 'da' ili 'ne'):");
                }
            }

            else if (sameNameAndDate > 1)
            {
                Console.WriteLine("Postoji više ljudi s tim imenom rođeni na taj dan. Njihovi OIB-i su:");
                foreach (var item in census)
                    if (deleteName == item.Value.Item1 && deleteDate == item.Value.Item2)
                        Console.WriteLine(item.Key);

                Console.WriteLine("Upiši OIB osobe koju želiš obrisati:");
                OIBToDelete = (Console.ReadLine().Trim());

                while (0 == correct)
                {
                    Console.WriteLine("Jeste li sigurni da želite obrisati stanovnika " + deleteName + " rođenog " + deleteDate.ToString("dd.MM.yyyy.") + "?");
                    Console.WriteLine("(da/ne)");
                    confirmation = Console.ReadLine().Trim();
                    if ("da" == confirmation)
                    {
                        correct = 1;
                        census.Remove(OIBToDelete);
                        Console.WriteLine("Stanovnik " + deleteName + " rođen " + deleteDate.ToString("dd.MM.yyyy.") + " je obrisan.");
                    }
                    else if ("ne" == confirmation)
                    {
                        correct = 1;
                        Console.WriteLine("Povratak na glavni izbornik.");
                    }
                    else
                        Console.WriteLine("Nepravilan unos, molimo ponovite (unesite 'da' ili 'ne'):");
                }
            }
        }

        static void Delete_Everyone(Dictionary<string, (string, DateTime)> census)
        {
            int correct = 0;

            while (0 == correct)
            {
                Console.WriteLine("Jeste li sigurni da želite obrisati cijeli popis stanovništva??");
                Console.WriteLine("(da/ne)");
                string confirmation = Console.ReadLine().Trim();
                if ("da" == confirmation)
                {

                    census.Clear();
                    Console.WriteLine("Svi stanovnici su obrisani.");
                    correct = 1;
                }
                else if ("ne" == confirmation)
                {
                    Console.WriteLine("Povratak na glavni izbornik.");
                    correct = 1;
                }
                else
                    Console.WriteLine("Nepravilan unos, molimo ponovite (unesite 'da' ili 'ne'):");
            }
        }

        static void Change_Person(Dictionary<string, (string, DateTime)> census)
        {
            int subchoice;

            Console.WriteLine("1 - Uredi OIB stanovnika");
            Console.WriteLine("2 - Uredi ime i prezime stanovnika");
            Console.WriteLine("3 - Uredi datum rođenja");
            Console.WriteLine("0 - Povratak na glavni izbornik");

            int.TryParse(Console.ReadLine().Trim(), out subchoice);

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
                    case 0:
                        break;
                    default:
                        break;
                }
        }

        static void Change_Person_OIB(Dictionary<string, (string, DateTime)> census)
        {
            int changeIt = 0, correct = 0, repeat = 0, verified = 0;
            string newOIB = "";
            string tempName = "";
            DateTime tempDate = new DateTime();
            string confirmation = "";

            Console.WriteLine("Unesi OIB koji želiš promijeniti:");
            string changeOIB = OIB_Check();

            if (census.ContainsKey(changeOIB))
                foreach (var item in census)
                    if (changeOIB == item.Key)
                    {
                        while (0 == correct)
                        {
                            if (0 == verified)
                            {
                                Console.WriteLine("Jeste li sigurni da želite promijeniti OIB stanovnika " + item.Value.Item1 + " rođenog " + item.Value.Item2.ToString("dd.MM.yyyy.") + "?");
                                Console.WriteLine("(da/ne)");
                                confirmation = Console.ReadLine().Trim();
                            }
                            if ("da" == confirmation)
                            {
                                correct = 1;
                                changeIt = 1;
                                verified = 1;
                                repeat = 0;
                                Console.WriteLine("Unesi novi OIB:");
                                newOIB = OIB_Check();

                                while (0 == repeat)
                                {
                                    if (census.ContainsKey(newOIB))
                                    {
                                        Console.WriteLine("Isti OIB već postoji! Unesite ispočetka: ");
                                        correct = 0;
                                        repeat = 1;
                                    }
                                    else repeat = 1;
                                }

                                tempName = item.Value.Item1;
                                tempDate = item.Value.Item2;
                            }
                            else if ("ne" == confirmation)
                            {
                                correct = 1;
                                changeIt = 2;
                                verified = 1;
                            }
                            else
                                Console.WriteLine("Nepravilan unos, molimo ponovite (unesite 'da' ili 'ne'):");
                        }
                    }

            if (1 == changeIt)
            {
                census.Remove(changeOIB);
                census.Add(newOIB, (tempName, tempDate));
                Console.WriteLine("OIB je promijenjen.");
            }
            else if (2 == changeIt) Console.WriteLine("Povratak na glavni izbornik.");
            else Console.WriteLine("Ne postoji stanovnik s tim OIB-om.");
        }

        static void Change_Person_Name(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi OIB stanovnika kojem želiš promijeniti ime i prezime:");
            string changeOIB = OIB_Check();
            string confirmation;
            int correct = 0;

            if (census.ContainsKey(changeOIB))
            {
                foreach (var item in census)
                    if (changeOIB == item.Key)
                    {
                        while (0 == correct)
                        {
                            Console.WriteLine("Jeste li sigurni da želite promijeniti ime stanovnika " + item.Value.Item1 + " rođenog " + item.Value.Item2.ToString("dd.MM.yyyy.") + "?");
                            Console.WriteLine("(da/ne)");
                            confirmation = Console.ReadLine().Trim();
                            if ("da" == confirmation)
                            {
                                correct = 1;
                                Console.WriteLine("Unesi novo ime:");
                                string newName = Name_Check();
                                census[item.Key] = (newName, item.Value.Item2);
                                Console.WriteLine("Ime je promijenjeno u " + newName + ".");
                            }
                            else if ("ne" == confirmation)
                            {
                                correct = 1;
                                Console.WriteLine("Povratak na glavni izbornik.");
                            }
                            else
                                Console.WriteLine("Nepravilan unos, molimo ponovite (unesite 'da' ili 'ne'):");
                        }
                    }
            }
            else Console.WriteLine("Ne postoji stanovnik s tim OIB-om.");
        }

        static void Change_Person_Date(Dictionary<string, (string, DateTime)> census)
        {
            Console.WriteLine("Unesi OIB stanovnika kojem želiš promijeniti datum rođenja:");
            string changeOIB = OIB_Check();
            string confirmation;
            int correct = 0;

            if (census.ContainsKey(changeOIB))
            {
                foreach (var item in census)
                    if (changeOIB == item.Key)
                    {
                        while (0 == correct)
                        {
                            Console.WriteLine("Jeste li sigurni da želite promijeniti datum rođenja stanovnika " + item.Value.Item1 + " rođenog " + item.Value.Item2.ToString("dd.MM.yyyy.") + "?");
                            Console.WriteLine("(da/ne)");
                            confirmation = Console.ReadLine().Trim();
                            if ("da" == confirmation)
                            {
                                correct = 1;
                                Console.WriteLine("Unesi novi datum rođenja:");
                                DateTime newDate = Date_Check();
                                census[item.Key] = (item.Value.Item1, newDate);
                                Console.WriteLine("Datum rođenja je promijenjen u " + newDate.ToString("dd.MM.yyyy.") + ".");
                            }
                            else if ("ne" == confirmation)
                            {
                                correct = 1;
                                Console.WriteLine("Povratak na glavni izbornik.");
                            }
                            else
                                Console.WriteLine("Nepravilan unos, molimo ponovite (unesite 'da' ili 'ne'):");
                        }
                    }
            }
            else Console.WriteLine("Ne postoji stanovnik s tim OIB-om.");
        }

        static void Statistics(Dictionary<string, (string, DateTime)> census)
        {
            int exit = 0, subchoice;

            while(0 == exit)
            {
                Console.WriteLine("\n1 - Postotak nezaposlenih i zaposlenih");
                Console.WriteLine("2 - Ispis najčešćeg imena i koliko ga stanovnika ima");
                Console.WriteLine("3 - Ispis najčešćeg prezimena i koliko ga stanovnika ima");
                Console.WriteLine("4 - Ispis datum na koji je rođen najveći broj ljudi i koji je to datum");
                Console.WriteLine("5 - Ispis broja ljudi rođenih u svakom od godišnjih doba");
                Console.WriteLine("6 - Ispis najmlađeg stanovnika");
                Console.WriteLine("7 - Ispis najstarijeg stanovnika");
                Console.WriteLine("8 - Prosječan broj godina (na 2 decimale)");
                Console.WriteLine("9 - Medijan godina");
                Console.WriteLine("0 - Povratak na glavni izbornik");

                int.TryParse(Console.ReadLine().Trim(), out subchoice);

                switch (subchoice)
                {
                    case 1:
                        Unemployment(census);
                        break;
                    case 2:
                        Most_Common_Name(census, 0);
                        break;
                    case 3:
                        Most_Common_Name(census, 1);
                        break;
                    case 4:
                        Most_Common_Date(census);
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
            Console.WriteLine("\nPostotak zaposlenih je " + employedRatio * 100 + "%, a nezaposlenih " + (1 - employedRatio) * 100 + "%.");
        }

        static void Most_Common_Name(Dictionary<string, (string, DateTime)> census, int choice)
        {
            var namesDictionary = new Dictionary<string, int>() { };
            string name = "";
            int count;

            foreach (var item in census)
            {
                if (0 == choice) name = item.Value.Item1.Substring(0, item.Value.Item1.IndexOf(" "));
                if (1 == choice) name = item.Value.Item1.Substring(item.Value.Item1.IndexOf(" ") + 1);

                if (namesDictionary.ContainsKey(name))
                {
                    if (namesDictionary.TryGetValue(name, out count))
                        namesDictionary[name] = count + 1;
                }
                else
                    namesDictionary.Add(name, 1);
            }

            List<(string, int)> SortedNames = new List<(string, int)>();

            foreach (var item in namesDictionary)
                SortedNames.Add((item.Key, item.Value));

            SortedNames.Sort((a, b) => b.Item2.CompareTo(a.Item2));

            if (0 == choice) Console.WriteLine("\nNajčešće ime je " + SortedNames[0].Item1 + ", pojavljuje se " + SortedNames[0].Item2 + " puta.");
            if (1 == choice) Console.WriteLine("\nNajčešće prezime je " + SortedNames[0].Item1 + ", pojavljuje se " + SortedNames[0].Item2 + " puta.");
        }

        static void Most_Common_Date(Dictionary<string, (string, DateTime)> census)
        {
            var datesDictionary = new Dictionary<DateTime, int>() { };
            DateTime birthDate;
            int count;

            foreach (var item in census)
            {
                birthDate = item.Value.Item2.Date;

                if (datesDictionary.ContainsKey(birthDate))
                {
                    if (datesDictionary.TryGetValue(birthDate, out count))
                        datesDictionary[birthDate] = count + 1;
                }
                else
                    datesDictionary.Add(birthDate, 1);
            }

            List<(DateTime, int)> SortedDates = new List<(DateTime, int)>();

            foreach (var item in datesDictionary)
                SortedDates.Add((item.Key, item.Value));

            SortedDates.Sort((a, b) => b.Item2.CompareTo(a.Item2));

            Console.WriteLine("\nNajčešći datum rođenja je " + SortedDates[0].Item1.ToString("dd.MM.yyyy.") + ", na njega je rođeno " + SortedDates[0].Item2 + " stanovnika.");
        }

        static void Seasons(Dictionary<string, (string, DateTime)> census)
        {
            int winter = 0, spring = 0, summer = 0, autumn = 0;

            foreach (var item in census)
            {
                var birthDate = (float)item.Value.Item2.Month + item.Value.Item2.Day / 100f;
                if (birthDate >= 12.21 || birthDate < 3.21) winter++;
                else if (birthDate >= 3.21 && birthDate < 6.21) spring++;
                else if (birthDate >= 6.21 && birthDate < 9.23) summer++;
                else if (birthDate >= 9.23 && birthDate < 12.21) autumn++;
            }

            Console.WriteLine("");
            List<(string, int)> SeasonsList = new List<(string, int)>();

            SeasonsList.Add(("Zima", winter));
            SeasonsList.Add(("Proljeće", spring));
            SeasonsList.Add(("Ljeto", summer));
            SeasonsList.Add(("Jesen", autumn));

            SeasonsList.Sort((a, b) => b.Item2.CompareTo(a.Item2));

            foreach (var item in SeasonsList)
            {
                Console.WriteLine(item.Item1 + ": rođeno " + item.Item2 + " stanovnika.");
            }
        }

        static void Youngest_Person(Dictionary<string, (string, DateTime)> census)
        {
            var minDays = 200 * 365.25;
            var YoungestSoFar = ("", "", DateTime.Now);

            foreach (var item in census)
            {
                TimeSpan age = DateTime.Now - item.Value.Item2;
                if (age.Days < minDays)
                {
                    minDays = age.Days;
                    YoungestSoFar = (item.Key, item.Value.Item1, item.Value.Item2);
                }
            }

            Console.WriteLine("\nNajmlađi stanovnik je " + YoungestSoFar.Item2 + " (" + YoungestSoFar.Item1 + ")" + ", rođen " + YoungestSoFar.Now.ToString("dd.MM.yyyy") + ".");
        }

        static void Oldest_Person(Dictionary<string, (string, DateTime)> census)
        {
            var maxDays = 0;
            var OldestSoFar = ("", "", DateTime.Now);

            foreach (var item in census)
            {
                TimeSpan age = DateTime.Now - item.Value.Item2;
                if (age.Days > maxDays)
                {
                    maxDays = age.Days;
                    OldestSoFar = (item.Key, item.Value.Item1, item.Value.Item2);
                }
            }

            Console.WriteLine("\nNajstariji stanovnik je " + OldestSoFar.Item2 + " (" + OldestSoFar.Item1 + ")" + ", rođen " + OldestSoFar.Now.ToString("dd.MM.yyyy") + ".");
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
            Console.WriteLine("\nProsjek godina stanovništva je " + 100 * yearsAvg / 100 + " godina.");
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

            Console.WriteLine("\nMedijan godina stanovništva je " + medianAge + " godina.");
        }
    }
}