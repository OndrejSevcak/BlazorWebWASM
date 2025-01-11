namespace PersonalPageWASM.Models.Tetris
{
    public class GameBoard
    {
        private readonly int _width = 10;
        private readonly int _height = 20;

        public Cell[,] Board { get; init; }

        public GameBoard()
        {
            Board = new Cell[_height, _width];
            InitializeBoard();
        }

        public int Width => _width;
        public int Height => _height;

        public void InitializeBoard()
        {
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    Board[row, col] = new Cell(row, col);
                }
            }
        }

        public bool IsMovePossible(Shape shape, MoveDirection direction)
        {
            foreach (var cell in shape.Cells)
            {
                int newRow = cell.Row;
                int newCol = cell.Col;
                switch (direction)
                {
                    case MoveDirection.down:
                        newRow++;
                        break;
                    case MoveDirection.left:
                        newCol--;
                        break;
                    case MoveDirection.right:
                        newCol++;
                        break;
                }
                if (newRow > _height || newCol < 1 || newCol > _width)
                {
                    return false;
                }
                if (Board[newRow - 1, newCol - 1].IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }

        public void TryRotateShape(Shape shape)
        {
            Shape original = new Shape(shape.ShapeType);
            original.Cells = shape.Cells.Select(c => new Cell(c.Row, c.Col)).ToList();

            int row = shape.Cells.Min(c => c.Row);
            int col = shape.Cells.Min(c => c.Col);

            switch (shape.ShapeType)
            {
                case ShapeEnum.I:
                    if (!shape.Rotated)
                    {    
                        foreach (var cell in shape.Cells)
                        {
                            cell.Row = row;
                            cell.Col = col;
                            col++;
                        }
                        if(shape.Cells.Any(c => c.Col > _width))
                        {
                            shape = original;
                        }
                        else
                        {
                            shape.Rotated = true;
                        }
                    }
                    else
                    {
                        foreach (var cell in shape.Cells)
                        {
                            cell.Row = row;
                            cell.Col = col;
                            row++;
                        }
                        if (shape.Cells.Any(c => c.Row > _height))
                        {
                            shape = original;
                        }
                        else
                        {
                            shape.Rotated = false;
                        }
                    }
                    break;

                case ShapeEnum.J:
                    if (!shape.Rotated)
                    {
                        foreach (var cell in shape.Cells)
                        {
                            cell.Row = row;
                            cell.Col = col;
                            col++;
                        }
                        if (shape.Cells.Any(c => c.Col > _width))
                        {
                            shape = original;
                        }
                        else
                        {
                            shape.Rotated = true;
                        }
                    }
                    else
                    {
                        foreach (var cell in shape.Cells)
                        {
                            cell.Row = row;
                            cell.Col = col;
                            row++;
                        }
                        if (shape.Cells.Any(c => c.Row > _height))
                        {
                            shape = original;
                        }
                        else
                        {
                            shape.Rotated = false;
                        }
                    }
                    break;


                default:
                    break;
            }
        }
    }
}
