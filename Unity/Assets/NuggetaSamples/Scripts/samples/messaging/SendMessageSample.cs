namespace samples.messaging
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class SendMessageSample : NSample
	{

		public SendMessageSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1SendMessage();
				} else 
				{
					sampleIO.log("Nuggeta start failed");
				}
			});
			gameApi.addNRawGameMessageHandler((NRawGameMessage rawMessage)=> 
			{
				sampleIO.log("Received message  :" + rawMessage.ToString());
			});
			nuggetaPlug.start();
		}

		 virtual protected void step1SendMessage()
		{
			sampleIO.log("Step 1 : Send RawMessage !");
			NRawGameMessage rawMessage = new NRawGameMessage();
			rawMessage.setContent("blabla");
			gameApi.sendMessage(rawMessage);
		}
	}
}
