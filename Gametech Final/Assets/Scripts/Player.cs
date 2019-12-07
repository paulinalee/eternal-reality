using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	CharacterController characterController;
	public float speed = 5.0f;
	public float gravity = 9.8f;
	private Vector3 direction = Vector3.zero;
    public float mouseSens = 2.0f;
    public Weapon weapon;
    private Dictionary<string, WeaponInfo> weapons;
    private bool acceptInput;
    public int health = 100;
    private int maxHealth;
    private int points = 300;
	// Use this for initialization
	void Start () {
        maxHealth = health;
		characterController = GetComponent<CharacterController>();
        acceptInput = true;
        // force the resolution
        Screen.SetResolution (1920 , 1080, true, 60);
	}
	
	// Update is called once per frame
	void Update () {
//       Debug.Log("position X: " + transform.position.x + " || position y: " + transform.position.y + " || position z: " + transform.position.z);
        if (acceptInput) {
            checkAttack();
            if (characterController.isGrounded){
                direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                direction *= speed;
                if (Input.GetButton ("Jump")) {
                    direction.y = speed;
                }
            } else {
                direction = new Vector3(Input.GetAxis("Horizontal"), direction.y,
                Input.GetAxis("Vertical"));
                direction.x *= speed;
                direction.z *= speed;
            }

            direction.y -= gravity * Time.deltaTime;
            direction = transform.TransformDirection(direction);
            if(Input.GetAxis("Mouse X") < 0)
                transform.Rotate(Vector3.up * -mouseSens);
            if(Input.GetAxis("Mouse X") > 0)
                transform.Rotate(Vector3.up * mouseSens);
            characterController.Move(direction * Time.deltaTime);
        }
	}

    void checkAttack() {
        if (Input.GetKeyUp(KeyCode.Q)) {
            Debug.Log("Skill 1 used!");
            weapon.Attack(1);
        } else if (Input.GetKeyUp(KeyCode.E)) {
            Debug.Log("Skill 2 used!");
            weapon.Attack(2);
        } else if (Input.GetKeyUp(KeyCode.R)) {
            Debug.Log("Skill 3 used!");
            weapon.Attack(3);
        }
    }

    public void setMovable(bool val) {
        acceptInput = val;
    }

    public void healthMod(int val) {
        health += val;
        Debug.Log(health);
    }
    
    public void changeWeapon(string weaponName) {
        weapons = GameObject.Find("WeaponSelect").GetComponent<WeaponSelect>().GetWeapons();
        weapon.updateWeapon(weapons[weaponName]);
    }

    public void addPoints(int newPoints) {
        points += newPoints;
    }

    public int getPoints() {
        return points;
    }

    public void usePoints(int pointsUsed) {
        points -= pointsUsed;
    }

    public int getHealth() {
        return health;
    }

    public int getMaxHealth() {
        return maxHealth;
    }
}