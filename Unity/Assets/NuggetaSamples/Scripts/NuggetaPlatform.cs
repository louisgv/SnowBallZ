namespace Nuggeta{
	
	using UnityEngine;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;
	using com.nuggeta.ngdl;
	using com.nuggeta.ngdl.nobjects;
	using com.nuggeta.net;
	using com.nuggeta.unity;
	
	using com.nuggeta;


	
	public class NuggetaPlatform :  MonoBehaviour, INuggetaPlatform
	{

		public  NUnityNet unityNet;
		public  NUnityStorage unityStorage;
		public  NUnityWebView unityWebView;



		public NuggetaPlatform(){


			#if UNITY_EDITOR
			unityNet = new NUnityNetEditor();
		
			#elif UNITY_ANDROID 
			unityNet = new NUnityNetAndroid();
			unityStorage = new NUnityStorageAndroid();
			unityWebView = new NUnityWebViewAndroid();
			
			#elif UNITY_IPHONE 
			unityNet = new NUnityNetIOS();
			unityStorage = new NUnityStorageIOS();
			unityWebView = new NUnityWebViewIOS();

			#elif UNITY_WEBPLAYER 
			unityNet = new NUnityNetWP();
			unityWebView = new NUnityWebViewWP();
			#endif	
		}
		
		void Awake () {
			DontDestroyOnLoad(this);
		}
		
		void Start(){
		}
		

		#region INuggetaPlatform implementation
		NUnityNet INuggetaPlatform.getNet ()
		{
			if(unityNet == null) {
				unityNet = new NUnityNetDefault();
			}
			return unityNet;
		}

		NUnityStorage INuggetaPlatform.getStorage ()
		{
			if(unityStorage == null) {
				unityStorage = new NUnityStorageDefault();
			}
			return unityStorage;
		}

		NUnityWebView INuggetaPlatform.getWebView ()
		{
			if(unityWebView == null) {
				unityWebView = new NUnityWebViewDefault();
			}
			return unityWebView;
		}
	
		#endregion

		void Update(){

			if(unityStorage is NUnityStorageDefault){
				((NUnityStorageDefault)unityStorage).Update();
			}
			#if UNITY_WEBPLAYER 
			if(unityWebView is NUnityWebViewWP){
				((NUnityWebViewDefault)unityWebView).Update();
			}
			#endif
		}


		
		public  void onHttpGetFailed(string data){

			#if UNITY_WEBPLAYER 
			if(unityNet is NUnityNetWP){
				long cid = long.Parse(data);
				((NUnityNetWP)unityNet).onHttpGetFailed(cid);
			}
			#else
			unityNet.onHttpGetFailed();
			#endif
		}
		
		public  void onHttpGetData(string data){

			#if UNITY_WEBPLAYER 
			if(unityNet is NUnityNetWP){
				string[] splits = data.Split(new string[]{"#cid#"},StringSplitOptions.None);
				long cid = long.Parse(splits[0]);
				((NUnityNetWP)unityNet).onHttpGetData(cid,splits[1]);
			}
			#else
			unityNet.onHttpGetData(data);
			#endif
		}
		
		public  void onHttpPostFailed(string data){

			#if UNITY_WEBPLAYER 
			if(unityNet is NUnityNetWP){
				long cid = long.Parse(data);
				((NUnityNetWP)unityNet).onHttpPostFailed(cid);

			}
			#else
			unityNet.onHttpPostFailed();
			#endif
		}
		
		public  void onHttpPostData(string data){

			#if UNITY_WEBPLAYER 
			if(unityNet is NUnityNetWP){
				string[] splits = data.Split(new string[]{"#cid#"},StringSplitOptions.None);
				long cid = long.Parse(splits[0]);
				((NUnityNetWP)unityNet).onHttpPostData(cid,splits[1]);
			}
			#else
				unityNet.onHttpPostData(data);
			#endif
		}


		
		public static void register(){	
			NuggetaContext.register( (new GameObject("NuggetaPlatform")).AddComponent<NuggetaPlatform>());
		}
		
		
	
	}
	
}
