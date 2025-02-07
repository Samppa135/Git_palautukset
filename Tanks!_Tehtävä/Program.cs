using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

class TankGame
{
    static int screenWidth = 800;
    static int screenHeight = 600;
    static Tank player1 = new Tank(new Vector2(100, 300), Color.Red, KeyboardKey.W, KeyboardKey.S, KeyboardKey.A, KeyboardKey.D, KeyboardKey.Space);
    static Tank player2 = new Tank(new Vector2(700, 300), Color.Blue, KeyboardKey.Up, KeyboardKey.Down, KeyboardKey.Left, KeyboardKey.Right, KeyboardKey.Enter);
    static List<Bullet> bullets = new List<Bullet>();
    static int player1Score = 0;
    static int player2Score = 0;

    static void Main()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "Tanks");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            float time = Raylib.GetTime();
            player1.Update(time, bullets);
            player2.Update(time, bullets);
            UpdateBullets();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawText($"Player 1: {player1Score}", 10, 10, 20, Color.Red);
            Raylib.DrawText($"Player 2: {player2Score}", screenWidth - 150, 10, 20, Color.Blue);
            player1.Draw();
            player2.Draw();
            foreach (var bullet in bullets) bullet.Draw();

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    static void UpdateBullets()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            bullets[i].Update();
            if (bullets[i].OutOfBounds(screenWidth, screenHeight)) bullets.RemoveAt(i);
        }
    }
}

class Tank
{
    public Vector2 Position;
    public Vector2 Direction;
    public Color TankColor;
    private float lastShotTime;
    private const float speed = 2.0f;
    private KeyboardKey up, down, left, right, shoot;

    public Tank(Vector2 position, Color color, KeyboardKey up, KeyboardKey down, KeyboardKey left, KeyboardKey right, KeyboardKey shoot)
    {
        Position = position;
        TankColor = color;
        Direction = new Vector2(1, 0);
        this.up = up; this.down = down; this.left = left; this.right = right; this.shoot = shoot;
    }

    public void Update(float time, List<Bullet> bullets)
    {
        if (Raylib.IsKeyDown(up)) Position.Y -= speed;
        if (Raylib.IsKeyDown(down)) Position.Y += speed;
        if (Raylib.IsKeyDown(left)) { Position.X -= speed; Direction = new Vector2(-1, 0); }
        if (Raylib.IsKeyDown(right)) { Position.X += speed; Direction = new Vector2(1, 0); }

        Position.X = Math.Clamp(Position.X, 0, 760);
        Position.Y = Math.Clamp(Position.Y, 0, 560);

        if (Raylib.IsKeyPressed(shoot) && time - lastShotTime > 1.0f)
        {
            bullets.Add(new Bullet(Position + Direction * 20, Direction, TankColor));
            lastShotTime = time;
        }
    }

    public void Draw()
    {
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 40, 40, TankColor);
        Raylib.DrawLine((int)Position.X + 20, (int)Position.Y + 20, (int)(Position.X + Direction.X * 30), (int)(Position.Y + Direction.Y * 30), Color.BLACK);
    }
}

class Bullet
{
    public Vector2 Position;
    public Vector2 Direction;
    private const float speed = 5.0f;
    private Color BulletColor;

    public Bullet(Vector2 position, Vector2 direction, Color color)
    {
        Position = position;
        Direction = direction;
        BulletColor = color;
    }

    public void Update()
    {
        Position += Direction * speed;
    }

    public bool OutOfBounds(int width, int height)
    {
        return Position.X < 0 || Position.X > width || Position.Y < 0 || Position.Y > height;
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)Position.X, (int)Position.Y, 5, BulletColor);
    }
}
