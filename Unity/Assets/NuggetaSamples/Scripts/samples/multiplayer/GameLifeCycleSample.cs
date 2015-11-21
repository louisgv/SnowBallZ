namespace samples.multiplayer
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class GameLifeCycleSample : NSample
	{

		public GameLifeCycleSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					joinGame();
				} else 
				{
					sampleIO.log("Nuggeta start failed");
				}
			});
			gameApi.addGameRunningStateChangeHandler((GameRunningStateChange gameRunningStateChange)=> 
			{
				sampleIO.log("GameState Changed : " + gameRunningStateChange.getGameRunningState().ToString());
				sampleIO.log("Use GameCharacteristics set on Game creation to see how it changes the behavior specially (min players , max players , auto start , auto stop");
			});
			nuggetaPlug.start();
		}

		private void joinGame()
		{
			gameApi.joinGameRequest("valid_game_id", (JoinGameResponse response)=> 
			{
				if (response.getJoinGameStatus() == JoinGameStatus.GAME_NOT_FOUND) 
				{
					sampleIO.log("Step 1 : Game not found, provide a valid game id");
				} else 
				if (response.getJoinGameStatus() == JoinGameStatus.ACCEPTED) 
				{
					sampleIO.log("Step 1 : Join Game successful");
					NGame game = response.getGame();
				} else 
				{
					sampleIO.log("Step 1 : Failed to join Game ");
					onExit();
				}
			});
		}
	}
}
