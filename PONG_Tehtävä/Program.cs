using Raylib_cs;
using System.Numerics;

class Pong
{
    // static == luokkamuuttuja
    public static int ScreenWidth = 800;
    public static int ScreenHeight = 450;

    static void Main(string[] args)
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Pong");
        Raylib.SetTargetFPS(60);

        // p1 is on the left side
        Player p1 = new Player {
            UpKey = KeyboardKey.W,
            DownKey = KeyboardKey.S
        };
        p1.SetPaddleX(50);

        // p2 is on the right side
        Player p2 = new Player
        {
            UpKey = KeyboardKey.Up,
            DownKey = KeyboardKey.Down
        };
        p2.SetPaddleX(ScreenWidth - 50);

        // pallo
        Ball ball = new Ball { Size = 15.0f};

        while (!Raylib.WindowShouldClose())
        {
            p1.MoveMaybe();
            p2.MoveMaybe();

            // Pallon liikkuminen
            ball.Move();

            // Osuminen mailoihin
            if (Collisions.Hit(ball, p1) || Collisions.Hit(ball, p2))
            {
                ball.BounceX();
            }
            // Check if ball is on the left of p1 paddle position
            else if (ball.Position.X < p1.Paddle.Position.X)
            {
                // opponent gets a point
                p2.Score++;
                ball.Reset();
            }
            // Check if ball is on the right of p2 paddle position
            else if (ball.Position.X > p2.Paddle.Position.X)
            {
                // opponent gets a point
                p1.Score++;
                ball.Reset();
            }

            GameHelper.Draw(ball, p1, p2);
        }

        Raylib.CloseWindow();
    }
}
