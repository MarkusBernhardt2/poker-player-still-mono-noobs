using Nancy.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

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

            IEnumerable<JToken> communityCards = player.SelectTokens("community_cards.Rank");
            var cardsQuality = AreGoodCards(player, communityCards);
             

            if (cardsQuality == CardsQuality.Brilliant)
            {
                return currentBuyIn - bet + 250;
            }
            if (cardsQuality == CardsQuality.VeryGood)
            {
                return currentBuyIn - bet + 100;
            }
            if (currentBuyIn - bet < 100 || cardsQuality == CardsQuality.OK)
            { 
                return currentBuyIn - bet;
            }
            

            return 0;

        }

        private static CardsQuality AreGoodCards(JToken player, IEnumerable<JToken> communityCards)
        {
            IEnumerable<JToken> pricyProducts = player.SelectTokens("hole_cards.Rank");

            string card1 = string.Empty;
            string card2 = string.Empty;
            string suit1 = string.Empty;
            string suit2 = string.Empty;
            int counter = 0;

            foreach (var card in pricyProducts)
            {
                if (counter == 0)
                    card1 = card.ToString();
                else
                    card2 = card.ToString();

                counter++;
            }

            var mycommunityCards = new List<string>();
            foreach (var card in communityCards)
            {
                mycommunityCards.Add(card.ToString());
            }

            counter = 0;
            IEnumerable<JToken> cardColors = player.SelectTokens("hole_cards.suit");
            foreach (var card in cardColors)
            {
                if (counter == 0)
                    suit1 = card.ToString();
                else
                    suit2 = card.ToString();

                counter++;
            }

            

            var goodCards = new List<string>() { "A", "K", "Q", "J", "10", "9", "8" };
            var goodCards2 = new List<string>() { "A", "K", "Q" };

            // Drilling
            if (mycommunityCards.FindAll(x => x == card1).Count >= 2 ||
                mycommunityCards.FindAll(x => x == card2).Count >= 2 ||
                (card1 == card2 && mycommunityCards.Contains(card1)))
            {
                return CardsQuality.Brilliant;
            }

            if ((card1 == card2 && goodCards.Contains( card1)) ||
                    (goodCards2.Contains(card1) && goodCards2.Contains(card2)) ||
                     ((mycommunityCards.Contains(card1) && goodCards2.Contains(card1)) || 
                     (mycommunityCards.Contains(card2) && goodCards2.Contains(card2))))
            {
                return CardsQuality.VeryGood;
            }

            if (suit1 == suit2 && mycommunityCards.Count == 0)
            {
                return CardsQuality.VeryGood;
            }
            if (suit1 == suit2)
            {
                return CardsQuality.OK;
            }

            if (goodCards2.Contains(card1) || goodCards2.Contains(card2) || card1 == card2 || mycommunityCards.Contains(card1) || mycommunityCards.Contains(card2))
            {
                return CardsQuality.OK;
            }

            return CardsQuality.Bad;
        }

        public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}

	}

    public enum CardsQuality
    {
        Brilliant,
        VeryGood,
        OK,
        Bad
    }
}

