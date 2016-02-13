using UnityEngine;
using System.Collections;

public class ZeroGravityPlayer : MonoBehaviour {
	public float speed = 5.0F;
	public float rotateSpeed = 2.0F;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		var straight = Input.GetAxisRaw("Vertical");
		var strafe = Input.GetAxisRaw("Horizontal");
		var pitch = Input.GetAxisRaw("Pitch");
		var yaw = Input.GetAxisRaw("Yaw");
		var rigidBody = gameObject.GetComponent<Rigidbody>();
		var up = Input.GetAxisRaw("Up");
		var down = Input.GetAxisRaw("Down");
		var roll = Input.GetAxisRaw("Roll");
		var stabalize = Input.GetAxisRaw("Stabalize");

		rigidBody.AddRelativeForce(speed * new Vector3(strafe, up - down, straight) * speed);
		rigidBody.AddRelativeTorque(new Vector3(pitch, yaw, roll) * rotateSpeed);
		if (stabalize > 0) {
			rigidBody.AddForce(-rigidBody.velocity * 0.1f, ForceMode.VelocityChange);
			rigidBody.AddTorque(-rigidBody.angularVelocity * 0.1f, ForceMode.VelocityChange);
		}
	}
}
