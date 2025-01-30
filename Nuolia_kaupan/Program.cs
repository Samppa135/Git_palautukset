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
    public Karki NuolenKarki { get; private set; }
    public Pera NuolenPera { get; private set; }
    public int VarrenPituus { get; private set; }

    public Nuoli(Karki karki, Pera pera, int varrenPituus)
    {
        NuolenKarki = karki;
        NuolenPera = pera;
        VarrenPituus = varrenPituus;
    }

    public double PalautaHinta()
    {
        double karkiHinta = (int)NuolenKarki;
        double peraHinta = (int)NuolenPera;
        double varsiHinta = VarrenPituus * 0.05;
        return karkiHinta + peraHinta + varsiHinta;
    }

    public static Nuoli LuoEliittiNuoli()
    {
        return new Nuoli(Karki.Timantti, Pera.Kotkansulka, 100);
    }

    public static Nuoli LuoAloittelijanuoli()
    {
        return new Nuoli(Karki.Puu, Pera.Kanansulka, 70);
    }

    public static Nuoli LuoPerusNuoli()
    {
        return new Nuoli(Karki.Teräs, Pera.Kanansulka, 85);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Kiva nähdä sinua taas seikkailija.");
        Console.WriteLine("Mites, haluatko:\n1. Valita itse osat?\n2. Ostaa valmiin nuolen?");
        int valinta = int.Parse(Console.ReadLine());

        Nuoli nuoli;
        if (valinta == 2)
        {
            Console.WriteLine("Valitse valmis nuoli:\n1. Eliittinuoli\n2. Aloittelijanuoli\n3. Perusnuoli");
            int valmisValinta = int.Parse(Console.ReadLine());
            nuoli = valmisValinta switch
            {
                1 => Nuoli.LuoEliittiNuoli(),
                2 => Nuoli.LuoAloittelijanuoli(),
                3 => Nuoli.LuoPerusNuoli(),
                _ => Nuoli.LuoPerusNuoli()
            };
        }
        else
        {
            Console.WriteLine("Mikäs kärki laitetaan (1: Puu, 2: Teräs, 3: Timantti):");
            Karki valittuKarki = ValitseKarki();

            Console.WriteLine("Mikäs perä laitetaan (1: Lehti, 2: Kanansulka, 3: Kotkansulka):");
            Pera valittuPera = ValitsePera();

            Console.WriteLine("Minkä pituinen varsi laitetaan (60-100 cm):");
            int varrenPituus = ValitsePituus();

            nuoli = new Nuoli(valittuKarki, valittuPera, varrenPituus);
        }

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