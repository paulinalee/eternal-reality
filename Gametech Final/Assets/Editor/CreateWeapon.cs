using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateWeapon : EditorWindow {

    public Texture2D weaponTexture;
    List<Skill> skills;
    int width = 600;
    int height = 700;
    bool showDescrip = false;
    string name = "";
    string descrip = "";
    //GUIContent content;

    void OnEnable() {
        //content = new GUIContent("Box", weaponTexture, "Tooltip");
        Debug.Log("START");
        weaponTexture = new Texture2D(2, 2);
        skills = new List<Skill>();
        Skill skill1 = new Skill(0, height / 3 - 20, width, height);
        skills.Add(skill1);
        Skill skill2 = new Skill(width / 3, height / 3 - 20, width, height);
        skills.Add(skill2);
        Skill skill3 = new Skill((width / 3) * 2, height / 3 - 20, width, height);
        skills.Add(skill3);
    }

    private void OnGUI()
    {
        //Debug.Log("CREATEWEAPON ONGUI");
        Rect weaponBoxPos = new Rect(position.width / 3, 10, position.width / 3, position.height / 3 - 50);
        GUI.Box(weaponBoxPos, weaponTexture);
        //maybe change to drag and drop
        foreach (Skill s in skills) {
            s.Draw();
        }
        Rect descriptionPos = new Rect(weaponBoxPos.x, weaponBoxPos.yMax, weaponBoxPos.width, 15);
        showDescrip = EditorGUI.Foldout(descriptionPos, showDescrip, "EDIT WEAPON");
        Rect foldoutPos = new Rect(descriptionPos.x, descriptionPos.yMax, descriptionPos.width, weaponBoxPos.height);
        if (showDescrip)
        {
            GUILayout.BeginArea(foldoutPos);
            GUILayout.BeginHorizontal("Box");
            GUILayout.Label("Name");
            name = EditorGUILayout.TextField(name);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Label("Description");
            descrip = EditorGUILayout.TextArea(descrip, GUILayout.Height(weaponBoxPos.height - 50));
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        else
        {
            if (weaponBoxPos.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    string path = EditorUtility.OpenFilePanel("Load Weapon Image", "", "png");
                    if (path.Length != 0)
                    {
                        if (path.StartsWith(Application.dataPath))
                        {
                            path = "Assets" + path.Substring(Application.dataPath.Length);
                        }
                        Debug.Log(path);
                        weaponTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                    }
                }
            }
        }
    }
}
