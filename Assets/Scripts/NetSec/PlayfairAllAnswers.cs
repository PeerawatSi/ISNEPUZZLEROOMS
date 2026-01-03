using UnityEngine;

public class PlayfairAllAnswers : MonoBehaviour
{
    public string[] possiblePlaintexts =
    {
        "SEC",
        "NET",
        "HI",
        "HELLO",
        "CODE",
        "PLAY",
        "SAFE",
        "LOCK"
    };

    public string[] possibleKeys =
    {
        "KEY",
        "SAFE",
        "LOCK",
        "CODE",
        "PLAY"
    };

    void Start()
    {
        Debug.Log("===== ALL PLAYFAIR ANSWERS =====");

        foreach (string key in possibleKeys)
        {
            Debug.Log("KEY = " + key);

            foreach (string pt in possiblePlaintexts)
            {
                string enc = PlayfairCipher.Encrypt(pt, key);
                Debug.Log(pt + " → " + enc);
            }
        }
    }
}
