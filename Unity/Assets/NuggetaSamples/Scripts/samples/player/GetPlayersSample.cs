namespace samples.player
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using com.nuggeta.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class GetPlayersSample : NSample
	{

		protected NPlayer createdPlayer;

		public GetPlayersSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1CreatePlayer();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1CreatePlayer()
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
					step2GetPlayers();
				} else 
				{
					sampleIO.log("Step 1 : Fail to Create Player\r\n");
					onExit();
				}
			});
		}

		private void step2GetPlayers()
		{
			sampleIO.log("Step 2 : Get Players");
			NuggetaQuery playersQuery = new NuggetaQuery();
			playersQuery.setQuery("$WHERE Login='player1'");
			gameApi.getPlayersRequest(playersQuery, (GetPlayersResponse response)=> 
			{
				GetPlayersStatus status = response.getGetPlayersStatus();
				if (GetPlayersStatus.SUCCESS == status) 
				{
					List<NPlayer> players = response.getPlayers();
					sampleIO.log("Step 1 : Get Players successfull : found " + players.Count + " players \r\n");
				} else 
				{
					sampleIO.log("Step 1 : Fail to  Get Players reason: " + status.name() + "\r\n");
					onExit();
				}
			});
		}
	}
}
