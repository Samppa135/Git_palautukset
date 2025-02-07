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
    static Rectangle wall = new Rectangle(350, 200, 100, 200);

    static void Main()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "Tanks");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            float time = (float)Raylib.GetTime();
            player1.Update(time, bullets, wall);
            player2.Update(time, bullets, wall);
            UpdateBullets();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            player1.Draw();
            player2.Draw();
            foreach (var bullet in bullets) bullet.Draw();
            Raylib.DrawRectangleRec(wall, Color.Gray);

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    static void UpdateBullets()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            bullets[i].Update();
            if (bullets[i].OutOfBounds(screenWidth, screenHeight) || Raylib.CheckCollisionPointRec(bullets[i].Position, wall))
            {
                bullets.RemoveAt(i);
            }
        }
    }
}

class Tank
{
    public Vector2 Position;
    public float Rotation;
    public Color TankColor;
    private float lastShotTime;
    private const float speed = 2.0f;
    private KeyboardKey up, down, left, right, shoot;

    public Tank(Vector2 position, Color color, KeyboardKey up, KeyboardKey down, KeyboardKey left, KeyboardKey right, KeyboardKey shoot)
    {
        Position = position;
        TankColor = color;
        this.up = up; this.down = down; this.left = left; this.right = right; this.shoot = shoot;
    }

    public void Update(float time, List<Bullet> bullets, Rectangle wall)
    {
        Vector2 oldPosition = Position;
        float oldRotation = Rotation;

        if (Raylib.IsKeyDown(left)) Rotation -= 2.0f;
        if (Raylib.IsKeyDown(right)) Rotation += 2.0f;

        Vector2 direction = new Vector2(MathF.Cos(Rotation * MathF.PI / 180), MathF.Sin(Rotation * MathF.PI / 180));
        if (Raylib.IsKeyDown(up)) Position += direction * speed;
        if (Raylib.IsKeyDown(down)) Position -= direction * speed;

        if (Raylib.IsKeyDown(shoot) && time - lastShotTime > 1.0f)
        {
            bullets.Add(new Bullet(Position + direction * 20, direction, TankColor));
            lastShotTime = time;
        }

        if (Raylib.CheckCollisionRecs(new Rectangle(Position.X - 20, Position.Y - 20, 40, 40), wall))
        {
            Position = oldPosition;
            Rotation = oldRotation;
        }
    }

    public void Draw()
    {
        Raylib.DrawRectanglePro(new Rectangle(Position.X, Position.Y, 40, 40), new Vector2(20, 20), Rotation, TankColor);
        Raylib.DrawRectanglePro(new Rectangle(Position.X, Position.Y, 20, 20), new Vector2(10, 10), Rotation, Color.DarkGray);
        Vector2 barrelEnd = new Vector2(Position.X + MathF.Cos(Rotation * MathF.PI / 180) * 30, Position.Y + MathF.Sin(Rotation * MathF.PI / 180) * 30);
        Raylib.DrawLine((int)Position.X, (int)Position.Y, (int)barrelEnd.X, (int)barrelEnd.Y, Color.Black);
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
