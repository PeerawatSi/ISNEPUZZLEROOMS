//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;

//public class TreeBuilder : MonoBehaviour
//{
//    [Header("Tree Nodes (UI Images)")]
//    public Image rootNode;
//    public Image leftNode;
//    public Image rightNode;
//    public Image leftLeftNode;
//    public Image leftRightNode;
//    public Image rightLeftNode;
//    public Image rightRightNode;

//    private List<Image> nodes = new List<Image>();
//    private int currentIndex = 0;

//    void Awake()
//    {
//        nodes.Add(rootNode);
//        nodes.Add(leftNode);
//        nodes.Add(rightNode);
//        nodes.Add(leftLeftNode);
//        nodes.Add(leftRightNode);
//        nodes.Add(rightLeftNode);
//        nodes.Add(rightRightNode);
//    }

//    public void InsertValue(int value)
//    {
//        if (currentIndex >= nodes.Count)
//        {
//            Debug.LogWarning("Node overflow");
//            return;
//        }

//        Image node = nodes[currentIndex];

//        if (node == null)
//        {
//            Debug.LogError("Node is NULL at index " + currentIndex);
//            return;
//        }

//        node.gameObject.SetActive(true);

//        TMP_Text text = node.GetComponentInChildren<TMP_Text>();

//        if (text == null)
//        {
//            Debug.LogError("TMP_Text not found in node " + node.name);
//            return;
//        }

//        text.text = value.ToString();
//        currentIndex++;
//    }


//    public List<int> GetCurrentOrder()
//    {
//        List<int> result = new List<int>();

//        foreach (Image node in nodes)
//        {
//            TMP_Text text = node.GetComponentInChildren<TMP_Text>();
//            if (text != null && text.text != "")
//                result.Add(int.Parse(text.text));
//        }

//        return result;
//    }

//    public void ResetTree()
//    {
//        currentIndex = 0;

//        foreach (Image node in nodes)
//        {
//            TMP_Text text = node.GetComponentInChildren<TMP_Text>();
//            if (text != null)
//                text.text = "";

//            node.gameObject.SetActive(false);
//        }
//    }
//}
