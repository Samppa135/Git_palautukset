using Raylib_cs;
using System.Numerics;

class Pong
{
    const int screenWidth = 800;
    const int screenHeight = 450;

    static void Main()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "Pong");
        Raylib.SetTargetFPS(60);

        // Pelaajien asetukset
        float paddleSpeed = 5.0f;
        Vector2 paddleSize = new Vector2(20, 100);

        // Pelaajat
        Rectangle player1 = new Rectangle(50, screenHeight / 2 - paddleSize.Y / 2, paddleSize.X, paddleSize.Y);
        Rectangle player2 = new Rectangle(screenWidth - 50 - paddleSize.X, screenHeight / 2 - paddleSize.Y / 2, paddleSize.X, paddleSize.Y);

        // Pisteet
        int player1Score = 0;
        int player2Score = 0;

        // Pallo
        Vector2 ballPosition = new Vector2(screenWidth / 2, screenHeight / 2);
        Vector2 ballSpeed = new Vector2(4.0f, 4.0f);
        float ballSize = 10.0f;

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsKeyDown(KeyboardKey.W) && player1.Y > 0)
                player1.Y -= paddleSpeed;

            if (Raylib.IsKeyDown(KeyboardKey.S) && player1.Y < screenHeight - paddleSize.Y)
                player1.Y += paddleSpeed;

            if (Raylib.IsKeyDown(KeyboardKey.Up))
                player2.Y -= paddleSpeed;

            if (Raylib.IsKeyDown(KeyboardKey.Down))
                player2.Y += paddleSpeed;

            // Pallon liikkuminen
            ballPosition.X += ballSpeed.X;
            ballPosition.Y += ballSpeed.Y;

            // Osuminen mailoihin
            if (Raylib.CheckCollisionCircleRec(ballPosition, ballSize, player1) ||
                Raylib.CheckCollisionCircleRec(ballPosition, ballSize, player2))
            {
                ballSpeed.X *= -1;
            }

            // Osuminen seiniin
            if (ballPosition.Y <= 0 || ballPosition.Y >= screenHeight) ballSpeed.Y *= -1;

            // Pisteiden lasku
            if (ballPosition.X <= 0)
            {
                player2Score++;
                ballPosition = new Vector2(screenWidth / 2, screenHeight / 2);
                ballSpeed.X *= -1;
            }
            if (ballPosition.X >= screenWidth)
            {
                player1Score++;
                ballPosition = new Vector2(screenWidth / 2, screenHeight / 2);
                ballSpeed.X *= -1;
            }

            // Piirtäminen
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawRectangleRec(player1, Color.White);
            Raylib.DrawRectangleRec(player2, Color.White);
            Raylib.DrawCircleV(ballPosition, ballSize, Color.White);

            Raylib.DrawText(player1Score.ToString(), screenWidth / 4, 20, 40, Color.White);
            Raylib.DrawText(player2Score.ToString(), 3 * screenWidth / 4, 20, 40, Color.White);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
