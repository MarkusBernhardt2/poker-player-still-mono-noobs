using Nancy.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "Default C# folding player";

		public static int BetRequest(JObject gameState)
		{
            int currentBuyIn = (int)gameState.SelectToken("current_buy_in");
            
         
            JToken player = gameState.SelectToken("$.players[?(@.name == 'still mono noobs')]");
            int bet = (int)player.SelectToken("bet");
            

            if (currentBuyIn - bet < 300)
            { 
                return currentBuyIn - bet;
            }

            return 0;

        }
        
		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}

	}
}

