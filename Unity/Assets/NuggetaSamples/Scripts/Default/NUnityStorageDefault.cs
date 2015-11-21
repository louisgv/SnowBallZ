using System;
using com.nuggeta.net;
using com.nuggeta.mimics.net;
using com.nuggeta.unity;
using UnityEngine;
using System.Runtime.InteropServices;
namespace Nuggeta
{
	public class NUnityStorageDefault : NUnityStorage
	{
		private string setItemKey= null;
		private string setItemValue= null;
		private string getItemKey= null;
		private string getItemValue= null;

		public void setItem(string key,string val){

			if(!System.Threading.Thread.CurrentThread.IsBackground){
				PlayerPrefs.SetString(key,val);	
			}else{
				setItemKey = key;
				setItemValue = val;
				int sleepTimeOut=0;
				while(setItemKey != null && sleepTimeOut < 1000){
					System.Threading.Thread.Sleep(1);
					sleepTimeOut++;
				}
			}
		}
		public string getItem(string key){
			
			if(!System.Threading.Thread.CurrentThread.IsBackground){
				return PlayerPrefs.GetString(key);
			}else{
				
				getItemValue = null;
				getItemKey = key;
				int sleepTimeOut=0;
				while(getItemKey != null && sleepTimeOut<1000){
					System.Threading.Thread.Sleep(1);
					sleepTimeOut++;
				}
				return getItemValue;
			}
		}
		
		public void Update(){
			if(getItemKey!=null){
				getItemValue =  PlayerPrefs.GetString(getItemKey);	
				getItemKey = null;
			}
			
			if(setItemKey!=null && setItemValue!=null){
				PlayerPrefs.SetString(setItemKey,setItemValue);	
				setItemKey = null;
				setItemValue = null;
			}
		}
		
	}
}

