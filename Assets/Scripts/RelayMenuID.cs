using UnityEngine;
using TMPro;

public class RelayMenuUI : MonoBehaviour
{
    public TMP_InputField joinCodeInput;
    public TMP_Text joinCodeDisplay;
    private NetworkRelayManager relay;

    void Awake()
    {
        relay = Object.FindFirstObjectByType<NetworkRelayManager>();
    }

    public async void OnHostClicked()
    {
        string code = await relay.HostRelayAsync();
        joinCodeDisplay.text = $"Join Code: {code}";
    }

    public async void OnJoinClicked()
    {
        string code = joinCodeInput.text.Trim();
        if (string.IsNullOrEmpty(code)) return;

        await relay.JoinRelayAsync(code);
    }
}
