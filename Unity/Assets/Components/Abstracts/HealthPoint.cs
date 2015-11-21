using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HealthPoint : MonoBehaviour
{
	public Canvas canvas;
	
	public GameObject healthPrefab;
	
	public float healthPanelOffset = 0.35f;
	public GameObject healthPanel;
	private Slider healthSlider;
	public int maxHealth = 9;
	
	private int health;
	
	void Start ()
	{
		health = maxHealth;
	}
	
	void OnGUI ()
	{
		Vector2 targetPos;
		
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		
		GUI.Box (new Rect (targetPos.x, Screen.height - 20, 60, 20), health + "/" + maxHealth);
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void OneDown ()
	{
		--health;
	}
}
