using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Raylib_cs;

class AsteroidsGame
{
    static int screenWidth = 800;
    static int screenHeight = 600;

    static PlayerShip player = new PlayerShip(new Vector2(screenWidth / 2, screenHeight / 2));
    static List<Bullet> bullets = new List<Bullet>();
    static List<Asteroid> asteroids = new List<Asteroid>();
    static Random random = new Random();

    static int lives = 3;
    static int score = 0;

    static void Main()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "Asteroids");
        Raylib.SetTargetFPS(60);

        // Luodaan aluksi muutama asteroidi
        for (int i = 0; i < 5; i++)
        {
            asteroids.Add(new Asteroid(GetRandomEdgePosition(), GetRandomVelocity(), 40));
        }

        while (!Raylib.WindowShouldClose() && lives > 0)
        {
            float deltaTime = Raylib.GetFrameTime();

            // Päivitetään pelaaja, luodit ja asteroidit
            player.Update(deltaTime, bullets);

            UpdateBullets();
            UpdateAsteroids();

            CheckCollisions();

            // Piirretään peli
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            // Pelaaja ja kohteet
            player.Draw();
            foreach (var bullet in bullets) bullet.Draw();
            foreach (var asteroid in asteroids) asteroid.Draw();

            // Elämät ja pisteet
            Raylib.DrawText("Lives: " + lives, 10, 10, 20, Color.White);
            Raylib.DrawText("Score: " + score, screenWidth - 120, 10, 20, Color.White);

            Raylib.EndDrawing();
        }

        // Peli loppui
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);
        Raylib.DrawText("GAME OVER", screenWidth / 2 - 100, screenHeight / 2 - 20, 40, Color.Red);
        Raylib.DrawText("Final Score: " + score, screenWidth / 2 - 100, screenHeight / 2 + 20, 30, Color.White);
        Raylib.EndDrawing();

        Raylib.CloseWindow();
    }

    static void UpdateBullets()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            bullets[i].Update();
            if (bullets[i].OutOfBounds(screenWidth, screenHeight))
            {
                bullets.RemoveAt(i);
            }
        }
    }

    static void UpdateAsteroids()
    {
        foreach (var asteroid in asteroids)
        {
            asteroid.Update(screenWidth, screenHeight);
        }
    }

    static void CheckCollisions()
    {
        // Tarkistetaan osuuko luoti asteroidin kanssa
        for (int i = asteroids.Count - 1; i >= 0; i--)
        {
            bool asteroidDestroyed = false;
            for (int j = bullets.Count - 1; j >= 0; j--)
            {
                if (Vector2.Distance(bullets[j].Position, asteroids[i].Position) < asteroids[i].Radius)
                {
                    bullets.RemoveAt(j);
                    // Asteroidi tuhoutuu, pistettä
                    score += 10;

                    // Jos asteroidi on iso, se hajoaa kahdeksi pienemmäksi
                    if (asteroids[i].Radius > 15)
                    {
                        asteroids.Add(new Asteroid(asteroids[i].Position, GetRandomVelocity(), asteroids[i].Radius / 2));
                        asteroids.Add(new Asteroid(asteroids[i].Position, GetRandomVelocity(), asteroids[i].Radius / 2));
                    }
                    asteroids.RemoveAt(i);
                    asteroidDestroyed = true;
                    break;
                }
            }
            if (asteroidDestroyed) continue;
        }

        // Tarkistetaan osuuko asteroidi pelaajaan
        foreach (var asteroid in asteroids)
        {
            if (Vector2.Distance(player.Position, asteroid.Position) < asteroid.Radius + 15) // 15 = aluksen "säde"
            {
                lives--;
                player.Respawn(new Vector2(screenWidth / 2, screenHeight / 2));
                break;
            }
        }
    }

    static Vector2 GetRandomEdgePosition()
    {
        // Satunnainen piste ruudun reunalta (ylä, ala, vasen, oikea)
        int side = random.Next(4);
        switch (side)
        {
            case 0: return new Vector2(random.Next(screenWidth), 0); // ylhäällä
            case 1: return new Vector2(random.Next(screenWidth), screenHeight); // alhaalla
            case 2: return new Vector2(0, random.Next(screenHeight)); // vasen
            default: return new Vector2(screenWidth, random.Next(screenHeight)); // oikea
        }
    }

    static Vector2 GetRandomVelocity()
    {
        float speed = (float)(random.NextDouble() * 100 + 20);
        float angle = (float)(random.NextDouble() * Math.PI * 2);
        return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed * 0.01f;
    }
}

