using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class ClientHud : MonoBehaviour
{
    [SerializeField] private Button HostButton;
    [SerializeField] private Button JoinButton;
    [SerializeField] private TMP_InputField InputField;

    public bool IsActive {  get; private set; } = false;

    void Start()
    {
        HostButton.onClick.AddListener(OnHostPress);
        JoinButton.onClick.AddListener(OnJoinPress);
    }

    private void OnHostPress()
    {
        if(IsActive) return;
        NetworkManager.Singleton.StartHost();
    }

    private void OnJoinPress()
    {
        if(IsActive) return;
        NetworkTransport lNetwork = NetworkManager.Singleton.gameObject.GetComponent<NetworkTransport>();
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(InputField.text, 7777);
        NetworkManager.Singleton.StartClient();
    }
}
