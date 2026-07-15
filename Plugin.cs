namespace UKRestarterTest;

using BepInEx;
using System;
using UnityEngine;

[BepInPlugin("Bryan_-000-.UKRestarterTest", "UKRestarterTest", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // this is just to make OpenURL run as late as possible
            // since if this instance is still alive when the next one starts loading, shit breaks
            Application.quitting += () =>
            {
                // idk why but env variables are passed, and doorstop sets and init variable for whatever reason
                // so i reset it since if you dont doorstop just skips loading bepinex :P
                Environment.SetEnvironmentVariable("DOORSTOP_INITIALIZED", null);
                Application.OpenURL(Paths.ExecutablePath);
            };

            Application.Quit();
        }
    }
}