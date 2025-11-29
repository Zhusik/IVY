using UnityEngine;

public class YellowFlowerButton : MonoBehaviour
{
    public FlowerPuzzleManager puzzle;

    public void OnClick()
    {
        // пример: жёлтый меняет себя и фиолетовый
        puzzle.ToggleFlower(puzzle.yellowAnimator);
        puzzle.ToggleFlower(puzzle.purpleAnimator);

        puzzle.CheckWin();
    }
}
