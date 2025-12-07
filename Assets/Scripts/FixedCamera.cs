using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    void Start()
    {
        // ใส่มุมที่คุณต้องการ
        transform.rotation = Quaternion.Euler(20f, 0f, 0f);
    }
}
