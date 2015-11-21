#if UNITY_IPHONE
namespace Nuggeta
{
	using System;
	using System.IO;
	using System.Net;
	using System.Text;
	using System.Collections.Generic;
	using com.nuggeta.ngdl.nobjects;
	using com.nuggeta.mimics.log;
	using System.Threading;
    using com.nuggeta.net;
	using com.nuggeta.mimics.net;
	using com.nuggeta.unity;
	using UnityEngine;
	using System.Runtime.InteropServices;

	
	public class NUnityNetIOS : NUnityNet
	{
		
		[DllImport("__Internal")]
		private static extern void _NuggetaPlatformIOS_GetHttp(string url);
		[DllImport("__Internal")]
		private static extern void _NuggetaPlatformIOS_PostHttp(string url,string data);	
		
		private NetConnectionReceivedHandler _currentGetConnectionReceivedHandler;
        private NetConnectionSendFailHandler _currentGetAsyncSendFailResult;
		private NetConnectionReceivedHandler _currentPostConnectionReceivedHandler;
        private NetConnectionSendFailHandler _currentPostAsyncSendFailResult;
		
        public void getHttp( String url, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
        {
            _currentGetConnectionReceivedHandler = connectionReceivedHandler;
          	_currentGetAsyncSendFailResult =  asyncSendFailResult;
          	_NuggetaPlatformIOS_GetHttp(url);
        }
		
		public  void postHttp( String url, String dataStr, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
        {       
		 	_currentPostConnectionReceivedHandler = connectionReceivedHandler;
      		_currentPostAsyncSendFailResult =  asyncSendFailResult;
      		_NuggetaPlatformIOS_PostHttp(url,dataStr);
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
