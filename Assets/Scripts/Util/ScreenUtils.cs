using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUtils
{
    #region Fields

    // cached for efficient boundary checking
    float screenLeft;
    float screenRight;
    float screenTop;
    float screenBottom;

    // save an instance of the singleton
    private static ScreenUtils instance;

    #endregion

    #region Properties

    /// <summary>
    ///   get the left edge in world coordinates
    /// </summary>
    public static float ScreenLeft { get => instance.screenLeft; }

    /// <summary>
    ///   get the right edge in world coordinates
    /// </summary>
    public static float ScreenRight { get => instance.screenRight; }

    /// <summary>
    ///   get the top edge in world coordinates
    /// </summary>
    public static float ScreenTop { get => instance.screenTop; }

    /// <summary>
    ///   get the bottom edge in world coordinates
    /// </summary>
    public static float ScreenBottom { get => instance.screenBottom; }

    #endregion


    /// <summary>
    ///   Initialize the screen utilities
    /// </summary>
    public static void Initialize()
    {
        if (instance != null) return;

        instance = new ScreenUtils();
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.y));
        Debug.Log(Screen.width + " " + Screen.height);
        Debug.Log(screenBounds);
        instance.screenLeft = -screenBounds.x;
        instance.screenRight = screenBounds.x;
        instance.screenTop = screenBounds.z;
        instance.screenBottom = -screenBounds.z;
        Debug.Log(instance.screenLeft);
        Debug.Log(instance.screenRight);
        Debug.Log(instance.screenTop);
        Debug.Log(instance.screenBottom);
    }
}
