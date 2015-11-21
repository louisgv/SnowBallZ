#if UNITY_ANDROID 
namespace Nuggeta
{
	using System;
	using System.IO;
	using System.Net;
	using System.Text;
	//using System.Web;
	using System.Collections.Generic;
	using com.nuggeta.ngdl.nobjects;
	using com.nuggeta.mimics.log;
	using System.Threading;
	using com.nuggeta.net;
	using com.nuggeta.mimics.net;
	using System.Runtime.InteropServices;
	using UnityEngine;
	using com.nuggeta.unity;
	
	
	
	
	public class NUnityNetAndroid : NUnityNet
	{
		private static IntPtr   JavaClass = IntPtr.Zero; 
		private static int mainThreadId;
		private static IntPtr   HttpGetId= IntPtr.Zero;  
		private static IntPtr   HttpPostId= IntPtr.Zero;  	
		
		
		public NetConnectionReceivedHandler _currentGetConnectionReceivedHandler =null;
		public NetConnectionSendFailHandler _currentGetAsyncSendFailResult =null;
		
		public NetConnectionReceivedHandler _currentPostConnectionReceivedHandler =null;
		public NetConnectionSendFailHandler _currentPostAsyncSendFailResult =null;
		
		public NUnityNetAndroid(){
			mainThreadId =  System.Threading.Thread.CurrentThread.ManagedThreadId;		
			
			IntPtr cls_JavaClass = AndroidJNI.FindClass("com/nuggeta/unity/android/NuggetaPlatformPlugin");   
			
			// create a global reference to the JavaClass object and fetch method id(s)..
			JavaClass = AndroidJNI.NewGlobalRef(cls_JavaClass);		
			
			HttpGetId = AndroidJNI.GetStaticMethodID(JavaClass, "_NuggetaPlatformPlugin_http_get","(Ljava/lang/String;)V");			
			HttpPostId = AndroidJNI.GetStaticMethodID(JavaClass, "_NuggetaPlatformPlugin_http_post","(Ljava/lang/String;Ljava/lang/String;)V");
			
		}
		
		public void getHttp( String url, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
		{
			
			_currentGetConnectionReceivedHandler = connectionReceivedHandler;
			_currentGetAsyncSendFailResult = asyncSendFailResult;
			
			// attach our thread to the java vm
			bool mainThread = (mainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId);
			AndroidJNI.AttachCurrentThread();
			
			
			// call
			jvalue[] args =  new jvalue[1];
			args[0] = new jvalue();
			args[0].l = AndroidJNI.NewStringUTF(url);
			
			
			AndroidJNI.CallStaticVoidMethod(JavaClass, HttpGetId,args);
			
			if(mainThread == false){
				AndroidJNI.DetachCurrentThread();
			}
		}
		
		
		
		
		public  void postHttp(String url, String dataStr, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
		{
			
			_currentPostConnectionReceivedHandler = connectionReceivedHandler;
			_currentPostAsyncSendFailResult = asyncSendFailResult;
			
			
			// attach our thread to the java vm
			bool mainThread = (mainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId);
			AndroidJNI.AttachCurrentThread();
			
			
			// call
			jvalue[] args =  new jvalue[2];
			args[0] = new jvalue();
			args[0].l = AndroidJNI.NewStringUTF(url);
			
			args[1] = new jvalue();
			args[1].l = AndroidJNI.NewStringUTF(dataStr);
			
			
			AndroidJNI.CallStaticVoidMethod(JavaClass, HttpPostId,args);
			
			if(mainThread == false){
				AndroidJNI.DetachCurrentThread();
			}
		}
		
		
		public void onHttpGetData(String data){
			_currentGetConnectionReceivedHandler(data);
		}
		
		public void onHttpGetFailed(){
			_currentGetAsyncSendFailResult();
		}
		
		public void onHttpPostData(String data){
			_currentPostConnectionReceivedHandler(data);
		}
		
		public void onHttpPostFailed(){
			_currentPostAsyncSendFailResult();
		}
		
		
		
		
		
	}
	
	
}

#endif
