using System.Net;
using System.IO;
using System.Reflection;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class MapDepManagement : MonoBehaviour
{
    public GameObject SEL1;
    public int nSEL1;
    public GameObject SEL2;
    public int nSEL2;
    private int nMin,nMax;
    private bool audioPlaying;
    public readonly string[] titles = {"HAZ CLIC\n EN UN ICONO","Deforestación","Pérdida de biodiversidad","Contaminación del agua","Contaminación del aire", "Contaminación del suelo", "Calentamiento global","Desperdicio alimentario","Acidificación de océanos","Ganadería intensiva","Sequías","Desastres naturales","Industria de la moda","Consumo de energía"};
    Transform[] EcoDependencies = new Transform[13];
    Transform screenText;
    public Sprite voidIcon;
    

    void Start()
    {
        screenText = this.transform.Find("ScreenText");
        EcoDependencies[0] = this.transform.Find("B1");
        EcoDependencies[1] = this.transform.Find("B2");
        EcoDependencies[2] = this.transform.Find("B3");
        EcoDependencies[3] = this.transform.Find("B4");
        EcoDependencies[4] = this.transform.Find("B5");
        EcoDependencies[5] = this.transform.Find("B6");
        EcoDependencies[6] = this.transform.Find("B7");
        EcoDependencies[7] = this.transform.Find("B8");
        EcoDependencies[8] = this.transform.Find("B9");
        EcoDependencies[9] = this.transform.Find("B10");
        EcoDependencies[10] = this.transform.Find("B11");
        EcoDependencies[11] = this.transform.Find("B12");
        EcoDependencies[12] = this.transform.Find("B13");
        ResetPanel();
    }

    public void ResetPanel()
    {
        SEL1.GetComponent<Image>().sprite = voidIcon;
        SEL2.GetComponent<Image>().sprite = voidIcon;
        SEL1.GetComponentInChildren<Text>().text = titles[0];
        SEL2.GetComponentInChildren<Text>().text = titles[0];
        nSEL1 = 0;
        nSEL2 = 0;
        nMin=0;
        nMax=0;
        ActivateExplanation(false);
        audioPlaying=false;
    }

    public void Update()
    {
        if (audioPlaying && !GetComponent<AudioSource>().isPlaying) //if it should be playing and is not, meaning has just stopped playing
        {
            ActivateExplanation(false);
        }
    }

    public IEnumerator PlayEcoDependency(int newID)
    {
        if (nSEL1 != 0 && nSEL2 != 0)
            ResetPanel();

        if (nSEL1 == 0)
        {
            nSEL1 = newID;
            SEL1.GetComponent<Image>().sprite = EcoDependencies[newID - 1].GetComponent<Image>().sprite;
            SEL1.GetComponentInChildren<Text>().text = titles[newID];
        }
        else if (nSEL2 == 0)
        {
            nSEL2 = newID;
            SEL2.GetComponent<Image>().sprite = EcoDependencies[newID - 1].GetComponent<Image>().sprite;
            SEL2.GetComponentInChildren<Text>().text = titles[newID];
        }
        else
            ResetPanel();

        if (nSEL1==nSEL2)
            ResetPanel();

        if (nSEL1 > 0 && nSEL2 > 0 && nSEL1 != nSEL2)
        {
            if (nSEL1<nSEL2)
            {
                nMin = nSEL1;
                nMax = nSEL2;
            }
            else
            {
                nMin = nSEL2;
                nMax = nSEL1;
            }

            #if UNITY_EDITOR
                string finalPath = "file://"+Application.streamingAssetsPath+"/Audio/"+nMin+"-"+nMax+".ogg";
            #else
                string finalPath = Application.streamingAssetsPath+"/Audio/"+nMin+"-"+nMax+".ogg";
            #endif
            StartCoroutine(LoadAudio(finalPath));

            yield return new WaitForSeconds(1);
            ActivateExplanation(true);
            GetComponent<AudioSource>().Play();
            audioPlaying=true;
        }   
    }

    public void ActivateExplanation(bool activate)
    {
        screenText.GetComponentInChildren<Image>().enabled = activate;

        for (int i=0; i< screenText.childCount;i++)
        {
            if (screenText.GetChild(i).GetComponent<Image>() is not null)
                screenText.GetChild(i).GetComponent<Image>().enabled = activate;
            if (screenText.GetChild(i).GetComponentInChildren<Text>() is not null)
                screenText.GetChild(i).GetComponentInChildren<Text>().enabled = activate;
        }

        if (activate)
        {
            screenText.SendMessage("SetFirstNum",nMin);
            screenText.SendMessage("SetSecondNum",nMax);
            screenText.SendMessage("BeginSlide");
        }
        else 
        {
            if (audioPlaying)
            {
                GetComponent<AudioSource>().Stop();
                audioPlaying=false;
            }
            screenText.SendMessage("StopSlide");
        }
    }

    IEnumerator LoadAudio(string audioName)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioName,AudioType.OGGVORBIS))
        {         
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                GetComponent<AudioSource>().clip = myClip;
            }
        }
    }

}
