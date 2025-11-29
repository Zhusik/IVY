using UnityEngine;

public class RedFlowerButton : MonoBehaviour
{
    public FlowerPuzzleManager puzzle;

    public void OnClick()
    {
        // красный переключает КОГО УГОДНО — пример:
        puzzle.ToggleFlower(puzzle.redAnimator);    // себя
        puzzle.ToggleFlower(puzzle.blueAnimator);   // плюс синий

        puzzle.CheckWin();
    }
}
