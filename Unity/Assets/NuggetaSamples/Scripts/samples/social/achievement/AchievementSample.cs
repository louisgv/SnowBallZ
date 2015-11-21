namespace samples.social.achievement
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class AchievementSample : NSample
	{

		public AchievementSample(String url):base(url)
		{
		}

		private List<NAchievement> achievements;

		private String achivementId = "ach1";


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1GetAchievements();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1GetAchievements()
		{
			sampleIO.log("Step 1 : Get Achievements ");
			gameApi.getAchievementsRequest((GetAchievementsResponse response)=> 
			{
				if (response.getGetAchievementsStatus() == GetAchievementsStatus.SUCCESS) 
				{
					achievements = response.getAchievements();
					sampleIO.log("Step 1 : Get Achievements successfull\r\n");
					step2Achieve();
				} else 
				{
					sampleIO.log("Step 1 : Fail to Get Achievements\r\n");
					onExit();
				}
			});
		}

		private void step2Achieve()
		{
			sampleIO.log("Step 2 : Achieve ");
			gameApi.achieveRequest(achivementId, (AchieveResponse response)=> 
			{
				if (response.getAchieveStatus() == AchieveStatus.SUCCESS) 
				{
					sampleIO.log("Step 2 : Achieve successfull \r\n");
				} else 
				if (response.getAchieveStatus() == AchieveStatus.ALREADY_ACHIEVED) 
				{
					sampleIO.log("Step 2 : this Achievement was already achieve by the player\r\n");
				} else 
				if (response.getAchieveStatus() == AchieveStatus.UNKNOWN_ACHIEVEMENT) 
				{
					sampleIO.log("Step 2 : this Achievement does not exist : please create the achievement with id 'ach1' from admin portal \r\n");
				} else 
				{
					sampleIO.log("Step 2 : Fail to Achieve\r\n");
					onExit();
				}
			});
		}
	}
}
