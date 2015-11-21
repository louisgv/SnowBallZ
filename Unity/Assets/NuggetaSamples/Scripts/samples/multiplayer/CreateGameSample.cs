namespace samples.multiplayer
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class CreateGameSample : NSample
	{

		private NGame game;

		private String gameId;

		public CreateGameSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1CreateGame();
				} else 
				{
					sampleIO.log("Nuggeta start failed");
				}
			});
			nuggetaPlug.start();
		}

		 virtual protected void step1CreateGame()
		{
			NGame game = new NGame();
			game.setName("My Game");
			NGameCharacteristics gameCharacteristics = new NGameCharacteristics();
			gameCharacteristics.setMinPlayer(2);
			gameCharacteristics.setAutoStop(false);
			gameCharacteristics.setAutoStart(true);
			;
			game.setGameCharacteristics(gameCharacteristics);
			sampleIO.log("Step 1 : Create Game ");
			gameApi.createGameRequest(game, (CreateGameResponse response)=> 
			{
				if (response.getCreateGameStatus() == CreateGameStatus.SUCCESS) 
				{
					gameId = response.getGameId();
					sampleIO.log("Step 1 : Create Game successful / game Id : " + gameId);
				} else 
				{
					sampleIO.log("Step 1 : Failed to create Game ");
					onExit();
				}
			});
		}
	}
}
