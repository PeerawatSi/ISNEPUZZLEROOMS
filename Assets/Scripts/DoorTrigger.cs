using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DoorTrigger : MonoBehaviour
{
    // รายชื่อห้องทั้งหมด
    public static List<string> remainingRooms = new List<string>()
    {
        "Room_NetworkSecurity",
        "Room_ComputerArchitecture",
        "Room_Network",
        "Room_Algorithm",
        "Room_OOP"
    };

    private void OnMouseDown()
    {
        // ถ้าเล่นครบทุกห้องแล้ว
        if (remainingRooms.Count == 0)
        {
            Debug.Log("All rooms completed!");
            return;
        }

        // สุ่ม index จากห้องที่เหลือ
        int index = Random.Range(0, remainingRooms.Count);

        // เลือกห้อง
        string selectedRoom = remainingRooms[index];

        // ลบห้องนี้ออก (กันซ้ำ)
        remainingRooms.RemoveAt(index);

        // โหลด Scene
        SceneManager.LoadScene(selectedRoom);
    }
}
