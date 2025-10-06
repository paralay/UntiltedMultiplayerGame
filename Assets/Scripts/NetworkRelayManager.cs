using UnityEngine;
using Unity.Netcode;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using System.Threading.Tasks;

public class NetworkRelayManager : MonoBehaviour
{
    private async Task InitServicesAsync()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
            await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async Task<string> HostRelayAsync(int maxPlayers = 4)
    {
        await InitServicesAsync();

        Allocation alloc = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(alloc.AllocationId);
        Debug.Log($"Relay Join Code: {joinCode}");

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        try
        {
            transport.SetRelayServerData(new RelayServerData(alloc, "dtls"));
            Debug.Log("Using DTLS (secure) Relay connection");
        }
        catch
        {
            transport.SetRelayServerData(new RelayServerData(alloc, "udp"));
            Debug.LogWarning("DTLS failed, falling back to UDP");
        }

        NetworkManager.Singleton.StartHost();
        return joinCode;
    }

    public async Task JoinRelayAsync(string joinCode)
    {
        await InitServicesAsync();

        JoinAllocation joinAlloc = await RelayService.Instance.JoinAllocationAsync(joinCode);
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        try
        {
            transport.SetRelayServerData(new RelayServerData(joinAlloc, "dtls"));
            Debug.Log("Client using DTLS (secure)");
        }
        catch
        {
            transport.SetRelayServerData(new RelayServerData(joinAlloc, "udp"));
            Debug.LogWarning("Client fallback to UDP");
        }

        NetworkManager.Singleton.StartClient();
    }
}
