using System;
using System.Collections.Generic;

enum PaaRaakaAine
{
    Nautaa,
    Kanaa,
    Kasviksia
}

enum Lisuke
{
    Perunaa,
    Riisia,
    Pastaa
}

enum Kastike
{
    Curry,
    Hapanimela,
    Pippuri,
    Chili
}

class Ateria
{
    public PaaRaakaAine PaaraakaAine { get; set; }
    public Lisuke Lisuke { get; set; }
    public Kastike Kastike { get; set; }

    public override string ToString()
    {
        return $"{PaaraakaAine.ToString().ToLower()} ja {Lisuke.ToString().ToLower()} {Kastike.ToString().ToLower()}-kastikkeella";
    }
}

class Program
{
    static void Main()
    {
        List<Ateria> ateriat = new List<Ateria>();

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("Valitse pääraaka-aine (0 = Nautaa, 1 = Kanaa, 2 = Kasviksia): ");
            PaaRaakaAine paaraakaAine = (PaaRaakaAine)int.Parse(Console.ReadLine());

            Console.WriteLine("Valitse lisuke (0 = Perunaa, 1 = Riisiä, 2 = Pastaa): ");
            Lisuke lisuke = (Lisuke)int.Parse(Console.ReadLine());

            Console.WriteLine("Valitse kastike (0 = Curry, 1 = Hapanimelä, 2 = Pippuri, 3 = Chili): ");
            Kastike kastike = (Kastike)int.Parse(Console.ReadLine());

            ateriat.Add(new Ateria { PaaraakaAine = paaraakaAine, Lisuke = lisuke, Kastike = kastike });
        }

        Console.WriteLine("\nValitut ateriat:");
        foreach (var ateria in ateriat)
        {
            Console.WriteLine(ateria);
        }
    }
}
