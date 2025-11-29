using UnityEngine;

public class PurpleFlowerButton : MonoBehaviour
{
    public FlowerPuzzleManager puzzle;

    public void OnClick()
    {
        // пример: фиолетовый меняет только себя
        puzzle.ToggleFlower(puzzle.purpleAnimator);

        puzzle.CheckWin();
    }
}
