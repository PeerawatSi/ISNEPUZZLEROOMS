using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PlayfairIntroManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject introPanel;

    [Header("Description Text")]
    public TextMeshProUGUI descriptionText;

    [Header("Cards")]
    public GameObject keySquareCard;
    public GameObject ruleCard;
    public GameObject ruletwoCard;
    public GameObject encryptCard;
    public GameObject decryptCard;

    [Header("Continue Button")]
    public Button continueButton;

    [Header("Intro Lines")]
    public string[] introLines =
    {
        "Welcome to the Network Security Room!",
        "Before you begin the puzzle, let's learn the basics of the Playfair Cipher.",
        "Playfair Cipher encrypts text by splitting it into letter pairs.",
        "It uses a 5x5 key square generated from a keyword.",
        "Let's explore how Playfair works step by step."
    };

    [Header("Ending Lines")]
    public string[] endingLines =
    {
        "Great! Now you understand the structure of the Playfair Cipher.",
        "Use this knowledge to solve the encryption puzzle.",
        "Good luck, agent."
    };

    public NPCIntroDialogue npcIntro;

    private int index = 0;
    private int cardStep = 0;
    private bool isTyping = false;
    private bool skipTyping = false;
    private bool endingStarted = false;

    void Start()
    {
        introPanel.SetActive(true);

        keySquareCard.SetActive(false);
        ruleCard.SetActive(false);
        ruletwoCard.SetActive(false);
        encryptCard.SetActive(false);
        decryptCard.SetActive(false);

        continueButton.gameObject.SetActive(false);

        StartCoroutine(PlayIntro());
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

            if (index >= introLines.Length && !endingStarted)
            {
                ShowNextCard();
                return;
            }

            index++;
        }
    }

    IEnumerator PlayIntro()
    {
        while (index < introLines.Length)
        {
            yield return StartCoroutine(TypeLine(introLines[index]));
            yield return WaitForClick();
            index++;
        }

        ShowNextCard();
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

    IEnumerator WaitForClick()
    {
        while (!Input.GetMouseButtonDown(0))
            yield return null;
    }

    void ShowNextCard()
    {
        descriptionText.text = "";
        cardStep++;

        keySquareCard.SetActive(false);
        ruleCard.SetActive(false);
        ruletwoCard.SetActive(false);
        encryptCard.SetActive(false);
        decryptCard.SetActive(false);

        if (cardStep == 1) keySquareCard.SetActive(true);
        else if (cardStep == 2) ruleCard.SetActive(true);
        else if (cardStep == 3) ruletwoCard.SetActive(true);
        else if (cardStep == 4) encryptCard.SetActive(true);
        else if (cardStep == 5) decryptCard.SetActive(true);
        else if (!endingStarted)
        {
            endingStarted = true;
            StartCoroutine(PlayEnding());
        }
    }

    IEnumerator PlayEnding()
    {
        descriptionText.text = "";

        foreach (string line in endingLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return WaitForClick();
        }

        continueButton.gameObject.SetActive(true);
    }

    // 🔥 ปุ่ม Continue เรียกฟังก์ชันนี้
    public void ClosePanel()
    {
        introPanel.SetActive(false);

        // 🔥 เรียกให้ NPC เล่น intro ต่อ
        if (npcIntro != null)
            npcIntro.StartIntro();
    }
}