class PlayerShip
{
    public Vector2 Position;
    private float Rotation;  // asteina
    private Vector2 Velocity;
    private const float acceleration = 0.1f;
    private const float maxSpeed = 5f;
    private const float friction = 0.98f;
    private float lastShotTime = 0;

    public PlayerShip(Vector2 startPos)
    {
        Position = startPos;
        Rotation = -90f; // Alkuun osoittaa ylös
        Velocity = Vector2.Zero;
    }

    public void Update(float deltaTime, List<Bullet> bullets)
    {
        // Kääntyminen
        if (Raylib.IsKeyDown(KeyboardKey.Left)) Rotation -= 3f;
        if (Raylib.IsKeyDown(KeyboardKey.Right)) Rotation += 3f;

        // Kiihdytys
        if (Raylib.IsKeyDown(KeyboardKey.Up))
        {
            Vector2 force = new Vector2((float)Math.Cos(Rotation * Math.PI / 180), (float)Math.Sin(Rotation * Math.PI / 180)) * acceleration;
            Velocity += force;
            if (Velocity.Length() > maxSpeed)
                Velocity = Vector2.Normalize(Velocity) * maxSpeed;
        }

        // Hidastuminen (kitka)
        Velocity *= friction;

        // Liikkuminen
        Position += Velocity;

        // Ruudun reunojen kiertäminen ympäri
        if (Position.X < 0) Position.X = 800;
        if (Position.X > 800) Position.X = 0;
        if (Position.Y < 0) Position.Y = 600;
        if (Position.Y > 600) Position.Y = 0;

        // Ampuminen (välilyönti)
        float currentTime = (float)Raylib.GetTime();
        if (Raylib.IsKeyDown(KeyboardKey.Space) && currentTime - lastShotTime > 0.3f)
        {
            Vector2 dir = new Vector2((float)Math.Cos(Rotation * Math.PI / 180), (float)Math.Sin(Rotation * Math.PI / 180));
            bullets.Add(new Bullet(Position + dir * 20, dir));
            lastShotTime = currentTime;
        }
    }

    public void Respawn(Vector2 pos)
    {
        Position = pos;
        Velocity = Vector2.Zero;
        Rotation = -90f;
    }

    public void Draw()
    {
        // Piirretään kolmio pelaajasta
        Vector2 tip = Position + new Vector2((float)Math.Cos(Rotation * Math.PI / 180), (float)Math.Sin(Rotation * Math.PI / 180)) * 20;
        Vector2 left = Position + new Vector2((float)Math.Cos((Rotation + 140) * Math.PI / 180), (float)Math.Sin((Rotation + 140) * Math.PI / 180)) * 15;
        Vector2 right = Position + new Vector2((float)Math.Cos((Rotation - 140) * Math.PI / 180), (float)Math.Sin((Rotation - 140) * Math.PI / 180)) * 15;

        Raylib.DrawTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Color color);

    }
}

class Bullet
{
    public Vector2 Position;
    private Vector2 Direction;
    private const float speed = 10f;
    private float radius = 3;

    public Bullet(Vector2 position, Vector2 direction)
    {
        Position = position;
        Direction = Vector2.Normalize(direction);
    }

    public void Update()
    {
        Position += Direction * speed;

        // Ruudun reunojen kiertäminen ympäri (tämän voi halutessaan poistaa, nyt luoti katoaa ruudun ulkopuolelle)
    }

    public bool OutOfBounds(int width, int height)
    {
        return Position.X < 0 || Position.X > width || Position.Y < 0 || Position.Y > height;
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)Position.X, (int)Position.Y, radius, Color.Yellow);
    }
}

class Asteroid
{
    public Vector2 Position;
    private Vector2 Velocity;
    public float Radius;

    public Asteroid(Vector2 position, Vector2 velocity, float radius)
    {
        Position = position;
        Velocity = velocity;
        Radius = radius;
    }

    public void Update(int screenWidth, int screenHeight)
    {
        Position += Velocity;

        // Kiertäminen ruudun reunoilla
        if (Position.X < 0)
        {
            Position.X = 0;
            Velocity.X = -Velocity.X;
        }
        if (Position.X > screenWidth)
        {
            Position.X = screenWidth;
            Velocity.X = -Velocity.X;
        }
        if (Position.Y < 0)
        {
            Position.Y = 0;
            Velocity.Y = -Velocity.Y;
        }
        if (Position.Y > screenHeight)
        {
            Position.Y = screenHeight;
            Velocity.Y = -Velocity.Y;
        }
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)Position.X, (int)Position.Y, Radius, Color.Gray);
    }
}
