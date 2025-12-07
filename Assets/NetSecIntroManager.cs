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

    [Header("Cards (with images)")]
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
        "Playfair Cipher encrypts text by splitting it into letter pairs (digraphs).",
        "It uses a 5x5 key square generated from a keyword.",
        "Let's explore step-by-step how Playfair works."
    };

    [Header("Ending Lines")]
    public string[] endingLines =
    {
        "Great! Now you understand the structure of the Playfair Cipher.",
        "Use this knowledge to solve the upcoming encryption puzzle!",
        "Good luck, agent."
    };

    private int index = 0;
    private int cardStep = 0;

    private bool isTyping = false;
    private bool skipTyping = false;
    private bool endingStarted = false;


    void OnEnable()
    {
        continueButton.onClick.AddListener(CloseIntro);
    }

    void OnDisable()
    {
        continueButton.onClick.RemoveListener(CloseIntro);
    }


    void Start()
    {
        introPanel.SetActive(true);

        keySquareCard.SetActive(false);
        ruleCard.SetActive(false);
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


    // ----------------- MAIN INTRO -------------------
    IEnumerator PlayIntro()
    {
        while (index < introLines.Length)
        {
            yield return StartCoroutine(TypeLine(introLines[index]));

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

        ShowNextCard();
    }

    // ----------------- TYPEWRITER EFFECT ------------
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


    // ----------------- CARDS FLOW -------------------
    void ShowNextCard()
    {
        descriptionText.text = "";
        cardStep++;

        keySquareCard.SetActive(false);
        ruleCard.SetActive(false);
        ruletwoCard.SetActive(false);
        encryptCard.SetActive(false);
        decryptCard.SetActive(false);

        if (cardStep == 1)
        {
            keySquareCard.SetActive(true);     // รูปตาราง 5x5
            return;
        }

        if (cardStep == 2)
        {
            ruleCard.SetActive(true);          // รูปกฎ row/column/rectangle
            return;
        }

        if (cardStep == 3)
        {
            ruletwoCard.SetActive(true);          // รูปกฎ row/column/rectangle
            return;
        }

        if (cardStep == 4)
        {
            encryptCard.SetActive(true);       // ตัวอย่าง encryption
            return;
        }

        if (cardStep == 5)
        {
            decryptCard.SetActive(true);       // ตัวอย่าง decryption
            return;
        }

        if (!endingStarted)
        {
            endingStarted = true;
            StartCoroutine(PlayEnding());
        }
    }


    // ----------------- ENDING -----------------------
    IEnumerator PlayEnding()
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


    // ----------------- CLOSE PANEL -------------------
    public void CloseIntro()
    {
        introPanel.SetActive(false);
    }
}
