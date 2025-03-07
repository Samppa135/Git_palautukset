using Raylib_cs;
using System.Numerics;

class Player : IFormattable
{
    // Pelaajien asetukset
    public static float PaddleSpeed = 5.0f;
    public static Vector2 PaddleSize = new Vector2(20, 100);

    // nämä ovat instanssi muuttujia (luokan alla, ei static)
    public KeyboardKey UpKey;
    public KeyboardKey DownKey;
    public Rectangle Paddle;
    public int Score;

    public void SetPaddleX(int x)
    {
        this.Paddle = new Rectangle(x, Pong.ScreenHeight / 2 - PaddleSize.Y / 2, PaddleSize.X, PaddleSize.Y);
    }

    public void MoveMaybe()
    {
        if (Raylib.IsKeyDown(DownKey) && Paddle.Y < Pong.ScreenHeight - Paddle.Size.Y)
        {
            this.Paddle.Y += Player.PaddleSpeed;
        }
        else if (Raylib.IsKeyDown(UpKey) && Paddle.Y > 0)
        {
            this.Paddle.Y -= Player.PaddleSpeed; 
        }
    }


    public string ToString(string? format = null, IFormatProvider? formatProvider = null)
    {
        return this.Score.ToString();
    }

    public void ResetPlayerScore()
    {

        this.Score = 0;
    }
}

