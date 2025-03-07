
using Raylib_cs;
using System.Runtime.CompilerServices;

static class Collisions
{
    public static bool Hit(Ball b, Player p)
    {
        return Raylib.CheckCollisionCircleRec(b.Position, b.Size, p.Paddle);
    }
}

