using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ActivateVideoPlayer(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateVideoPlayer(bool active)
    {
        for(int i=0;i<5;i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(active);
        }
    }
}
