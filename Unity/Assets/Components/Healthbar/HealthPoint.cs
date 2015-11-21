using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HealthPoint : MonoBehaviour
{	
	public GameObject healthPrefab;
	
	public float healthPanelOffset = 0.54f;
	public GameObject healthPanel;
	private Slider healthSlider; 
	
	public int maxHealth = 3;
	
	private int health;
	private Text objName;
	
	void Start ()
	{
		health = maxHealth;
			
		healthPanel = Instantiate (healthPrefab) as GameObject;

		healthPanel.transform.SetParent (GameObject.FindGameObjectWithTag ("WorldHUD").transform, false);
		
		healthSlider = healthPanel.GetComponentInChildren<Slider> ();
		
		objName = healthPanel.GetComponentInChildren<Text> ();
		
		objName.text = gameObject.name;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		healthSlider.value = health / maxHealth;
		
		Vector3 worldPos = new Vector3 (transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
		Vector3 screenPos = Camera.main.WorldToScreenPoint (worldPos);
		healthPanel.transform.position = new Vector3 (screenPos.x, screenPos.y, screenPos.z);
		
	}
	
	public void OneDown ()
	{
		--health;
		if (health == 0) {
			Destroy (gameObject);
		}
	}
}
