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
                        string findOIB = "";

                        do
                        {
                            repeat = 0;
                            findOIB = (Console.ReadLine());

                            if (11 != findOIB.Length)
                            {
                                Console.WriteLine("OIB treba biti dug 11 znamenki! Unesite ga opet: ");
                                repeat = 1;
                            }

                            if (0 == repeat) {
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
                            }
                        } while (0 != repeat);

                        break;




                    case 3:
                        Console.WriteLine("Unesi ime i prezime: ");
                        string findName = "";

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


                        Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina, sat:minuta:sekunda):");
                        DateTime findDate;

                        do
                        {
                            repeat = 0;
                            findDate = DateTime.Parse(Console.ReadLine());

                            if (findDate > DateTime.Now)
                            {
                                repeat = 1;
                                Console.WriteLine("Unesen je datum koji se još nije dogodio, unesite ispravan datum rođenja: ");
                            }

                        } while (0 != repeat);

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
                            newOIB = (Console.ReadLine());

                            if (census.ContainsKey(newOIB))
                            {
                                Console.WriteLine("Isti OIB već postoji! Unesite ispočetka: ");
                                repeat = 1;
                            }

                            if (11 != newOIB.Length)
                            {
                                Console.WriteLine("OIB treba biti dug 11 znamenki! Unesite ga opet: ");
                                repeat = 1;
                            }
                        } while (0 != repeat);

                        Console.WriteLine("Unesi ime i prezime:");
                        var nameAndSurname = "";

                        do {
                            repeat = 1;

                            nameAndSurname = (Console.ReadLine());

                            foreach (var character in nameAndSurname)
                            {
                                if (' ' == character)
                                {
                                    repeat = 0;
                                }
                            }

                            if(1 == repeat)
                                Console.WriteLine("Pogrešno uneseno ime i prezime! Unesi ga opet: ");

                        } while (0 != repeat);



                        Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina, sat:minuta:sekunda):");
                        DateTime dateOfBirth;

                        do
                        {
                            repeat = 0;
                            dateOfBirth = DateTime.Parse(Console.ReadLine());

                            if(dateOfBirth > DateTime.Now)
                            {
                                repeat = 1;
                                Console.WriteLine("Unesen je datum koji se još nije dogodio, unesite ispravan datum rođenja: ");
                            }

                        } while (0 != repeat);

                        census.Add(newOIB, (nameAndSurname, dateOfBirth));
                        break;


                    case 5:
                        Console.WriteLine("Unesi OIB:");
                        string deleteOIB = "";

                        do
                        {
                            repeat = 1;
                            deleteOIB = (Console.ReadLine());

                            if (census.ContainsKey(deleteOIB))
                            {
                                repeat = 0;
                            }

                            if(1 == repeat)
                                Console.WriteLine("Ne postoji osoba s tim OIB-om.");

                            if (11 != deleteOIB.Length && 0 == repeat)
                            {
                                Console.WriteLine("OIB treba biti dug 11 znamenki! Unesite ga opet: ");
                                repeat = 1;
                            }
                        } while (0 != repeat);

                        foreach (var item in census)
                        {
                            if(deleteOIB == item.Key)
                            {
                                census.Remove(deleteOIB);
                                Console.WriteLine("Osoba " + item.Value.nameAndSurname + " rođena " + item.Value.dateOfBirth + " je obrisana.");
                            }
                        }

                        break;






                    case 6:

                        Console.WriteLine("Unesi ime i prezime: ");
                        string deleteName = "";
                        string OIBToDelete = "";

                        do
                        {
                            repeat = 1;

                            deleteName = (Console.ReadLine());

                            foreach (var character in deleteName)
                            {
                                if (' ' == character)
                                {
                                    repeat = 0;
                                }
                            }

                            if (1 == repeat)
                                Console.WriteLine("Pogrešno uneseno ime i prezime! Unesi ga opet: ");

                        } while (0 != repeat);


                        Console.WriteLine("Unesi datum rođenja (dan, mjesec, godina, sat:minuta:sekunda):");
                        DateTime deleteDate;

                        do
                        {
                            repeat = 0;
                            deleteDate = DateTime.Parse(Console.ReadLine());

                            if (deleteDate > DateTime.Now)
                            {
                                repeat = 1;
                                Console.WriteLine("Unesen je datum koji se još nije dogodio, unesite ispravan datum rođenja: ");
                            }

                        } while (0 != repeat);

                        int sameNameAndDate = 0;

                        foreach (var item in census)
                        {
                            if (deleteName == item.Value.nameAndSurname && deleteDate == item.Value.dateOfBirth)
                            {
                                sameNameAndDate++;
                                OIBToDelete = item.Key;

                                //census.Remove(OIBToDelete);
                                //Console.WriteLine("Osoba " + item.Value.nameAndSurname + " rođena " + item.Value.dateOfBirth + " je obrisana.");
                            }
                            //else if (deleteName != item.Value.nameAndSurname || deleteDate != item.Value.dateOfBirth)
                               // Console.WriteLine("Ne postoji osoba s tim imenom rođena na taj datum.");
                        }

                        if(0 == sameNameAndDate)
                            Console.WriteLine("Ne postoji osoba s tim imenom rođena na taj datum.");
                        else if(1 == sameNameAndDate)
                        {
                            census.Remove(OIBToDelete);
                            Console.WriteLine("Osoba " + deleteName + " rođena " + deleteDate + " je obrisana.");
                        }
                        
                        else if(sameNameAndDate > 1)
                        {
                            //pozvat funkciju za maknit po oibu
                        }

                        break;

                    case 7:

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

                                //dodat funkcije pa onda slozit
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

                        break;

                    case 0:
                        exit = 1;
                        break;
                }
            }
        }
    }
}