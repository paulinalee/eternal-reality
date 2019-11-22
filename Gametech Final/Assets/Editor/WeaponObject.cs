using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponObject {

    public string name = "";
    public string description = "";
    public string imgpath = "";
    public List<SkillObject> skills = new List<SkillObject>();
}

[Serializable]
public class SkillObject {
    public string name = "";
    public string description = "";
    public string imgpath = "";
    public List<LevelObject> levels = new List<LevelObject>();
}

[Serializable]
public class LevelObject {
    public float power = 0.0f;
    public float range = 0.0f;
    public float speed = 0.0f;
    //type
}