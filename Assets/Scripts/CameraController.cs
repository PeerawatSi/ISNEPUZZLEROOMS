using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed = 3f;
    public float rotateSpeed = 5f;

    private Vector3 targetPos;
    private Quaternion targetRot;
    private bool isMoving = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * moveSpeed
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRot,
            Time.deltaTime * rotateSpeed
        );

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            isMoving = false;
        }
    }

    public void FocusOn(Transform target)
    {
        Vector3 dir = (transform.position - target.position).normalized;
        targetPos = target.position + dir * 2.5f + Vector3.up * 1.2f;
        targetRot = Quaternion.LookRotation(target.position - targetPos);
        isMoving = true;
    }
}
