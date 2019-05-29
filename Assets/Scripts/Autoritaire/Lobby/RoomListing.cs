using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{

    [SerializeField]
    private Text _roomNameText;

    private Text roomNameText
    {
        get { return _roomNameText; }
    }
    
    public string roomName { get; private set; }
    
    public bool updated { get; set; }


    // Start is called before the first frame update
    private void Start()
    {
        GameObject lobbyCanvasObj = MainCanvasManager.instance.lobbyCanvas.gameObject;
        if (lobbyCanvasObj == null)
        {
            return;
        }

        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(()=>lobbyCanvas.onClickJoinRoom(roomNameText.text));
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void setRoomNameText(string unNomDeRoom)
    {
        roomName = unNomDeRoom;
        roomNameText.text = roomName;
    }
}
