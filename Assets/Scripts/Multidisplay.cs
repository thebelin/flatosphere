using UnityEngine;
using System.Collections;

public class MultiDisplay : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON.
        // Check if additional displays are available and activate each.
        for (int i = 1; i < Display.displays.Length; i++)
            Display.displays[i].Activate();

        // Set the last display as the target display (the last display should be the one with the highest index and is the one most recently activated)
        Camera.main.targetDisplay = Display.displays.Length - 1;
    }

}
