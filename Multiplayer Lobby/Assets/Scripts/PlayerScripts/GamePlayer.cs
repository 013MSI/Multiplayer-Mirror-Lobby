using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;

public class GamePlayer : NetworkBehaviour
{
    
    [SyncVar] public string PlayerName;
    [SyncVar] public int ConnectionId;

    private NetworkManagerS game;
    private NetworkManagerS Game
    {
        get
        {
            if (game != null)
            {
                return game;
            }
            return game = NetworkManagerS.singleton as NetworkManagerS;
        }
    }
    public override void OnStartAuthority()
    {
        gameObject.name = "LocalGamePlayer";
        Debug.Log("Labeling the local player: " + this.PlayerName);
    }
    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Game.GamePlayers.Add(this);
        Debug.Log("Added to GamePlayer list: " + this.PlayerName);
    }
    public override void OnStopClient()
    {
        Debug.Log(PlayerName + " is quiting the game.");
        Game.GamePlayers.Remove(this);
        Debug.Log("Removed player from the GamePlayer list: " + this.PlayerName);
    }
    [Server]
    public void SetPlayerName(string playerName)
    {
        this.PlayerName = playerName;
    }
    [Server]
    public void SetConnectionId(int connId)
    {
        this.ConnectionId = connId;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
