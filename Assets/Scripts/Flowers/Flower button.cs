using UnityEngine;

public class Flowerbutton : MonoBehaviour
{
    public Animator animator;
    private bool isOpen = false;

    public void ToggleFlower()
    {
        if (animator == null) return;

        isOpen = !isOpen;
        animator.SetBool("IsOpen", isOpen);
    }
}