#if UNITY_ANDROID
using System;
using com.nuggeta.net;
using com.nuggeta.mimics.net;
using UnityEngine;
using com.nuggeta.unity;
namespace Nuggeta
{
	public class NUnityWebViewAndroid : NUnityWebView
		{


			private  IntPtr   JavaClass = IntPtr.Zero; 
			private  int mainThreadId;
			private  IntPtr   OpenWebViewId= IntPtr.Zero;  
			private  IntPtr   CloseWebViewId= IntPtr.Zero;  

			
			public NUnityWebViewAndroid(){
				mainThreadId =  System.Threading.Thread.CurrentThread.ManagedThreadId;		
				
				IntPtr cls_JavaClass = AndroidJNI.FindClass("com/nuggeta/unity/android/NuggetaPlatformPlugin"); 
				
				// create a global reference to the JavaClass object and fetch method id(s)..
				JavaClass = AndroidJNI.NewGlobalRef(cls_JavaClass);		

				OpenWebViewId = AndroidJNI.GetStaticMethodID(JavaClass, "openWebView", "(Ljava/lang/String;)V");
				CloseWebViewId = AndroidJNI.GetStaticMethodID(JavaClass, "closeWebView","()V");
						
			}

			public void openWebView(String url){
				// attach our thread to the java vm; obviously the main thread is already attached but this is good practice..
				bool mainThread = (mainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId);
				
				AndroidJNI.AttachCurrentThread();
				
				
				// call
				jvalue[] args =  new jvalue[1];
				args[0] = new jvalue();
				args[0].l = AndroidJNI.NewStringUTF(url);
				
				
				
				AndroidJNI.CallStaticVoidMethod(JavaClass, OpenWebViewId,args);
				
				if(mainThread == false){
					AndroidJNI.DetachCurrentThread();
				}
			}
		
			public void closeWebView(){

				// attach our thread to the java vm
				bool mainThread = (mainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId);
				AndroidJNI.AttachCurrentThread();
				
				
				// call
				jvalue[] args =  new jvalue[0];
				
				
				
				
			AndroidJNI.CallStaticVoidMethod(JavaClass, CloseWebViewId,args);
				
				if(mainThread == false){
					AndroidJNI.DetachCurrentThread();
				}
			
			}

			public void Update(){

			}


			
		}
}

#endif

