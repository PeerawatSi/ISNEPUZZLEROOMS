using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    // รายชื่อห้องทั้งหมด (ต้องตรงตามชื่อ Scene)
    public List<string> allRooms = new List<string>()
    {
        "Room_NetworkSecurity",
        "Room_ComputerArchitecture",
        "Room_Network",
        "Room_Algorithm",
        "Room_OOP"
    };

    private List<string> remainingRooms;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // อยู่ค้างทุก Scene
    }

    private void Start()
    {
        ResetRooms(); // ตั้งค่ารอบใหม่
    }

    public void ResetRooms()
    {
        remainingRooms = new List<string>(allRooms);
    }

    public void GoToNextRoom()
    {
        if (remainingRooms == null || remainingRooms.Count == 0)
        {
            ResetRooms();
        }

        int index = Random.Range(0, remainingRooms.Count);
        string nextRoom = remainingRooms[index];

        remainingRooms.RemoveAt(index);

        SceneManager.LoadScene(nextRoom);
    }
}
