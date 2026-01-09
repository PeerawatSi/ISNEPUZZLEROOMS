using UnityEngine;
using TMPro;
using System.Collections;

public class NPCIntroTutorial : MonoBehaviour
{
    public TextMeshPro textMesh;
    public float typeSpeed = 0.02f;

    private bool introPlayed = false;

    void Start()
    {
        if (!introPlayed)
            StartIntro();
    }

    public void StartIntro()
    {
        introPlayed = true;
        StartCoroutine(ShowIntro());
    }

    IEnumerator ShowIntro()
    {
      
        yield return StartCoroutine(TypeSentence(
            "Hi, welcome to the ISNE Puzzle Room!"
        ));

        yield return new WaitForSeconds(1.5f);

       
        yield return StartCoroutine(TypeSentence(
            "Try to Click on me :)"
        ));

        // รอให้อ่าน
        yield return new WaitForSeconds(1f);

        // ลบข้อความออก
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
