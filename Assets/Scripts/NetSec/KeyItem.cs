using UnityEngine;
using TMPro;

public class KeyItem : MonoBehaviour
{
    public TextMeshPro keyText; // ⭐ ลาก KeyText มาใส่
    private bool revealed = false;

    void Start()
    {
        if (keyText != null)
            keyText.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (revealed) return;
        if (NetsecPuzzleManager.Instance == null) return;
        if (!NetsecPuzzleManager.Instance.PuzzleStarted) return; // 🔒 กันก่อนเริ่ม puzzle

        revealed = true;

        keyText.text =
            "KEY = " + NetsecPuzzleManager.Instance.currentKey;

        keyText.gameObject.SetActive(true);

        Debug.Log("KEY REVEALED: " + NetsecPuzzleManager.Instance.currentKey);
    }
}
