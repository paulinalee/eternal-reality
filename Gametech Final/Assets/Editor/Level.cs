using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Level : GUIContent {

    float power, speed, range;
    string type;

    public Level() {
        power = 0;
        speed = 0;
        range = 0;
        type = null;
    }

<<<<<<< HEAD
    public Level(LevelObject levobj)
    {
        power = levobj.power;
        speed = levobj.speed;
        range = levobj.range;
        type = null;
    }

=======
>>>>>>> 59af5e65d10e2a492600b45ec07aa9723a8c0fec
    public void Draw() {
        GUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal("Box");
        GUILayout.Label("Attack Power");
        power = EditorGUILayout.FloatField(power);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("Box");
        GUILayout.Label("Attack Speed");
        speed = EditorGUILayout.FloatField(speed);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("Box");
        GUILayout.Label("Attack Range");
        range = EditorGUILayout.FloatField(range);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }
<<<<<<< HEAD

    public void setValues(ref LevelObject levelobj) {
        levelobj.power = power;
        levelobj.speed = speed;
        levelobj.range = range;
    }
=======
>>>>>>> 59af5e65d10e2a492600b45ec07aa9723a8c0fec
}
