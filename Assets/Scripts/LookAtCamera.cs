using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            // ให้ข้อความหันไปทางกล้อง
            transform.LookAt(Camera.main.transform.position);

            // หมุน 180 องศา เพราะ LookAt จะหันหลัง
            transform.Rotate(0, 180f, 0);
        }
    }
}
