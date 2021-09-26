// (C) SAP 2021

using System.Collections.Generic;
using NUnit.Framework;
using SAP.ProgrammingChallenge.CardGame.Entity;

namespace SAP.ProgrammingChallenge.CardGameTest.Entity
{
    [TestFixture]
    class PlayerTest
    {
        [Test]
        public void Given_Player_When_Created_Then_Name()
        {
            var sut = new Player("Player Test");

            Assert.AreEqual("Player Test", sut.Name);
        }

        [Test]
        public void Given_Player_When_Created_Then_CardCountZero()
        {
            var sut = new Player("Player Test");

            Assert.AreEqual(0, sut.CardCount);
        }

        [Test]
        public void Given_Player_When_CardsAreAddedToDrawPile_Then_CardCountIsUpdated()
        {
            var sut = new Player("Player Test");

            sut.AddCard(new Card { FaceValue = 1 });
            sut.AddCard(new Card { FaceValue = 2 });

            Assert.AreEqual(2, sut.CardCount);
        }

        [Test]
        public void Given_Player_When_CardsAreAddedToDiscardPile_Then_CardCountIsUpdated()
        {
            var sut = new Player("Player Test");

            sut.AddCardToDiscardPile(new Card { FaceValue = 1 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 2 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 3 });

            Assert.AreEqual(3, sut.CardCount);
        }

        [Test]
        public void Given_Player_When_CardsAreAddedToDrawPileAndDiscardPile_Then_CardCountIncludesDrawPileAndDiscardPile()
        {
            var sut = new Player("Player Test");

            sut.AddCard(new Card { FaceValue = 1 });
            sut.AddCard(new Card { FaceValue = 2 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 3 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 4 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 5 });

            Assert.AreEqual(5, sut.CardCount);
        }

        [Test]
        public void Given_PlayerWithDrawPile_When_DrawCard_Then_TopCardIsDrawn()
        {
            var sut = new Player("Player Test");
            sut.AddCard(new Card { FaceValue = 1 });
            sut.AddCard(new Card { FaceValue = 2 });

            var topCard = sut.DrawCard();

            Assert.IsNotNull(topCard);
            Assert.AreEqual(2, topCard.FaceValue);
        }

        [Test]
        public void Given_PlayerWithZeroCards_When_DrawCard_Then_TopCardIsNull()
        {
            var sut = new Player("Player Test");
           
            var topCard = sut.DrawCard();

            Assert.IsNull(topCard);
        }

        [Test]
        public void Given_PlayerWithEmptyDrawPile_When_DiscardPileIsAvailable_Then_TopCardIsFromDiscardPile()
        {
            var sut = new Player("Player Test");
            sut.AddCardToDiscardPile(new Card { FaceValue = 9 });

            var topCard = sut.DrawCard();

            Assert.IsNotNull(topCard);
            Assert.AreEqual(9, topCard.FaceValue);
        }

        [Test]
        public void Given_PlayerWithEmptyDrawPileAndCardsAreAvailableOnDiscardPile_When_DrawnCard_Then_CardCountIsDecremented()
        {
            var sut = new Player("Player Test");
            sut.AddCardToDiscardPile(new Card { FaceValue = 2 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 3 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 4 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 5 });

            var topCard = sut.DrawCard();

            Assert.IsNotNull(topCard);
            Assert.AreEqual(3, sut.CardCount);
        }

        [Test]
        public void Given_PlayerWithEmptyDrawPile_When_DiscardPileIsAvailable_Then_CardsAreShuffledAndKeptInDrawPile()
        {
            var sut = new Player("Player Test");
            sut.AddCardToDiscardPile(new Card { FaceValue = 5 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 4 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 3 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 2 });
            sut.AddCardToDiscardPile(new Card { FaceValue = 1 });

            var nonShuffledCardFaceValue = 1;
            var cardSimilarity = 0;
            var topCard = sut.DrawCard();
            while (topCard != null)
            {
                if (nonShuffledCardFaceValue == topCard.FaceValue) cardSimilarity++;
                nonShuffledCardFaceValue++;
                topCard = sut.DrawCard();
            }
            
            Assert.AreNotEqual(5, cardSimilarity);
        }
    }
}
