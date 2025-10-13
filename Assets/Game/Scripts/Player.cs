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

    private MeshRenderer playerRenderer;

    public NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>
    (
        default,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<Color32> PlayerColor = new NetworkVariable<Color32>
    (
        default,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    void Awake()
    {
        playerRenderer = GetComponent<MeshRenderer>();
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

            playerCamera.gameObject.SetActive(true); // UDP camera sucks
        }
        else
        {
            playerCamera.gameObject.SetActive(false);
        }

        UpdatePlayer();

        playerRenderer.material = new Material(mat);
        playerRenderer.material.color = PlayerColor.Value;

        nameTag.text = PlayerName.Value.ToString();

        PlayerName.OnValueChanged += (oldName, newName) => 
        {
                nameTag.text = newName.ToString();

                if (IsOwner) //Triggers the nameChange for everyone
                { 
                    nameTag.text += "\n(you)"; 
                }
        };

        PlayerColor.OnValueChanged += (oldColor, newColor) => 
        {
                playerRenderer.material.color = newColor;
        };


        if (IsOwner) { nameTag.text += "\n(you)"; }
    }

    public void UpdatePlayer()
    {
        if (IsOwner)
        {
            PlayerColor.Value = ClientData.playerColor;

            string lLocalName = PlayerPrefs.GetString("PlayerName", $"Player{OwnerClientId}");
            PlayerName.Value = lLocalName;
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
