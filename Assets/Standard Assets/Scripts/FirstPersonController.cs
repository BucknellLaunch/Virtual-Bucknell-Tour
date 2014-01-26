using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {

	// Charactor controller to move
	CharacterController characterController;

	// Move
	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 20.0f;
	
	float verticalRotation = 0;
	public float upDownRange = 60.0f;
	
	float verticalVelocity = 0;



	Quaternion originalRotation;
	
	void Start () {
		// Lock cursor and get cc from camera
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController>();

		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
		originalRotation = transform.localRotation;
	}
	
	void Update () {
		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		if( characterController.isGrounded && Input.GetButton("Jump"))
		{
			verticalVelocity = jumpSpeed;
		}
		else
		{
			verticalVelocity = 0;	// Need to be removed once we have terrian
		}
		
		
		Vector3 speed = new Vector3( sideSpeed, verticalVelocity, forwardSpeed );
		
		speed = transform.rotation * speed;
		
		characterController.Move( speed * Time.deltaTime );



		
		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, rotLeftRight, 0);
		
		
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		
	}

}




