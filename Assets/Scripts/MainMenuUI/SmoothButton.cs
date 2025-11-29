using UnityEngine;
using UnityEngine.EventSystems;

public class SmoothButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 normalScale;
    private Vector3 targetScale;
    public float speed = 8f;

    void Start()
    {
        normalScale = transform.localScale;
        targetScale = normalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = normalScale * 1.07f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = normalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        targetScale = normalScale * 0.94f;
    }
}
