
#if UNITY_IPHONE
using System;
using com.nuggeta.net;
using com.nuggeta.mimics.net;
using com.nuggeta.unity;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Nuggeta
{
	public class NUnityWebViewIOS : NUnityWebView
		{
			[DllImport("__Internal")]
			private static extern void _NuggetaPlatformIOS_OpenWebView(string url);
			[DllImport("__Internal")]
			private static extern void _NuggetaPlatformIOS_CloseWebView();
			
			public void openWebView(String url){
				_NuggetaPlatformIOS_OpenWebView(url);
			}
		
			public void closeWebView(){
				_NuggetaPlatformIOS_CloseWebView();
			}

		public void Update(){
			
		}

			
		}
}
#endif

