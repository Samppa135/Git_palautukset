using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using static System.Formats.Asn1.AsnWriter;

class TankGame
{
    static int screenWidth = 800;
    static int screenHeight = 600;
    // Pelaajat ja heidän ohjaimet
    static Tank player1 = new Tank(new Vector2(100, 300), Color.Red, KeyboardKey.W, KeyboardKey.S, KeyboardKey.A, KeyboardKey.D, KeyboardKey.Space, true);  // Pelaaja 1
    static Tank player2 = new Tank(new Vector2(700, 300), Color.Blue, KeyboardKey.Up, KeyboardKey.Down, KeyboardKey.Left, KeyboardKey.Right, KeyboardKey.Enter, false); // Pelaaja 2
    static List<Bullet> bullets = new List<Bullet>();  // Luodaan lista ammuksille
    static Rectangle wall = new Rectangle(350, 200, 100, 200);  // Seinä, johon ammus voi törmätä
    static int score1 = 0;  // Pelaaja 1:n pistetilanne
    static int score2 = 0;  // Pelaaja 2:n pistetilanne

    static void Main()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "Tanks");  // Alustetaan peliikkuna
        Raylib.SetTargetFPS(60);  // Peli pyörii 60 kuvassa sekunnissa

        while (!Raylib.WindowShouldClose())  // Pelisilmukka, joka pyörii kunnes ikkuna suljetaan
        {
            float time = (float)Raylib.GetTime();  // Aika, käytetään ammusten laukaisussa
            player1.Update(time, bullets, wall, ref score1, player2);  // Päivitetään pelaaja 1:n tila
            player2.Update(time, bullets, wall, ref score2, player1);  // Päivitetään pelaaja 2:n tila
            UpdateBullets();  // Päivitetään ammukset

            Raylib.BeginDrawing();  // Alustetaan piirto
            Raylib.ClearBackground(Color.Green);  // Taustaväri

            // Näytetään pisteet
            Raylib.DrawText("Player 1: " + score1, 10, 10, 20, Color.Red);  // Pelaaja 1:n pisteet
            Raylib.DrawText("Player 2: " + score2, screenWidth - 120, 10, 20, Color.Blue);  // Pelaaja 2:n pisteet

            player1.Draw();  // Piirretään pelaaja 1
            player2.Draw();  // Piirretään pelaaja 2
            foreach (var bullet in bullets) bullet.Draw();  // Piirretään kaikki ammukset
            Raylib.DrawRectangleRec(wall, Color.Brown);  // Piirretään seinä

            Raylib.EndDrawing();  // Lopetetaan piirto
        }

        Raylib.CloseWindow();  // Suljetaan peliikkuna
    }

    // Päivitetään ammukset
    static void UpdateBullets()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)  // Käydään läpi kaikki ammukset
        {
            bullets[i].Update();  // Päivitetään ammus
            // Poistetaan ammus jos se menee ruudun ulkopuolelle tai osuu seinään
            if (bullets[i].OutOfBounds(screenWidth, screenHeight) || Raylib.CheckCollisionPointRec(bullets[i].Position, wall))
            {
                bullets.RemoveAt(i);  // Poistetaan ammus listasta
            }
        }
    }
}

class Tank
{
    public Vector2 Position;  // Tankin sijainti
    public float Rotation;  // Tankin kierto
    public Color TankColor;  // Tankin väri
    private float lastShotTime;  // Aika viimeisestä laukauksesta
    private const float speed = 2.0f;  // Tankin liikkumisnopeus
    private KeyboardKey up, down, left, right, shoot;  // Ohjausnäppäimet
    private Vector2 startPosition;  // Tankin alkuperäinen sijainti
    private bool isPlayer1;  // Tieto siitä, onko tankki pelaaja 1 vai pelaaja 2
    private int score1;
    private int score2;

    // Tankin konstruktori
    public Tank(Vector2 position, Color color, KeyboardKey up, KeyboardKey down, KeyboardKey left, KeyboardKey right, KeyboardKey shoot, bool isPlayer1)
    {
        Position = position;
        startPosition = position;
        TankColor = color;
        this.up = up; this.down = down; this.left = left; this.right = right; this.shoot = shoot;
        this.isPlayer1 = isPlayer1;
    }

