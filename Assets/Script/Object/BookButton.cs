using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookButton : MonoBehaviour
{
    private Book book;
    [SerializeField] private int page;

    void Awake()
    {
        book = GameObject.Find("CloseUpBook").GetComponent<Book>();
        transform.GetComponent<Button>().onClick.AddListener(() => book.TurnOver(page));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
