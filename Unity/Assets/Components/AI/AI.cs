using UnityEngine;
using System.Collections;

public class AI : Controller
{
	
	public float shootDelay = 1.0f;
	
	public float shootAngle = 90.0f;
	
	public float speed = 45.0f;
	
	// Use this for initialization
	public enum AIMode
	{
		TRASH,
		SOSO,
		AOK
	}
	
	public AIMode mode = AIMode.TRASH;
	
	void Start ()
	{
//		StartCoroutine (test ());
		// TODO: Set Random Bot Mode!
		
	}
	
	void BeTrashAI ()
	{
		// Rotate for some degree
		mode = AIMode.TRASH;
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
		
		if (mode.Equals (AIMode.TRASH)) {
			TrashMode ();
		}
			
	}
	
	void TrashMode ()
	{
		speed = Random.Range (45.0f, 90.0f);
		
		transform.Rotate (0, Time.deltaTime * speed, 0, Space.World);
		
		Charge ();
		
		if (Random.value > 0.99f) {
			Shoot ();
		}
		
		if (Random.value > 0.99f) {
			Duck ();
		}
	}
}
