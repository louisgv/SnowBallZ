namespace samples.storable
{
	using com.nuggeta.game.core.api.handlers;
	using com.nuggeta.game.core.ngdl.nobjects;
	using com.nuggeta.ngdl.nobjects;
	using common;
	using System;
	using System.Collections.Generic;

	public class StorableSample : NSample
	{

		private String nameSpace = "nrawmessage";

		private NRawMessage storable;

		public StorableSample(String url):base(url)
		{
		}


		 override public void run()
		{
			nuggetaPlug.setStartResponseHandler((StartResponse startresponse)=> 
			{
				if (startresponse.getStartStatus() == StartStatus.READY) 
				{
					sampleIO.connected();
					step1SaveStorable();
				} else 
				{
					sampleIO.log("Nuggeta start failed\r\n");
				}
			});
			nuggetaPlug.start();
		}

		private void step1SaveStorable()
		{
			storable = new NRawMessage();
			storable.setContent("this is the RawMessage content");
			sampleIO.log("Step 1 : Saving Storable ");
			gameApi.saveStorableRequest(storable, nameSpace, (SaveStorableResponse response)=> 
			{
				if (response.getSaveStorableStatus() == SaveStorableStatus.SUCCESS) 
				{
					sampleIO.log("Step 1 : Save storable successfull\r\n");
					step2FindStorables();
				} else 
				{
					sampleIO.log("Step 1 : Fail to update storable\r\n");
					onExit();
				}
			});
		}

		private void step2FindStorables()
		{
			NuggetaQuery nuggetaQuery = new NuggetaQuery();
			nuggetaQuery.setDomain(nameSpace);
			nuggetaQuery.setQuery("$WHERE content $LIKE '%RawMessage%' ");
			sampleIO.log("Step 2 : Find Storables ");
			gameApi.findStorablesRequest(nuggetaQuery, (FindStorablesResponse response)=> 
			{
				if (response.getFindStorablesStatus() == FindStorablesStatus.SUCCESS) 
				{
					List<Storable> storables = response.getStorables();
					if (storables.Count != 0) 
					{
						sampleIO.log("Step 2 : Find Storables successfull : found " + storables.Count + " storables\r\n");
						storable = ((NRawMessage) storables[0]);
						step3SaveStorable();
					} else 
					{
						sampleIO.log("Step 2 :Error Find Storables return no result \r\n");
						onExit();
					}
				} else 
				{
					sampleIO.log("Step 2 : Fail to Find Storables\r\n");
					onExit();
				}
			});
		}

		private void step3SaveStorable()
		{
			storable.setContent("a new the content");
			sampleIO.log("Step 3 : Save storable");
			gameApi.saveStorableRequest(storable, nameSpace, (SaveStorableResponse response)=> 
			{
				if (response.getSaveStorableStatus() == SaveStorableStatus.SUCCESS) 
				{
					sampleIO.log("Step 3 : Save Storable successfull\r\n");
					step4RemoveStorable();
				} else 
				{
					sampleIO.log("Step 3 : Fail to Save Storable\r\n");
				}
			});
		}

		private void step4RemoveStorable()
		{
			sampleIO.log("Step 4 : Removing storable");
			gameApi.removeStorableRequest(storable.getStoreId(), nameSpace, (RemoveStorableResponse response)=> 
			{
				if (response.getRemoveStorableStatus() == RemoveStorableStatus.SUCCESS) 
				{
					sampleIO.log("Step 4 : Remove storable successfull\r\n");
					step5FindStorables();
				} else 
				{
					sampleIO.log("Step 4 : Fail to remove storable\r\n");
				}
			});
		}

		private void step5FindStorables()
		{
			NuggetaQuery nuggetaQuery = new NuggetaQuery();
			nuggetaQuery.setDomain(nameSpace);
			sampleIO.log("Step 5 : Get Storables ");
			gameApi.findStorablesRequest(nuggetaQuery, (FindStorablesResponse response)=> 
			{
				if (response.getFindStorablesStatus() == FindStorablesStatus.SUCCESS) 
				{
					List<Storable> storables = response.getStorables();
					if (storables.Count == 0) 
					{
						sampleIO.log("Step 5 : Get Storables successfull : there is no elements\r\n");
					} else 
					{
						sampleIO.log("Step 5 : Error Get Storables should have return 0 elements\r\n");
					}
					onExit();
				} else 
				{
					sampleIO.log("Step 5 : Fail to Get Storables\r\n");
					onExit();
				}
			});
		}
	}
}
