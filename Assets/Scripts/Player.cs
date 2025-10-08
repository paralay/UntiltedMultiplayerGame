using System.Drawing;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public float MoveSpeed { get; private set; } = 10f;

    [SerializeField] private Material mat;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Text nameTag;

    public NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>(
    default,
    NetworkVariableReadPermission.Everyone,
    NetworkVariableWritePermission.Owner);

    public NetworkVariable<Color32> PlayerColor = new NetworkVariable<Color32>(
        default,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    void Start()
    {

    }
    void Update()
    {
        transform.position += MoveSpeed * Time.deltaTime * 
            transform.TransformDirection(
            new Vector3
            (
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            )
        );
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            StartUI.instance.PlayerConnect();
            StartUI.instance.playerUpdatedEvent += UpdatePlayer;
            PlayerColor.Value = clientData.playerColor;

            

            string localName = PlayerPrefs.GetString("PlayerName", $"Player{OwnerClientId}");
            PlayerName.Value = localName;
            playerCamera.gameObject.SetActive(true);
        }
        else
        {
            playerCamera.gameObject.SetActive(false);
        }

        GetComponent<MeshRenderer>().material = new Material(mat);
        GetComponent<MeshRenderer>().material.color = PlayerColor.Value;

        nameTag.text = PlayerName.Value.ToString();

        PlayerName.OnValueChanged += (oldName, newName) => {
                nameTag.text = newName.ToString();
                if (IsOwner) { nameTag.text += "\n(you)"; 
            }
        };

        PlayerColor.OnValueChanged += (oldColor, newColor) => {
                GetComponent<MeshRenderer>().material.color = newColor;
        };


        if (IsOwner) { nameTag.text += "\n(you)"; }
    }

    public void UpdatePlayer()
    {
        if (IsOwner)
        {
            PlayerColor.Value = clientData.playerColor;

            string localName = PlayerPrefs.GetString("PlayerName", $"Player{OwnerClientId}");
            PlayerName.Value = localName;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsOwner)
        {
            StartUI.instance.playerUpdatedEvent -= UpdatePlayer;
            StartUI.instance.PlayerDisconnect();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerCamera.gameObject.SetActive(false);
        }
    }
}
