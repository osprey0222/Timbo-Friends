
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler

{
    private Color m_InitColor;
    public UnityEvent OnClick;
    private void Start()
    {
        m_InitColor = GetComponent<Image>().color;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
        {
            OnClick.Invoke();

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(0f, 255f, 0f);
        GetComponentInChildren<CanvasGroup>().alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = m_InitColor;
        GetComponentInChildren<CanvasGroup>().alpha = 0f;
    }
}