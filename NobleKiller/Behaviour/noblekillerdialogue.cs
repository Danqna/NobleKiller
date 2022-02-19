
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;

namespace NobleKiller.Behaviour
{
    class noblekillerdialogue : CampaignBehaviorBase
    {

		// 'Unable to cast object of type 'NobleKiller.MySubModule' to type 'TaleWorlds.MountAndBlade.MBSubModuleBase'.'
		public static readonly noblekillerdialogue Instance = new noblekillerdialogue();
		public static Hero NobleKillerTarget { get; set; }
		public static Hero NobleKillerAssassin { get; set; }

		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnSessionLaunched));
		}

		public void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{			
			AddDialogs(campaignGameStarter);			
		}

		protected void AddDialogs(CampaignGameStarter starter)
		{
			//Ignore for now, think this requires knowledge of XML files
			//TextObject textObject = new TextObject("{=*}(Assassinate) It would be in both of our best interests to murder {nk_hero}", null);
			//textObject.SetTextVariable("nk_hero", NobleKillerTarget.Name);

			// AddPlayerLine/DialogLine ( NAME_OF_THIS_TOPIC, GROUP_THIS_TOPIC_BELONGS_TO, WHERE_THIS_WILL_GO -> etc)

			// SELECT TARGET - PLAYER DIALOGUE

			// We need no checks for this dialogue line because the player can change their mind
			starter.AddPlayerLine("assassin_target_start", "hero_main_options", "assassin_noble_response",
				"(Select for Assassination) You have made a poor choice in enemies.",
				new ConversationSentence.OnConditionDelegate(this.noble_killer_hero_check),this.noble_killer_select, 100, null);			

			// SELECT TARGET - RESPONSE DIALOGUE

			starter.AddDialogLine("assassin_noble_response", "assassin_noble_response", "hero_main_options",
				"I am rubber, you are glue.", null, null, 100, null);


			// INFORM ASSASSIN - USING THE CONDITIONAL TO CHECK IF OPTION SHOULD SHOW
				
			starter.AddPlayerLine("assassin_start", "hero_main_options", "assassin_response",
				"(Assassinate) Start the murder",
				new ConversationSentence.OnConditionDelegate(this.noble_killer_assassin_check), noble_killer_consequence);

			starter.AddDialogLine("assassin_response", "assassin_response", "hero_main_options",
			"To be honest, I've always hated them. Let me consult the alchemist...", null, null, 100, null);
				
			

		}

		/// <summary>
		/// Set the target variable if this option is selected. Does NOT mean the noble will die, just that they will be the target if we speak to an assassin.
		/// </summary>
		private void noble_killer_select()
		{
			NobleKillerTarget = Hero.OneToOneConversationHero;
		}

		/// <summary>
		/// This is where the good stuff happens. Let's GET OUR MURDER ON AHAHAHAHAHAHAHAHAHA
		/// </summary>
		private void noble_killer_consequence()
        {
			// Sorry
			List<string> ourHero = new List<string>();
			ourHero.Add(NobleKillerTarget.Name.ToString());
			// CampaignCheats.KillHero(ourHero); Only works with cheats enabled
			KillCharacterAction.ApplyByDeathMarkForced(NobleKillerTarget, false);
			InformationManager.DisplayMessage(new InformationMessage("And so " + NobleKillerTarget.Name.ToString() + " passed into the darkness..."));
			NobleKillerTarget = null;			
		}

		/// <summary>
		/// This is where we just make sure the conversation is with a Hero
		/// </summary>
		/// <returns></returns>
		private bool noble_killer_hero_check()
		{
			if (Hero.OneToOneConversationHero != null && Hero.OneToOneConversationHero != NobleKillerTarget)
			{				
				return true;
			}
			return false;
		}

		/// <summary>
		/// Here we need to check that the player is speaking with another noble, but not the same noble. We're not asking the noble to kill themselves (as much as we might want to).
		/// </summary>
		/// <returns></returns>
		private bool noble_killer_assassin_check()
		{
			if (Hero.OneToOneConversationHero != null && Hero.OneToOneConversationHero != NobleKillerTarget && NobleKillerTarget != null)
			{
				return true;
			}
			return false;
		}

		public override void SyncData(IDataStore dataStore)
		{
		}

	}
}
