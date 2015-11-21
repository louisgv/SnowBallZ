namespace samples.invitation
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class InvitationNotificationEventsSample : NSample
	{

		public InvitationNotificationEventsSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					sampleIO.log("Listening to Invitations events");
				} else 
				{
					sampleIO.log("Nuggeta start failed");
				}
			});
			gameApi.addNInvitationRequestNotificationHandler((NInvitationRequestNotification invitationRequestNotification)=> 
			{
				sampleIO.log("Received a player invitation " + invitationRequestNotification.ToString());
			});
			gameApi.addNInvitationAcceptedNotificationHandler((NInvitationAcceptedNotification nInvitationAcceptedNotification)=> 
			{
				sampleIO.log("Received a player invitation accepted" + nInvitationAcceptedNotification.ToString());
			});
			gameApi.addNInvitationRefusedNotificationHandler((NInvitationRefusedNotification invitationRefusedNotification)=> 
			{
				sampleIO.log("Received a player invitation reject" + invitationRefusedNotification.ToString());
			});
			gameApi.addNInvitationExpiredNotificationHandler((NInvitationExpiredNotification invitationExpiredNotification)=> 
			{
				sampleIO.log("Invitation expired" + invitationExpiredNotification.ToString());
			});
			nuggetaPlug.start();
		}
	}
}
