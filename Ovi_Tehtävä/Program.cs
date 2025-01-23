using System;

enum DoorState
{
    Kiinni,
    Auki,
    Lukossa
}

class Program
{
    static void Main()
    {
        DoorState ovi = DoorState.Kiinni;

        while (true)
        {
            Console.WriteLine($"Oven tila: {ovi}");
            Console.WriteLine("Komennot: Avaa, Sulje, Lukitse, Avaa lukko");
            Console.Write("Anna komento: ");
            string komento = Console.ReadLine().Trim().ToLower();

            switch (komento)
            {
                case "avaa":
                    if (ovi == DoorState.Kiinni)
                    {
                        ovi = DoorState.Auki;
                        Console.WriteLine("Ovi on nyt auki.");
                    }
                    else
                    {
                        Console.WriteLine("Ovea ei voi avata suoraan.");
                    }
                    break;

                case "sulje":
                    if (ovi == DoorState.Auki)
                    {
                        ovi = DoorState.Kiinni;
                        Console.WriteLine("Ovi on nyt kiinni.");
                    }
                    else
                    {
                        Console.WriteLine("Ovea ei voi sulkea nyt.");
                    }
                    break;

                case "lukitse":
                    if (ovi == DoorState.Kiinni)
                    {
                        ovi = DoorState.Lukossa;
                        Console.WriteLine("Ovi on nyt lukossa.");
                    }
                    else
                    {
                        Console.WriteLine("Avoinna olevaa ovea ei voi lukita.");
                    }
                    break;

                case "avaa lukko":
                    if (ovi == DoorState.Lukossa)
                    {
                        ovi = DoorState.Kiinni;
                        Console.WriteLine("Lukko avattu, ovi on nyt kiinni.");
                    }
                    else
                    {
                        Console.WriteLine("Ovi ei ole lukossa.");
                    }
                    break;

                default:
                    Console.WriteLine("Väärin. Kokeile uudelleen.");
                    break;
            }
        }
    }
}
