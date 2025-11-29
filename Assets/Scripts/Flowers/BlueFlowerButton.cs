using UnityEngine;

public class BlueFlowerButton : MonoBehaviour
{
    public FlowerPuzzleManager puzzle;

    public void OnClick()
    {
        // пример: синий меняет себя и жёлтый
        puzzle.ToggleFlower(puzzle.blueAnimator);
        puzzle.ToggleFlower(puzzle.yellowAnimator);

        puzzle.CheckWin();
    }
}
