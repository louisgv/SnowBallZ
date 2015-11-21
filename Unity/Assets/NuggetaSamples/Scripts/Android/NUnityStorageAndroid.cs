#if UNITY_ANDROID
using System;
using com.nuggeta.net;
using com.nuggeta.mimics.net;
using com.nuggeta.unity;
using UnityEngine;


namespace Nuggeta
{
		public class NUnityStorageAndroid : NUnityStorage
		{
			private  IntPtr   JavaClass = IntPtr.Zero; 
			private  int mainThreadId;
			private IntPtr GetItemId = IntPtr.Zero;  
			private IntPtr SetItemId = IntPtr.Zero; 

			public NUnityStorageAndroid(){
				mainThreadId =  System.Threading.Thread.CurrentThread.ManagedThreadId;		
				
				IntPtr cls_JavaClass = AndroidJNI.FindClass("com/nuggeta/unity/android/NuggetaPlatformPlugin");   
				
				// create a global reference to the JavaClass object and fetch method id(s)..
				JavaClass = AndroidJNI.NewGlobalRef(cls_JavaClass);		

				GetItemId = AndroidJNI.GetStaticMethodID(JavaClass, "_NuggetaPlatformPlugin_getItem","(Ljava/lang/String;)Ljava/lang/String;");

				SetItemId = AndroidJNI.GetStaticMethodID(JavaClass, "_NuggetaPlatformPlugin_setItem", "(Ljava/lang/String;Ljava/lang/String;)V");
			}

			public void setItem(string key,string val){

				// attach our thread to the java vm; obviously the main thread is already attached but this is good practice..
				bool mainThread = (mainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId);
				
				AndroidJNI.AttachCurrentThread();
				
				
				// call
				jvalue[] args =  new jvalue[2];
				args[0] = new jvalue();
				args[0].l = AndroidJNI.NewStringUTF(key);	
				args[1] = new jvalue();
				args[1].l = AndroidJNI.NewStringUTF(val);
				
				AndroidJNI.CallStaticVoidMethod(JavaClass, SetItemId,args);
				
				if(mainThread == false){
					AndroidJNI.DetachCurrentThread();
				}

			}
			public string getItem(string key){
				// attach our thread to the java vm		
				bool mainThread = (mainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId);
				AndroidJNI.AttachCurrentThread();
				
				// call
				jvalue[] args =  new jvalue[1];
				args[0] = new jvalue();
				args[0].l = AndroidJNI.NewStringUTF(key);	 
				
				string result = AndroidJNI.CallStaticStringMethod(JavaClass, GetItemId,args);
				
				// detach our thread to the java vm
				if(mainThread == false){
					AndroidJNI.DetachCurrentThread();
				}
				
				return result;
			}
			
			public void Update(){
			
			}
				
		}
}
#endif

