using UnityEngine;

public class CameraRaycastSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera boardCamera;

    public GameObject boardCanvas;
    public CountdownTimer countdownTimer; // assign ใน Inspector

    void Start()
    {
        mainCamera.enabled = true;
        boardCamera.enabled = false;

        if (boardCanvas != null)
            boardCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Board1"))
                {
                    SwitchToBoardCamera();
                }
            }
        }
    }

    public void SwitchToBoardCamera()
    {
        mainCamera.enabled = false;
        boardCamera.enabled = true;

        if (boardCanvas != null)
            boardCanvas.SetActive(true);

        // ⭐ เริ่มโชว์เวลา + เริ่มนับถอยหลัง
        if (countdownTimer != null)
        {
            countdownTimer.ShowTutorialTimer();
            countdownTimer.StartCountdownTutorial();
        }
    }

    public void SwitchToMain()
    {
        mainCamera.enabled = true;
        boardCamera.enabled = false;

        if (boardCanvas != null)
            boardCanvas.SetActive(false);
    }
}
