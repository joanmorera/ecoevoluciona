using System.Numerics;
using System;
using System.Net;
using System.Linq;
using System.Net.Mime;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AutoSlide : MonoBehaviour
{
    public Text uiText;
    private string file;
    private string textToWrite;
    private float timer;
    private float speed;
    private bool filled;

    private int currMin;
    private int currMax;
    string[,] array2DTexts = new string[14, 14];

    private void Start()
    {
        filled = false;
        timer = 0f;
        speed = 15f;

        currMin = 1;
        currMax = 2;


        //Load text from file, always with format XX-YY: and the text per string.
        int n1 = 0;
        int n2 = 0;
        int oldN1 = 0;
        int oldN2 = 0;
        string oldString = "";
        #if UNITY_EDITOR
            file = File.ReadAllText(Application.dataPath + "/Resources/Text/MapDep_es.txt");
        #else
            TextAsset mytxtData = (TextAsset)Resources.Load<TextAsset>("Text/MapDep_es");
            file = mytxtData.text;
        #endif
        
        for (int i=0;i<file.Length-3;i++)
        {
            if (file.Substring(i+3,1).Equals("$")) // case of reading the control character 3 positions ahead
            {     
                if (n1!=0 || n2!=0)
                {
                    oldN1=n1;
                    oldN2=n2;
                    oldString="";
                }
                n1=0;
                n2=0;           
                if (file.Substring(i+1,1).Equals("-")) // case of 2nd number of 1 digit
                {
                    Int32.TryParse(file.Substring(i+2,1),out n2);
                    try
                    {
                        if (!file.Substring(i-1,1).Equals(""))   // case of 1st number of 2 digits
                        {
                            Int32.TryParse(file.Substring(i-1,2),out n1);            // case XX-Y
                            if (oldN1!=0 || oldN2!=0)
                            {
                                oldString = array2DTexts[oldN1,oldN2];
                                array2DTexts[oldN1,oldN2] = oldString.Substring(0,oldString.Length-4);
                            }
                        }
                    } 
                    catch
                    {
                        // case of 1st number of 1 digit
                        // case X-Y
                        Int32.TryParse(file.Substring(i,1),out n1);
                        if (oldN1!=0 || oldN2!=0)
                        {
                            oldString = array2DTexts[oldN1,oldN2];
                            array2DTexts[oldN1,oldN2] = oldString.Substring(0,oldString.Length-3);
                        }
                    }
                }
                else // case of 2nd number of 2 digits
                {
                    Int32.TryParse(file.Substring(i+1,2),out n2);
                    try
                    {
                        if (!file.Substring(i-2,1).Equals(""))   // case of 1st number of 2 digits
                        {
                            Int32.TryParse(file.Substring(i-2,2),out n1);   // case XX-YY
                            if (oldN1!=0 || oldN2!=0)
                            {
                                oldString = array2DTexts[oldN1,oldN2];
                                array2DTexts[oldN1,oldN2] = oldString.Substring(0,oldString.Length-5);
                            }
                        }
                    } 
                    catch
                    {
                        // case of 1st number of 1 digit
                        // case X-YY
                        Int32.TryParse(file.Substring(i-1,1),out n1);
                        if (oldN1!=0 || oldN2!=0)
                        {
                            oldString = array2DTexts[oldN1,oldN2];
                            array2DTexts[oldN1,oldN2] = oldString.Substring(0,oldString.Length-4);
                        }
                    }
                }
                array2DTexts[n1,n2]="";
            }
            else
            {
                array2DTexts[n1,n2] = array2DTexts[n1,n2]+file.Substring(i+3,1);
            }
        }
    }    


    //PRE: we call this function AFTER having called SetFirstNum and SetSecondNum
    public void BeginSlide()
    {
        uiText.text = array2DTexts[currMin,currMax];
        filled = true;
    }

    public void SetFirstNum(int n)
    {
        currMin=n;
    }

    public void SetSecondNum(int n)
    {
        currMax=n;
    }

    private void Update()
    {
        if (filled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                RectTransform rectTransform = uiText.GetComponent<RectTransform>();
                rectTransform.SetTop(300+timer*speed);
            }
        }
        else
        {
            timer = 0;
        }
    }

    public void SpeedUpSlide()
    {
        speed++;
    }

    public void SpeedDownSlide()
    {
        speed--;
    }

    public void StopSlide()
    {
        filled = false;
    }
}

