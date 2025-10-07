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
        transform.position += MoveSpeed * Time.deltaTime * new Vector3
            (
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            ); 
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            PlayerColor.Value = Random.ColorHSV();

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
        };

        PlayerColor.OnValueChanged += (oldColor, newColor) => {
                GetComponent<MeshRenderer>().material.color = newColor;
        };


        if (IsOwner) { nameTag.text += " (you)"; }
    }
}
