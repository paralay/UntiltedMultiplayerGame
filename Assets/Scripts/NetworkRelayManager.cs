using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class NetworkRelayManager : MonoBehaviour
{
    private async Task InitServicesAsync()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
            await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    // connectionType : "dtls" (sécurisé) ou "udp" (compatibilité)
    public async Task<string> HostRelayAsync(int maxPlayers = 4, string connectionType = "dtls")
    {
        await InitServicesAsync();

        var allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
        var relayServerData = AllocationUtils.ToRelayServerData(allocation, connectionType);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        Debug.Log($"Relay Join Code: {joinCode} ({connectionType})");

        NetworkManager.Singleton.StartHost();
        return joinCode;
    }

    public async Task JoinRelayAsync(string joinCode, string connectionType = "dtls")
    {
        await InitServicesAsync();

        var allocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
        var relayServerData = AllocationUtils.ToRelayServerData(allocation, connectionType);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        Debug.Log($"Client joining with {connectionType}");
        NetworkManager.Singleton.StartClient();
    }
}
