using UnityEngine;
using TMPro;

public class NPCNetsecUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI plaintextText;
    public TMP_InputField answerInput;
    public TextMeshProUGUI resultText;
    public CountdownTimer countdownTimer;


    private bool solved = false;

    void Start()
    {
        panel.SetActive(false);
    }

    void OnMouseDown()
    {
        if (NetsecPuzzleManager.Instance == null) return;

        // ยังไม่เริ่ม puzzle → ห้ามเปิด
        if (!NetsecPuzzleManager.Instance.PuzzleStarted)
        {
            Debug.Log("Puzzle not started yet");
            return;
        }

        panel.SetActive(true);

        plaintextText.text =
            "PLAINTEXT: " + NetsecPuzzleManager.Instance.currentPlaintext;

        answerInput.text = "";
        resultText.text = "";
        solved = false;
    }

    public void SubmitAnswer()
    {
        if (NetsecPuzzleManager.Instance == null) return;

        bool correct =
            NetsecPuzzleManager.Instance.CheckAnswer(answerInput.text);

        if (correct)
        {
            resultText.text = "Correct!";
            solved = true;

            // หยุดเวลา (ตัวเดียวกับที่นับจริง)
            countdownTimer.StopCountdown();
            // ปลดล็อกประตู
            NetsecPuzzleManager.Instance.MarkSolved();
        }
        else
        {
            resultText.text = "Wrong!";
        }
    }

    public void Close()
    {
        panel.SetActive(false);

        // ❌ ถ้ายังไม่ solved → โดนปรับเวลา
        //if (!solved)
        //{
        //    FindObjectOfType<CountdownTimer>()?.ReduceTime(10f);
        //    Debug.Log("Wrong answer → -10 seconds");
        //}
    }
}
