namespace samples.invitation
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class RefuseInvitationSample : NSample
	{

		private NGame game;

		public RefuseInvitationSample(String url):base(url)
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
			gameApi.refuseInvitationRequest("invitationId", (RefuseInvitationResponse refuseInvitationResponse)=> 
			{
				RefuseInvitationStatus refuseInvitationStatus = refuseInvitationResponse.getRefuseInvitationStatus();
				String invitationId = refuseInvitationResponse.getInvitationId();
				if (refuseInvitationStatus == RefuseInvitationStatus.SUCCESS) 
				{
					sampleIO.log("Refuse invitation " + invitationId + " done ");
				} else 
				{
					sampleIO.log("Refuse invitation " + invitationId + " failed : " + refuseInvitationStatus.ToString());
				}
			});
		}
	}
}
