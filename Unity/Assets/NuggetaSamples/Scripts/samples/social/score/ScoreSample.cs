namespace samples.social.score
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class ScoreSample : NSample
	{

		private List<NScore> scores;

		private String leaderboardId = "ld1";

		public ScoreSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1GetScores();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1GetScores()
		{
			sampleIO.log("Step 1 : Get Scores ");
			gameApi.getScoresRequest(leaderboardId, LeaderboardPeriod.ALL, 0, 10, (GetScoresResponse response)=> 
			{
				if (response.getGetScoresStatus() == GetScoresStatus.SUCCESS) 
				{
					scores = response.getScores();
					sampleIO.log("Step 1 : Get Scores successfull\r\n");
					step2SubmitScore();
				} else 
				if (response.getGetScoresStatus() == GetScoresStatus.UNKNOWN_LEADERBOARD) 
				{
					sampleIO.log("Step 2 : this Leaderboard does not exist : please create the Leaderboard with id 'ld1' from admin portal \r\n");
				} else 
				{
					sampleIO.log("Step 1 : Fail to Get Scores\r\n");
					onExit();
				}
			});
		}

		private void step2SubmitScore()
		{
			sampleIO.log("Step 2 : Submit Score ");
			NScore score = null;
			if (scores.Count > 0) 
			{
				score = scores[0];
				score.setValue(score.getValue() + 1);
			} else 
			{
				score = new NScore();
				score.setValue(((double) 123));
			}
			gameApi.submitScoreRequest(score, leaderboardId, (SubmitScoreResponse response)=> 
			{
				if (response.getSubmitScoreStatus() == SubmitScoreStatus.SUCCESS) 
				{
					sampleIO.log("Step 2 : Submit Score successfull \r\n");
				} else 
				if (response.getSubmitScoreStatus() == SubmitScoreStatus.UNKNOWN_LEADERBOARD) 
				{
					sampleIO.log("Step 2 : this Leaderboard does not exist : please create the Leaderboard with id 'ld1' from admin portal \r\n");
				} else 
				{
					sampleIO.log("Step 2 : Fail to Submit Score\r\n");
					onExit();
				}
			});
		}
	}
}
