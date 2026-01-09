using UnityEngine;
using TMPro;
using System.Collections;

public class AlgoDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject okButton;

    [Header("Timer System")]
    public CountdownTimer countdownTimer;

    private bool timerStarted = false;



    private string[] dialogueLines =
    {
        "Welcome to the Algorithm Room!",
        "In this room, you will learn how algorithms organize data.",
        "An algorithm is a step-by-step process used to solve a problem.",
        "I have hidden several numbers inside the books.",
        "They might be at the top left near me...",
        "Hmm... and I really like CYAN and YELLOW!",
        "Find those 5 numbers and use them to build the correct tree.",
        "Be careful — the structure of the tree matters.",
        "Once the timer starts, the challenge begins.",
        "Good luck!"
    };


    private int currentIndex = 0;
    private bool hasPlayed = false;

    private bool isTyping = false;
    private bool skipTyping = false;

    private Coroutine activeCoroutine = null;


    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }


    // เรียกเมื่อคลิก NPC
    public void OpenPanel()
    {
        if (hasPlayed) return;

        hasPlayed = true;
        currentIndex = 0;

        dialoguePanel.SetActive(true);

        StartDialogueCoroutine(PlayDialogue());
    }


    void Update()
    {
        if (!dialoguePanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
                skipTyping = true;
            else
                currentIndex++;
        }
    }


    // ---------- ฟังก์ชันจัดการ coroutine ป้องกันซ้อน ----------
    void StartDialogueCoroutine(IEnumerator routine)
    {
        // ถ้ามี coroutine เดิมกำลังทำงาน → หยุดก่อน
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        activeCoroutine = StartCoroutine(routine);
    }
    // ------------------------------------------------------------


    IEnumerator PlayDialogue()
    {
        while (currentIndex < dialogueLines.Length)
        {
            yield return StartCoroutine(TypeLine(dialogueLines[currentIndex]));

            float waitTime = 1.2f;
            float timer = 0f;

            while (timer < waitTime)
            {
                if (!skipTyping && Input.GetMouseButtonDown(0))
                    break;

                timer += Time.deltaTime;
                yield return null;
            }

            
            if (currentIndex == dialogueLines.Length - 1)
            {
                StartTimerIfNeeded();
            }

            currentIndex++;
        }

        ClosePanel();
    }




    void StartTimerIfNeeded()
    {
        if (timerStarted) return;

        timerStarted = true;

        if (countdownTimer != null)
        {
            countdownTimer.ShowTutorialTimer();
            countdownTimer.StartCountdownTutorial();
        }

        Debug.Log("Timer Started");
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


    public void ClosePanel()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        // ถ้าปิด panel ก่อนจบ dialogue → เริ่มเวลา
        StartTimerIfNeeded();
    }



    // ---------------- Dialogue หลังตอบถูก -------------------

    public void ContinueAfterCorrectAnswer()
    {
        dialoguePanel.SetActive(true);

        // หยุด dialogue เดิมก่อน (สำคัญมาก)
        ClosePanel();
        dialoguePanel.SetActive(true);

        StartDialogueCoroutine(PlayAfterCorrectAnswer());
    }

    // ================== Dialogue ตอนตอบถูก ==================

    IEnumerator PlayAfterCorrectAnswer()
    {
        yield return StartCoroutine(TypeLine("Yor are SOO GOOOOOD!!!"));
        yield return new WaitForSeconds(0.4f);

        //yield return StartCoroutine(TypeLine("Let’s open the door and begin your ISNE journey!"));
        //yield return new WaitForSeconds(0.4f);

        ClosePanel();
    }

    // ================== Dialogue ตอนตอบผิด ==================

    public void ShowWrongAnswer()
    {
        ClosePanel();
        dialoguePanel.SetActive(true);
        StartDialogueCoroutine(PlayWrongAnswer());
    }

    IEnumerator PlayWrongAnswer()
    {
        yield return StartCoroutine(TypeLine("Hmm... something is not quite right."));
        yield return new WaitForSeconds(0.4f);

        yield return StartCoroutine(TypeLine("Check the tree structure carefully and try again."));
        yield return new WaitForSeconds(1.2f);

        ClosePanel();
    }


    // ================== Dialogue ตอนเจอเลข ==================

    public void ShowFoundNumberDialogue(int number)
    {
        // ปิดของเดิมก่อน กันซ้อน
        ClosePanel();

        dialoguePanel.SetActive(true);
        StartDialogueCoroutine(PlayFoundNumber(number));
    }

    IEnumerator PlayFoundNumber(int number)
    {
        yield return StartCoroutine(TypeLine($"You found Number : {number}"));
        yield return new WaitForSeconds(1.2f);

        ClosePanel();
    }

    public void ShowTreeInstruction(AlgoTreeType type)
    {
        StartCoroutine(DelayedTreeInstruction(type));
    }

    IEnumerator DelayedTreeInstruction(AlgoTreeType type)
    {
        yield return new WaitForSeconds(2f); // รอให้ dialogue เจอเลขจบ

        ClosePanel();
        dialoguePanel.SetActive(true);

        StartDialogueCoroutine(PlayTreeInstruction(type));
    }


    IEnumerator PlayTreeInstruction(AlgoTreeType type)
    {
        string line = "";

        switch (type)
        {
            case AlgoTreeType.BST:
                line = "Now, arrange them into a Binary Search Tree.";
                break;

            case AlgoTreeType.Balanced:
                line = "Now, arrange them into a Balanced Binary Tree.";
                break;
        }

        yield return StartCoroutine(TypeLine(line));
        yield return new WaitForSeconds(1.5f);

        ClosePanel();
    }





}
