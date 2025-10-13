using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StartUI : MonoBehaviour
{
    public static StartUI instance;

    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject DisconnectButton;
    [SerializeField] private GameObject HUD;

    [SerializeField] private Slider Slider;
    [SerializeField] private TextMeshProUGUI SliderText;

    [SerializeField] private FlexibleColorPicker picker;

    [SerializeField] private RawImage mainScreen;

    public event Action playerUpdatedEvent;

    void Start()
    {
        instance = this;

        // This is bad
        picker.onColorChange.AddListener((Color lColor) =>
        {
            ClientData.playerColor = lColor;
            UpdatePlayerState();
        });

        Slider.onValueChanged.AddListener((float lfloat) => 
        { 
            ClientData.sensibility = lfloat;
            SliderText.text = lfloat.ToString();
            UpdatePlayerState();
        });
    }

    public void UpdatePlayerState()
    {
        playerUpdatedEvent?.Invoke(); // Use this method to invoke the playerUpdateEvent outside this UI
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hidemenu(!HUD.activeSelf);
        }
    }

    private void Hidemenu(bool lState)
    {
        if (lState)
        {
            Panel.transform.localScale = Vector3.zero;
            HUD.SetActive(true);
        }
        else
        {
            Panel.transform.localScale = Vector3.one;
            HUD.SetActive(false);
        }
    }

    public void PlayerConnect()
    {
        Hidemenu(true);
        DisconnectButton.SetActive(true);
        HUD.SetActive(true);
        mainScreen.gameObject.SetActive(false);
    }

    public void PlayerDisconnect()
    {
        Hidemenu(false);
        DisconnectButton.SetActive(false);
        HUD.SetActive(false);
        mainScreen.gameObject.SetActive(true);
    }
}
