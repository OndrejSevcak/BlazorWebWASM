namespace PersonalPageWASM.Models.Tetris
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsOccupied { get; set; }
        public ShapeEnum? ShapeType { get; set; }

        public Cell(int row, int col, bool isOccupied = false, ShapeEnum? type = null) 
        {
            Row = row;
            Col = col;
            IsOccupied = isOccupied;
            ShapeType = type;
        }
    }
}
