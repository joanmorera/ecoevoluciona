using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EcoDependency : MonoBehaviour
{
    public int ID;

    void Start()
    {
    
    }

    public void OnClickedEcoDependency()
    {
        this.transform.parent.BroadcastMessage("PlayEcoDependency",this.ID);

    }


}
