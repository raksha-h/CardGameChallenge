// (C) SAP 2021

using System;
using System.Collections.Generic;
using System.Linq;
using SAP.ProgrammingChallenge.CardGame.Entity;
using SAP.ProgrammingChallenge.CardGame.Exceptions;

namespace SAP.ProgrammingChallenge.CardGame
{
    public class Game
    {
        private readonly uint _playerCount;
        
        public Game(uint playerCount)
        {
            if (playerCount < 2) throw new InvalidPlayerCountException();

            _playerCount = playerCount;
            CreatePlayers();
            StaleMateCards = new List<Card>();
        }

        public List<Player> Players { get; set; }

        public List<Card> StaleMateCards { get; set; }

        public void Run()
        {
            //get a fresh deck and shuffle it
            var deck = new Deck();
            deck.Shuffle();
           
            DistributeCards(deck);

            Play();
        }

        private void CreatePlayers()
        {
            Players = new List<Player>();
            for (uint playerNumber = 1; playerNumber <= _playerCount; playerNumber++)
            {
                Players.Add(new Player($"Player {playerNumber}"));
            }
        }

        public void DistributeCards(Deck deck)
        {
            var topCard = deck.PopTopCard();
            var playerTurnIndex = 0;
            while(topCard != null)
            {
                Players[playerTurnIndex].AddCard(topCard);
                
                playerTurnIndex++;
                if (playerTurnIndex >= _playerCount)
                {
                    playerTurnIndex = 0;
                }

                topCard = deck.PopTopCard();
            };
        }
        
        private void Play()
        {
            Player winner;
            do
            {
                PlayOneRound();
            } while (!IsWinnerAvailable(out winner));

            Console.WriteLine($"\n{winner.Name} wins the game!");
        }

        public bool IsWinnerAvailable(out Player winner)
        {
            var validPlayers = Players.Where(player => player.CardCount != 0).ToList();
            if (validPlayers.Count() == 1)
            {
                winner = validPlayers.First();
                return true;
            }

            winner = null;
            return false;
        }

        public void PlayOneRound()
        {
            //Everyone draws top card from draw pile the puts it on table 
            var cardsOnTable = new List<PlayerCard>();
            foreach (var player in Players.Where(p => p.CardCount != 0))
            {
                var card = player.DrawCard();
                var playerCard = new PlayerCard { Player = player, Card = card };
                cardsOnTable.Add(playerCard);
            }

            //Check which player has the biggest card
            var roundWinner = GetRoundWinner(cardsOnTable);

            if (roundWinner != null)
            {
                Console.WriteLine($"{roundWinner.Name} wins this round");

                foreach (var card in cardsOnTable.Select(pc => pc.Card))
                {
                    roundWinner.AddCardToDiscardPile(card);
                    
                }

                if (StaleMateCards.Count == 0) return;

                foreach (var staleMateCard in StaleMateCards)
                {
                    roundWinner.AddCard(staleMateCard);
                }

                StaleMateCards = new List<Card>();
            }
            else
            {
                Console.WriteLine("No winner in this round");

                //winner cannot be decided so keep the card aside for next round
                foreach (var card in cardsOnTable.Select(pc => pc.Card))
                {
                    StaleMateCards.Add(card);
                }
            }
        }

        private Player GetRoundWinner(List<PlayerCard> playerCards)
        {
            var orderedPlayerCards = playerCards.OrderByDescending(pc => pc.Card.FaceValue).ToList();
            if (orderedPlayerCards[0].Card.FaceValue > orderedPlayerCards[1].Card.FaceValue)
            {
                return orderedPlayerCards[0].Player;
            }

            return null;
        }
    }
}
