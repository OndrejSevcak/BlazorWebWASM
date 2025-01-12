namespace PersonalPageWASM.Models.Tetris
{
    public class Shape
    {
        public List<Cell> Cells { get; set; }
        public ShapeEnum ShapeType { get; set; }
        public string Color { get; set; }
        public bool Rotated { get; set; }
        public bool Dropped { get; set; }

        public Shape(ShapeEnum shapeType)
        {
            ShapeType = shapeType;
            Cells = new List<Cell>();
            Initialize();
        }

        private void Initialize()
        {
            switch (ShapeType)
            {       
                case ShapeEnum.I:
                    Cells.Add(new Cell(1, 5));
                    Cells.Add(new Cell(2, 5));
                    Cells.Add(new Cell(3, 5));
                    Cells.Add(new Cell(4, 5));
                    Color = "bg-info";
                    break;

                case ShapeEnum.J:
                    Cells.Add(new Cell(1, 5));
                    Cells.Add(new Cell(2, 5));
                    Cells.Add(new Cell(3, 5));
                    Cells.Add(new Cell(3, 4));
                    Color = "bg-primary";
                    break;

                case ShapeEnum.L:
                    Cells.Add(new Cell(1, 5));
                    Cells.Add(new Cell(2, 5));
                    Cells.Add(new Cell(3, 5));
                    Cells.Add(new Cell(3, 6));
                    Color = "bg-orange";
                    break;

                case ShapeEnum.O:
                    Cells.Add(new Cell(1, 5));
                    Cells.Add(new Cell(2, 5));
                    Cells.Add(new Cell(1, 6));
                    Cells.Add(new Cell(2, 6));
                    Color = "bg-warning";
                    break;

                case ShapeEnum.S:
                    Cells.Add(new Cell(1, 5));
                    Cells.Add(new Cell(1, 6));
                    Cells.Add(new Cell(2, 5));
                    Cells.Add(new Cell(2, 4));
                    Color = "bg-success";
                    break;

                case ShapeEnum.T:
                    Cells.Add(new Cell(1, 5));
                    Cells.Add(new Cell(2, 4));
                    Cells.Add(new Cell(2, 5));
                    Cells.Add(new Cell(2, 6));
                    Color = "bg-purple";
                    break;

                case ShapeEnum.Z:
                    Cells.Add(new Cell(1, 4));
                    Cells.Add(new Cell(1, 5));
                    Cells.Add(new Cell(2, 5));
                    Cells.Add(new Cell(2, 6));
                    Color = "bg-danger";
                    break;

                default:
                    break;
            }
        }

        public void MoveShape(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.down:
                    foreach (var cell in Cells)
                    {
                        cell.Row++;
                    }
                    break;
                case MoveDirection.left:
                    foreach (var cell in Cells)
                    {
                        cell.Col--;
                    }
                    break;
                case MoveDirection.right:
                    foreach (var cell in Cells)
                    {
                        cell.Col++;
                    }
                    break;
                case MoveDirection.bottom:
                    break;
                default:
                    break;
            }
        }
    }
}
