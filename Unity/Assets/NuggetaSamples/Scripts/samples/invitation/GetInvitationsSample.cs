namespace samples.invitation
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using com.nuggeta.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class GetInvitationsSample : NSample
	{

		public GetInvitationsSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					getInvitations();
				} else 
				{
					sampleIO.log("Nuggeta start failed");
				}
			});
			nuggetaPlug.start();
		}

		private void getInvitations()
		{
			NuggetaQuery nuggetaQuery = new NuggetaQuery();
			String playerId = "setPlayerId";
			nuggetaQuery.setQuery("$WHERE Invitee.Id = '" + playerId + "' $AND Status.Value = 0");
			gameApi.getInvitationsRequest(nuggetaQuery, (GetInvitationsResponse getInvitationsResponse)=> 
			{
				GetInvitationsStatus getInvitationsStatus = getInvitationsResponse.getGetInvitationsStatus();
				if (getInvitationsStatus == GetInvitationsStatus.SUCCESS) 
				{
					sampleIO.log("Get invitations response : " + getInvitationsResponse);
				} else 
				{
					sampleIO.log("GetInvitations failed : " + getInvitationsStatus.ToString());
				}
			});
		}
	}
}
