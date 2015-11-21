namespace samples.player
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class LoginPlayerSample : NSample
	{

		protected NPlayer createdPlayer;

		public LoginPlayerSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1LoginPlayer();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1LoginPlayer()
		{
			sampleIO.log("Step 1 : Login Player ");
			gameApi.loginRequest("toto", "pwd123456", (LoginResponse response)=> 
			{
				if (response.getLoginStatus() == LoginStatus.CONNECTED) 
				{
					createdPlayer = response.getPlayer();
					sampleIO.log("Step 1 : Login Player successfull\r\n");
				} else 
				{
					sampleIO.log("Step 1 : Fail to Login Player\r\n");
					onExit();
				}
			});
		}
	}
}
