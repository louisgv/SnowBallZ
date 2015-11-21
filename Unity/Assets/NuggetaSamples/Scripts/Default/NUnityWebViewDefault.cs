using System;
using com.nuggeta.mimics.net;
using com.nuggeta.unity;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Nuggeta
{
	public class NUnityWebViewDefault : NUnityWebView
		{

			public void openWebView(String url){
				Application.OpenURL(url);
			}
		
			public void closeWebView(){
			}

			public void Update(){
				
			}

			
		}
}

