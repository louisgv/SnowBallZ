namespace samples.player.friend.request
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class AcceptFriendRequestSample : NSample
	{

		protected NFriendRequest friendRequest;

		public AcceptFriendRequestSample(String url):base(url)
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
			gameApi.addNFriendshipNotificationHandler((NFriendshipNotification friendshipnotification)=> 
			{
				sampleIO.log("NOTIFICATION : Received FriendshipNotification  " + friendshipnotification);
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
						step3AcceptFriendRequest();
					}
				} else 
				{
					sampleIO.log("Step 3 : GetFriendRequests failure " + status.ToString());
				}
			});
		}

		 virtual protected void step3AcceptFriendRequest()
		{
			sampleIO.log("Step 3 : AcceptFriendRequest");
			gameApi.acceptFriendRequestRequest(friendRequest.getId(), (AcceptFriendRequestResponse response)=> 
			{
				AcceptFriendRequestStatus status = response.getAcceptFriendRequestStatus();
				if (AcceptFriendRequestStatus.SUCCESS == status) 
				{
					sampleIO.log("Step 3 : AcceptFriendRequest SUCCESS ");
				} else 
				{
					sampleIO.log("Step 3 : AcceptFriendRequest Failure " + status.ToString());
				}
			});
		}
	}
}
