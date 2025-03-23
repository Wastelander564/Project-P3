using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageLog : MonoBehaviour
{
    public GameObject messagePrefab;  // De prefab die een TextMeshProUGUI bevat
    public Transform contentTransform;  // De content van de ScrollView

    public void AddCraftingMessage(string message, string color)
    {
        // Creëer een nieuw bericht object vanuit de prefab
        GameObject newMessage = Instantiate(messagePrefab, contentTransform);

        // Stel de tekst in via TextMeshProUGUI
        TextMeshProUGUI textComponent = newMessage.GetComponent<TextMeshProUGUI>();
        textComponent.text = message;

        if (ColorUtility.TryParseHtmlString(color, out Color parsedColor))
        {
            textComponent.color = parsedColor;
        }

        // Zet het bericht helemaal naar de voorgrond binnen hetzelfde Canvas
        newMessage.transform.SetAsLastSibling();
        // Start coroutine om het bericht na een vertraging te verwijderen, ongeacht de Time.timeScale
        StartCoroutine(RemoveMessageAfterRealtime(newMessage, 3f));
    }

    public void DeleteMessages()
    {
        for (int i = contentTransform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(contentTransform.GetChild(i).gameObject);
        }
    }

    // Coroutine om bericht te verwijderen na de opgegeven tijd, onafhankelijk van Time.timeScale
    private IEnumerator RemoveMessageAfterRealtime(GameObject messageObject, float delay)
    {
        // Wacht de opgegeven tijd in echte seconden, onafhankelijk van de tijdschaal
        yield return new WaitForSecondsRealtime(delay);

        // Verwijder het bericht uit de ScrollView
        Destroy(messageObject);
    }
}
