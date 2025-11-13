using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtons : MonoBehaviour
{
    public bool isOn01 = false;
    public bool isOn02 = false;
    public bool isOn03 = false;
    public bool isOn04 = false;
    public bool isOn05 = false;
    public bool isOn06 = false;
    public GameObject panel01;
        public GameObject panel02;
    public GameObject panel03;
        
    public GameObject panel04;
    public GameObject panel05;
    public GameObject panel06;

    public void Btn01()
    {
        isOn01 = !isOn01;
        panel01.SetActive(isOn01);
    }

    public void Btn02()
    {
        isOn02 = !isOn02;
        panel02.SetActive(isOn02);
    }

    public void Btn03()
    {
        isOn03 = !isOn03;
        panel03.SetActive(isOn03);
    }

        
    public void Btn04()
    {
        isOn04 = !isOn04;
        panel04.SetActive(isOn04);
    }

    public void Btn05()
    {
        isOn05 = !isOn05;
        panel05.SetActive(isOn05);
    }

    public void Btn06()
    {
        isOn06 = !isOn06;
        panel06.SetActive(isOn06);
    }
}
