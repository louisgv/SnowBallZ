namespace samples.player.friend.request
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using com.nuggeta.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class AskForFriendSample : NSample
	{

		protected NPlayer playerTwo;

		public AskForFriendSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1CreatePlayerOne();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1CreatePlayerOne()
		{
			sampleIO.log("Step 1 : SavePlayerOne");
			NPlayer player = new NPlayer();
			player.setId("pid01");
			player.setLogin("player1");
			player.setPassword("p1");
			gameApi.savePlayerRequest(player, (SavePlayerResponse response)=> 
			{
				step2CreatePlayerTwo();
			});
		}

		private void step2CreatePlayerTwo()
		{
			sampleIO.log("Step 2 : CreatePlayerTwo");
			NPlayer player = new NPlayer();
			player.setId("pid02");
			player.setLogin("player2");
			player.setPassword("p2");
			gameApi.savePlayerRequest(player, (SavePlayerResponse response)=> 
			{
				step3LoginPlayerOne();
			});
		}

		private void step3LoginPlayerOne()
		{
			sampleIO.log("Step 3 : LoginPlayerOne");
			gameApi.loginRequest("player1", "p1", (LoginResponse loginResponse)=> 
			{
				if (LoginStatus.CONNECTED == loginResponse.getLoginStatus()) 
				{
					step4GetPlayerTwo();
				} else 
				{
					sampleIO.log("Step 3 : LoginPlayerOne failure ");
				}
			});
		}

		private void step4GetPlayerTwo()
		{
			sampleIO.log("Step 4 : GetPlayerTwo");
			NuggetaQuery playersQuery = new NuggetaQuery();
			playersQuery.setQuery("$WHERE Login = 'player2'");
			gameApi.getPlayersRequest(playersQuery, (GetPlayersResponse getPlayersResponse)=> 
			{
				if (GetPlayersStatus.SUCCESS == getPlayersResponse.getGetPlayersStatus()) 
				{
					List<NPlayer> players = getPlayersResponse.getPlayers();
					if (players.Count == 1) 
					{
						playerTwo = players[0];
						step5AskForFriendPlayerTwo();
					} else 
					{
						sampleIO.log("Step 4 : GetPlayerTwo :  retrieve more than one player !");
						onExit();
					}
				} else 
				{
					sampleIO.log("Step 4 : GetPlayerTwo failure " + getPlayersResponse.getGetPlayersStatus());
					onExit();
				}
			});
		}

		private void step5AskForFriendPlayerTwo()
		{
			sampleIO.log("Step 5 : AskForFriendPlayerTwo");
			gameApi.askForFriendRequest(playerTwo.getId(), (AskForFriendResponse askForFriendResponse)=> 
			{
				AskForFriendStatus status = askForFriendResponse.getAskForFriendStatus();
				if (AskForFriendStatus.PENDING == status) 
				{
					sampleIO.log("Step 5 : AskForFriendPlayerTwo SUCCESS !");
				} else 
				{
					sampleIO.log("Step 5 : AskForFriendPlayerTwo Failure  " + status);
				}
			});
		}
	}
}
