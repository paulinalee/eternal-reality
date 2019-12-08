using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SkillCooldown : MonoBehaviour
{
    private bool onCooldown;
    private Text cdText;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        cdText = transform.Find("CDTime").GetComponent<Text>();
    }

    void OnEnable()
    {
        onCooldown = true;
    }

    void OnDisable()
    {
        onCooldown = false;
    }

    public void setTimer(float val) {
        timer = val;
    }
    // Update is called once per frame
    void Update()
    {
        if (onCooldown) {
            timer -= Time.deltaTime;
            int timeLeft = Convert.ToInt32(Math.Ceiling(timer));
            cdText.text = timeLeft.ToString();
        }
    }
}
