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
    [SerializeField] private Button DisconnectButton;

    [SerializeField] private Text log;

    public bool ReloadBool {  get; private set; } = false;

    void Start()
    {
        DisconnectButton.gameObject.SetActive(false);

        HostButton.onClick.AddListener(OnHostPress);
        JoinButton.onClick.AddListener(OnJoinPress);
        DisconnectButton.onClick.AddListener(OnDisconnect);
        NetworkManager.Singleton.OnClientConnectedCallback += SingletonOnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += SingletonOnClientDisconnectCallback;
    }

    private void OnDisconnect()
    {
        NetworkManager.Singleton.Shutdown(true);
    }

    private void SingletonOnClientDisconnectCallback(ulong obj)
    {
        log.text += obj + " Disconected \n";

        if(obj != NetworkManager.Singleton.LocalClientId) return;
        HostButton.gameObject.SetActive(true);
        JoinButton.gameObject.SetActive(true);
        IPField.gameObject.SetActive(true);
        PortField.gameObject.SetActive(true);

        DisconnectButton.gameObject.SetActive(false);
    }

    private void SingletonOnClientConnectedCallback(ulong obj)
    {
        log.text += obj + " Connected \n";

        if (obj != NetworkManager.Singleton.LocalClientId) return;

        HostButton.gameObject.SetActive(false);
        JoinButton.gameObject.SetActive(false);
        IPField.gameObject.SetActive(false);
        PortField.gameObject.SetActive(false);

        DisconnectButton.gameObject.SetActive(true);
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
