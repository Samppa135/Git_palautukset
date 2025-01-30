using System;

enum Karki
{
    Puu = 3,
    Teräs = 5,
    Timantti = 50
}

enum Pera
{
    Lehti = 0,
    Kanansulka = 1,
    Kotkansulka = 5
}

class Nuoli
{
    private Karki nuolenKarki;
    private Pera nuolenPera;
    private int varrenPituus;

    public Nuoli(Karki karki, Pera pera, int varrenPituus)
    {
        this.nuolenKarki = karki;
        this.nuolenPera = pera;
        this.varrenPituus = varrenPituus;
    }

    public Karki GetNuolenKarki()
    {
        return nuolenKarki;
    }

    public Pera GetNuolenPera()
    {
        return nuolenPera;
    }

    public int GetVarrenPituus()
    {
        return varrenPituus;
    }

    public double PalautaHinta()
    {
        double karkiHinta = (int)nuolenKarki;
        double peraHinta = (int)nuolenPera;
        double varsiHinta = varrenPituus * 0.05;
        return karkiHinta + peraHinta + varsiHinta;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Mikäs kärki laitetaan (1: Puu, 2: Teräs, 3: Timantti):");
        Karki valittuKarki = ValitseKarki();

        Console.WriteLine("Mikäs perä laitetaan (1: Lehti, 2: Kanansulka, 3: Kotkansulka):");
        Pera valittuPera = ValitsePera();

        Console.WriteLine("Minkä pituinen varsi laitetaan (60-100 cm):");
        int varrenPituus = ValitsePituus();

        Nuoli nuoli = new Nuoli(valittuKarki, valittuPera, varrenPituus);
        Console.WriteLine($"Se tekisi: {nuoli.PalautaHinta():0.00} kultaa");
    }

    static Karki ValitseKarki()
    {
        int valinta = int.Parse(Console.ReadLine());
        return valinta switch
        {
            1 => Karki.Puu,
            2 => Karki.Teräs,
            3 => Karki.Timantti,
            _ => Karki.Puu
        };
    }

    static Pera ValitsePera()
    {
        int valinta = int.Parse(Console.ReadLine());
        return valinta switch
        {
            1 => Pera.Lehti,
            2 => Pera.Kanansulka,
            3 => Pera.Kotkansulka,
            _ => Pera.Lehti
        };
    }

    static int ValitsePituus()
    {
        int pituus = int.Parse(Console.ReadLine());
        if (pituus < 60 || pituus > 100)
        {
            Console.WriteLine("Hei, sanoin 60-100 cm. Laitetaan sitten 60 cm.");
            return 60;
        }
        return pituus;
    }
}
