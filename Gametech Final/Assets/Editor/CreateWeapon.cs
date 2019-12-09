using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateWeapon : EditorWindow {

    Vector2 scrollpos;
    public Texture2D weaponTexture;
    List<Skill> skills;
    int width = 600;
    int height = 700;
    bool showDescrip = false;
    string name = "weapon";
    string descrip = "";
    string path = "";
    int maxlv = 2;
    WeaponObject weapon;

    void OnEnable() {
        //content = new GUIContent("Box", weaponTexture, "Tooltip");
        //Debug.Log("START");
        weaponTexture = new Texture2D(2, 2);
        skills = new List<Skill>();
        Skill skill1 = new Skill(0, height / 3 - 20, width, height);
        skills.Add(skill1);
        Skill skill2 = new Skill(width / 3, height / 3 - 20, width, height);
        skills.Add(skill2);
        Skill skill3 = new Skill((width / 3) * 2, height / 3 - 20, width, height);
        skills.Add(skill3);
        weapon = new WeaponObject();
    }

    private void OnGUI()
    {
        //Rect workarea = GUILayoutUtility.GetRect(0, 10000, 0, 10000);
        //scrollpos = GUI.BeginScrollView(workarea, scrollpos, new Rect(0, 0, 70, 500));
        //Debug.Log("CREATEWEAPON ONGUI");
        if (GUILayout.Button("SAVE", GUILayout.Width(100)))
        {
            setValues();
            string json = JsonUtility.ToJson(weapon);
            Debug.Log("FILE SAVED!");
            File.WriteAllText(Application.streamingAssetsPath + "/Weapons/" + name + ".txt", json);
        }
        Rect weaponBoxPos = new Rect(position.width / 3, 10, position.width / 3, position.height / 3 - 50);
        GUI.Box(weaponBoxPos, weaponTexture);
        //maybe change to drag and drop
        foreach (Skill s in skills)
        {
            s.Draw();
            if (s.levels.Count > 2 && maxlv < s.levels.Count) {
                maxlv = s.levels.Count;
                //height += 100;
            }
        }
        Rect descriptionPos = new Rect(weaponBoxPos.x, weaponBoxPos.yMax, weaponBoxPos.width, 15);
        showDescrip = EditorGUI.Foldout(descriptionPos, showDescrip, "EDIT WEAPON");
        Rect foldoutPos = new Rect(descriptionPos.xMax, weaponBoxPos.yMin, descriptionPos.width, weaponBoxPos.height);
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
        //GUI.EndScrollView();
        if (weaponBoxPos.Contains(Event.current.mousePosition))
        {
            if (Event.current.type == EventType.MouseDown)
            {
                path = EditorUtility.OpenFilePanel("Load Weapon Image", Application.dataPath + "/Resources", "png");
                if (path.Length != 0)
                {
                    if (path.StartsWith(Application.dataPath))
                    {
                        path = "Assets" + path.Substring(Application.dataPath.Length);
                    }
                    weaponTexture = AssetDatabase.LoadAssetAtPath<Sprite>(path).texture;
                }
            }
        }
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
