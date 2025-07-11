using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageTurning : MonoBehaviour
{
    public Button NextButton;
    public Button PreviousButton;
    public GameObject[] Pages;
    private int PageNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        NextButton?.onClick.AddListener(NextPage);
        PreviousButton?.onClick.AddListener(PreviousPage);
        for(int i = 0; i < Pages.Length; i++)
        {
            if (i != 0)
                Pages[i].SetActive(false);
        }
        if (Pages.Length == 1)
        {
            NextButton.gameObject.SetActive(false);
            PreviousButton.gameObject.SetActive(false);
        }
        else
        {
            PreviousButton.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        PreviousButton.gameObject.SetActive(true);
        Pages[PageNum - 1].SetActive(false);
        ++PageNum;
        Pages[PageNum - 1].SetActive(true);
        if(PageNum == Pages.Length)
        {
            NextButton.gameObject.SetActive(false);
        }
    }
    public void PreviousPage()
    {
        NextButton.gameObject.SetActive(true);
        Pages[PageNum - 1].SetActive(false);
        --PageNum;
        Pages[PageNum - 1].SetActive(true);
        if (PageNum == 1)
        {
            PreviousButton.gameObject.SetActive(false);
        }
    }

}
