using Microsoft.AspNetCore.Components;
using PersonalPageWASM.Models.Tetris;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace PersonalPageWASM.Services
{
    public class TetrisGameService
    {
        public GameBoard GameBoard { get; private set; }
        public Stack<Shape> MergedShapes { get; set; }
        public GameState State { get; set; }

        public event Action UpdateUI;

        public bool GameIsRunning { get; set; }

        public TetrisGameService()
        {
            GameBoard = new GameBoard();
            MergedShapes = new Stack<Shape>();
            State = new GameState();
        }

        public async Task NewGame()
        {
            GameIsRunning = true;
            GameBoard = new GameBoard();
            MergedShapes = new Stack<Shape>();
            State = new GameState();
            State.CurrentShape = GenerateNewShape();
            State.NextShape = GenerateNewShape();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            State.ElapsedTime = sw.Elapsed;

            while (!State.IsGameOver)
            {
                if(GameBoard.IsMovePossible(State.CurrentShape, MoveDirection.down))
                {
                    State.CurrentShape.MoveShape(MoveDirection.down);
                    if (!State.CurrentShape.Dropped)
                    {
                        await Task.Delay(500);
                    }
                }
                else
                {
                    MergedShapes.Push(State.CurrentShape);
                    State.Score += 10;
                    foreach (var cell in State.CurrentShape.Cells)
                    {
                        GameBoard.Board[cell.Row - 1, cell.Col - 1].IsOccupied = true;
                        GameBoard.Board[cell.Row - 1, cell.Col - 1].ShapeType = State.CurrentShape.ShapeType;
                    }
                    if(MergedShapes.Min(s => s.Cells.Min(c => c.Row)) <= 4 || State.ElapsedTime > new TimeSpan(0,59,0))
                    {
                        State.IsGameOver = true;
                        GameIsRunning = false;
                        sw.Stop();
                    }
                    State.CurrentShape = State.NextShape;
                    State.NextShape = GenerateNewShape();
                }
                State.ElapsedTime = sw.Elapsed;
                UpdateUI?.Invoke();
            }
        }

        public Shape GenerateNewShape()
        {
            Random random = new Random();
            int shapeIndex = random.Next(0, 6);

            ShapeEnum shapeType = (ShapeEnum)shapeIndex;
            Shape shape = new Shape(shapeType);

            return shape;
        }
    }
}
