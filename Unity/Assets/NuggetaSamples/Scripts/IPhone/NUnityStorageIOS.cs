#if UNITY_IPHONE
using System;
using com.nuggeta.net;
using com.nuggeta.mimics.net;
using com.nuggeta.unity;
using UnityEngine;
using System.Runtime.InteropServices;
namespace Nuggeta
{
	public class NUnityStorageIOS : NUnityStorage
		{

			[DllImport("__Internal")]
			private static extern string _NuggetaPlatformIOS_GetItem(string key);
			[DllImport("__Internal")]
			private static extern void _NuggetaPlatformIOS_SetItem(string key,string value);	


			public void setItem(string key,string val){
				_NuggetaPlatformIOS_SetItem(key,val); 
			}
			public string getItem(string key){
				return _NuggetaPlatformIOS_GetItem(key);
			}
			public void Update(){
				
			}
				
		}
}
#endif

