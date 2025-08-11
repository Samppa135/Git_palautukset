using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Text.Json;
using Raylib_cs;

class Ammustyyppi
{
    public string nimi { get; set; }
    public float paino { get; set; }
    public string vari { get; set; }
    public int rajahtaysSade { get; set; }
}

class Ammus
{
    public Vector2 sijainti;
    public Vector2 nopeus;
    public Ammustyyppi tyyppi;

    public Ammus(Vector2 sijainti, Vector2 nopeus, Ammustyyppi tyyppi)
    {
        this.sijainti = sijainti;
        this.nopeus = nopeus;
        this.tyyppi = tyyppi;
    }

    public void Paivita()
    {
        nopeus.Y += 0.2f * tyyppi.paino; // Painovoima riippuu painosta
        sijainti += nopeus;
    }

    public void Piirra()
    {
        Color vari = (Color)typeof(Color).GetProperty(tyyppi.vari).GetValue(null);
        Raylib.DrawCircleV(sijainti, 5, vari);
    }
}

class Program
{
    static void Main()
    {
        int screenWidth = 800;
        int screenHeight = 600;

        Raylib.InitWindow(screenWidth, screenHeight, "Tanks");
        Raylib.SetTargetFPS(60);

        List<Ammustyyppi> ammustyypit = LataaAmmustyypit("ammustyypit.json");
        List<Ammus> ammukset = new List<Ammus>();

        Vector2 tykinPaikka = new Vector2(100, screenHeight - 100);
        Vector2 tykinSuunta = new Vector2(3, -5);

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ONE))
                ammukset.Add(new Ammus(tykinPaikka, tykinSuunta, ammustyypit[0]));

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_TWO))
                ammukset.Add(new Ammus(tykinPaikka, tykinSuunta, ammustyypit[1]));

            // Päivitys
            foreach (var ammus in ammukset)
                ammus.Paivita();

            // Piirto
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SKYBLUE);

            Raylib.DrawText("1 = NopeaAmmus, 2 = RaskasAmmus", 10, 10, 20, Color.BLACK);

            foreach (var ammus in ammukset)
                ammus.Piirra();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    static List<Ammustyyppi> LataaAmmustyypit(string tiedosto)
    {
        string json = File.ReadAllText(tiedosto);
        return JsonSerializer.Deserialize<List<Ammustyyppi>>(json);
    }
}
