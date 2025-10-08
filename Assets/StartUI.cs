using UnityEngine;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Timeline;
using System;

public class StartUI : MonoBehaviour
{
    public static StartUI instance;

    [SerializeField] GameObject Panel;
    [SerializeField] GameObject DisconnectButton;
    [SerializeField] GameObject HUD;

    [SerializeField] Slider Slider;
    [SerializeField] TextMeshProUGUI SliderText;

    [SerializeField] FlexibleColorPicker picker;

    [SerializeField] RawImage mainScreen;

    private Vector3 pos;

    public event Action playerUpdatedEvent;

    void Start()
    {
        pos = Panel.transform.position;
        instance = this;

        picker.onColorChange.AddListener((Color lColor) =>
        {
            clientData.playerColor = lColor;
            UpdatePlayerState();
        });

        Slider.onValueChanged.AddListener((float lfloat) => 
        { 
            clientData.sensibility = lfloat;
            SliderText.text = lfloat.ToString();
            UpdatePlayerState();
        });
    }

    public void UpdatePlayerState()
    {
        playerUpdatedEvent?.Invoke();
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
            pos = Panel.transform.position;
            Panel.transform.position = Vector3.one * -1000;
            HUD.SetActive(true);
        }
        else
        {
            Panel.transform.position = pos;
            HUD.SetActive(false);
        }
    }


    public void PlayerConnect()
    {
        Hidemenu(true);
        DisconnectButton.SetActive(true);
        HUD.SetActive(true);
        mainScreen.gameObject.SetActive(false);
        Debug.Log("enter");
    }

    public void PlayerDisconnect()
    {
        Hidemenu(false);
        DisconnectButton.SetActive(false);
        HUD.SetActive(false);
        mainScreen.gameObject.SetActive(true);
        Debug.Log("exit");
    }
}
