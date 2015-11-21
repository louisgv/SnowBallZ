using UnityEngine;
using System.Collections;

public abstract class Controller : MonoBehaviour
{
	public GameObject snowBallPrefab;
	
	private GameObject snowBallInstance;
	
	private SnowBallScript snowBallInstanceScript;
	
	public GameObject wallsPrefab;
	
	private GameObject wallsInstance;

	public GameObject centerEyeAnchor;
	
	public enum CharacterState
	{
		IS_AIMING,
		IS_DUCKING,
		IS_SHOOTING,
		IS_CHARGING
	}
	
	public void Awake ()
	{
		//head = transform.GetChild (0);
		//body = transform.GetChild (1);
		
		//wallsInstance = Instantiate (wallsPrefab, transform.position, transform.rotation) as GameObject;
		wallsPrefab.transform.GetComponent<WallsScript> ().owner = gameObject; // Association
	}
	
	public CharacterState state = CharacterState.IS_AIMING;
	
	public void Charge (Vector3 position)
	{
		if (snowBallInstanceScript == null) {		
			float ballDistance = 0.5f;
		
			float ballHeight = 1.8f;
		
			snowBallInstance = Instantiate (
				snowBallPrefab,
				position, 
				transform.rotation) as GameObject;
		
			snowBallInstance.transform.SetParent (centerEyeAnchor.transform);
		
			snowBallInstanceScript = snowBallInstance.GetComponent<SnowBallScript> ();
		
			snowBallInstanceScript.owner = gameObject; // Association
		

		} 
		snowBallInstanceScript.Charge ();
		state = CharacterState.IS_CHARGING;
		Debug.Log("building snowball");
	}
	
	public void Aim ()
	{
		if (snowBallInstanceScript != null) {	
			snowBallInstanceScript.Aim ();
		}
		state = CharacterState.IS_AIMING;
	}
	
	public void Shoot ()
	{
		if (snowBallInstanceScript != null) {
			Vector3 temp =  new Vector3(transform.TransformPoint(Vector3.zero).x,
			                            transform.TransformPoint(Vector3.zero).y,
			                            transform.TransformPoint(Vector3.zero).z);
			                            ;
			snowBallInstanceScript.transform.SetParent(null);
			//snowBallInstanceScript.enabled = false;
			snowBallInstanceScript.transform.localRotation = Quaternion.identity;
			snowBallInstanceScript.Shoot (centerEyeAnchor.transform.forward);
			//snowBallInstanceScript.transform.localPosition = Vector3.zero;
			//snowBallInstanceScript.transform.position= temp;
			snowBallInstanceScript = null;
		}
	}
	
	public void Duck ()
	{
		state = CharacterState.IS_DUCKING;			
	}
	
	private float duckSpeed = 10.0f;
	
	public void Update ()
	{
		if (state.Equals (CharacterState.IS_DUCKING)) {
			//head.localPosition = Vector3.Lerp (head.localPosition, Vector3.zero, Time.deltaTime * duckSpeed);
		} else {
			//head.localPosition = Vector3.Lerp (head.localPosition, Vector3.up * 0.9f, Time.deltaTime * duckSpeed);
		}
		
		if (state.Equals (CharacterState.IS_SHOOTING)) {
			Shoot ();
			state = CharacterState.IS_AIMING;
		}
	}
}
