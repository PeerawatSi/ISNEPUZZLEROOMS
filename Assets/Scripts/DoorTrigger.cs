using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{

    // ใส่ชื่อ Scene ของแต่ละห้อง
    public string[] roomNames = new string[]
    {
        "Room_NetworkSecurity",
        "Room_ComputerArchitecture",
        "Room_Network",
        "Room_Algorithm",
        "Room_OOP"
    };

    private void OnMouseDown()
    {
        if (roomNames.Length == 0) return;

        // สุ่ม index ห้อง
        int index = Random.Range(0, roomNames.Length);

        // โหลด Scene ตามที่สุ่ม
        SceneManager.LoadScene(roomNames[index]);
    }
}
