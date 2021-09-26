// (C) SAP 2021

using System;
using System.Collections.Generic;

namespace SAP.ProgrammingChallenge.CardGame.Entity
{
    public class Deck
    {
        private readonly List<Card> _cards;
        private readonly Random _random;

        public Deck(bool isEmpty = false)
        {
            _cards = new List<Card>();
            _random = new Random();

            if(!isEmpty) CreateFreshDeck();
        }

        public Deck(Deck deck)
        {
            _cards = deck._cards;
        }

        public int CardsCount => _cards.Count;

        public Card PopTopCard()
        {
            if (_cards.Count != 0)
            {
                var lastItemIndex = _cards.Count - 1;
                var topCard = _cards[lastItemIndex];
                _cards.RemoveAt(lastItemIndex);

                return topCard;
            }

            return null;
        }

        /// <summary>
        /// Fisher-Yates Shuffle Algorithm
        /// </summary>
        public void Shuffle()
        {
            var n = _cards.Count;
            while (n > 1)
            {
                n--;
                var k = _random.Next(n + 1);
                (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
            }
        }

        public void Add(Card card)
        {
            _cards.Add(card);
        }

        private void CreateFreshDeck()
        {
            for (int cardType = 1; cardType <= 4; cardType++)
            {
                for (uint faceValue = 1; faceValue <= 10; faceValue++)
                {
                    _cards.Add(new Card { FaceValue = faceValue });
                }
            }
        }
    }
}
