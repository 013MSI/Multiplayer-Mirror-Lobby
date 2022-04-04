using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public static TitleScreenManager instance;

    [SerializeField] private NetworkManagerS networkManager;

    [Header("UI Panels")]
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject PlayerNamePanel;
    [SerializeField] private GameObject HostOrJoinPanel;
    [SerializeField] private GameObject EnterIPAddressPanel;

    [Header("PlayerName UI")]
    [SerializeField] private TMP_InputField playerNameInputField;

    [Header("Enter IP UI")]
    [SerializeField] private TMP_InputField IpAddressField;

    [Header("Misc. UI")]
    [SerializeField] private Button returnToMainMenu;

    private const string PlayerPrefsNameKey = "PlayerName";

    public void ReturnToMainMenu()
    {
	    MainMenuPanel.SetActive(true);
	    PlayerNamePanel.SetActive(false);
	    HostOrJoinPanel.SetActive(false);
	    EnterIPAddressPanel.SetActive(false);
	    returnToMainMenu.gameObject.SetActive(false);
    }

    void MakeInstance()
    {
	    if (instance == null)
		    instance = this;
    }

    public void StartGame()
    {
	    //SceneManager.LoadScene("SampleScene");
	    MainMenuPanel.SetActive(false);
	    PlayerNamePanel.SetActive(true);
	    GetSavedPlayerName();
	    returnToMainMenu.gameObject.SetActive(true);
    }
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void GetSavedPlayerName()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsNameKey))
	    {
		    playerNameInputField.text = PlayerPrefs.GetString(PlayerPrefsNameKey);
            Debug.Log(playerNameInputField.text);
	    }
    }

    public void SavePlayerName()
    {
        string playerName = null;
        Debug.Log(playerName);
        Debug.Log(playerNameInputField.text);
	    if (!string.IsNullOrEmpty(playerNameInputField.text))
	    {
		    playerName = playerNameInputField.text;
		    PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
		    PlayerNamePanel.SetActive(false);
		    HostOrJoinPanel.SetActive(true);
            Debug.Log("Inside if" + playerName);
            Debug.Log("Inside if" + playerNameInputField.text);
	    }
    }

    public void HostGame()
    {
	    Debug.Log("Hosting a game...");
	    networkManager.StartHost();
	    HostOrJoinPanel.SetActive(false);
	    returnToMainMenu.gameObject.SetActive(false);
    }
    public void JoinGame()
    {
	    HostOrJoinPanel.SetActive(false);
	    EnterIPAddressPanel.SetActive(true);
    }

    public void ConnectToGame()
    {
	    if (!string.IsNullOrEmpty(IpAddressField.text))
	    {
		Debug.Log("Client will connect to: " + IpAddressField.text);
		networkManager.networkAddress = IpAddressField.text;
		networkManager.StartClient();
	    }
	    EnterIPAddressPanel.SetActive(false);
	    returnToMainMenu.gameObject.SetActive(false);
    }
    
    void Awake()
    {
        MakeInstance();
        ReturnToMainMenu();
    }
}
