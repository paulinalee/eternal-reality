using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	CharacterController characterController;
	public float speed = 5.0f;
	public float gravity = 9.8f;
	private Vector3 direction = Vector3.zero;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
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
        characterController.Move(direction * Time.deltaTime);

	}
}
