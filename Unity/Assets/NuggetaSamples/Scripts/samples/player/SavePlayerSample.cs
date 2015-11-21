namespace samples.player
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class SavePlayerSample : NSample
	{

		protected NPlayer createdPlayer;

		public SavePlayerSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1SavePlayer();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1SavePlayer()
		{
			sampleIO.log("Step 1 : Save Player");
			NPlayer player = new NPlayer();
			player.setLogin("player1");
			player.setPassword("p1");
			gameApi.savePlayerRequest(player, (SavePlayerResponse response)=> 
			{
				if (response.getSavePlayerStatus() == SavePlayerStatus.SUCCESS) 
				{
					createdPlayer = response.getPlayer();
					sampleIO.log("Step 1 : Save Player successfull\r\n");
				} else 
				{
					sampleIO.log("Step 1 : Fail to Create Player\r\n");
					onExit();
				}
			});
		}
	}
}
