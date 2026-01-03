using TMPro;
using UnityEngine;

public class SimplePuzzle : MonoBehaviour
{
    public TMP_InputField answerInput;
    public GameObject correctIcon;   // ✔ รูปถูก หรือกรอบเรืองแสง
    public GameObject wrongIcon;     // ✖ รูปผิด (มีหรือไม่ก็ได้)

    private void Start()
    {
        correctIcon?.SetActive(false);
        wrongIcon?.SetActive(false);

        // ผูก event ตรวจทุกครั้งที่ input เปลี่ยน
        answerInput.onValueChanged.AddListener(CheckAnswer);
    }

    void CheckAnswer(string userInput)
    {
        if (userInput == "2")   // คำตอบที่ถูก
        {
            if (correctIcon != null) correctIcon.SetActive(true);
            if (wrongIcon != null) wrongIcon.SetActive(false);

            // ถ้าอยากให้ล็อก input ตอนตอบถูก:
            // answerInput.interactable = false;
        }
        else
        {
            if (correctIcon != null) correctIcon.SetActive(false);
            if (wrongIcon != null) wrongIcon.SetActive(true);
        }
    }
}
