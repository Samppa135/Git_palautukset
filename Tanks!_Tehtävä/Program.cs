using System;
using System.Collections.Generic;
using Raylib_cs;

class Program
{
    static void Main()
    {
        const int screenWidth = 800;
        const int screenHeight = 600;
        Raylib.InitWindow(screenWidth, screenHeight, "Tanks Game");
        Raylib.SetTargetFPS(60);

        Tank player1 = new Tank(100, 250, Raylib_cs.Color.Blue, KeyboardKey.KEY_W, KeyboardKey.KEY_S, KeyboardKey.KEY_A, KeyboardKey.KEY_D, KeyboardKey.KEY_SPACE);
        Tank player2 = new Tank(600, 250, Raylib_cs.Color.Red, KeyboardKey.KEY_UP, KeyboardKey.KEY_DOWN, KeyboardKey.KEY_LEFT, KeyboardKey.KEY_RIGHT, KeyboardKey.KEY_ENTER);
        List<Bullet> bullets = new List<Bullet>();

        while (!Raylib.WindowShouldClose())
        {
            // Päivitä tankkien liike
            player1.Update(bullets);
            player2.Update(bullets);

            // Ammusten päivittäminen
            foreach (var bullet in bullets)
            {
                bullet.Update();
            }

            // Poistetaan ruudun ulkopuolelle menneet ammukset
            bullets.RemoveAll(b => !b.IsActive);

            // Piirrä peli
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib_cs.Color.Green);

            // Piirrä kentän seinät
            Raylib.DrawRectangle(300, 100, 50, 400, Raylib_cs.Color.Red);
            Raylib.DrawRectangle(450, 100, 50, 400, Raylib_cs.Color.Red);

            // Piirrä tankit ja ammukset
            player1.Draw();
            player2.Draw();
            foreach (var bullet in bullets)
            {
                bullet.Draw();
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}

class Tank
{
    public int X { get; set; }
    public int Y { get; set; }
    public Color Color { get; set; }
    public int Speed = 4;
    public int Direction = 1;
    private KeyboardKey up, down, left, right, shoot;

    public Tank(int x, int y, Color color, KeyboardKey up, KeyboardKey down, KeyboardKey left, KeyboardKey right, KeyboardKey shoot)
    {
        X = x;
        Y = y;
        Color = color;
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
        this.shoot = shoot;
    }

    public void Update(List<Bullet> bullets)
    {
        if (Raylib.IsKeyDown(up)) Y -= Speed;
        if (Raylib.IsKeyDown(down)) Y += Speed;
        if (Raylib.IsKeyDown(left)) { X -= Speed; Direction = -1; }
        if (Raylib.IsKeyDown(right)) { X += Speed; Direction = 1; }
        if (Raylib.IsKeyPressed(shoot)) bullets.Add(new Bullet(X + 20, Y + 15, Direction));
    }

    public void Draw()
    {
        Raylib.DrawRectangle(X, Y, 40, 40, Color);
        Raylib.DrawRectangle(X - 5, Y, 10, 40, Raylib_cs.Color.DarkBlue);
        Raylib.DrawRectangle(X + 35, Y, 10, 40, Raylib_cs.Color.DarkBlue);
        Raylib.DrawRectangle(X + 15, Y - 10, 10, 20, Raylib_cs.Color.Black);
    }
}

class Bullet
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Speed { get; set; }
    public bool IsActive { get; set; } = true;

    public Bullet(int x, int y, int direction)
    {
        X = x;
        Y = y;
        Speed = 8 * direction;
    }

    public void Update()
    {
        X += Speed;
        if (X > 800 || X < 0) IsActive = false;
    }

    public void Draw()
    {
        if (IsActive)
            Raylib.DrawRectangle(X, Y, 10, 5, Color.Black);
    }
}
