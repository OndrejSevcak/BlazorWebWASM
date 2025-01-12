using Microsoft.AspNetCore.Components;
using PersonalPageWASM.Services;

namespace PersonalPageWASM.Pages
{
    public partial class Tetris : ComponentBase
    {
        [Inject]
        private TetrisGameService _service { get; set; }

        protected override void OnInitialized()
        {
            _service.UpdateUI += UpdateUI;   
        }

        private void LogCell(int row, int col)
        {
            Console.WriteLine($"Clicked row {row} col {col}");
        }

        private void StartNewGame()
        {
            _service.NewGame();
        }

        private void MoveLeft()
        {
            if(_service.GameBoard.IsMovePossible(_service.State.CurrentShape, Models.Tetris.MoveDirection.left))
            {
                _service.State.CurrentShape.MoveShape(Models.Tetris.MoveDirection.left);
                StateHasChanged();
            }    
        }

        private void MoveRight()
        {
            if (_service.GameBoard.IsMovePossible(_service.State.CurrentShape, Models.Tetris.MoveDirection.right))
            {
                _service.State.CurrentShape.MoveShape(Models.Tetris.MoveDirection.right);
                StateHasChanged();
            }
        }

        private void Rotate()
        {
            var currentShape = _service.State.CurrentShape;
            _service.GameBoard.TryRotateShape(ref currentShape);
            _service.State.CurrentShape = currentShape;
        }

        private void Drop()
        {
            _service.State.CurrentShape.Dropped = true;
        }

        private void EndGame()
        {
            _service.State.IsGameOver = true;
            _service.GameIsRunning = false;
        }

        private void UpdateUI()
        {
            Console.WriteLine("Updating UI");
            StateHasChanged();
        }
    }
}
