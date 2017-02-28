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

            //return 5;
            try
            {
                RootObject test = CreateClass(gameState.ToString());

                var us = FindUs(test);

                if (us == null)
                {
                    return 5;
                }
            }
            catch(Exception )
            {
                return 3;
            }
            



            return 10;
        }

        public static Player FindUs(RootObject root)
        {
            foreach (var element in root.players)
            {
                if (element.name == "still mono noobs")
                    return element;
            }

            return null;
        }

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}

        public static RootObject CreateClass(string gameState)
        {

            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            RootObject routes_list =
                   (RootObject)json_serializer.DeserializeObject(gameState);
            return routes_list;
        }
	}
}

