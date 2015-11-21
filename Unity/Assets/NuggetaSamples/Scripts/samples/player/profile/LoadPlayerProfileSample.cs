namespace samples.player.profile
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class LoadPlayerProfileSample : NSample
	{

		protected NPlayerProfile playerProfile;

		public LoadPlayerProfileSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1GetPlayerProfile();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1GetPlayerProfile()
		{
			sampleIO.log("Step 1 : Get PlayerProfile ");
			gameApi.getPlayerProfileRequest((GetPlayerProfileResponse response)=> 
			{
				if (response.getGetPlayerProfileStatus() == GetPlayerProfileStatus.SUCCESS) 
				{
					playerProfile = response.getProfile();
					sampleIO.log("Step 1 : Get PlayerProfile successfull\r\n");
					step2UpdatePlayerProfile();
				} else 
				{
					sampleIO.log("Step 1 : Fail to Get PlayerProfile\r\n");
					onExit();
				}
			});
		}

		private void step2UpdatePlayerProfile()
		{
			sampleIO.log("Step 2 : Update Player Profile ");
			gameApi.savePlayerProfileRequest(playerProfile, (SavePlayerProfileResponse response)=> 
			{
				if (response.getSavePlayerProfileStatus() == SavePlayerProfileStatus.SUCCESS) 
				{
					sampleIO.log("Step 2 : Save PlayerProfile successfull \r\n");
				} else 
				{
					sampleIO.log("Step 2 : Fail to Save PlayerProfile\r\n");
					onExit();
				}
			});
		}
	}
}
