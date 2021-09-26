// (C) SAP 2021

using System;

namespace SAP.ProgrammingChallenge.CardGame.Entity
{
    public class Player
    {
        private readonly string _name;
        private Deck _drawPile;
        private Deck _discardPile;

        public Player(string name)
        {
            _name = name;
            _drawPile = new Deck(true);
            _discardPile = new Deck(true);
        }

        public string Name => _name;

        public int CardCount => _drawPile.CardsCount + _discardPile.CardsCount;

        public Card DrawCard()
        {
            if (CardCount == 0) return null;

            var topCard = _drawPile.PopTopCard();
            if (topCard != null)
            {
                Print(topCard);
                return topCard;
            }

            if (_discardPile.CardsCount == 0) return null;
            
            UseCardsFromDiscardPile();

            var newTopCard = _drawPile.PopTopCard();
            Print(newTopCard);
            return newTopCard;
        }

        public void AddCard(Card card)
        {
            _drawPile.Add(card);
        }

        public void AddCardToDiscardPile(Card card)
        {
            _discardPile.Add(card);
        }

        private void UseCardsFromDiscardPile()
        {
            _discardPile.Shuffle();
            _drawPile = new Deck(_discardPile);
            _discardPile = new Deck(true);
        }

        private void Print(Card card)
        {
            Console.WriteLine($"{_name} ({CardCount + 1} cards) : {card.FaceValue}");
        }
    }
}
