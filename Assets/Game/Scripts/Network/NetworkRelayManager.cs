using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
//using Unity.Services.Multiplayer.Relay;
using UnityEngine;

public class NetworkRelayManager : MonoBehaviour
{
    //public async void StartHost()
    //{
    //    await UnityServices.InitializeAsync();

    //    if (!AuthenticationService.Instance.IsSignedIn)
    //        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    //    var allocation = await MultiplayerService.Instance.Relay.Allocations.CreateAsync(4);
    //    var joinCode = await MultiplayerService.Instance.Relay.Allocations.GetJoinCodeAsync(allocation.Id);

    //    Debug.Log($"Join Code: {joinCode}");

    //    var relayServerData = new RelayServerData(allocation);
    //    var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
    //    transport.SetRelayServerData(relayServerData);

    //    NetworkManager.Singleton.StartHost();
    //}

    //public async void StartClient(string joinCode)
    //{
    //    await UnityServices.InitializeAsync();

    //    if (!AuthenticationService.Instance.IsSignedIn)
    //        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    //    var joinAllocation = await MultiplayerService.Instance.Relay.Allocations.JoinAsync(joinCode);

    //    var relayServerData = new RelayServerData(joinAllocation);
    //    var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
    //    transport.SetRelayServerData(relayServerData);

    //    NetworkManager.Singleton.StartClient();
    //}
}
