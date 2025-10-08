using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInputField : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<InputField>().text = PlayerPrefs.GetString("PlayerName", "Player");
        GetComponent<InputField>().onValueChanged.AddListener(ValueChange);
    }

    void ValueChange(string pString)
    {
        PlayerPrefs.SetString("PlayerName", pString);
        StartUI.instance.UpdatePlayerState();
    }
}
