using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, Screen.height - 50, 100, 30), "Restart"))
        {
            if (OnClicked != null)
                OnClicked();
        }
    }

}
