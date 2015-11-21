using UnityEngine;
using System.Collections;

public class ScalePositionScript : MonoBehaviour {

	public GameObject cameraToMove;
	public Vector3 scale;
	public Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = new Vector3 (cameraToMove.transform.position.x,
		                            cameraToMove.transform.position.y,
		                            cameraToMove.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		cameraToMove.transform.position = startPosition + new Vector3(
			transform.localPosition.x * scale.x, 
			transform.localPosition.y * scale.y,
			transform.localPosition.z * scale.z);
	}
}
