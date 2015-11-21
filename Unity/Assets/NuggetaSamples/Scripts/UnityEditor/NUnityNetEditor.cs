#if UNITY_EDITOR
namespace Nuggeta
{
	using System;
	using System.IO;
	using System.Net;
	using System.Text;
	using System.Collections.Generic;
	using com.nuggeta.ngdl.nobjects;
	using com.nuggeta.log;
	using System.Threading;
    using com.nuggeta.net;
	using com.nuggeta.mimics.net;
	using com.nuggeta.unity;

	
	public class NUnityNetEditor : NUnityNet
	{

        private static NLogger log = NLoggerFactory.getLogger("HttpRequest");
		private  ManualResetEvent allDone = new ManualResetEvent(false);
		private NetConnectionReceivedHandler getConnectionReceivedHandler;
        private NetConnectionSendFailHandler getAsyncSendFailResult;
		private NetConnectionReceivedHandler postConnectionReceivedHandler;
        private NetConnectionSendFailHandler postAsyncSendFailResult;

       
        public void getHttp( String url, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
        {
            this.getConnectionReceivedHandler = connectionReceivedHandler;
            this.getAsyncSendFailResult = asyncSendFailResult;
            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);
                httpWReq.BeginGetResponse(result =>
                {
                    HttpGetStreamCallback(result, httpWReq);
                }, null);
            }
            catch (Exception e)
            {
                log.errorCause(" connection failed ", e);
                asyncSendFailResult();
            }
        }


        public void HttpGetStreamCallback(IAsyncResult asynchronousResult, HttpWebRequest request)
		{
			try{
				
                // End the operation
				HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    getAsyncSendFailResult();
                    return;
                }
				Stream streamResponse = response.GetResponseStream();
				StreamReader streamRead = new StreamReader(streamResponse);
                String responseData = streamRead.ReadToEnd();
                getConnectionReceivedHandler(responseData);
				
				streamResponse.Close();
				streamRead.Close();
				response.Close();
            }
            catch (Exception e)
            {
                log.errorCause( "http get failed ", e);
                getAsyncSendFailResult();
            }
			
		}
		
		
		
		public  void postHttp( String url, String dataStr, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
        {
            this.postConnectionReceivedHandler = connectionReceivedHandler;
            this.postAsyncSendFailResult = asyncSendFailResult;
             //log.debug("send url " + name + " " + url);
			try {
				
				HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);
				
				httpWReq.Method = "POST";
				httpWReq.ContentType = "text/xml";


                byte[] data = Encoding.UTF8.GetBytes(dataStr);
				httpWReq.ContentLength = data.Length;
				
				httpWReq.BeginGetRequestStream(result => {
                    sendMessageAsync(result, httpWReq, data, dataStr);
				},httpWReq);
				
				allDone.WaitOne();
				
			} catch ( Exception e) {
				log.errorCause("error while  sending command " + e.Message, e);
                postAsyncSendFailResult();
			}
			
		}


         private void sendMessageAsync(IAsyncResult asynchronousResult, HttpWebRequest request, byte[] data, String message)
		{
			
			try{
				
				// End the operation
				Stream postStream = request.EndGetRequestStream(asynchronousResult);
				
				postStream.Write(data, 0, data.Length);
				postStream.Close();
				
				// Start the asynchronous operation to get the response
				request.BeginGetResponse(result => {
                    receiveMessageAsync(result, request, message, postAsyncSendFailResult);
				},null);
			} catch ( Exception e) {
				log.errorCause("error while  sending command " + e.Message, e);
                postAsyncSendFailResult();
			}
		}

         private void receiveMessageAsync(IAsyncResult asynchronousResult, HttpWebRequest request, String message, NetConnectionSendFailHandler asyncSendFailResult)
		{
			
			try{
				
                // End the operation
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

                if (response.StatusCode != HttpStatusCode.OK)
                {
					log.error( "post failed ");
                    postAsyncSendFailResult();
                    return;
                }


                Stream streamResponse = response.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                String responseData = streamRead.ReadToEnd();
                postConnectionReceivedHandler(responseData);

                streamResponse.Close();
                streamRead.Close();
                response.Close();
                allDone.Set();
            }
            catch (Exception e)
            {
                log.errorCause( " post failed ", e);
                postAsyncSendFailResult();
            }
		}
		
		
		//callback when unitySendMessage , not use here
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

#endif
