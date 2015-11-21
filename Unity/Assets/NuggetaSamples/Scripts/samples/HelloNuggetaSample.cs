namespace samples
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class HelloNuggetaSample : NSample
	{

		public HelloNuggetaSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					sampleIO.log("Hello NUGGETA!");
				} else 
				{
					sampleIO.log("Nuggeta start failed");
				}
			});
			nuggetaPlug.start();
		}
	}
}
