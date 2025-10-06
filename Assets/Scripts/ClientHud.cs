using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class ClientHud : MonoBehaviour
{
    [SerializeField] private Button HostButton;
    [SerializeField] private Button JoinButton;
    [SerializeField] private TMP_InputField IPField;
    [SerializeField] private TMP_InputField PortField;

    public bool ReloadBool {  get; private set; } = false;

    void Start()
    {
        HostButton.onClick.AddListener(OnHostPress);
        JoinButton.onClick.AddListener(OnJoinPress);
    }

    private void OnHostPress()
    {
        if(ReloadBool) return;
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(IPField.text, ushort.Parse(PortField.text));
        NetworkManager.Singleton.StartHost();
    }

    private void OnJoinPress()
    {
        if(ReloadBool) return;
        NetworkTransport lNetwork = NetworkManager.Singleton.gameObject.GetComponent<NetworkTransport>();
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(IPField.text,ushort.Parse(PortField.text));
        NetworkManager.Singleton.StartClient();
    }
}
