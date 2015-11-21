namespace samples.multiplayer
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using com.nuggeta.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class FindGamesSample : NSample
	{

		private NGame game;

		private String gameId;

		public FindGamesSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					findGames();
				} else 
				{
					sampleIO.log("Nuggeta start failed");
				}
			});
			nuggetaPlug.start();
		}

		 virtual protected void findGames()
		{
			NGame game = new NGame();
			game.setName("My Game");
			NGameCharacteristics gameCharacteristics = new NGameCharacteristics();
			gameCharacteristics.setAutoStop(true);
			game.setGameCharacteristics(gameCharacteristics);
			sampleIO.log("Step 1 : Find games / Useful to build a games lobby.");
			NuggetaQuery nuggetaQuery = new NuggetaQuery();
			gameApi.getGamesRequest(nuggetaQuery, (GetGamesResponse getgamesresponse)=> 
			{
				List<NGame> games = getgamesresponse.getGames();
				int gamesCount = games.Count;
				if (gamesCount > 0) 
				{
					sampleIO.log("Step 1 : Found " + gamesCount + " games. Select one an join it with the 'JoinGameSample'");
					for (int i = 0; i < gamesCount; i++) 
					{
						NGame findGame = games[i];
						sampleIO.log("Game name/id :  " + findGame.getId());
					}
				} else 
				{
					sampleIO.log("Step 1 : No game Found. Create one and relaunch the sample to see what happens");
				}
			});
		}
	}
}
