namespace samples.player.friend.request
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class GetFriendRequestsSample : NSample
	{

		public GetFriendRequestsSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1LoginPlayerTwo();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1LoginPlayerTwo()
		{
			sampleIO.log("Step 1 : LoginPlayerTwo");
			gameApi.loginRequest("player2", "p2", (LoginResponse loginResponse)=> 
			{
				if (LoginStatus.CONNECTED == loginResponse.getLoginStatus()) 
				{
					step2GetFriendRequests();
				} else 
				{
					sampleIO.log("Step 3 : LoginPlayerTwo failure ");
				}
			});
		}

		private void step2GetFriendRequests()
		{
			sampleIO.log("Step 2 : GetFriendRequests");
			gameApi.getFriendRequestsRequest((GetFriendRequestsResponse response)=> 
			{
				GetFriendRequestsStatus status = response.getGetFriendRequestsStatus();
				if (GetFriendRequestsStatus.SUCCESS == status) 
				{
					List<NFriendRequest> friendRequests = response.getFriendRequests();
					int size = friendRequests.Count;
					sampleIO.log("Step 2 : GetFriendRequests SUCCESS found " + size + " Requests");
					foreach(NFriendRequest friendRequest in friendRequests) {
						sampleIO.log(friendRequest.ToString());
					}
				} else 
				{
					sampleIO.log("Step 3 : GetFriendRequests failure " + status);
				}
			});
		}
	}
}
