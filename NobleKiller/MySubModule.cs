using NobleKiller.Behaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;

namespace NobleKiller
{
	class MySubModule : MBSubModuleBase
	{
		protected override void OnSubModuleLoad()
		{
			base.OnSubModuleLoad();
			try
			{								
				
			}
			catch (Exception e)
			{
				
			}
		}

		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			try
			{
				base.OnGameStart(game, gameStarterObject);
				if (!(game.GameType is Campaign))
				{
					return;
				}
				AddBehaviors(gameStarterObject as CampaignGameStarter);
			}
			catch (Exception e)
			{
				
			}
		}

		public override void OnGameInitializationFinished(Game game)
		{


		}
		
		private void AddBehaviors(CampaignGameStarter gameStarterObject)
		{
			if (gameStarterObject != null)
			{
				gameStarterObject.AddBehavior(noblekillerdialogue.Instance);
			}
		}
	}
}