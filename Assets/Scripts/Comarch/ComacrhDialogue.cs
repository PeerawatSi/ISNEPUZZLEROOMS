using UnityEngine;
using TMPro;
using System.Collections;

public class ComacrhDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Timer System")]
    public CountdownTimer countdownTimer;

    [Header("Puzzle")]
    public LogicGatePuzzleManager puzzleManager;

    // ================== INTRO DIALOGUE ==================

    private string[] dialogueLines =
    {
        "Welcome to the Computer Architecture Room.",
        "This room will test your understanding of logic gates.",
        "Logic gates are the foundation of all digital systems.",
        "Each gate receives binary inputs — either 0 or 1.",
        "Based on its logic, it produces a specific output.",
        "Different gates behave differently, even with the same inputs.",
        "In front of you, you will see logic gate symbols.",
        "Your task is to identify each gate correctly.",
        "Choose the correct answer before time runs out.",
        "Focus carefully...",
        "Let’s begin the logic challenge!"
    };

    private int currentIndex = 0;
    private bool hasPlayed = false;
    private bool puzzleStarted = false;

    private bool isTyping = false;
    private bool skipTyping = false;

    private Coroutine activeCoroutine;

    // ================== START ==================

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // ================== OPEN NPC ==================

    public void OpenPanel()
    {
        if (hasPlayed) return;

        hasPlayed = true;
        currentIndex = 0;

        dialoguePanel.SetActive(true);
        StartDialogueCoroutine(PlayDialogue());
    }

    // ================== UPDATE ==================

    void Update()
    {
        if (!dialoguePanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
                skipTyping = true;
        }
    }

    // ================== COROUTINE CONTROL ==================

    void StartDialogueCoroutine(IEnumerator routine)
    {
        ForceStopDialogue();
        activeCoroutine = StartCoroutine(routine);
    }

    // ================== INTRO DIALOGUE ==================

    IEnumerator PlayDialogue()
    {
        while (currentIndex < dialogueLines.Length)
        {
            yield return StartCoroutine(TypeLine(dialogueLines[currentIndex]));

            // ประโยคสุดท้าย → เริ่ม puzzle
            if (currentIndex == dialogueLines.Length - 1)
            {
                StartPuzzleAndTimer();
                yield break; // 🔥 หยุด dialogue ทันที
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            currentIndex++;
        }
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        isTyping = true;
        skipTyping = false;

        foreach (char c in line)
        {
            if (skipTyping)
            {
                dialogueText.text = line;
                break;
            }

            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
    }

    // ================== START PUZZLE ==================

    void StartPuzzleAndTimer()
    {
        if (puzzleStarted) return;
        puzzleStarted = true;

        ForceStopDialogue();
        CloseDialogue();

        if (countdownTimer != null)
        {
            countdownTimer.gameObject.SetActive(true);
            countdownTimer.ShowTutorialTimer();
            countdownTimer.StartCountdownTutorial();
        }

        if (puzzleManager != null)
        {
            puzzleManager.StartPuzzle();
        }
    }

    // ================== CLOSE ==================

    void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    // ================== CORRECT (แต่ละเฟรม) ==================

    public void ShowCorrectFeedback()
    {
        ForceStopDialogue();

        dialoguePanel.SetActive(true);
        activeCoroutine = StartCoroutine(PlayCorrectFeedback());
    }

    IEnumerator PlayCorrectFeedback()
    {
        yield return StartCoroutine(TypeLine("Correct!"));
        yield return new WaitForSeconds(0.8f);
        CloseDialogue();
    }

    // ================== FINAL (ครบทุกเฟรม) ==================

    public void ContinueAfterCorrectAnswer()
    {
        ForceStopDialogue();

        dialoguePanel.SetActive(true);
        activeCoroutine = StartCoroutine(PlayAfterCorrectAnswer());
    }

    IEnumerator PlayAfterCorrectAnswer()
    {
        yield return StartCoroutine(TypeLine("Correct."));
        yield return new WaitForSeconds(0.4f);

        yield return StartCoroutine(TypeLine("You understand how this logic gate works."));
        yield return new WaitForSeconds(0.4f);

        yield return StartCoroutine(TypeLine("This logic is used inside real processors."));
        yield return new WaitForSeconds(0.4f);

        yield return StartCoroutine(TypeLine("The path forward is now open."));
        yield return new WaitForSeconds(0.4f);

        CloseDialogue();
    }

    // ================== FORCE STOP ==================

    public void ForceStopDialogue()
    {
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        activeCoroutine = null;
        isTyping = false;
        skipTyping = false;
    }
}
