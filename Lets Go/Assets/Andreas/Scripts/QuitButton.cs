using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log ("Button geklickt - Spiel beenden");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit ();
#endif
    }
}
