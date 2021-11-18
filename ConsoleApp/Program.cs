﻿using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var census = new Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)>()
            {

            };

            int exit = 0, repeat = 0, subchoice = 0;

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

                var choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Stanovništvo:");
                        Console.WriteLine("OIB:\tIme i prezime:\tDatum rođenja:");
                        foreach (var item in census)
                        {
                            Console.WriteLine(item.Key + "\t" + item.Value.nameAndSurname + "\t" + item.Value.dateOfBirth);
                        }
                        break;

                    case 2:
                        Console.WriteLine("Unesi OIB: ");
                        string findOIB = OIB_Length_Check(census);


                        foreach (var item in census)
                        {
                            if (findOIB == item.Key)
                            {
                                Console.WriteLine("Osoba s OIB-om " + findOIB + ":");
                                Console.WriteLine("Ime i prezime:\tDatum rođenja:");
                                Console.WriteLine(item.Value.nameAndSurname + "\t" + item.Value.dateOfBirth);
                            }
                            else
                                Console.WriteLine("Ne postoji osoba s tim OIB-om.");
                        }

                        break;

                    case 3:
                        Console.WriteLine("Unesi ime i prezime: ");
                        string findName = Name_Space_Check(census);

                        Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina, sat:minuta:sekunda):");
                        DateTime findDate = Date_Check(census);

                        foreach (var item in census)
                        {
                            if (findName == item.Value.nameAndSurname && findDate == item.Value.dateOfBirth)
                            {
                                Console.WriteLine("OIB osobe " + findName + " rođene " + findDate + ":");
                                Console.WriteLine(item.Key);
                            }
                            else if (findName != item.Value.nameAndSurname || findDate != item.Value.dateOfBirth)
                                Console.WriteLine("Ne postoji osoba s tim imenom rođena na taj datum.");
                        }

                        break;

                    case 4:
                        Console.WriteLine("Unesi OIB:");
                        string newOIB = "";

                        do
                        {
                            repeat = 0;
                            newOIB = OIB_Length_Check(census);

                            if (census.ContainsKey(newOIB))
                            {
                                Console.WriteLine("Isti OIB već postoji! Unesite ispočetka: ");
                                repeat = 1;
                            }
                        } while (0 != repeat);


                        Console.WriteLine("Unesi ime i prezime:");
                        var nameAndSurname = Name_Space_Check(census);

                        Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina, sat:minuta:sekunda):");
                        DateTime dateOfBirth = Date_Check(census);

                        census.Add(newOIB, (nameAndSurname, dateOfBirth));
                        break;


                    case 5:
                        Console.WriteLine("Unesi OIB:");
                        string deleteOIB = OIB_Length_Check(census);
                        repeat = 1;

                        if (census.ContainsKey(deleteOIB))
                        {
                            repeat = 0;
                        }

                        if (1 == repeat)
                            Console.WriteLine("Ne postoji osoba s tim OIB-om.");

                        foreach (var item in census)
                        {
                            if (deleteOIB == item.Key)
                            {
                                census.Remove(deleteOIB);
                                Console.WriteLine("Osoba " + item.Value.nameAndSurname + " rođena " + item.Value.dateOfBirth + " je obrisana.");
                            }
                        }

                        break;

                    case 6:

                        Console.WriteLine("Unesi ime i prezime: ");
                        string deleteName = Name_Space_Check(census);
                        string OIBToDelete = "";

                        Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina, sat:minuta:sekunda):");
                        DateTime deleteDate = Date_Check(census);

                        int sameNameAndDate = 0;

                        foreach (var item in census)
                        {
                            if (deleteName == item.Value.nameAndSurname && deleteDate == item.Value.dateOfBirth)
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
                                if (deleteName == item.Value.nameAndSurname && deleteDate == item.Value.dateOfBirth)
                                    Console.WriteLine(item.Key);

                            Console.WriteLine("Upiši OIB osobe koju želiš obrisati:");
                            OIBToDelete = (Console.ReadLine());
                            census.Remove(OIBToDelete);
                            Console.WriteLine("Osoba " + deleteName + " rođena " + deleteDate + " s OIB-om " + OIBToDelete + " je obrisana.");
                        }

                        break;

                    case 7:
                        //probat clear
                        foreach (var item in census)
                        {
                            census.Remove(item.Key);
                        }
                        Console.WriteLine("Svi stanovnici su obrisani.");

                        break;


                    case 8:

                        Console.WriteLine("1 - Uredi OIB stanovnika");
                        Console.WriteLine("2 - Uredi ime i prezime stanovnika");
                        Console.WriteLine("3 - Uredi datum rođenja");

                        subchoice = int.Parse(Console.ReadLine());

                        switch (subchoice)
                        {
                            case 1:
                                Console.WriteLine("Unesi OIB koji želiš promijeniti:");
                                string changeOIB = OIB_Length_Check(census);

                                foreach (var item in census)
                                {
                                    if (changeOIB == item.Key) { }
                                    //napravit
                                }

                                break;

                            case 2:
                                Console.WriteLine("Unesi OIB stanovnika kojem želiš promijeniti ime i prezime:");
                                changeOIB = OIB_Length_Check(census);
                                //napravit ovdi i u oibu provjeru jel osoba postoji
                                foreach (var item in census)
                                    if (changeOIB == item.Key)
                                    {
                                        Console.WriteLine("Mijenja se ime i prezime osobe " + item.Value.nameAndSurname + " rođene " + item.Value.dateOfBirth + ".");
                                        Console.WriteLine("Unesi novo ime:");
                                        string newName = Name_Space_Check(census);
                                        census[item.Key] = (newName, item.Value.dateOfBirth);
                                        Console.WriteLine("Ime je promijenjeno u " + newName + ".");
                                    }

                                break;

                            case 3:
                                Console.WriteLine("Unesi OIB stanovnika kojem želiš promijeniti datum rođenja:");
                                changeOIB = OIB_Length_Check(census);

                                foreach (var item in census)
                                    if (changeOIB == item.Key)
                                    {
                                        Console.WriteLine("Mijenja se datum rođenja osobe " + item.Value.nameAndSurname + " rođene " + item.Value.dateOfBirth + ".");
                                        Console.WriteLine("Unesi novi datum rođenja:");
                                        DateTime newDate = Date_Check(census);
                                        census[item.Key] = (item.Value.nameAndSurname, newDate);
                                        Console.WriteLine("Datum rođenja je promijenjen u " + newDate + ".");
                                    }

                                break;
                        }

                        break;

                    case 9:

                        Console.WriteLine("1 - Postotak nezaposlenih (od 0 do 23 godine i od 65 do 100 godine) i postotak zaposlenih(od 23 do 65 godine)");
                        Console.WriteLine("2 - Ispis najčešćeg imena i koliko ga stanovnika ima");
                        Console.WriteLine("3 - Ispis najčešćeg prezimena i koliko ga stanovnika ima");
                        Console.WriteLine("4 - Ispis datum na koji je rođen najveći broj ljudi i koji je to datum");
                        Console.WriteLine("5 - Ispis broja ljudi rođenih u svakom od godišnjih doba (poredat godišnja doba s obzirom na broj ljudi rođenih u istim)");
                        Console.WriteLine("6 - Ispis najmlađeg stanovnika");
                        Console.WriteLine("7 - Ispis najstarijeg stanovnika");
                        Console.WriteLine("8 - Prosječan broj godina (na 2 decimale)");
                        Console.WriteLine("9 - Medijan godina");

                        OldestPerson(census);

                        break;

                    case 0:
                        exit = 1;
                        break;
                }
            }
        }

        static string OIB_Length_Check(Dictionary<string, (string, DateTime)> census)
        {

            var repeat = 0;
            var findOIB = "";

            do
            {
                repeat = 0;
                findOIB = (Console.ReadLine());

                if (11 != findOIB.Length)
                {
                    Console.WriteLine("OIB treba biti dug 11 znamenki! Unesite ga opet: ");
                    repeat = 1;
                }
            } while (0 != repeat);

            return findOIB;
        }

        static string Name_Space_Check(Dictionary<string, (string, DateTime)> census)
        {

            string findName = "";
            int repeat = 1;

            do
            {
                repeat = 1;

                findName = (Console.ReadLine());

                foreach (var character in findName)
                {
                    if (' ' == character)
                    {
                        repeat = 0;
                    }
                }

                if (1 == repeat)
                    Console.WriteLine("Pogrešno uneseno ime i prezime! Unesi ga opet: ");

            } while (0 != repeat);

            return findName;
        }

        static DateTime Date_Check(Dictionary<string, (string, DateTime)> census)
        {

            int repeat = 0;
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
            {//dodat prijestupne godine
                TimeSpan age = DateTime.Now - item.Value.Item2;
                if (age.Days > (23 * 365) && age.Days < (65 * 365)) employed++;
                else unemployed++;
            }
            Console.WriteLine("Postotak zaposlenih je " + (float)employed/(census.Count)*100 + "%, a postotak nezaposlenih je " + (float)unemployed / (census.Count) *100 + "%.");
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

        static void YoungestPerson(Dictionary<string, (string, DateTime)> census)
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

            Console.WriteLine("Najmlađi stanovnik je " + YoungestSoFar.Item1 + ", rođen " + YoungestSoFar.Item2 + ".");
        }

        static void OldestPerson(Dictionary<string, (string, DateTime)> census)
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

            Console.WriteLine("Najstariji stanovnik je " + OldestSoFar.Item1 + ", rođen " + OldestSoFar.Item2 + ".");
        }

        static void AverageAge(Dictionary<string, (string, DateTime)> census)
        {
            foreach (var item in census)
            {
                //popodne napravit
            }
        }
    }
}
