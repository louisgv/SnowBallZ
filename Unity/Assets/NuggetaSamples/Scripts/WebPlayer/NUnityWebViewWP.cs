
#if UNITY_WEBPLAYER
using System;
using com.nuggeta.net;
using com.nuggeta.mimics.net;
using com.nuggeta.unity;
using UnityEngine;
namespace Nuggeta
{
	public class NUnityWebViewWP : NUnityWebView
		{


		private bool closeRequest = false;	

			public void openWebView(String url){
				Application.ExternalEval(" var _nugWin =window.open('"+url +"','_blank','width=640,height=480,left=100,top=100')");
			}
		
			public void closeWebView(){
				closeRequest = true;
			}

			public void Update(){
				if(closeRequest){
					closeRequest = false;			
					Application.ExternalEval( "_nugWin.close()");																			
				}
			}


			
		}
}
#endif

