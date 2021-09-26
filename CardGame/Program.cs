// (C) SAP 2021

using System;

namespace SAP.ProgrammingChallenge.CardGame
{
    public class Program
    {
        private const uint PlayerCount = 2;

        public static void Main(string[] args)
        {
            var game = new Game(PlayerCount);
            game.Run();
            Console.ReadKey();
        }
    }
}
