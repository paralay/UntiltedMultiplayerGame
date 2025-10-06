using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float MoveSpeed { get; private set; } = 10f;

    [SerializeField] private Material mat;
    [SerializeField] private Camera playerCamera;

    void Start()
    {
        if(IsOwner) GetComponent<MeshRenderer>().material = mat;
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
            playerCamera.gameObject.SetActive(true);
        }
        else
        {
            playerCamera.gameObject.SetActive(false);
        }
    }
}
