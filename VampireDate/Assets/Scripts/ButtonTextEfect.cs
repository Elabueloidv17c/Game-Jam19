using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Text))]
public class ButtonTextEfect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler {

    private Text textUI;
    public Color normalColor;
    public Color hoverColor;
    public Color clickColor;

    void Start()
    {
        textUI = GetComponent<Text>();
        //GetComponentInParent<Button>().onClick.AddListener(() => { textUI.color = clickColor; });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textUI.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
            textUI.color = clickColor;
        else
            textUI.color = hoverColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        textUI.color = clickColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        textUI.color = hoverColor;
        GetComponentInParent<Button>().OnPointerClick(eventData);
    }
}
