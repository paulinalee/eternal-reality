using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevel
{
    // Start is called before the first frame update

    private float power, speed, range;

    public SkillLevel(float pwr, float spd, float rng) {
        power = pwr;
        speed = spd;
        range = rng;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getPower() {
		return power;
	}

	public float getSpeed() {
		return speed;
	}

	public float getRange() {
		return range;
	}
}
