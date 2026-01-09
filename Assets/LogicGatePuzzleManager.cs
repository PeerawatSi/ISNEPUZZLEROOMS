using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LogicGatePuzzleManager : MonoBehaviour
{
    public enum LogicGateType
    {
        AND, OR, NOT, NAND, NOR, XOR, XNOR
    }

    [System.Serializable]
    public class LogicGateData
    {
        public LogicGateType gateType;
        public Sprite gateSprite;
    }

    [Header("Logic Gates Data")]
    public LogicGateData[] logicGates;

    [Header("Frames")]
    public SpriteRenderer frame1Renderer;
    public SpriteRenderer frame2Renderer;

    [Header("Answer UI")]
    public GameObject answerPanel;
    public Button[] answerButtons;

    [Header("Puzzle State")]
    public bool puzzleStarted = false;

    // ✅ เปลี่ยน mindset → Dialogue System
    [Header("Dialogue System")]
    public ComacrhDialogue dialogue;   // ลากจาก Inspector ตรง ๆ

    private LogicGateData frame1Gate;
    private LogicGateData frame2Gate;
    private LogicGateData selectedGate;
    private int selectedFrameIndex = -1;

    private bool frame1Solved = false;
    private bool frame2Solved = false;

    void Start()
    {
        answerPanel.SetActive(false);
        frame1Renderer.sprite = null;
        frame2Renderer.sprite = null;
    }

    // =========================
    // START PUZZLE
    // =========================
    public void StartPuzzle()
    {
        puzzleStarted = true;
        frame1Solved = false;
        frame2Solved = false;
        RandomizeFrames();
    }

    void RandomizeFrames()
    {
        List<LogicGateData> temp = new List<LogicGateData>(logicGates);

        frame1Gate = temp[Random.Range(0, temp.Count)];
        temp.Remove(frame1Gate);

        frame2Gate = temp[Random.Range(0, temp.Count)];

        frame1Renderer.sprite = frame1Gate.gateSprite;
        frame2Renderer.sprite = frame2Gate.gateSprite;

        Debug.Log($"Frame1: {frame1Gate.gateType}, Frame2: {frame2Gate.gateType}");
    }

    // =========================
    // SELECT FRAME
    // =========================
    public void SelectFrame(int frameIndex)
    {
        if (!puzzleStarted) return;

        if (frameIndex == 1 && frame1Solved) return;
        if (frameIndex == 2 && frame2Solved) return;

        selectedFrameIndex = frameIndex;
        selectedGate = (frameIndex == 1) ? frame1Gate : frame2Gate;

        ResetButtonColors();
        answerPanel.SetActive(true);

        Debug.Log("Selected Gate: " + selectedGate.gateType);
    }

    // =========================
    // ANSWER
    // =========================
    public void Answer(int answerIndex)
    {
        if (!puzzleStarted || selectedGate == null) return;

        LogicGateType answer = (LogicGateType)answerIndex;

        Button clickedButton =
            UnityEngine.EventSystems.EventSystem.current
            .currentSelectedGameObject
            .GetComponent<Button>();

        if (answer == selectedGate.gateType)
        {
            clickedButton.image.color = Color.green;
            Debug.Log("Correct!");

            if (selectedFrameIndex == 1) frame1Solved = true;
            if (selectedFrameIndex == 2) frame2Solved = true;

            answerPanel.SetActive(false);

            // ✅ เหมือน AutoCheckMath เป๊ะ
            if (dialogue != null)
                dialogue.ShowCorrectFeedback();

            // ถ้าครบสองเฟรม
            if (frame1Solved && frame2Solved)
            {
                puzzleStarted = false;

                if (dialogue != null)
                    dialogue.ContinueAfterCorrectAnswer();
            }
        }
        else
        {
            clickedButton.image.color = Color.red;
            Debug.Log("Wrong!");
        }
    }

    void ResetButtonColors()
    {
        foreach (Button btn in answerButtons)
            btn.image.color = Color.white;
    }
}
