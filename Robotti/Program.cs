using System;

public abstract class RobottiKäsky
{
    public abstract void Suorita(Robotti robotti);
}

public class Käynnistä : RobottiKäsky
{
    public override void Suorita(Robotti robotti)
    {
        robotti.OnKäynnissä = true;
    }
}

public class Sammuta : RobottiKäsky
{
    public override void Suorita(Robotti robotti)
    {
        robotti.OnKäynnissä = false;
    }
}

public class YlösKäsky : RobottiKäsky
{
    public override void Suorita(Robotti robotti)
    {
        if (robotti.OnKäynnissä)
        {
            robotti.Y++;
        }
    }
}

public class AlasKäsky : RobottiKäsky
{
    public override void Suorita(Robotti robotti)
    {
        if (robotti.OnKäynnissä)
        {
            robotti.Y--;
        }
    }
}

public class VasenKäsky : RobottiKäsky
{
    public override void Suorita(Robotti robotti)
    {
        if (robotti.OnKäynnissä)
        {
            robotti.X--;
        }
    }
}

public class OikeaKäsky : RobottiKäsky
{
    public override void Suorita(Robotti robotti)
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
    public RobottiKäsky?[] Käskyt { get; } = new RobottiKäsky?[3];

    public void Suorita()
    {
        foreach (RobottiKäsky? käsky in Käskyt)
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
            Console.WriteLine("Syötä käsky (Käynnistä, Sammuta, Ylös, Alas, Vasen, Oikea): ");
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
