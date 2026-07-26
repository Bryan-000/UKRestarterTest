namespace UKRestarterTest;

using BepInEx;
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
                // idk why but env variables are passed, and doorstop sets some to prevent like loading twice or smt?? idk
                // so i reset it since if you dont, doorstop just skips loading bepinex, and thus, all your mods :P
                Environment.SetEnvironmentVariable("DOORSTOP_INITIALIZED", null);
                Environment.SetEnvironmentVariable("DOORSTOP_DISABLE", null);

                Application.OpenURL(Paths.ExecutablePath);
            };

            Application.Quit();
        }
    }
}