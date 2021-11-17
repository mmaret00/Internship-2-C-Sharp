using System;
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

            int exit = 0, repeat = 0; ;

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



                    case 0:
                        exit = 1;
                        break;
                }
            }
        }
    }
}
