using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateWeapon : EditorWindow {

<<<<<<< HEAD
    Vector2 scrollpos;
=======
>>>>>>> 59af5e65d10e2a492600b45ec07aa9723a8c0fec
    public Texture2D weaponTexture;
    List<Skill> skills;
    int width = 600;
    int height = 700;
    bool showDescrip = false;
<<<<<<< HEAD
    string name = "weapon";
    string descrip = "";
    string path = "";
    WeaponObject weapon;

    void OnEnable() {
        //content = new GUIContent("Box", weaponTexture, "Tooltip");
        //Debug.Log("START");
=======
    string name = "";
    string descrip = "";
    //GUIContent content;

    void OnEnable() {
        //content = new GUIContent("Box", weaponTexture, "Tooltip");
        Debug.Log("START");
>>>>>>> 59af5e65d10e2a492600b45ec07aa9723a8c0fec
        weaponTexture = new Texture2D(2, 2);
        skills = new List<Skill>();
        Skill skill1 = new Skill(0, height / 3 - 20, width, height);
        skills.Add(skill1);
        Skill skill2 = new Skill(width / 3, height / 3 - 20, width, height);
        skills.Add(skill2);
        Skill skill3 = new Skill((width / 3) * 2, height / 3 - 20, width, height);
        skills.Add(skill3);
<<<<<<< HEAD
        weapon = new WeaponObject();
=======
>>>>>>> 59af5e65d10e2a492600b45ec07aa9723a8c0fec
    }

    private void OnGUI()
    {
<<<<<<< HEAD
        //scrollpos = EditorGUILayout.BeginScrollView(scrollpos, GUILayout.Height(height));
        //Debug.Log("CREATEWEAPON ONGUI");
        if (GUILayout.Button("SAVE", GUILayout.Width(100)))
        {
            setValues();
            string json = JsonUtility.ToJson(weapon);
            Debug.Log("FILE SAVED!");
            File.WriteAllText(Application.dataPath + "/Weapons/" + name + ".txt", json);
        }
        Rect weaponBoxPos = new Rect(position.width / 3, 10, position.width / 3, position.height / 3 - 50);
        GUI.Box(weaponBoxPos, weaponTexture);
        //maybe change to drag and drop
        foreach (Skill s in skills)
        {
=======
        //Debug.Log("CREATEWEAPON ONGUI");
        Rect weaponBoxPos = new Rect(position.width / 3, 10, position.width / 3, position.height / 3 - 50);
        GUI.Box(weaponBoxPos, weaponTexture);
        //maybe change to drag and drop
        foreach (Skill s in skills) {
>>>>>>> 59af5e65d10e2a492600b45ec07aa9723a8c0fec
            s.Draw();
        }
        Rect descriptionPos = new Rect(weaponBoxPos.x, weaponBoxPos.yMax, weaponBoxPos.width, 15);
        showDescrip = EditorGUI.Foldout(descriptionPos, showDescrip, "EDIT WEAPON");
<<<<<<< HEAD
        Rect foldoutPos = new Rect(descriptionPos.xMax, weaponBoxPos.yMin, descriptionPos.width, weaponBoxPos.height);
=======
        Rect foldoutPos = new Rect(descriptionPos.x, descriptionPos.yMax, descriptionPos.width, weaponBoxPos.height);
>>>>>>> 59af5e65d10e2a492600b45ec07aa9723a8c0fec
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
<<<<<<< HEAD
        if (weaponBoxPos.Contains(Event.current.mousePosition))
        {
            if (Event.current.type == EventType.MouseDown)
            {
                path = EditorUtility.OpenFilePanel("Load Weapon Image", "", "png");
                if (path.Length != 0)
                {
                    if (path.StartsWith(Application.dataPath))
                    {
                        path = "Assets" + path.Substring(Application.dataPath.Length);
                    }
                    weaponTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                }
            }
        }
        //EditorGUILayout.EndScrollView();
    }

    public void setValues()
    {
        weapon.name = name;
        weapon.description = descrip;
        weapon.imgpath = path;
        weapon.skills.Clear();
        foreach (Skill s in skills)
        {
            SkillObject skillobj = new SkillObject();
            s.setValues(ref skillobj);
            weapon.skills.Add(skillobj);
        }
=======
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
>>>>>>> 59af5e65d10e2a492600b45ec07aa9723a8c0fec
    }
}
