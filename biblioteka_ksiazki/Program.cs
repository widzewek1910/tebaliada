using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Ksiegarnia
{
    class Ksiazka
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public int RokWydania { get; set; }
        public string Gatunek { get; set; }
    }

    class Program
    {
        private static List<Ksiazka> ksiazki = new List<Ksiazka>();
        private const string plikJson = "ksiazki.json";

        static void Main(string[] args)
        {
            WczytajKsiazki();

            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj książkę");
                Console.WriteLine("2. Wyświetl listę książek");
                Console.WriteLine("3. Wyświetl dane książki");
                Console.WriteLine("4. Usuń książkę");
                Console.WriteLine("5. Zakończ");

                int opcja;
                if (!int.TryParse(Console.ReadLine(), out opcja))
                {
                    Console.WriteLine("Niepoprawna opcja. Wybierz ponownie.");
                    continue;
                }

                switch (opcja)
                {
                    case 1:
                        DodajKsiazke();
                        break;
                    case 2:
                        WyswietlListeKsiazek();
                        break;
                    case 3:
                        WyswietlDaneKsiazki();
                        break;
                    case 4:
                        UsunKsiazke();
                        break;
                    case 5:
                        ZapiszKsiazki();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Niepoprawna opcja. Wybierz ponownie.");
                        break;
                }
            }
        }

        static void WczytajKsiazki()
        {
            if (File.Exists(plikJson))
            {
                string json = File.ReadAllText(plikJson);
                ksiazki = JsonConvert.DeserializeObject<List<Ksiazka>>(json);
            }
            else
            {
                Console.WriteLine("Nie znaleziono pliku z danymi. Utworzono nowy plik.");
            }
        }

        static void ZapiszKsiazki()
        {
            string json = JsonConvert.SerializeObject(ksiazki, Formatting.Indented);
            File.WriteAllText(plikJson, json);
            Console.WriteLine("Zapisano dane do pliku.");
        }

        static void DodajKsiazke()
        {
            Ksiazka nowaKsiazka = new Ksiazka();

            Console.WriteLine("Podaj tytuł:");
            nowaKsiazka.Tytul = Console.ReadLine();

            Console.WriteLine("Podaj autora:");
            nowaKsiazka.Autor = Console.ReadLine();

            Console.WriteLine("Podaj rok wydania:");
            int rok;
            if (!int.TryParse(Console.ReadLine(), out rok))
            {
                Console.WriteLine("Niepoprawny format roku. Operacja przerwana.");
                return;
            }
            nowaKsiazka.RokWydania = rok;

            Console.WriteLine("Podaj gatunek:");
            nowaKsiazka.Gatunek = Console.ReadLine();

            nowaKsiazka.Id = ksiazki.Count + 1;
            ksiazki.Add(nowaKsiazka);
            Console.WriteLine("Książka dodana pomyślnie.");

           
            ZapiszKsiazki();
        }

        static void WyswietlListeKsiazek()
        {
            foreach (var ksiazka in ksiazki)
            {
                Console.WriteLine($"ID: {ksiazka.Id}, Tytuł: {ksiazka.Tytul}");
            }
        }

        static void WyswietlDaneKsiazki()
        {
            Console.WriteLine("Podaj ID książki:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id) || id < 1 || id > ksiazki.Count)
            {
                Console.WriteLine("Niepoprawne ID książki. Operacja przerwana.");
                return;
            }

            Ksiazka wybranaKsiazka = ksiazki[id - 1];
            Console.WriteLine($"ID: {wybranaKsiazka.Id}");
            Console.WriteLine($"Tytuł: {wybranaKsiazka.Tytul}");
            Console.WriteLine($"Autor: {wybranaKsiazka.Autor}");
            Console.WriteLine($"Rok wydania: {wybranaKsiazka.RokWydania}");
            Console.WriteLine($"Gatunek: {wybranaKsiazka.Gatunek}");
        }

        static void UsunKsiazke()
        {
            Console.WriteLine("Podaj ID książki do usunięcia:");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id) || id < 1 || id > ksiazki.Count)
            {
                Console.WriteLine("Niepoprawne ID książki. Operacja przerwana.");
                return;
            }

            ksiazki.RemoveAt(id - 1);
            Console.WriteLine("Książka usunięta pomyślnie.");
        }
    }
}
