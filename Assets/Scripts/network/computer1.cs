using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class computer1 : MonoBehaviour
{
    public NetworkDialogue networkDialogue;
    public GameObject com1Panel;
    public TextMeshProUGUI com1Text;
    public TextMeshProUGUI com1resultText;
    public TMP_InputField com1inputField;
    public Button com1submitButton;
    public Button com1closeButton;
    public computer2 computer2; 

    private bool activated = false;
    private string networkAddress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        close();
        if(com1resultText != null)
        {
            com1resultText.gameObject.SetActive(false);
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
           // Calculate network address when opening
           CalculateNetworkAddress();
           com1Panel.SetActive(true);
        }
        else
        {
            Debug.Log("locked");
        }
    }

    void CalculateNetworkAddress()
    {
        // Extract the first 3 octets from server IP to get network address
        string[] parts = networkDialogue.ipaddress.Split('.');
        if(parts.Length == 4)
        {
            if(networkDialogue.subnetMask == 8)
            {
                // /8 = 255.0.0.0
                networkAddress = parts[0] + ".0.0.0";
            }
            else if(networkDialogue.subnetMask == 16)
            {
                // /16 = 255.255.0.0
                networkAddress = parts[0] + "." + parts[1] + ".0.0";
            }
            else // /24
            {
                // /24 = 255.255.255.0
                networkAddress = parts[0] + "." + parts[1] + "." + parts[2] + ".0";
            }
        
            com1Text.text = "What is the network address? (Server IP: " + networkDialogue.ipaddress + ", Subnet: /" + networkDialogue.subnetMask + ")";
        }
    }

    public void CheckAnswer()
    {
        if(com1inputField.text == networkAddress)
        {
            Debug.Log("Correct Answer: " + networkAddress);
            com1resultText.gameObject.SetActive(true);
            com1resultText.text = "Correct! Network address is " + networkAddress + ". You may proceed to the next computer.";
            computer2.open();
        }
        else
        {
            Debug.Log("Wrong Answer. Expected: " + networkAddress);
            com1resultText.gameObject.SetActive(true);
            com1resultText.text = "Incorrect. Try again.";
        }
    }

    public void open()
    {
        activated = true;
    }
    
    public void close()
    {
        com1Panel.SetActive(false);
    }
}