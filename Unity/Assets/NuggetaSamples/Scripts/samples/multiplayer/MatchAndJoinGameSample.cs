namespace samples.multiplayer
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class MatchAndJoinGameSample : NSample
	{

		public MatchAndJoinGameSample(String url):base(url)
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
			gameApi.addNMatchAndJoinGameExpiredNotificationHandler((NMatchAndJoinGameExpiredNotification matchAndJoinGameExpiredNotification)=> 
			{
				sampleIO.log("MatchAndJoin demand aborted. Not enough Players in game " + matchAndJoinGameExpiredNotification.getGameId());
			});
			gameApi.addGameRunningStateChangeHandler((GameRunningStateChange gameRunningStateChange)=> 
			{
				sampleIO.log("Game state changed to " + gameRunningStateChange.getGameRunningState().ToString());
			});
			nuggetaPlug.start();
		}

		private void joinGame()
		{
			gameApi.matchAndJoinGameRequest(null, null, 5000, (MatchAndJoinGameResponse matchAndJoinGameResponse)=> 
			{
				MatchAndJoinGameStatus matchAndJoinGameStatus = matchAndJoinGameResponse.getMatchAndJoinGameStatus();
				if (matchAndJoinGameStatus == MatchAndJoinGameStatus.ACCEPTED) 
				{
					NGame game = matchAndJoinGameResponse.getGame();
					sampleIO.log("Step 1 : You have joined the Game " + game.getId());
				} else 
				{
					sampleIO.log("Failed to find a game " + matchAndJoinGameStatus.ToString());
					onExit();
				}
			});
		}
	}
}
