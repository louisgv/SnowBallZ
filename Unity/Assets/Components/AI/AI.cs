using UnityEngine;
using System.Collections;

public class AI : Controller
{
	
	public float shootDelay = 1.0f;
	
	public float shootAngle = 90.0f;
	
	// Use this for initialization
	void Start ()
	{
		Charge ();
		StartCoroutine (test ());
	}
	
	IEnumerator test ()
	{
		yield return new WaitForSeconds (shootDelay);
		Aim ();
		transform.Rotate (new Vector3 (0, shootAngle));
		Shoot ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
