﻿using UnityEngine;
using System.Collections;

public class SnowBallScript : MonoBehaviour
{
	
	public const float SIZE_MAX = 1.5f;
	
	public float sizeSpeed = 0.18f;
	 
	public float speed = 45.0f;
	 
	public GameObject owner;
	
	private Vector3 direction;
	private Vector3 startPosition;
	public GameObject snowballImpactPrefab;
	 
	public enum BallState
	{
		IS_AIMING,
		IS_CHARGING,
		IS_SHOOTING,
		IS_EXPLODING
	}
	 
	public BallState state = BallState.IS_AIMING;
	 
	// Use this for initialization
	void Start ()
	{
		startPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		direction = transform.forward;
		transform.localScale *= 0.1f;
	}
	
	public void Aim ()
	{
		state = BallState.IS_AIMING;
	}
	
	public void Charge ()
	{
		state = BallState.IS_CHARGING;
	}
	
	public void Shoot (Vector3 shootDirection)
	{
		direction = shootDirection;
		state = BallState.IS_SHOOTING;
	}
	
	private void Damage (Collider other)
	{
		Debug.Log ("Damaging " + other.name);
		
		if (other.transform.parent != null) {
			// Getting parent's HP and decrease them
			HealthPoint otherHP = other.transform.parent.GetComponent<HealthPoint> ();
			otherHP.OneDown ();
			state = BallState.IS_EXPLODING;
		}
	}
	
	public void OnTriggerEnter (Collider other)
	{		
		//TODO: IF other is AI or Player, decrease health
		if (owner != null) {
			if (other.CompareTag ("Wall") && !owner.Equals (other.transform.parent.GetComponent<WallsScript> ().owner)) {
				Damage (other);
				Instantiate(snowballImpactPrefab, this.transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}
			if (other.CompareTag ("Character") && !owner.Equals (other.transform.parent.gameObject)) {
				Damage (other);
				Instantiate(snowballImpactPrefab, this.transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state.Equals (BallState.IS_SHOOTING)) {
			transform.Translate (direction * Time.deltaTime * speed);
		}
		
		if (state.Equals (BallState.IS_CHARGING)) {
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one * SIZE_MAX, sizeSpeed * Time.deltaTime);			
		}
		
		if (state.Equals (BallState.IS_EXPLODING)) {
			//TODO: Play the exploding animation
		}
		
		if (Vector3.Distance(startPosition, transform.position) > 300.0f) {
			Destroy (gameObject);
		}
	}
}
