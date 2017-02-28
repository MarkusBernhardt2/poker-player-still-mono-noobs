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
            int currentBuyIn = (int)gameState.SelectToken("game_state.current_buy_in");

            return currentBuyIn + 10;


        }
        
		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}

	}
}

