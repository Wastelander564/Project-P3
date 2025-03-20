using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordChecker : MiniGame
{
    public TMP_InputField inputField;  // Input veld van de speler
    public string correctCode = "NOTAROBOT";  // De juiste code

    void Start()
    {
        inputField.onEndEdit.AddListener(CheckCode);  // Voegt een event toe voor als speler op Enter drukt
    }

    void CheckCode(string input)
    {
        if (input.ToUpper().Replace(" ", "") == correctCode.Replace(" ", ""))//zet alles naar hoofletters en trimt de spaties weg
        {
            Debug.Log("Correcte code! Minigame sluit.");
            Win();
        }
        else
        {
            lives--;
        }

        inputField.text = "";  // Maak het invoerveld leeg na invoer
    }
}
