using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public Transform MenuQuit;

    void Start()
    {
        ActivateQuitMenu(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void ActivateQuitMenu(bool active)
    {
        MenuQuit.GetChild(0).gameObject.SetActive(active);
    }

}
