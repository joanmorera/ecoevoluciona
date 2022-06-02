using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuScene : MonoBehaviour
{
    public Button B1;
    public Button B2;
    public Button B3;
    public Button B4;
    public Button B5;
    public Button B6;
    public Button B7;
    public Button B8;
    public Button B9;
    public Button B10;
    public Button B11;
    public Button B12;

    void Awake() 
    {
        B2.interactable = false;
        B3.interactable = false;
        B4.interactable = false;
        B5.interactable = false;
        B6.interactable = false;
        B7.interactable = false;
        B8.interactable = false;
        B9.interactable = false;
        B10.interactable = false;
        B11.interactable = false;
        B12.interactable = false;
    }

    void Start()
    {
        Change_MAIN_Menu();
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        print("Streaming Assets Path: " + Application.streamingAssetsPath);
        FileInfo[] allFiles = directoryInfo.GetFiles("*.*");
    }

    void Update()
    {
        
    }

    public void Change_INTRO_Menu()
    {
        Change_Menu_To("IntroMenu");
    }

    public void Change_MAIN_Menu()
    {
        Change_Menu_To("MainMenu");
    }

    public void Change_MAPECO_Menu()
    {
        Change_Menu_To("MapEcodependencies");
        Transform map = this.transform.Find("MapEcodependencies");
        if (map != null)
            map.SendMessage("ResetPanel");
    }

    void Change_Menu_To(string menu)
    {
        int i = 0;

        for (i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).name != menu)
                this.transform.GetChild(i).gameObject.SetActive(false);
            else
                this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
