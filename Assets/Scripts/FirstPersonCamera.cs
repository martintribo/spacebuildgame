using UnityEngine;
using System.Collections;

public class FirstPersonCamera : MonoBehaviour {
    public Transform player;

    private Vector2 currentAngle = new Vector2(0, 0);
    private CapsuleCollider playerCollider;

    // Use this for initialization
    void Start () {
        playerCollider = player.GetComponent<CapsuleCollider>();

        if (playerCollider == null)
            Debug.LogError("Capsure Collider Object Required for Player");
    }
	
	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.Locked; //lock cursor to center
        Cursor.visible = false;

        var rawX = Input.GetAxisRaw("Mouse X");
        var rawY = Input.GetAxisRaw("Mouse Y");

        currentAngle += new Vector2(rawX, rawY);

        var axisOfRotation = transform.rotation * Vector3.right;

        transform.localRotation = Quaternion.AngleAxis(-currentAngle.y, transform.InverseTransformDirection(axisOfRotation));
        transform.localRotation *= Quaternion.AngleAxis(currentAngle.x, transform.InverseTransformDirection(Vector3.up));

        //Apply player rotation about Y Axis
        player.transform.localRotation = Quaternion.AngleAxis(currentAngle.x, player.transform.InverseTransformDirection(Vector3.up));

        //Force Camera position to Player Position
        this.transform.position = new Vector3(player.position.x, playerCollider.bounds.max.y, player.position.z);
    }
}
