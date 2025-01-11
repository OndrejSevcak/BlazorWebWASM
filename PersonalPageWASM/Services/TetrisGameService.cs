using Microsoft.AspNetCore.Components;
using PersonalPageWASM.Models.Tetris;
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

        public TetrisGameService()
        {
            GameBoard = new GameBoard();
            MergedShapes = new Stack<Shape>();
            State = new GameState();
        }

        public async Task NewGame()
        {
            GameBoard = new GameBoard();
            MergedShapes = new Stack<Shape>();
            State = new GameState();
            State.CurrentShape = GenerateNewShape();
            State.NextShape = GenerateNewShape();

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
                    foreach (var cell in State.CurrentShape.Cells)
                    {
                        GameBoard.Board[cell.Row - 1, cell.Col - 1].IsOccupied = true;
                        GameBoard.Board[cell.Row - 1, cell.Col - 1].ShapeType = State.CurrentShape.ShapeType;
                    }
                    State.CurrentShape = State.NextShape;
                    State.NextShape = GenerateNewShape();
                }
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
