using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    public Weapon weapon;
    public GameObject name, description;
    void Start()
    {
        Refresh();
        name = GameObject.Find("WeaponName");
        description = GameObject.Find("WeaponDescription");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T)) {
            Debug.Log("keycode pressed");
            canvas.enabled = !canvas.enabled;
            Refresh();
        }
    }

    void Refresh() {
        Debug.Log("refreshing ui");
    }
}
