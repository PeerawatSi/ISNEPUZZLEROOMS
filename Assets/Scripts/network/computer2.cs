using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class computer2 : MonoBehaviour
{
    public NetworkDialogue networkDialogue;
    private string myipaddress;
    public GameObject com2Panel;
    public TextMeshProUGUI com2Text;
    public TextMeshProUGUI com2resultText;
    public TMP_InputField com2inputField;
    public Button com2submitButton;
    public Button com2closeButton;
    private bool activated = false;

    private bool currentStep = false;
    private string networkPrefix;

    void Start()
    {
        close();
        if(com2resultText != null)
        {
            com2resultText.gameObject.SetActive(false);
        }
    }

    
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if(activated)
        {
           // Calculate network prefix when opening
           CalculateNetworkPrefix();
           com2Panel.SetActive(true);
           ShowQuestion();
        }
        else
        {
            Debug.Log("locked");
        }
    }

    void CalculateNetworkPrefix()
    {
        // Extract network prefix based on subnet mask
        string[] parts = networkDialogue.ipaddress.Split('.');
        if(parts.Length == 4)
        {
            if(networkDialogue.subnetMask == 8)
            {
                networkPrefix = parts[0] + ".";
            }
            else if(networkDialogue.subnetMask == 16)   
            {
                networkPrefix = parts[0] + "." + parts[1] + ".";
            }
            else // /24
            {
                networkPrefix = parts[0] + "." + parts[1] + "." + parts[2] + ".";
            }
        }
    }

    void ShowQuestion()
    {
        if(!currentStep)
        {
            string hint = "";
            if(networkDialogue.subnetMask == 8)
                hint = "Format: " + networkPrefix + "Y.Z.W";
            else if(networkDialogue.subnetMask == 16)
                hint = "Format: " + networkPrefix + "Z.W";
            else
                hint = "Format: " + networkPrefix + "W";
            
            com2Text.text = "Assign an IP address to this PC.\n" + hint + "\n(Network: " + networkPrefix.TrimEnd('.') + ", Subnet: /" + networkDialogue.subnetMask + ")";
        }else{
            com2Text.text = "Enter command to ping the server (Server IP: " + networkDialogue.ipaddress + "):";
        }
    }

    public void CheckAnswer()
    {
        if(!currentStep)
        {
            // STEP 1: Check IP address assignment
            if(com2inputField.text.StartsWith(networkPrefix))
            {
                string[] parts = com2inputField.text.Split('.');
            
                // Validate based on subnet mask
                bool isValid = false;
                if(networkDialogue.subnetMask == 8 && parts.Length == 4)
                {
                    // For /8, need X.Y.Z.W format where X matches
                    isValid = int.TryParse(parts[1], out int octet2) && 
                             int.TryParse(parts[2], out int octet3) && 
                             int.TryParse(parts[3], out int octet4) &&
                             octet2 >= 0 && octet2 <= 255 &&
                             octet3 >= 0 && octet3 <= 255 &&
                             octet4 >= 1 && octet4 <= 254;
                }
                else if(networkDialogue.subnetMask == 16 && parts.Length == 4)
                {
                    // For /16, need X.Y.Z.W format where X.Y matches
                    isValid = int.TryParse(parts[2], out int octet3) && 
                             int.TryParse(parts[3], out int octet4) &&
                             octet3 >= 0 && octet3 <= 255 &&
                             octet4 >= 1 && octet4 <= 254;
                }
                else if(networkDialogue.subnetMask == 24 && parts.Length == 4)
                {
                    // For /24, need X.Y.Z.W format where X.Y.Z matches
                    isValid = int.TryParse(parts[3], out int octet4) &&
                             octet4 >= 1 && octet4 <= 254;
                }
            
                if(isValid && com2inputField.text != networkDialogue.ipaddress)
                {
                    myipaddress = com2inputField.text;
                    Debug.Log("Correct IP Address: " + myipaddress);
                    com2resultText.gameObject.SetActive(true);
                    com2resultText.text = "Correct! IP address assigned: " + myipaddress;
                    com2inputField.text = "";
                    currentStep = true;
                    ShowQuestion();
                }else{
                    com2resultText.gameObject.SetActive(true);      
                    com2resultText.text = "Incorrect. IP shouldn't be the same as server IP and must be valid.";
                }
            }else{
                com2resultText.gameObject.SetActive(true);
                com2resultText.text = "Incorrect. IP must start with " + networkPrefix;
            }
        }
        else
        {
            // STEP 2: Check ping command
            if(com2inputField.text == "ping " + networkDialogue.ipaddress)
            {
                Debug.Log("Correct Answer");
                com2resultText.gameObject.SetActive(true);
                com2resultText.text = "Correct! You have successfully pinged the server.";
                networkDialogue.Outtro();
                close();
            }else{
                Debug.Log("Wrong Answer");
                com2resultText.gameObject.SetActive(true);
                com2resultText.text = "Incorrect command. Use: ping " + networkDialogue.ipaddress;
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
        com2Panel.SetActive(false);
        currentStep = false; // Reset when closing
    }
}