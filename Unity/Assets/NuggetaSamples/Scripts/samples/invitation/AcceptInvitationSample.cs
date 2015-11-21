namespace samples.invitation
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class AcceptInvitationSample : NSample
	{

		private NGame game;

		public AcceptInvitationSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					acceptInvitation();
				} else 
				{
					sampleIO.log("Nuggeta start failed");
				}
			});
			nuggetaPlug.start();
		}

		private void acceptInvitation()
		{
			gameApi.acceptInvitationRequest("invitationId", (AcceptInvitationResponse acceptInvitationResponse)=> 
			{
				AcceptInvitationStatus acceptInvitationStatus = acceptInvitationResponse.getAcceptInvitationStatus();
				String invitationId = acceptInvitationResponse.getInvitationId();
				if (acceptInvitationStatus == AcceptInvitationStatus.SUCCESS) 
				{
					sampleIO.log("accepted invitation " + invitationId);
				} else 
				{
					sampleIO.log("Accept invitation " + invitationId + " failed : " + acceptInvitationStatus.ToString());
				}
			});
		}
	}
}
