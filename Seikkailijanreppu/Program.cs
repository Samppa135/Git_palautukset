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
}

// Tavarat
class Nuoli : Tavara { public Nuoli() : base(0.1, 0.05) { } }
class Jousi : Tavara { public Jousi() : base(1, 4) { } }
class Köysi : Tavara { public Köysi() : base(1, 1.5) { } }
class Vesi : Tavara { public Vesi() : base(2, 2) { } }
class RuokaAnnos : Tavara { public RuokaAnnos() : base(1, 0.5) { } }
class Miekka : Tavara { public Miekka() : base(5, 3) { } }

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
}

// Pääohjelma
class Program
{
    static void Main()
    {
        Reppu reppu = new Reppu(10, 15, 10);

        while (true)
        {
            Console.WriteLine("\nRepun tila:");
            Console.WriteLine($"Tavarat: {reppu.NykyinenMaara}/{reppu.MaksimiMaara}");
            Console.WriteLine($"Paino: {reppu.NykyinenPaino}/{reppu.MaksimiPaino}");
            Console.WriteLine($"Tilavuus: {reppu.NykyinenTilavuus}/{reppu.MaksimiTilavuus}\n");

            Console.WriteLine("Mitäs haluaisit lisätä?");
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
                Console.WriteLine("Tavara on lisätty reppuusi!");
            }
            else
            {
                Console.WriteLine("Tavaran lisääminen epäonnistui. Ei tarpeeksi tilaa tai painoraja ylittyi.");
            }
        }
    }
}
