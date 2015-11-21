namespace samples.player.friend.request
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class RefuseFriendRequestSample : NSample
	{

		protected NFriendRequest friendRequest;

		public RefuseFriendRequestSample(String url):base(url)
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
				LoginStatus status = loginResponse.getLoginStatus();
				if (LoginStatus.CONNECTED == status) 
				{
					step2GetFriendRequests();
				} else 
				{
					sampleIO.log("Step 3 : LoginPlayerTwo failure " + status.ToString());
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
					if (size == 1) 
					{
						friendRequest = friendRequests[0];
						step3RefuseFriendRequest();
					}
				} else 
				{
					sampleIO.log("Step 3 : GetFriendRequests failure " + status.ToString());
				}
			});
		}

		 virtual protected void step3RefuseFriendRequest()
		{
			sampleIO.log("Step 3 : RefuseFriendRequest");
			gameApi.refuseFriendRequestRequest(friendRequest.getId(), (RefuseFriendRequestResponse response)=> 
			{
				RefuseFriendRequestStatus status = response.getRefuseFriendRequestStatus();
				if (RefuseFriendRequestStatus.SUCCESS == status) 
				{
					sampleIO.log("Step 3 : RefuseFriendRequest SUCCESS ");
				} else 
				{
					sampleIO.log("Step 3 : RefuseFriendRequest Failure " + status.ToString());
				}
			});
		}
	}
}
