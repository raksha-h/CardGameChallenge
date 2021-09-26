// (C) SAP 2021

using NUnit.Framework;
using SAP.ProgrammingChallenge.CardGame.Entity;

namespace SAP.ProgrammingChallenge.CardGameTest.Entity
{
    [TestFixture]
    public class DeckTest
    {
        [Test]
        public void Given_Deck_When_Created_Then_ItShouldHave40Cards()
        {
            var sut = new Deck();

            Assert.AreEqual(40, sut.CardsCount, "A fresh deck should have 40 cards");
        }

        [Test]
        public void Given_Deck_When_Created_Then_40CardsCanBeDrawn()
        {
            var sut = new Deck();

            for (var cardNumber = 1; cardNumber <= 40; cardNumber++)
            {
                var topCard = sut.PopTopCard();
                Assert.IsNotNull(topCard);
            }
        }


        [Test]
        public void Given_Deck_When_40CardsDrawn_Then_NextCardIsNull()
        {
            var sut = new Deck();

            for (var cardNumber = 0; cardNumber < 40; cardNumber++)
            {
                var topCard = sut.PopTopCard();
                Assert.IsNotNull(topCard);
            }

            var nextCard = sut.PopTopCard();
            Assert.IsNull(nextCard);
        }

        [Test]
        public void Given_Deck_When_Created_Then_CardsAreInOrder()
        {
            var sut = new Deck();

            for (var cardNumber = 39; cardNumber >= 0; cardNumber--)
            {
                var expectedFaceValue = (cardNumber % 10) + 1;
                var topCard = sut.PopTopCard();
                Assert.AreEqual(expectedFaceValue, topCard.FaceValue);
            }
        }

        [Test]
        public void Given_Deck_When_Shuffled_Then_CardsAreNotInOrder()
        {
            var sut = new Deck();

            sut.Shuffle();

            var cardSimilarity = 0;
            for (var cardNumber = 39; cardNumber >= 0; cardNumber--)
            {
                var nonShuffledFaceValue = (cardNumber % 10) + 1;
                var topCard = sut.PopTopCard();

                if (nonShuffledFaceValue == topCard.FaceValue) cardSimilarity++;
            }

            Assert.AreNotEqual(40, cardSimilarity, "Deck is not shuffled");
        }

        [Test]
        public void Given_Deck_When_Shuffled_Then_40CardsCanBeDrawn()
        {
            var sut = new Deck();

            sut.Shuffle();

            for (var cardNumber = 1; cardNumber <= 40; cardNumber++)
            {
                var topCard = sut.PopTopCard();
                Assert.IsNotNull(topCard);
            }
        }

        [Test]
        public void Given_Deck_When_CreatedEmpty_Then_CardCountIsZero()
        {
            var sut = new Deck(true);

            Assert.AreEqual(0, sut.CardsCount, "Empty deck should not have any cards");
        }


        [Test]
        public void Given_EmptyDeck_When_CardIsAdded_Then_CountIsIncreased()
        {
            var sut = new Deck(true);

            sut.Add(new Card {FaceValue = 1});

            Assert.AreEqual(1, sut.CardsCount);
        }

        [Test]
        public void Given_EmptyDeck_When_TopCardIsPopped_Then_CardIsNull()
        {
            var sut = new Deck(true);

            var topCard = sut.PopTopCard();

            Assert.IsNull(topCard);
        }

        [Test]
        public void Given_Deck_When_TopCardIsRemoved_Then_CountIsDecreased()
        {
            var sut = new Deck();
            Assert.AreEqual(40, sut.CardsCount);

            var topCard = sut.PopTopCard();

            Assert.IsNotNull(topCard);
            Assert.AreEqual(39, sut.CardsCount);
        }


        [Test]
        public void Given_Deck_When_CreatedFromAnotherDeck_Then_CardsCountAreEqual()
        {
            var deck = new Deck();

            var sut = new Deck(deck);

            Assert.AreEqual(deck.CardsCount, sut.CardsCount);
        }
    }
}
