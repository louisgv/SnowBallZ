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

	
	public class NUnityNetDefault : NUnityNet
	{

        public void getHttp( String url, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
        {
			asyncSendFailResult();
        }
		
		public  void postHttp( String url, String dataStr, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
        {       
			asyncSendFailResult();
		}
		
		public void onHttpGetData(String data){
			
		}
		
		public void onHttpGetFailed(){
			
		}
		
		public void onHttpPostData(String data){
			
		}
		
		public void onHttpPostFailed(){
			
		}

	}

}

