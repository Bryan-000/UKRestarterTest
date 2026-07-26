namespace UKRestarterTest;

using BepInEx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[BepInPlugin("Bryan_-000-.UKRestarterTest", "UKRestarterTest", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public void Start() =>
        LogEnvVars();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // this is just to make OpenURL run as late as possible
            // since if this instance is still alive when the next one starts loading, shit breaks
            Application.quitting += () =>
            {
                LogEnvVars();
                // idk why but env variables are passed, and doorstop sets some to prevent like loading twice or smt?? idk
                // so i reset it since if you dont, doorstop just skips loading bepinex, and thus, all your mods :P
                Environment.SetEnvironmentVariable("DOORSTOP_INITIALIZED", null);
                Environment.SetEnvironmentVariable("DOORSTOP_DISABLE", null);

                Application.OpenURL(Paths.ExecutablePath);
            };

            Application.Quit();
        }
    }

    public void LogEnvVars()
    {
        IEnumerable<(string, string)> vars = GrabEnvVars().OrderBy(var => var.var);
        Logger.LogInfo("Enviroment Variables:\n\n" +
            string.Join("\n\n",
                vars.Select(var =>
                    $"\"{var.Item1}\": \"{var.Item2}\""
                )
            )
        );
    }

    public IEnumerable<(string var, string val)> GrabEnvVars()
    {
        IDictionaryEnumerator idict = Environment.GetEnvironmentVariables().GetEnumerator();
        while (idict.MoveNext())
            yield return (idict.Key.ToString(), idict.Value.ToString());
    }
}