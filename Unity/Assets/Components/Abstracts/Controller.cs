using UnityEngine;
using System.Collections;

public abstract class Controller : MonoBehaviour
{
	public GameObject snowBallPrefab;
	
	private GameObject snowBallInstance;
	
	private SnowBallScript snowBallInstanceScript;
	
	private Transform head;
	
	private Transform body;
	
	private Transform walls;
	
	public enum CharacterState
	{
		IS_AIMING,
		IS_DUCKING,
		IS_SHOOTING,
		IS_CHARGING
	}
	
	public void Awake ()
	{
		head = transform.GetChild (0);
		body = transform.GetChild (1);
		walls = transform.GetChild (2);
		
		walls.GetComponent<WallsScript> ().owner = gameObject; // Association
	}
	
	public CharacterState state = CharacterState.IS_AIMING;
	
	public void Charge ()
	{
		state = CharacterState.IS_CHARGING;
		
		float ballDistance = 0.5f;
		
		float ballHeight = 1.8f;
		
		snowBallInstance = Instantiate (
			snowBallPrefab,
			transform.position + Vector3.one * ballDistance + Vector3.up * ballHeight, 
			transform.rotation) as GameObject;
		
		snowBallInstance.transform.SetParent (transform);
		
		snowBallInstanceScript = snowBallInstance.GetComponent<SnowBallScript> ();
		
		snowBallInstanceScript.owner = gameObject; // Association
		
		snowBallInstanceScript.Charge ();
	}
	
	public void Aim ()
	{
		if (snowBallInstanceScript != null) {	
			snowBallInstanceScript.Aim ();
		}
	}
	
	public void Shoot ()
	{
		if (snowBallInstanceScript != null) {
			snowBallInstanceScript.Shoot (transform.forward);
			
			
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
			head.localPosition = Vector3.Lerp (head.localPosition, Vector3.zero, Time.deltaTime * duckSpeed);
		} else {
			head.localPosition = Vector3.Lerp (head.localPosition, Vector3.up * 0.9f, Time.deltaTime * duckSpeed);
		}
	}
}
