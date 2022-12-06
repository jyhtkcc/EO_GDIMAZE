using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mazegame.Model;

namespace Mazegame
{
    public partial class Form1 : Form
    {
        private const int CELL_SIZE = 15;
        private Timer gameTimer;
        private Maze _maze;
        private Brush _wallBrush = Brushes.Black, playerBrush = Brushes.Red, finishBrush = Brushes.Blue;
 
        public enum CellType
        {
            Cell,
            Wall
        }

        public Form1()
        {
            NewGame();
            InitializeComponent();


            gameTimer = new Timer {Interval = 100};   
            gameTimer.Tick += GameTimerOnTick;
            gameTimer.Start();
       }

   
        private void NewGame()
        {
            var width = 20;
            var height = 20;

            if (width % 2 == 0)
            {
                width--;
            }

            if (height % 2 == 0)
            {
                height--;
            }

            _maze = new Maze(new Size(width, height));
            _maze.Generate();
            _maze.Player.Finish += () =>

               {
                var response = MessageBox.Show("탈출 성공! 재도전하시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (response == DialogResult.Yes)
                {
                    NewGame();
                }
            };
        }

        private void _newGameButton_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    _maze.Player.Move(0, -1);
                    break;

                case Keys.Down:
                    _maze.Player.Move(0, 1);
                    break;

                case Keys.Left:
                    _maze.Player.Move(-1, 0);
                    break;

                case Keys.Right:
                    _maze.Player.Move(1, 0);
                    break;
            }
        }

        // 플레이어 움직임의 화면을 실시간으로 갱신시킨다 .  
        private void GameTimerOnTick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;

            for (var i = 0; i < _maze.Size.Width; i++)

                for (var j = 0; j < _maze.Size.Height; j++)
                {
                    var cell = _maze[i, j];
                    switch (cell)
                    {
                        case CellType.Wall:
                            graphics.FillRectangle(_wallBrush,i * CELL_SIZE,  j * CELL_SIZE + 50, CELL_SIZE, CELL_SIZE);
                            break;

                        default:
                            break;
                    }
                }
            graphics.FillEllipse(playerBrush, _maze.Player.Position.X * CELL_SIZE, _maze.Player.Position.Y * CELL_SIZE + 50, CELL_SIZE,  CELL_SIZE);
            graphics.FillEllipse(finishBrush, _maze.EndPoint.X * CELL_SIZE,  _maze.EndPoint.Y * CELL_SIZE + 50,   CELL_SIZE,   CELL_SIZE);
        }
    }
}
