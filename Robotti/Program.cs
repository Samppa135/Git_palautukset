using System;

public interface IRobottiKäsky
{
    void Suorita(Robotti robotti);
}

public class Käynnistä : IRobottiKäsky
{
    public void Suorita(Robotti robotti)
    {
        robotti.OnKäynnissä = true;
    }
}

public class Sammuta : IRobottiKäsky
{
    public void Suorita(Robotti robotti)
    {
        robotti.OnKäynnissä = false;
    }
}

public class YlösKäsky : IRobottiKäsky
{
    public void Suorita(Robotti robotti)
    {
        if (robotti.OnKäynnissä)
        {
            robotti.Y++;
        }
    }
}

public class AlasKäsky : IRobottiKäsky
{
    public void Suorita(Robotti robotti)
    {
        if (robotti.OnKäynnissä)
        {
            robotti.Y--;
        }
    }
}

public class VasenKäsky : IRobottiKäsky
{
    public void Suorita(Robotti robotti)
    {
        if (robotti.OnKäynnissä)
        {
            robotti.X--;
        }
    }
}

public class OikeaKäsky : IRobottiKäsky
{
    public void Suorita(Robotti robotti)
    {
        if (robotti.OnKäynnissä)
        {
            robotti.X++;
        }
    }
}

public class Robotti
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool OnKäynnissä { get; set; }
    public IRobottiKäsky[] Käskyt { get; } = new IRobottiKäsky[3];

    public void Suorita()
    {
        foreach (IRobottiKäsky käsky in Käskyt)
        {
            käsky?.Suorita(this);
            Console.WriteLine($"[{X} {Y} {OnKäynnissä}]");
        }
    }
}

class Program
{
    static void Main()
    {
        Robotti robotti = new Robotti();

        // Käyttäjän komennot ja syötteet taulokkoon
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("Syötä käsky isolla alkukirjaimella (Käynnistä, Sammuta, Ylös, Alas, Vasen, Oikea): ");
            string syote = Console.ReadLine();

            switch (syote)
            {
                case "Käynnistä":
                    robotti.Käskyt[i] = new Käynnistä();
                    break;
                case "Sammuta":
                    robotti.Käskyt[i] = new Sammuta();
                    break;
                case "Ylös":
                    robotti.Käskyt[i] = new YlösKäsky();
                    break;
                case "Alas":
                    robotti.Käskyt[i] = new AlasKäsky();
                    break;
                case "Vasen":
                    robotti.Käskyt[i] = new VasenKäsky();
                    break;
                case "Oikea":
                    robotti.Käskyt[i] = new OikeaKäsky();
                    break;
                default:
                    Console.WriteLine("Tuntematon käsky.");
                    i--; // Käyttäjä voi yrittää uudestaan
                    break;
            }
        }

        // Suoritus
        robotti.Suorita();
    }
}
