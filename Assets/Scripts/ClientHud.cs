using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ClientHud : MonoBehaviour
{
    [SerializeField] private Button HostButton;
    [SerializeField] private Button JoinButton;

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
        NetworkManager.Singleton.StartClient();
    }
}
