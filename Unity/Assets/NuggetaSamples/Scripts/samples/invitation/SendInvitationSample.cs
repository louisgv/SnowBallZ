namespace samples.invitation
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using com.nuggeta.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class SendInvitationSample : NSample
	{

		private NGame game;

		public SendInvitationSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					invite();
				} else 
				{
					sampleIO.log("Nuggeta start failed");
				}
			});
			nuggetaPlug.start();
		}

		private void invite()
		{
			NInvitation invitation = new NInvitation();
			NRawMessage nRawMessage = new NRawMessage();
			nRawMessage.setContent("Invitation message of choice");
			invitation.setContent(nRawMessage);
			gameApi.invitePlayerRequest("player id to invite", invitation, (InvitePlayerResponse invitePlayerResponse)=> 
			{
				InvitePlayerStatus invitePlayerStatus = invitePlayerResponse.getInvitePlayerStatus();
				if (invitePlayerStatus == InvitePlayerStatus.SUCCESS) 
				{
					sampleIO.log("Invitation sent");
				} else 
				{
					sampleIO.log("Invitation failed" + invitePlayerStatus.ToString());
				}
			});
		}
	}
}
