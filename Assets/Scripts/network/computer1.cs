using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class computer1 : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI resultText;
    public TMP_InputField inputField;
    public Button submitButton;
    public Button closeButton;
    public computer2 computer2; 

    private bool activated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        close();
        if(resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(activated)
        {
           dialoguePanel.SetActive(true);
        }
        else
        {
            Debug.Log("locked");
        }
    }

    public void CheckAnswer()
    {
        if(inputField.text == "192.168.1.0")
        {
            Debug.Log("Correct Answer");
            resultText.gameObject.SetActive(true);
            resultText.text = "Correct! You may proceed to ping the server.";
            computer2.open();
        }
        else
        {
            Debug.Log("Wrong Answer");
            resultText.gameObject.SetActive(true);
            resultText.text = "incorrect";
        }
    }




    public void open()
    {
        activated = true;
        
    }
    public void close()
    {
        dialoguePanel.SetActive(false);
    }
}
