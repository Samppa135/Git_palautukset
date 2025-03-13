using System;

struct Koordinaatti
{
    // Koordinaatin X ja Y arvot määritellään vain luku- ja asetusmuuttujilla (readonly tai private set).
    public int X { get; private set; }
    public int Y { get; private set; }

    // Konstruktorissa asetetaan koordinaatit
    public Koordinaatti(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Metodi, joka tarkistaa onko koordinaatti toisen koordinaatin vieressä
    public bool OnkoVieressa(Koordinaatti toinen)
    {
        return (Math.Abs(X - toinen.X) == 1 && Y == toinen.Y) || (Math.Abs(Y - toinen.Y) == 1 && X == toinen.X);
    }
}

class Program
{
    static void Main()
    {
        // Pyydetään käyttäjältä kolme koordinaattia
        Console.WriteLine("Anna kolme koordinaattia (X Y):");

        Koordinaatti[] koordinaatit = new Koordinaatti[3];

        for (int i = 0; i < 3; i++)
        {
            Console.Write($"Koordinaatti {i + 1} (X Y): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            koordinaatit[i] = new Koordinaatti(x, y);
        }

        // Tarkistetaan ja tulostetaan viereiset koordinaatit
        for (int i = 0; i < koordinaatit.Length; i++)
        {
            Console.WriteLine($"Koordinaatti {koordinaatit[i].X},{koordinaatit[i].Y} viereiset:");

            for (int j = 0; j < koordinaatit.Length; j++)
            {
                if (i != j && koordinaatit[i].OnkoVieressa(koordinaatit[j]))
                {
                    Console.WriteLine($"- Koordinaatti {koordinaatit[j].X},{koordinaatit[j].Y}");
                }
            }
            Console.WriteLine(); // tyhjä rivi erottamaan koordinaatit
        }
    }
}
