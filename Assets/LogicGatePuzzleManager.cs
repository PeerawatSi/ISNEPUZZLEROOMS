using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LogicGatePuzzleManager : MonoBehaviour
{
    [System.Serializable]
    public class LogicGateData
    {
        public string gateName;
        public Sprite gateSprite;
    }

    [Header("Logic Gates Data")]
    public LogicGateData[] logicGates;

    [Header("Frames")]
    public SpriteRenderer frame1Renderer;
    public SpriteRenderer frame2Renderer;

    [Header("Answer UI")]
    public GameObject answerPanel;
    public Button[] answerButtons; // 7 ปุ่ม

    private LogicGateData frame1Gate;
    private LogicGateData frame2Gate;
    private LogicGateData selectedGate;

    void Start()
    {
        answerPanel.SetActive(false);
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
    }

    // =========================
    // Frame ถูกคลิก
    // =========================
    public void SelectFrame(int frameIndex)
    {
        selectedGate = frameIndex == 1 ? frame1Gate : frame2Gate;

        ResetButtonColors();
        answerPanel.SetActive(true);

        Debug.Log("Selected Gate: " + selectedGate.gateName);
    }

    // =========================
    // ตอบคำถาม
    // =========================
    public void Answer(string answer)
    {
        if (selectedGate == null)
        {
            Debug.LogWarning("No frame selected!");
            return;
        }

        // หาปุ่มที่ถูกกด (จาก EventSystem)
        Button clickedButton =
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject
            .GetComponent<Button>();

        if (answer == selectedGate.gateName)
        {
            clickedButton.image.color = Color.green;
            Debug.Log("Correct!");
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
        {
            btn.image.color = Color.white;
        }
    }
}
