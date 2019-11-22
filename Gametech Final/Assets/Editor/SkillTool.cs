using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SkillTool : EditorWindow
{

    [MenuItem("Editor/Skill Tree Creator")]
    public static void ShowWindow()
    {
        GetWindow<SkillTool>(true, "Skill Tree Creator", true);
    }

    private void OnGUI()
    {
        Rect button1pos = new Rect(position.width / 4, position.height / 2, position.width / 4, 25);
        if (GUI.Button(button1pos, "Create New Weapon")) {
            GetWindowWithRect<CreateWeapon>(new Rect(0, 0, 600, 700));
            Close();
        }
        Rect button2pos = new Rect(position.width / 2, position.height / 2, position.width / 4, 25);
        if (GUI.Button(button2pos, "Load Weapon")) {
            string path = EditorUtility.OpenFilePanel("Load Saved Weapon Data", "", "txt");
            if (path.Length != 0)
            {
                var fileText = File.ReadAllText(path);
                LoadWeapon load = GetWindowWithRect<LoadWeapon>(new Rect(0, 0, 600, 700));
                load.init(fileText);
                //Close();
            }
        }
    }
}