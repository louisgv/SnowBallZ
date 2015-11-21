namespace samples.multiplayer
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class SendGameMessageSample : NSample
	{

		protected NGame game;

		public SendGameMessageSample(String url):base(url)
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
			nuggetaPlug.addNRawGameMessageHandler((NRawGameMessage rawGameMessage)=> 
			{
				sampleIO.log("Received game message  :" + rawGameMessage.ToString());
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
					game = response.getGame();
					sendGameMessage();
				} else 
				{
					sampleIO.log("Step 1 : Failed to join Game ");
					onExit();
				}
			});
		}

		 virtual protected void sendGameMessage()
		{
			NRawGameMessage rawGameMessage = new NRawGameMessage();
			rawGameMessage.setContent("i'm in the game guys");
			gameApi.sendMessageToGame(game.getId(), rawGameMessage);
		}
	}
}
