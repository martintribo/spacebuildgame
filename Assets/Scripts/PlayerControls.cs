using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    public float speed = 0.25f;
    public VoxelGrid myGrid;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        var straight = Input.GetAxisRaw("Straight");
        var strafe = Input.GetAxisRaw("Strafe");

        this.transform.Translate(speed * new Vector3(strafe, 0, straight), Space.Self);

        var action = Input.GetButtonDown("Action");

        if (action && myGrid != null)
            myGrid.addVoxel((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z, Color.green);
    }
}
