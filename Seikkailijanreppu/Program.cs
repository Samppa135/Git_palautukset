using System;

// Tavara-luokka
abstract class Tavara
{
    public double Paino { get; }
    public double Tilavuus { get; }

    protected Tavara(double paino, double tilavuus)
    {
        Paino = paino;
        Tilavuus = tilavuus;
    }

    
    public abstract override string ToString();
}

class Nuoli : Tavara
{
    public Nuoli() : base(0.1, 0.05) { }
    public override string ToString() => "Nuoli";
}

class Jousi : Tavara
{
    public Jousi() : base(1, 4) { }
    public override string ToString() => "Jousi";
}

class Köysi : Tavara
{
    public Köysi() : base(1, 1.5) { }
    public override string ToString() => "Köysi";
}

class Vesi : Tavara
{
    public Vesi() : base(2, 2) { }
    public override string ToString() => "Vesi";
}

class RuokaAnnos : Tavara
{
    public RuokaAnnos() : base(1, 0.5) { }
    public override string ToString() => "Ruoka-annos";
}

class Miekka : Tavara
{
    public Miekka() : base(5, 3) { }
    public override string ToString() => "Miekka";
}

// Reppu-luokka
class Reppu
{
    private Tavara[] tavarat;
    private int nykyinenMaara;
    private double nykyinenPaino;
    private double nykyinenTilavuus;

    public int MaksimiMaara { get; }
    public double MaksimiPaino { get; }
    public double MaksimiTilavuus { get; }

    public int NykyinenMaara => nykyinenMaara;
    public double NykyinenPaino => nykyinenPaino;
    public double NykyinenTilavuus => nykyinenTilavuus;

    public Reppu(int maksimiMaara, double maksimiPaino, double maksimiTilavuus)
    {
        MaksimiMaara = maksimiMaara;
        MaksimiPaino = maksimiPaino;
        MaksimiTilavuus = maksimiTilavuus;
        tavarat = new Tavara[maksimiMaara];
        nykyinenMaara = 0;
        nykyinenPaino = 0;
        nykyinenTilavuus = 0;
    }

    public bool Lisää(Tavara tavara)
    {
        if (nykyinenMaara >= MaksimiMaara ||
            nykyinenPaino + tavara.Paino > MaksimiPaino ||
            nykyinenTilavuus + tavara.Tilavuus > MaksimiTilavuus)
        {
            return false;
        }

        tavarat[nykyinenMaara] = tavara;
        nykyinenMaara++;
        nykyinenPaino += tavara.Paino;
        nykyinenTilavuus += tavara.Tilavuus;
        return true;
    }

    // Repun sisältö
    public override string ToString()
    {
        if (nykyinenMaara == 0)
            return "Tulit ostamaan kylän torilta asioita seikkailuusi.";

        string sisalto = "Reppun sisältö: ";
        for (int i = 0; i < nykyinenMaara; i++)
        {
            sisalto += tavarat[i].ToString();
            if (i < nykyinenMaara - 1)
                sisalto += ", ";
        }
        return sisalto;
    }
}

// Pääohjelma
class Program
{
    static void Main()
    {
        Reppu reppu = new Reppu(10, 15, 10);

        // Tulostetaan repun alkuperäinen sisältö
        Console.WriteLine("Tarina: " + reppu.ToString());

        while (true)
        {
            Console.WriteLine("\nRepun tila:");
            Console.WriteLine($"Tavarat: {reppu.NykyinenMaara}/{reppu.MaksimiMaara}");
            Console.WriteLine($"Paino: {reppu.NykyinenPaino}/{reppu.MaksimiPaino}");
            Console.WriteLine($"Tilavuus: {reppu.NykyinenTilavuus}/{reppu.MaksimiTilavuus}\n");

            Console.WriteLine("Mitäs haluaisit ostaa?");
            Console.WriteLine("1. Nuoli");
            Console.WriteLine("2. Jousi");
            Console.WriteLine("3. Köysi");
            Console.WriteLine("4. Vesi");
            Console.WriteLine("5. Ruoka-annos");
            Console.WriteLine("6. Miekka");
            Console.WriteLine("7. Lopeta");
            Console.Write("Valinta: ");

            string valinta = Console.ReadLine();
            Tavara lisättävä = valinta switch
            {
                "1" => new Nuoli(),
                "2" => new Jousi(),
                "3" => new Köysi(),
                "4" => new Vesi(),
                "5" => new RuokaAnnos(),
                "6" => new Miekka(),
                "7" => null,
                _ => null
            };

            if (lisättävä == null)
            {
                Console.WriteLine("Lopeta pakkaaminen.");
                break;
            }

            if (reppu.Lisää(lisättävä))
            {
                Console.WriteLine("Tavara on lisätty reppuusi seikkailija!");
            }
            else
            {
                Console.WriteLine("Tavaran lisääminen epäonnistui. Ei tarpeeksi tilaa tai painoraja ylittyi.");
            }

            // Tulostetaan repun sisältö jokaisen tavaran lisäämisen jälkeen
            Console.WriteLine("Repun sisältö: " + reppu.ToString());
        }
    }
}
