using UnityEngine;
using System.Collections;

public class CharacterControllerScript : Controller {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
		Vector3 fwd = centerEyeAnchor.transform.forward;
		Debug.DrawLine (transform.position, transform.position + fwd * 10f, Color.red);
		RaycastHit hit;
		if (Physics.Raycast(transform.position, fwd, out hit, 10)) {
			if(hit.collider.gameObject.tag == "Mound"){
				Charge(hit.point + Vector3.up * 1.5f);
			}
			else{
				Aim();
			}
		}

		if (state == CharacterState.IS_AIMING) {
			//Debug.Log("CHARACTER STATE AIMING");
			if(Input.GetButton("Fire1")){
				Shoot();
				//Debug.Log("shooting!!!!");
			}

		}
	}
}
