using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class computer2 : MonoBehaviour
{
    public NetworkDialogue networkDialogue;
    private string myipaddress;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI resultText;
    public TMP_InputField inputField;
    public Button submitButton;
    public Button closeButton;
    private bool activated = false;

    private bool currentStep = false;

    void Start()
    {
        close();
        if(resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }
    }

    
    void Update()
    {

    }

    public void OnMouseDown()
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
        if(!currentStep)
        {
            /*
            if(inputField.text == "192.168.1.0")
            {
                Debug.Log("Correct Answer");
                resultText.gameObject.SetActive(true);
                resultText.text = "Correct! You may proceed to ping the server.";
                dialogueText.text = "Enter command to ping the server (serverip: " + networkDialogue.ipaddress + "):";
                currentStep = true;
            }else
            {
                Debug.Log("Wrong Answer");
                resultText.gameObject.SetActive(true);
                resultText.text = "incorrect";
            }
            */
            // Check if IP address is in correct format: 192.168.1.X
            if(inputField.text.StartsWith("192.168.1."))
            {
                string[] parts = inputField.text.Split('.');
                if(parts.Length == 4 && int.TryParse(parts[3], out int lastOctet))
                {
                    if(lastOctet >= 0 && lastOctet <= 255 && lastOctet != networkDialogue.lastdigit) // Valid range, not server IP
                    {
                        myipaddress = inputField.text;
                        Debug.Log("Correct IP Address: " + myipaddress);
                        resultText.gameObject.SetActive(true);
                        resultText.text = "Correct! IP address assigned: " + myipaddress;
                        inputField.text = "";
                        dialogueText.text = "Enter command to ping the server (serverip: " + networkDialogue.ipaddress + "):";
                        currentStep = true;
                    }
                    else
                    {
                        resultText.gameObject.SetActive(true);      
                        resultText.text = "Incorrect. ipaddress shouldn't be the same with server ip.";
                    }
                }
                else
                {
                    resultText.gameObject.SetActive(true);
                    resultText.text = "Incorrect IP format.";
                }
            }
            else
            {
                resultText.text = "Incorrect. IP must start with 192.168.1.";
            }

        }
        else
        {
            if(inputField.text == "ping " + networkDialogue.ipaddress)
            {
                Debug.Log("Correct Answer");
                resultText.gameObject.SetActive(true);
                resultText.text = "Correct! You have successfully pinged the server.";
                networkDialogue.Outtro();
                close();
                
            }
            else
            {
                Debug.Log("Wrong Answer");
                resultText.gameObject.SetActive(true);
                resultText.text = "incorrect command";
            }
        }
        
    }
    

    public void open()
    {
        Debug.Log("activated");
        activated = true;
    }

    public void close()
    {
        dialoguePanel.SetActive(false);
    }
}
