using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Skill : GUIContent {

    public Texture2D skillTexture;
    float x, y;
    float w, h;
    string name, description = "";
    List<Level> levels;
    int currentlevs, addedlevs;

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

    public void Draw() {
        Rect skillBox = new Rect(x, y, w / 3, h / 3 - 50);
        GUI.Box(skillBox, skillTexture);
        Rect levelBox = new Rect(x, y + (h / 3 - 50) + 5, w / 3, h - (h / 3 - 50));
        GUILayout.BeginArea(levelBox);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Skill Name");
        name = EditorGUILayout.TextField(name);
        GUILayout.EndHorizontal();
        GUILayout.Label("Skill Description:");
        description = EditorGUILayout.TextArea(description, GUILayout.Height(50));
        foreach (Level l in levels) {
            l.Draw();
        }
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
        Debug.Log("adding!");
        levels.Add(new Level());
        currentlevs = addedlevs;
        Draw();
    }

}