    // Päivitetään tankin tila
    public void Update(float time, List<Bullet> bullets, Rectangle wall, ref int score, Tank opponent)
    {
        Vector2 oldPosition = Position;
        float oldRotation = Rotation;

        // Ohjaus: Kierrä tankkia vasemmalle ja oikealle
        if (Raylib.IsKeyDown(left)) Rotation -= 2.0f;
        if (Raylib.IsKeyDown(right)) Rotation += 2.0f;

        // Liikutetaan tankkia eteen ja taakse
        Vector2 direction = new Vector2(MathF.Cos(Rotation * MathF.PI / 180), MathF.Sin(Rotation * MathF.PI / 180));
        if (Raylib.IsKeyDown(up)) Position += direction * speed;
        if (Raylib.IsKeyDown(down)) Position -= direction * speed;

        // Ammuksen laukaisu
        if (Raylib.IsKeyDown(shoot) && time - lastShotTime > 1.0f)
        {
            bullets.Add(new Bullet(Position + direction * 20, direction, TankColor));  // Lisää uusi ammus
            lastShotTime = time;  // Päivitetään viimeinen laukaisuaika
        }

        // Tarkistetaan, osuuko ammus vastustajan tankkiin
        foreach (var bullet in bullets)
        {
            if (Raylib.CheckCollisionCircleRec(bullet.Position, 5, new Rectangle(opponent.Position.X - 20, opponent.Position.Y - 20, 40, 40)))
            {
                // Pisteen antaminen ammusten osumisen mukaan
                if (isPlayer1) score1++;  // Jos pelaaja 1:n ammus osuu, lisää hänen pisteet
                if (!isPlayer1) score2++; // Jos pelaaja 2:n ammus osuu, lisää hänen pisteet

                // Molemmat tankit respawnataan
                Respawn();  // Palautetaan oma tankki alkuperäiseen paikkaan
                opponent.Respawn();  // Palautetaan vastustajan tankki alkuperäiseen paikkaan
                break;
            }
        }

        // Tarkistetaan, osuuko tankki seinään
        if (Raylib.CheckCollisionRecs(new Rectangle(Position.X - 20, Position.Y - 20, 40, 40), wall))
        {
            Position = oldPosition;  // Palautetaan tankin sijainti jos osuu seinään
            Rotation = oldRotation;  // Palautetaan tankin kierto
        }
    }

    // Tankin respawn
    public void Respawn()
    {
        Position = startPosition;  // Tankki palautetaan alkuperäiseen sijaintiin
    }

    // Piirretään tankki
    public void Draw()
    {
        Raylib.DrawRectanglePro(new Rectangle(Position.X, Position.Y, 40, 40), new Vector2(20, 20), Rotation, TankColor);  // Tankin runko
        Raylib.DrawRectanglePro(new Rectangle(Position.X, Position.Y, 20, 20), new Vector2(10, 10), Rotation, Color.DarkGray);  // Tankin pyörät
        Vector2 barrelEnd = new Vector2(Position.X + MathF.Cos(Rotation * MathF.PI / 180) * 30, Position.Y + MathF.Sin(Rotation * MathF.PI / 180) * 30);  // Tykin pää
        Raylib.DrawLine((int)Position.X, (int)Position.Y, (int)barrelEnd.X, (int)barrelEnd.Y, Color.Black);  // Piirretään tykki
    }
}

class Bullet
{
    public Vector2 Position;  // Ammuksen sijainti
    public Vector2 Direction;  // Ammuksen suunta
    private const float speed = 5.0f;  // Ammuksen nopeus
    private Color BulletColor;  // Ammuksen väri

    public Bullet(Vector2 position, Vector2 direction, Color color)
    {
        Position = position;
        Direction = direction;
        BulletColor = color;
    }

    // Päivitetään ammus
    public void Update()
    {
        Position += Direction * speed;  // Liikutetaan ammusta eteenpäin
    }

    // Tarkistetaan, meneekö ammus ruudun ulkopuolelle
    public bool OutOfBounds(int width, int height)
    {
        return Position.X < 0 || Position.X > width || Position.Y < 0 || Position.Y > height;
    }

    // Piirretään ammus
    public void Draw()
    {
        Raylib.DrawCircle((int)Position.X, (int)Position.Y, 5, BulletColor);  // Piirretään ympyrä ammukselle
    }
}
