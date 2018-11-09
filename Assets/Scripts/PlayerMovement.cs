using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 6.0f;
	public float lookSpeed = 6.0f;
	public GameObject arrowPrefab;
	public Transform arrowSpawn;
	public float pullTime = 3f;
	public float pullSpeed = 0.2f;
	public float minPull = 10f;
	public float maxPull = 20f;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private float vertical = 0f;
	private float horizontal = 0f;
	private float gravity = 20.0f;
	private float arrowSpeed = 0f;
	private GameObject arrow = null;

	void Start() {
		controller = GetComponent<CharacterController>();
		arrowSpeed = minPull;
	}

	void Update() {
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection = moveDirection * moveSpeed;
		}
		if (Input.GetButtonDown("Jump")) {
			if (arrow) {
				Destroy(arrow);
			}
			arrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity, Camera.main.transform);
			arrow.transform.localRotation = Quaternion.identity;
		}
		if (Input.GetButton("Jump")) {
			arrowSpeed = Mathf.Clamp(arrowSpeed + pullSpeed, minPull, maxPull);
		}
		if (Input.GetButtonUp("Jump")) {
			arrow.transform.parent = null;
			arrow.GetComponent<ArrowScript>().Fire(arrowSpeed);
			arrowSpeed = minPull;
		}

		// Apply gravity
		moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);

		horizontal = Input.GetAxis("Mouse X") * lookSpeed;
		vertical += Input.GetAxis("Mouse Y") * -lookSpeed;
		vertical = Mathf.Clamp(vertical, -75, 75);

		controller.transform.Rotate(0, horizontal, 0);
		Camera.main.transform.localRotation = Quaternion.Euler(vertical, 0, 0);
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body != null && !body.isKinematic) {
			body.velocity += hit.controller.velocity;
		}
	}

}
