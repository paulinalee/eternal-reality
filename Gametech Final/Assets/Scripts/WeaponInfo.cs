using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponInfo {

    public string name = "";
    public string description = "";
    public string imgpath = "";
    public List<SkillInfo> skills = new List<SkillInfo>();
}

[Serializable]
public class SkillInfo {
    public string name = "";
    public string description = "";
    public string imgpath = "";
    public List<LevelInfo> levels = new List<LevelInfo>();
}

[Serializable]
public class LevelInfo {
    public float power = 0.0f;
    public float range = 0.0f;
    public float speed = 0.0f;
    //type
}