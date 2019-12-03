using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevel
{
    // Start is called before the first frame update

    private int power, speed, range;

    public SkillLevel(int pwr, int spd, int rng) {
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

    public int getPower() {
		return power;
	}

	public int getSpeed() {
		return speed;
	}

	public int getRange() {
		return range;
	}
}
