using System.Collections.Generic;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    public List<Book> allBooks = new List<Book>();
    public int booksToOpen = 5;

    public int minNumber = 1;
    public int maxNumber = 99;

    private List<int> usedNumbers = new List<int>();

    void Start()
    {
        SetupBooks();
    }

    void SetupBooks()
    {
        foreach (Book b in allBooks)
        {
            b.canClick = false;
        }

        List<Book> temp = new List<Book>(allBooks);
        for (int i = 0; i < temp.Count; i++)
        {
            int rand = Random.Range(i, temp.Count);
            (temp[i], temp[rand]) = (temp[rand], temp[i]);
        }

        temp = temp.GetRange(0, booksToOpen);

        usedNumbers.Clear();

        foreach (Book book in temp)
        {
            int num = GetUniqueNumber();
            book.SetNumber(num);
            book.canClick = true;
        }

        Debug.Log("ห้อง Algorithm: เปิดได้ 5 เล่ม");
    }

    int GetUniqueNumber()
    {
        int rand;
        do
        {
            rand = Random.Range(minNumber, maxNumber + 1);
        }
        while (usedNumbers.Contains(rand));

        usedNumbers.Add(rand);
        return rand;
    }
}
