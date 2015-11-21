namespace common
{
	using com.nuggeta;
	using com.nuggeta.api;
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using com.nuggeta.network.plug;
	using System;
	using System.Collections.Generic;

	public abstract class NSample
	{

		protected NuggetaGamePlug nuggetaPlug;

		protected NuggetaGameApi gameApi;

		protected NSampleIO sampleIO;

		public NSample(String url)
		{
			nuggetaPlug = new NuggetaGamePlug(url);
			/*nuggetaPlug.setConnectionInterruptedListener(()=> 
			{
				sampleIO.log("onConnectionInterrupted");
			});*/
			nuggetaPlug.setConnectionLostListener(()=> 
			{
				sampleIO.log("onConnectionLost");
			});
			gameApi = nuggetaPlug.gameApi();
			gameApi.addNDisconnectedNotificationHandler((NDisconnectedNotification ndisconnectednotification)=> 
			{
				sampleIO.log("onNDisconnectedNotification : " + ndisconnectednotification);
			});
		}

		 virtual public void onUpdate()
		{
			nuggetaPlug.pump();
		}

		 virtual public void onPaused()
		{
			if (nuggetaPlug != null) 
			{
			}
		}

		 virtual public void onResume()
		{
			if (nuggetaPlug != null) 
			{
			}
		}

		 virtual public void onExit()
		{
			if (nuggetaPlug != null) 
			{
				nuggetaPlug.stop();
			}
		}

		public abstract void run();

		 virtual public void setIo(NSampleIO sampleIO)
		{
			this.sampleIO = sampleIO;
		}
	}
}
