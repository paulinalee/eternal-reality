using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Skill : GUIContent {

    public Texture2D skillTexture;
    float x, y;
    float w, h;
    string name, description = "";
    string path = "";
    public List<Level> levels;
    int currentlevs, addedlevs;
    Vector2 scrollPos;
    public Skill(float xpos, float ypos, float windoww, float windowh) {
        x = xpos;
        y = ypos;
        w = windoww;
        h = windowh;
        levels = new List<Level>();
        levels.Add(new Level());
        currentlevs = 1;
        addedlevs = 1;
    }

    public Skill(float xpos, float ypos, float windoww, float windowh, SkillObject skillobj)
    {
        x = xpos;
        y = ypos;
        w = windoww;
        h = windowh;
        name = skillobj.name;
        description = skillobj.description;
        path = skillobj.imgpath;
        skillTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        //load image
        levels = new List<Level>();
        for (int i = 0; i < skillobj.levels.Count; ++i) {
            Level l = new Level(skillobj.levels[i]);
            levels.Add(l);
            currentlevs = i;
            addedlevs = i;
        }
    }

    public void Draw() {
        Rect skillBox = new Rect(x, y, w / 3, h / 3 - 50);
        GUI.Box(skillBox, skillTexture);
        if (skillBox.Contains(Event.current.mousePosition))
        {
            if (Event.current.type == EventType.MouseDown)
            {
                path = EditorUtility.OpenFilePanel("Load Skill Image", Application.dataPath + "/Resources", "png");
                if (path.Length != 0)
                {
                    if (path.StartsWith(Application.dataPath))
                    {
                        path = "Assets" + path.Substring(Application.dataPath.Length);
                    }
                    skillTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                }
            }
        }
        Rect levelBox = new Rect(x, y + (h / 3 - 50) + 5, w / 3, h - (h / 3 - 50));
        GUILayout.BeginArea(levelBox);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Skill Name");
        name = EditorGUILayout.TextField(name);
        GUILayout.EndHorizontal();
        GUILayout.Label("Skill Description:");
        description = EditorGUILayout.TextArea(description, GUILayout.Height(50));
        scrollPos =  EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(w / 3), GUILayout.Height(h / 4));
        foreach (Level l in levels) {
            l.Draw();
        }
        EditorGUILayout.EndScrollView();
        GUILayout.BeginHorizontal();
        GUILayout.Space(levelBox.width / 2 - 10);
        if (GUILayout.Button("+", GUILayout.Width(20)))
        {
            addedlevs += 1;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        if (addedlevs > currentlevs) {
            addLevel();
        }
    }

    private void addLevel() {
        levels.Add(new Level());
        currentlevs = addedlevs;
        Draw();
    }

    public void setValues(ref SkillObject skillobj) {
        skillobj.name = name;
        skillobj.description = description;
        skillobj.imgpath = path;
        skillobj.levels.Clear();
        foreach (Level l in levels) {
            LevelObject levobj = new LevelObject();
            l.setValues(ref levobj);
            skillobj.levels.Add(levobj);
        }
    }

}
