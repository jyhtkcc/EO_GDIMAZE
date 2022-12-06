using System;
using System.Drawing;
using static Mazegame.Form1;

namespace Mazegame.Model
{
    public class Player
    {
        public readonly Maze Maze;
        public Point Position { get; set; } = new Point(1,1);
        public event Action Finish;


        public Player(Maze maze)
        {
            Maze = maze;
        }

        // dx,dy 다음 움직임 좌표값
        public void Move(int dx, int dy)
        {
            if (Maze[Position.X + dx, Position.Y + dy] != CellType.Wall)
            {
                Position = new Point(Position.X + dx, Position.Y + dy);
                
            }
            if (Position == Maze.EndPoint)
                Finish?.Invoke();
        }
    }
}
