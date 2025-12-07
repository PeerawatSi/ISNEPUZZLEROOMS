using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialogue3D : MonoBehaviour
{
    public TextMeshPro textMesh; // ลาก TextMeshPro ที่อยู่บนหัว
    public string[] sentences; // ใส่ประโยคทีละบรรทัด
    public float typeSpeed = 0.05f;

    private bool isTyping = false;
    private int currentSentence = 0;

    void OnMouseDown()
    {
        if (!isTyping)
        {
            StartCoroutine(ShowDialogue());
        }
    }

    IEnumerator ShowDialogue()
    {
        isTyping = true;

        for (currentSentence = 0; currentSentence < sentences.Length; currentSentence++)
        {
            textMesh.text = "";
            foreach (char letter in sentences[currentSentence].ToCharArray())
            {
                textMesh.text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }

            if (currentSentence == sentences.Length - 1) // ประโยคสุดท้าย
            {
                textMesh.text += " [OK]";
                bool waitForClick = true;
                while (waitForClick)
                {
                    if (Input.GetMouseButtonDown(0)) // คลิกไหนก็ได้
                        waitForClick = false;
                    yield return null;
                }

                StartCoroutine(StartCountdown(10)); // ตัวอย่าง 10 วินาที
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }

        isTyping = false;
    }

    IEnumerator StartCountdown(int seconds)
    {
        int timeLeft = seconds;
        while (timeLeft > 0)
        {
            textMesh.text = timeLeft.ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        textMesh.text = "0";
    }
}
