using System.Numerics;

class Ball
{
    // Ball olion instanssimuuttujat 
    public float Size = 2.0f; // default small ball
    public Vector2 Speed = new Vector2(4.0f, 4.0f);
    public Vector2 Position = new Vector2(Pong.ScreenWidth / 2, Pong.ScreenHeight / 2);

    public void Move()
    {
        // Osuminen kattoon tai lattiaan
        if (Position.Y <= 0 || Position.Y >= Pong.ScreenHeight)
        {
            this.BounceY();
        }

        this.Position.X += this.Speed.X;
        this.Position.Y += this.Speed.Y;
    }

    public void BounceX()
    {
        this.Speed.X *= -1;
    }

    public void BounceY()
    {
        this.Speed.Y *= -1;
    }

    public void Reset()
    {
        this.Position = new Vector2(Pong.ScreenWidth / 2, Pong.ScreenHeight / 2);
        this.Speed.X *= -1;
    }
}

