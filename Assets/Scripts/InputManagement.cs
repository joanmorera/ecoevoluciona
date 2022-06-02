using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagement : MonoBehaviour
{

    private void Update()
    {   
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                GameObject.FindWithTag("Finish").SendMessage("ActivateQuitMenu",true); //Finds QuitGame_obj
            }
        }
    }

}
