using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class LogicGateIntroManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject introPanel;

    [Header("Description Text")]
    public TextMeshProUGUI descriptionText;

    [Header("Gate Cards")]
    public GameObject andCard;
    public GameObject orCard;
    public GameObject notCard;

    [Header("Continue Button")]
    public Button continueButton;

    [Header("Description Lines")]
    public string[] descriptionLines =
    {
        "Logic gates are the basic building blocks of digital circuits.",
        "Each gate takes inputs (0 or 1) and produces one output.",
        "Different gates behave differently depending on their logic.",
        "Let's review the three basic gates before you start the puzzle."
    };

    [Header("Ending Lines")]
    public string[] endingLines =
    {
        "Great! Now you understand the basic logic gates.",
        "You should be ready to solve the puzzles inside this room.",
        "Go ahead and show your skills!"
    };

    public NPCIntroDialogue npcIntro;

    private int index = 0;
    private bool isTyping = false;
    private bool skipTyping = false;

    private int gateStep = 0;
    private bool endingStarted = false;

    void Start()
    {
        introPanel.SetActive(true);

        andCard.SetActive(false);
        orCard.SetActive(false);
        notCard.SetActive(false);

        continueButton.gameObject.SetActive(false);

        StartCoroutine(PlayDescription());
    }

    void Update()
    {
        if (!introPanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                skipTyping = true;
                return;
            }

            if (index >= descriptionLines.Length && !endingStarted)
            {
                ShowNextGateCard();
                return;
            }

            index++;
        }
    }

    IEnumerator PlayDescription()
    {
        while (index < descriptionLines.Length)
        {
            yield return StartCoroutine(TypeLine(descriptionLines[index]));

            float t = 0f;
            float waitTime = 0.5f;

            while (t < waitTime)
            {
                if (Input.GetMouseButtonDown(0)) break;
                t += Time.deltaTime;
                yield return null;
            }

            index++;
        }

        ShowNextGateCard();
    }

    IEnumerator TypeLine(string line)
    {
        descriptionText.text = "";

        isTyping = true;
        skipTyping = false;

        foreach (char c in line)
        {
            if (skipTyping)
            {
                descriptionText.text = line;
                break;
            }

            descriptionText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
    }

    // ========================================================
    //   Show cards one by one
    // ========================================================
    void ShowNextGateCard()
    {
        descriptionText.text = "";
        gateStep++;

        andCard.SetActive(false);
        orCard.SetActive(false);
        notCard.SetActive(false);

        if (gateStep == 1)
        {
            andCard.SetActive(true);
            return;
        }

        if (gateStep == 2)
        {
            orCard.SetActive(true);
            return;
        }

        if (gateStep == 3)
        {
            notCard.SetActive(true);
            return;
        }

        if (!endingStarted)
        {
            endingStarted = true;
            StartCoroutine(PlayEndingDescription());
        }
    }

    // ========================================================
    //   Ending Description
    // ========================================================
    IEnumerator PlayEndingDescription()
    {
        descriptionText.text = "";

        foreach (string line in endingLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(0.4f);

            while (!Input.GetMouseButtonDown(0))
                yield return null;
        }

        continueButton.gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        introPanel.SetActive(false);

        // 🔥 เรียกให้ NPC เล่น intro ต่อ
        if (npcIntro != null)
            npcIntro.StartIntro();
    }


}
