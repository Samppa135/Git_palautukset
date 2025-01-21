using System;
using Raylib_cs;
using System.Numerics;

class Program
{
    static void Main()
    {
        // Asetetaan ikkunan koko
        const int screenWidth = 800;
        const int screenHeight = 600;
        Raylib.InitWindow(screenWidth, screenHeight, "DVD Bouncing Text");

        // Alustetaan tekstin sijainti ja suunta
        Vector2 position = new Vector2(screenWidth / 2, screenHeight / 2);
        Vector2 direction = new Vector2(1, 1);
        float speed = 200.0f;

        // Haetaan fontti ja lasketaan tekstin koko
        Raylib_cs.Font font = Raylib.GetFontDefault();
        string text = "DVD";
        float fontSize = 50;
        float spacing = 2;
        Vector2 textSize = Raylib.MeasureTextEx(font, text, fontSize, spacing);

        Raylib_cs.Color color = Raylib_cs.Color.Yellow;

        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
            position += direction * speed * deltaTime;

            // Törmäystarkistus oikea reuna
            if (position.X + textSize.X >= screenWidth)
            {
                position.X = screenWidth - textSize.X;
                direction.X *= -1;
                color = RandomColor();
                speed += 10;
            }
            // Törmäystarkistus vasen reuna
            else if (position.X <= 0)
            {
                position.X = 0;
                direction.X *= -1;
                color = RandomColor();
                speed += 10;
            }
            // Törmäystarkistus alareuna
            if (position.Y + textSize.Y >= screenHeight)
            {
                position.Y = screenHeight - textSize.Y;
                direction.Y *= -1;
                color = RandomColor();
                speed += 10;
            }
            // Törmäystarkistus yläreuna
            else if (position.Y <= 0)
            {
                position.Y = 0;
                direction.Y *= -1;
                color = RandomColor();
                speed += 10;
            }

            // Piirrä sisältö
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib_cs.Color.Black);
            Raylib.DrawTextEx(font, text, position, fontSize, spacing, color);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    static Raylib_cs.Color RandomColor()
    {
        Random rand = new Random();
        return new Raylib_cs.Color(rand.Next(256), rand.Next(256), rand.Next(256), 255);
    }
}
