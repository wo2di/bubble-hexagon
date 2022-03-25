using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.iOS.Xcode;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
public class iOSPostProcessBuild
{
    [PostProcessBuild(0)]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if(target == BuildTarget.iOS) { AddPListValues(pathToBuiltProject); }
    }

    static void AddPListValues(string pathToXcode)
    {
        string plistPath = pathToXcode + "/Info.plist";
        PlistDocument plistObj = new PlistDocument();
        plistObj.ReadFromString(File.ReadAllText(plistPath));
        PlistElementDict plistRoot = plistObj.root;
        plistRoot.SetString("NSUserTrackingUsageDescription", "Your data will be used to provide you a better and personalized ads");
        plistRoot.SetString("UIUserInterfaceStyle", "Dark");
        File.WriteAllText(plistPath, plistObj.WriteToString());
    }
}
