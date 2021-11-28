using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Italic;
        this.GetComponentInChildren<TextMeshProUGUI>().fontSize += 10;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        this.GetComponentInChildren<TextMeshProUGUI>().fontSize -= 10;
    }
}
