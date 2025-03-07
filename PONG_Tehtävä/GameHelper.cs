using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class GameHelper
{
    public static void Draw(Ball ball, Player p1, Player p2)
    {
        // Piirtäminen
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        Raylib.DrawRectangleRec(p1.Paddle, Color.White);
        Raylib.DrawRectangleRec(p2.Paddle, Color.White);
        Raylib.DrawCircleV(ball.Position, ball.Size, Color.White);

        Raylib.DrawText(p1.ToString(), Pong.ScreenWidth / 4, 20, 40, Color.White);
        Raylib.DrawText(p2.ToString(), 3 * Pong.ScreenWidth / 4, 20, 40, Color.White);

        Raylib.EndDrawing();
    }
}
