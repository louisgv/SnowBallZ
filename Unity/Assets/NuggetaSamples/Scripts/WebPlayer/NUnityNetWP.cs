#if UNITY_WEBPLAYER
namespace Nuggeta
{
	using System;
	using System.IO;
	using System.Net;
	using System.Text;
	using System.Collections;
	using System.Collections.Generic;
	using com.nuggeta.ngdl.nobjects;
	using com.nuggeta.mimics.log;
	using System.Threading;
    using com.nuggeta.net;
	using com.nuggeta.mimics.net;
	using com.nuggeta.unity;
	using UnityEngine;



	
	public class NUnityNetWP : NUnityNet
	{

		private static long nextCallbackId=0;

		public class HttpContext			
		{ 
			public NetConnectionReceivedHandler connectionReceivedHandler;
			public NetConnectionSendFailHandler asyncSendFailResult;	
		} 

		//private NLogger logger = NLoggerFactory.getLogger("NUnityNetWP");

		private Dictionary<long,HttpContext> dictByCallbackId = new Dictionary<long,HttpContext>();


		private static string js = "function NHttpRequestImpl(){}function importNHttpRequestImpl(){if(NHttpRequestImpl.prototype.init){return}NHttpRequestImpl.prototype.init=function(){return this};NHttpRequestImpl.prototype.get=function(e,cid){var _$this=this;_$this.cid=cid;if(window.XMLHttpRequest){_$this.xhr_object=new XMLHttpRequest}else if(window.ActiveXObject){_$this.xhr_object=new ActiveXObject('Microsoft.XMLHTTP')}else{u.getUnity().SendMessage('NuggetaPlatform','onHttpGetFailed',_$this.cid);return}if(_$this.xhr_object){_$this.xhr_object.open('GET',e,true);_$this.xhr_object.onreadystatechange=function(){if(_$this.xhr_object.readyState==4){if(_$this.xhr_object.status==200){u.getUnity().SendMessage('NuggetaPlatform','onHttpGetData',_$this.cid+'#cid#'+_$this.xhr_object.responseText)}else{u.getUnity().SendMessage('NuggetaPlatform','onHttpGetFailed',_$this.cid)}}if(_$this.xhr_object.readyState<=0){u.getUnity().SendMessage('NuggetaPlatform','onHttpGetFailed',_$this.cid)}};_$this.xhr_object.send(null)}};NHttpRequestImpl.prototype.post=function(e,t,cid){var _$this=this;_$this.cid=cid;if(window.XMLHttpRequest){_$this.xhr_object=new XMLHttpRequest}else if(window.ActiveXObject){_$this.xhr_object=new ActiveXObject('Microsoft.XMLHTTP')}else{u.getUnity().SendMessage('NuggetaPlatform','onHttpPostFailed',_$this.cid);return}if(_$this.xhr_object){_$this.xhr_object.open('POST',e,true);_$this.xhr_object.onreadystatechange=function(){if(_$this.xhr_object.readyState==4){if(_$this.xhr_object.status==200){u.getUnity().SendMessage('NuggetaPlatform','onHttpPostData',_$this.cid+'#cid#'+_$this.xhr_object.responseText)}else{u.getUnity().SendMessage('NuggetaPlatform','onHttpPostFailed',_$this.cid)}}if(_$this.xhr_object.readyState<=0){u.getUnity().SendMessage('NuggetaPlatform','onHttpPostFailed',_$this.cid)}};_$this.xhr_object.setRequestHeader('Content-type','application/x-www-form-urlencoded');_$this.xhr_object.send(t)}}}importNHttpRequestImpl();";

		public NUnityNetWP(){
			Application.ExternalEval(js);
		}


        public void getHttp( String url, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
        {
			nextCallbackId++;
			//logger.debug("Get Http cid="+nextCallbackId+ "url="+url);

			HttpContext httpContext=new HttpContext();
			httpContext.connectionReceivedHandler = connectionReceivedHandler;
			httpContext.asyncSendFailResult = asyncSendFailResult;

			dictByCallbackId.Add(nextCallbackId,httpContext);

			Application.ExternalEval("new NHttpRequestImpl().init().get('"+url+"','"+ nextCallbackId +"')" );


        }
  	
		
		public  void postHttp( String url, String dataStr, NetConnectionReceivedHandler connectionReceivedHandler, NetConnectionSendFailHandler asyncSendFailResult)
        {
			nextCallbackId++;
			//logger.debug("Post Http cid="+nextCallbackId+ " url="+url + " data="+dataStr);

			HttpContext httpContext=new HttpContext();
			httpContext.connectionReceivedHandler = connectionReceivedHandler;
			httpContext.asyncSendFailResult = asyncSendFailResult;

			dictByCallbackId.Add(nextCallbackId,httpContext);
	
			Application.ExternalEval("new NHttpRequestImpl().init().post('"+url+"','"+dataStr+"','"+nextCallbackId+"')" );

		}


		public  void onHttpGetFailed(long callbackId){

			//logger.debug("onHttpGetFailed cid="+callbackId);

			HttpContext httpContext=null;
			dictByCallbackId.TryGetValue(callbackId,out httpContext);
			if(httpContext!=null){
				httpContext.asyncSendFailResult();
			}else{
				//logger.debug("onHttpGetFailed can't find callback");
			}

			dictByCallbackId.Remove(callbackId);
		}

		public  void onHttpGetData(long callbackId,string data){

			//logger.debug("onHttpGetData cid="+callbackId);

			HttpContext httpContext=null;
			dictByCallbackId.TryGetValue(callbackId,out httpContext);
			if(httpContext!=null){
				httpContext.connectionReceivedHandler(data);
			}else{
				//logger.debug("onHttpGetData can't find callback");
			}
			dictByCallbackId.Remove(callbackId);

		}

		public  void onHttpPostFailed(long callbackId){

			//logger.debug("onHttpPostFailed cid="+callbackId);

			HttpContext httpContext=null;
			dictByCallbackId.TryGetValue(callbackId,out httpContext);
			if(httpContext!=null){
				httpContext.asyncSendFailResult();
			}else{
				//logger.debug("onHttpPostFailed can't find callback");
			}
		}

		public  void onHttpPostData(long callbackId,string data){


			//logger.debug("onHttpPostData cid="+callbackId + " data="+data);

			HttpContext httpContext=null;
			dictByCallbackId.TryGetValue(callbackId,out httpContext);
			if(httpContext!=null){
				if(data!=null && data.Length >0){
					//logger.debug("onHttpPostData call DONE cid="+callbackId);
					httpContext.connectionReceivedHandler(data);
				}else{
					//logger.debug("onHttpPostData call DONE but not data cid="+callbackId+ " data="+data);
				}


			}else{
				//logger.debug("onHttpPostData can't find callback");
			}
			dictByCallbackId.Remove(callbackId);
		}


		public void onHttpGetData(String data){
			
		}
		
		public void onHttpGetFailed(){
			
		}
		
		public void onHttpPostData(String data){
			
		}
		
		public void onHttpPostFailed(){
			
		}

        
	};
	
	
}

#endif
