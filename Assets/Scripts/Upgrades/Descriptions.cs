using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class descriptions : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI textToChange;  // Sleep hier je tekstveld in
    public string hoverText = "Je hovert over de knop!";

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textToChange != null)
            textToChange.text = hoverText;
    }
}
