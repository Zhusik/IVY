using UnityEngine;

public class FlowerPuzzleManager : MonoBehaviour
{
    [Header("Аниматоры всех цветков")]
    public Animator redAnimator;
    public Animator blueAnimator;
    public Animator yellowAnimator;
    public Animator purpleAnimator;
    public Animator Ive_Card;

    public GameObject redFlowerObject;
    public GameObject blueFlowerObject;
    public GameObject yellowFlowerObject;
    public GameObject purpleFlowerObject;

    public GameObject Vine;

    [Header("Имя bool-параметра в Animator")]
    public string boolName = "IsOpen";
    public string exit = "exit";

    private bool puzzleCompleted = false;

    // Переключает состояние одного цветка
    public void ToggleFlower(Animator anim)
    {
        if (anim == null) return;

        bool current = anim.GetBool(boolName);
        anim.SetBool(boolName, !current);
    }

    // Проверка: все ли открыты
    public void CheckWin()
    {
        if (puzzleCompleted) return;

        bool allOpen =
            redAnimator.GetBool(boolName) &&
            blueAnimator.GetBool(boolName) &&
            yellowAnimator.GetBool(boolName) &&
            purpleAnimator.GetBool(boolName);

        if (allOpen)
        {
            puzzleCompleted = true;

            // Запускаем анимацию двери
            if (Ive_Card != null)
                Ive_Card.SetBool(boolName, true);

            if (redFlowerObject != null) redFlowerObject.SetActive(false);
            if (blueFlowerObject != null) blueFlowerObject.SetActive(false);
            if (yellowFlowerObject != null) yellowFlowerObject.SetActive(false);
            if (purpleFlowerObject != null) purpleFlowerObject.SetActive(false);
        }

    }
}
