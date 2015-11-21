namespace NuggetaSamples{

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.nuggeta;
using com.nuggeta.network;
using com.nuggeta.ngdl.nobjects;
using Nuggeta;
using samples;
using common;

public class NuggetaSamples : MonoBehaviour,NSampleIO {
	
	private Texture imgNuggeta ;
	private string nuggetaText="Nuggeta Samples";
	private GUIStyle textStyle;
	
	//Active Nuggeta Sample
	private bool changeNuggetaImg = false;
	private NSample activeSample;
	private	Console console;
	
	void Start () {
		
		
		console = new GameObject("Console").AddComponent<Console>();

			
		//UI
		imgNuggeta = Resources.Load("nuggeta_off") as Texture;
		textStyle = new GUIStyle();
		textStyle.fontSize = 20;
		textStyle.alignment = TextAnchor.MiddleCenter;


		
		// Register NuggetaPlatform
        NuggetaPlatform.register();
		

		//select Sample to run		
			activeSample = new HelloNuggetaSample("52.25.225.138:5010");
		activeSample.setIo(this);
		

		activeSample.run();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)){ 
			Application.Quit(); 
		}else{
			if(activeSample!=null){
				activeSample.onUpdate();
			}
		}
	}
	
	
    void OnApplicationPause(bool pauseStatus) {
		if(activeSample!=null){
			if(pauseStatus){
				activeSample.onPaused();
			}else{
				activeSample.onResume();
			}
		}
	}

	void OnApplicationQuit() {		
		if(activeSample!=null){
			activeSample.onExit();
		}		
	}


	public void log(string message){
		console.log(message);
	}

	public void connected(){
		changeNuggetaImg = true;		
	}

		//UI
	void OnGUI(){
		if(changeNuggetaImg){
			changeNuggetaImg = false;
			imgNuggeta = Resources.Load("nuggeta_on") as Texture;
		}
			
		if (imgNuggeta) {  

			Rect rect = GUILayoutUtility.GetRect(new GUIContent(nuggetaText),textStyle);
			GUI.Label(new Rect( (Screen.width -rect.width) /2, 20 , rect.width, rect.height),nuggetaText,textStyle);

			
			float marginX=0.1f;
			float marginY=0.1f;
			
			float availableWidth = Screen.width - marginX*Screen.width;
			float availableHeight = Screen.height/3;
			
			float scalingX =1;
			float scalingY =1;
			
			float x,y,width,height,scaling;
			
			if(imgNuggeta.width > availableWidth){
				scalingX = availableWidth / imgNuggeta.width	;
			}
			
			if(imgNuggeta.height > availableHeight){
				scalingY = availableHeight / imgNuggeta.height	;
			}
			
			if(scalingX<scalingY){
				scaling = scalingX;	
			}else{
				scaling = scalingY;	
			}
			
			x =  marginX/2 * Screen.width + (availableWidth - imgNuggeta.width*scaling)/2;	
			y = rect.height + 20 +marginY/2 * Screen.height + (availableHeight - imgNuggeta.height*scaling)/2;	
			width = imgNuggeta.width*scaling ;
			height = imgNuggeta.height*scaling;			
			

			GUI.DrawTexture(new Rect(x, y, width, height), imgNuggeta,ScaleMode.ScaleToFit);	

		}

		
		
	}

}

}
