using UnityEngine;
using TMPro;
using System.Collections;

public class AutoCheckMath : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField answerInput;

    public GameObject correctIcon;
    public GameObject wrongIcon;

    [Header("Timer System")]
    public CountdownTimer countdownTimer;

    [Header("Camera System")]
    public CameraRaycastSwitch camSwitch;  // ใส่สคริปต์สลับกล้อง

    [Header("Dialogue System")]
    public IntroDialogue introDialogue;    // ★ เพิ่มตรงนี้เพื่อเรียกบทหลังตอบถูก

    private Coroutine cameraDelayRoutine;

    private void Start()
    {
        // ปิดไอคอนตอนเริ่ม
        if (correctIcon != null) correctIcon.SetActive(false);
        if (wrongIcon != null) wrongIcon.SetActive(false);

        // ฟัง event ตอนกรอกเลข
        answerInput.onValueChanged.AddListener(CheckAnswer);
    }

    void CheckAnswer(string userInput)
    {
        bool isCorrect = (userInput == "2");

        // แสดงถูก/ผิด
        if (correctIcon != null) correctIcon.SetActive(isCorrect);
        if (wrongIcon != null) wrongIcon.SetActive(!isCorrect);

        // ถ้าถูก → หยุดเวลา
        if (isCorrect && countdownTimer != null)
        {
            countdownTimer.StopCountdown();
        }

        // ★ ถ้าถูก → เรียกบทสนทนาหลังตอบถูก
        if (isCorrect && introDialogue != null)
        {
            introDialogue.ContinueAfterCorrectAnswer();
        }

        // ถ้าถูก → รอ 2 วิแล้วค่อยกลับกล้อง
        if (isCorrect)
        {
            if (cameraDelayRoutine != null)
                StopCoroutine(cameraDelayRoutine);

            cameraDelayRoutine = StartCoroutine(ReturnCameraAfterDelay());
        }
    }

    IEnumerator ReturnCameraAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (camSwitch != null)
            camSwitch.SwitchToMain();
    }
}
