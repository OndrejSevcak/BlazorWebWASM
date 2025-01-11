namespace PersonalPageWASM.Models.Tetris
{
    public class GameState
    {
        public bool IsGameOver { get; set; }
        public Shape CurrentShape { get; set; }
        public Shape NextShape { get; set; }
    }
}
