namespace samples.player.friend
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class GetFriendsSample : NSample
	{

		public GetFriendsSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1LoginPlayerOne();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1LoginPlayerOne()
		{
			sampleIO.log("Step 1 : LoginPlayerOne");
			gameApi.loginRequest("player1", "p1", (LoginResponse loginResponse)=> 
			{
				if (LoginStatus.CONNECTED == loginResponse.getLoginStatus()) 
				{
					step2GetFriends();
				} else 
				{
					sampleIO.log("Step 1 : LoginPlayerOne failure ");
				}
			});
		}

		private void step2GetFriends()
		{
			sampleIO.log("Step 2 : GetFriends");
			gameApi.getFriendsRequest((GetFriendsResponse response)=> 
			{
				GetFriendsStatus status = response.getGetFriendsStatus();
				if (GetFriendsStatus.SUCCESS == status) 
				{
					List<NPlayer> friends = response.getFriends();
					int size = friends.Count;
					sampleIO.log("Step 2 : GetFriends SUCCESS found " + size + " demands");
					foreach(NPlayer friend in friends) {
						sampleIO.log(friend.ToString());
					}
				} else 
				{
					sampleIO.log("Step 2 : GetFriends failure " + status);
				}
			});
		}
	}
}
