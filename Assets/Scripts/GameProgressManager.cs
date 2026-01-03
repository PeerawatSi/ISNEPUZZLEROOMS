using UnityEngine;
using System.Collections.Generic;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    private HashSet<string> clearedRooms = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarkRoomCleared(string roomName)
    {
        clearedRooms.Add(roomName);
        Debug.Log("Room cleared: " + roomName);
    }

    public bool IsRoomCleared(string roomName)
    {
        return clearedRooms.Contains(roomName);
    }
}
