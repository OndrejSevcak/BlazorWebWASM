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

        public void TryRotateShape(ref Shape shape)
        {
            Shape original = new Shape(shape.ShapeType)
            {
                Cells = shape.Cells.Select(c => new Cell(c.Row, c.Col)).ToList(),
                Color = shape.Color,
                Rotated = shape.Rotated
            };

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
                        shape.Cells[0].Row = row + 1;
                        shape.Cells[1].Row = row + 1;
                        shape.Cells[2].Row = row + 1;

                        shape.Cells[0].Col = col;
                        shape.Cells[1].Col = col + 1;
                        shape.Cells[2].Col = col + 2;

                        shape.Cells[3].Row = row;
                        shape.Cells[3].Col = col;

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
                        shape.Cells[0].Row = row;
                        shape.Cells[1].Row = row + 1;
                        shape.Cells[2].Row = row + 2;

                        shape.Cells[0].Col = col + 1;
                        shape.Cells[1].Col = col + 1;
                        shape.Cells[2].Col = col + 1;

                        shape.Cells[3].Row = row + 2;
                        shape.Cells[3].Col = col;

                        if (shape.Cells.Any(c => c.Col > _width))
                        {
                            shape = original;
                        }
                        else
                        {
                            shape.Rotated = false;
                        }
                    }
                    break;

                case ShapeEnum.L:
                    if (!shape.Rotated)
                    {
                        shape.Cells[0].Row = row + 1;
                        shape.Cells[1].Row = row + 1;
                        shape.Cells[2].Row = row + 1;

                        shape.Cells[0].Col = col - 1;
                        shape.Cells[1].Col = col;
                        shape.Cells[2].Col = col + 1;

                        shape.Cells[3].Row = row + 2;
                        shape.Cells[3].Col = col - 1;

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
                        shape.Cells[0].Row = row - 1;
                        shape.Cells[1].Row = row;
                        shape.Cells[2].Row = row + 1;

                        shape.Cells[0].Col = col + 1;
                        shape.Cells[1].Col = col + 1;
                        shape.Cells[2].Col = col + 1;

                        shape.Cells[3].Row = row + 1;
                        shape.Cells[3].Col = col + 2;

                        if (shape.Cells.Any(c => c.Col > _width))
                        {
                            shape = original;
                        }
                        else
                        {
                            shape.Rotated = false;
                        }
                    }
                    break;

                case ShapeEnum.S:
                    //Cells.Add(new Cell(1, 5));
                    //Cells.Add(new Cell(1, 6));
                    //Cells.Add(new Cell(2, 5));
                    //Cells.Add(new Cell(2, 4));

                    if (!shape.Rotated)
                    {
                        shape.Cells[0].Row = row;
                        shape.Cells[0].Col = col + 1;

                        shape.Cells[1].Row = row + 1;
                        shape.Cells[1].Col = col + 1;

                        shape.Cells[2].Row = row + 1;
                        shape.Cells[2].Col = col;

                        shape.Cells[3].Row = row + 2;
                        shape.Cells[3].Col = col;

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
                        shape.Cells[0].Row = row;
                        shape.Cells[0].Col = col + 1;

                        shape.Cells[1].Row = row;
                        shape.Cells[1].Col = col + 2;

                        shape.Cells[2].Row = row + 1;
                        shape.Cells[2].Col = col;

                        shape.Cells[3].Row = row + 1;
                        shape.Cells[3].Col = col + 1;

                        if (shape.Cells.Any(c => c.Col > _width))
                        {
                            shape = original;
                        }
                        else
                        {
                            shape.Rotated = false;
                        }
                    }

                    break;

                case ShapeEnum.Z:
                    //Cells.Add(new Cell(1, 4));
                    //Cells.Add(new Cell(1, 5));
                    //Cells.Add(new Cell(2, 5));
                    //Cells.Add(new Cell(2, 6));

                    if (!shape.Rotated)
                    {
                        shape.Cells[0].Row = row;
                        shape.Cells[0].Col = col + 1;

                        shape.Cells[1].Row = row + 1;
                        shape.Cells[1].Col = col + 1;

                        shape.Cells[2].Row = row + 1;
                        shape.Cells[2].Col = col;

                        shape.Cells[3].Row = row + 2;
                        shape.Cells[3].Col = col;

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
                        shape.Cells[0].Row = row;
                        shape.Cells[0].Col = col;

                        shape.Cells[1].Row = row;
                        shape.Cells[1].Col = col + 1;

                        shape.Cells[2].Row = row + 1;
                        shape.Cells[2].Col = col + 1;

                        shape.Cells[3].Row = row + 1;
                        shape.Cells[3].Col = col + 2;

                        if (shape.Cells.Any(c => c.Col > _width))
                        {
                            shape = original;
                        }
                        else
                        {
                            shape.Rotated = false;
                        }
                    }

                    break;

                case ShapeEnum.T:
                    //Cells.Add(new Cell(1, 5));
                    //Cells.Add(new Cell(2, 4));
                    //Cells.Add(new Cell(2, 5));
                    //Cells.Add(new Cell(2, 6));

                    if (!shape.Rotated)
                    {
                        shape.Cells[0].Row = row;
                        shape.Cells[0].Col = col + 1;

                        shape.Cells[1].Row = row + 1;
                        shape.Cells[1].Col = col + 1;

                        shape.Cells[2].Row = row + 1;
                        shape.Cells[2].Col = col + 2;

                        shape.Cells[3].Row = row + 2;
                        shape.Cells[3].Col = col + 1;

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
                        shape.Cells[0].Row = row;
                        shape.Cells[0].Col = col;

                        shape.Cells[1].Row = row + 1;
                        shape.Cells[1].Col = col - 1;

                        shape.Cells[2].Row = row + 1;
                        shape.Cells[2].Col = col;

                        shape.Cells[3].Row = row + 1;
                        shape.Cells[3].Col = col + 1;

                        if (shape.Cells.Any(c => c.Col > _width))
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
