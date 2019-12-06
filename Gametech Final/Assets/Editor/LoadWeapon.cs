using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LoadWeapon : EditorWindow
{

    Vector2 scrollpos;
    public Texture2D weaponTexture;
    List<Skill> skills;
    int width = 600;
    int height = 700;
    bool showDescrip = false;
    string name = "weapon";
    string descrip = "";
    string path = "";
    string oldName = "";
    WeaponObject weapon;

    public void init(string text)
    {
        weapon = JsonUtility.FromJson<WeaponObject>(text);
        //load image, name, descrip from file
        path = weapon.imgpath;
        name = weapon.name;
        oldName = name;
        descrip = weapon.description;
        weaponTexture = new Texture2D(2, 2);
        weaponTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        //load skills from file
        skills = new List<Skill>();
        Skill skill1 = new Skill(0, height / 3 - 20, width, height, weapon.skills[0]);
        skills.Add(skill1);
        Skill skill2 = new Skill(width / 3, height / 3 - 20, width, height, weapon.skills[1]);
        skills.Add(skill2);
        Skill skill3 = new Skill((width / 3) * 2, height / 3 - 20, width, height, weapon.skills[2]);
        skills.Add(skill3);
    }

    private void OnGUI()
    {
        //scrollpos = EditorGUILayout.BeginScrollView(scrollpos, GUILayout.Height(height));
        //Debug.Log("CREATEWEAPON ONGUI");
        if (GUILayout.Button("SAVE", GUILayout.Width(100)))
        {
            setValues();
            string json = JsonUtility.ToJson(weapon);
            Debug.Log("FILE SAVED!");
            File.Delete(Application.dataPath + "/Weapons/" + oldName + ".txt"); // delete the old file
            string metaPath = Application.dataPath + "/Weapons/" + oldName + ".txt.meta";
            if (File.Exists(metaPath)) {
                File.Delete(metaPath); // cleanup metadata so the editor doesnt scream
            }
            File.WriteAllText(Application.dataPath + "/Weapons/" + name + ".txt", json);
        }
        Rect weaponBoxPos = new Rect(position.width / 3, 10, position.width / 3, position.height / 3 - 50);
        GUI.Box(weaponBoxPos, weaponTexture);
        //maybe change to drag and drop
        foreach (Skill s in skills)
        {
            s.Draw();
        }
        Rect descriptionPos = new Rect(weaponBoxPos.x, weaponBoxPos.yMax, weaponBoxPos.width, 15);
        showDescrip = EditorGUI.Foldout(descriptionPos, showDescrip, "EDIT WEAPON");
        Rect foldoutPos = new Rect(descriptionPos.xMax, weaponBoxPos.yMax, descriptionPos.width, weaponBoxPos.height);
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
                    Debug.Log(path);
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
    }
}
