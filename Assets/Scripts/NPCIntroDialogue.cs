using UnityEngine;
using TMPro;
using System.Collections;

public class NPCIntroDialogue : MonoBehaviour
{
    public TextMeshPro textMesh;
    public string[] sentences;
    public float typeSpeed = 0.05f;

    private bool isTyping = false;
    private bool introDone = false;

    public void StartIntro()
    {
        StartCoroutine(ShowIntro());
    }

    IEnumerator ShowIntro()
    {
        yield return StartCoroutine(TypeSentence("Hi, welcome to the ISNE Puzzle Room!"));

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(TypeSentence("Try to Click on me :)"));

        introDone = true;

        yield return new WaitForSeconds(1f);
        textMesh.text = "";
    }

    IEnumerator TypeSentence(string sentence)
    {
        textMesh.text = "";

        foreach (char letter in sentence)
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
