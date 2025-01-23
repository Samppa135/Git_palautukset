using System;

namespace Ritari_vs_Orkki
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Alkuteksti sinisellä
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tapaat örkin metsässä joka hyökkää kimppuusi. Taistelu alkaa!");
            Console.ResetColor();

            // Ritarin ja örkin terveyspisteet
            int ritariHP = 15;
            int orkkiHP = 15;

            while (ritariHP > 0 && orkkiHP > 0)
            {
                // Näytä tilanne valkoisella
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Ritarin HP: {ritariHP} \nÖrkin HP: {orkkiHP}");
                Console.ResetColor();

                // Näytä komennot keltaisella
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Hyökkää\n2. Puolusta");
                Console.ResetColor();

                // Kysy pelaajalta komentoa
                string? vastaus;
                while (true)
                {
                    vastaus = Console.ReadLine();
                    if (vastaus == "1" || vastaus == "2")
                    {
                        break;
                    }
                    Console.WriteLine("Virheellinen komento. Yritä uudestaan.");
                }

                // Vahinkopisteet
                Random random = new Random();
                int ritarinVahinko = random.Next(1, 5);
                int orkkiVahinko = random.Next(2, 6);

                if (vastaus == "1")
                {
                    // Pelaaja hyökkää - vihreällä
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Ritari hyökkäsi ja teki {ritarinVahinko} vahinkoa örkille!");
                    Console.ResetColor();

                    orkkiHP -= ritarinVahinko;
                }
                else if (vastaus == "2")
                {
                    // Pelaaja puolustaa - vihreällä
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ritari puolustautui kilvellä ja vähensi vahinkoa!");
                    Console.ResetColor();

                    orkkiVahinko /= 2; // Puolitetaan örkin vahinko
                }

                // Örkki hyökkää - punaisella
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Örkki hyökkäsi ja teki {orkkiVahinko} vahinkoa ritarille!");
                Console.ResetColor();

                ritariHP -= orkkiVahinko;

                // Varmistetaan, ettei HP mene negatiiviseksi
                ritariHP = Math.Max(0, ritariHP);
                orkkiHP = Math.Max(0, orkkiHP);
            }

            // Pelin lopetus
            if (ritariHP <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Örkki voitti taistelun!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ritari voitti taistelun!");
                Console.ResetColor();
            }
        }
    }
}
