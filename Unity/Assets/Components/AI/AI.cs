using UnityEngine;
using System.Collections;

public class AI : Controller
{
	
	public float shootDelay = 1.0f;
	
	public float shootAngle = 90.0f;
	
	public float speed = 45.0f;

	public float AimTimer = 4f;

	int index = 0;
	// Use this for initialization
	public enum AIMode
	{
		TRASH,
		SOSO,
		AOK
	}
	
	private Transform head, body;
	
	public AIMode mode = AIMode.TRASH;
	
	void Start ()
	{
//		StartCoroutine (test ());
		// TODO: Set Random Bot Mode!
		//head = transform.GetChild (0);
		//body = transform.GetChild (1);
		
	}
	
	public void BeTrashAI ()
	{
		// Rotate for some degree
		mode = AIMode.TRASH;
	}
	
	public void BeSoSoAI ()
	{
		mode = AIMode.SOSO;
	}
	
	private float duckSpeed = 10.0f;
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
		/*
		if (state.Equals (CharacterState.IS_DUCKING)) {
			head.localPosition = Vector3.Lerp (head.localPosition, Vector3.zero, Time.deltaTime * duckSpeed);
		} else {
			head.localPosition = Vector3.Lerp (head.localPosition, Vector3.up * 0.9f, Time.deltaTime * duckSpeed);
		}*/
		if (mode.Equals (AIMode.TRASH)) {
			TrashMode ();
		}
		
		if (mode.Equals (AIMode.SOSO)) {
			SoSoMode ();
		}
			
	}
	
	void SoSoMode ()
	{
		//	Choose A Random Target that is not itself
		GameObject[] pool = GameObject.FindGameObjectsWithTag ("Character");

		
		Charge (centerEyeAnchor.transform.forward);
		
		if (pool.Length > 2) {
			if(AimTimer < 0){
				index = Random.Range (0, pool.Length);
				while (pool [index].transform.parent.gameObject.Equals( gameObject)) {
					index = Random.Range (0, pool.Length);	
				}
				AimTimer = 4f;
			}
				Vector3 direction = pool [index].transform.position - transform.position;
			
				//transform.rotation = Quaternion.FromToRotation(transform.rotation, transform.forward);				
			
				if (Random.value > 0.99f) {
					Duck ();
				}
				if (Random.value > 0.99f) {
					Shoot (transform.forward);
				}
					

			AimTimer -= Time.deltaTime;
		}
	}
	
	void TrashMode ()
	{
		speed = Random.Range (45.0f, 90.0f);
		
		transform.Rotate (0, Time.deltaTime * speed, 0, Space.World);
		
		Charge (transform.position+ Vector3.forward * 5f);
		
		if (Random.value > 0.99f) {
			Shoot ();
		}
		
		if (Random.value > 0.99f) {
			Duck ();
		}
	}
}
