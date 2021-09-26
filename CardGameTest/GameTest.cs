// (C) SAP 2021

using NUnit.Framework;
using SAP.ProgrammingChallenge.CardGame;
using SAP.ProgrammingChallenge.CardGame.Entity;
using SAP.ProgrammingChallenge.CardGame.Exceptions;

namespace SAP.ProgrammingChallenge.CardGameTest
{
    [TestFixture]
    class GameTest
    {
        [TestCase(0u)]
        [TestCase(1u)]
        public void Given_Game_When_CreatedForInvalidPlayerCount_Then_ThrowsInvalidPlayerCountException(uint playerCount)
        {
            Assert.Throws<InvalidPlayerCountException>(() =>
            {
                var sut = new Game(0);
            });
        }

        [TestCase(2u)]
        [TestCase(3u)]
        [TestCase(4u)]
        public void Given_Game_When_CreatedForValidPlayerCount_Then_DoesNotThrowsException(uint playerCount)
        {
            Assert.DoesNotThrow(() =>
            {
                var sut = new Game(playerCount);
            });
        }

        [TestCase(2u)]
        [TestCase(3u)]
        [TestCase(4u)]
        public void Given_Game_When_PlayersCreated_Then_CheckNames(uint playerCount)
        {
            var sut = new Game(playerCount);

            Assert.AreEqual(playerCount, sut.Players.Count);

            for (int i = 0; i < playerCount; i++)
            {
                Assert.AreEqual($"Player {i+1}", sut.Players[i].Name);
            }
        }

        [Test]
        public void Given_GameWith2PlayersAndADeck_When_DistributeCards_Then_CardsAreDistributedEquallyAndDeckIsEmpty()
        {
            var sut = new Game(2);
            var deck = new Deck();

            sut.DistributeCards(deck);

            Assert.AreEqual(0, deck.CardsCount);
            Assert.AreEqual(20, sut.Players[0].CardCount);
            Assert.AreEqual(20, sut.Players[1].CardCount);
        }

        [Test]
        public void Given_GameWithPlayer1AsWinner_When_IsWinnerAvailable_Then_ReturnsPlayer1()
        {
            var sut = new Game(2);
            sut.Players[0].AddCard(new Card { FaceValue = 1});

            var isWinnerAvailable = sut.IsWinnerAvailable(out var winner);

            Assert.IsTrue(isWinnerAvailable);
            Assert.AreEqual("Player 1", winner.Name);
           
        }

        [Test]
        public void Given_GameWithPlayer2AsWinner_When_IsWinnerAvailable_Then_ReturnsPlayer2()
        {
            var sut = new Game(2);
            sut.Players[1].AddCard(new Card { FaceValue = 1 });
            sut.Players[1].AddCard(new Card { FaceValue = 2 });

            var isWinnerAvailable = sut.IsWinnerAvailable(out var winner);

            Assert.IsTrue(isWinnerAvailable);
            Assert.AreEqual("Player 2", winner.Name);
        }

        [Test]
        public void Given_GameWith2Players_When_2CardsOnTable_Then_PlayerWithHighValueCardWinsRound()
        {
            var sut = new Game(2);
            sut.Players[0].AddCard(new Card { FaceValue = 4 });
            sut.Players[0].AddCard(new Card { FaceValue = 5 });
            sut.Players[1].AddCard(new Card { FaceValue = 2 });
            sut.Players[1].AddCard(new Card { FaceValue = 1 });

            sut.PlayOneRound();
            //Player 1 (2 cards) : 5
            //Player 2 (2 cards) : 1
            //Player 1 wins this round

            Assert.AreEqual(3, sut.Players[0].CardCount);
            Assert.AreEqual(1, sut.Players[1].CardCount);
        }

        [Test]
        public void Given_GameWith2Players_When_2CardsOnTableWithSameValue_Then_CardsAreKeptAsideForNextRoundAsStaleMateCards()
        {
            var sut = new Game(2);
            sut.Players[0].AddCard(new Card { FaceValue = 4 });
            sut.Players[0].AddCard(new Card { FaceValue = 9 });
            sut.Players[0].AddCard(new Card { FaceValue = 10 });
            sut.Players[1].AddCard(new Card { FaceValue = 8 });
            sut.Players[1].AddCard(new Card { FaceValue = 10 });

            sut.PlayOneRound();
            //Player 1 (3 cards) : 10
            //Player 2 (2 cards) : 10
            //No winner in this round

            Assert.AreEqual(2, sut.StaleMateCards.Count);
            Assert.AreEqual(2, sut.Players[0].CardCount);
            Assert.AreEqual(1, sut.Players[1].CardCount);
        }

        [Test]
        public void Given_GameWith2Players_When_2CardsOnTableWithSameValue_Then_NextRoundWinnerGetsTheseCardsToo()
        {
            var sut = new Game(2);
            sut.Players[0].AddCard(new Card { FaceValue = 4 });
            sut.Players[0].AddCard(new Card { FaceValue = 5 });
            sut.Players[1].AddCard(new Card { FaceValue = 8 });
            sut.Players[1].AddCard(new Card { FaceValue = 5 });

            sut.PlayOneRound();
            //Player 1 (2 cards) : 5
            //Player 2 (2 cards) : 5
            //No winner in this round

            Assert.AreEqual(2, sut.StaleMateCards.Count);
            Assert.AreEqual(1, sut.Players[0].CardCount);
            Assert.AreEqual(1, sut.Players[1].CardCount);

            sut.PlayOneRound(); //next round
            //Player 1 (1 cards) : 4
            //Player 2 (1 cards) : 8
            //Player 2 wins this round

            Assert.AreEqual(0, sut.StaleMateCards.Count);
            Assert.AreEqual(0, sut.Players[0].CardCount);
            Assert.AreEqual(4, sut.Players[1].CardCount);
        }


        [Test] public void Given_GameWith2Players_When_2RoundsLeadsToStaleMate_Then_ThirdRoundWinnerGetsAll6Cards()
        {
            var sut = new Game(2);
            sut.Players[0].AddCard(new Card { FaceValue = 3 });
            sut.Players[0].AddCard(new Card { FaceValue = 4 });
            sut.Players[0].AddCard(new Card { FaceValue = 5 });
            sut.Players[1].AddCard(new Card { FaceValue = 2 });
            sut.Players[1].AddCard(new Card { FaceValue = 4 });
            sut.Players[1].AddCard(new Card { FaceValue = 5 });

            sut.PlayOneRound(); //round 1
            //Player 1 (3 cards) : 5
            //Player 2 (3 cards) : 5
            //No winner in this round

            Assert.AreEqual(2, sut.StaleMateCards.Count);
            Assert.AreEqual(2, sut.Players[0].CardCount);
            Assert.AreEqual(2, sut.Players[1].CardCount);

            sut.PlayOneRound(); //round 2
            //Player 1 (2 cards) : 4
            //Player 2 (2 cards) : 4
            //No winner in this round

            Assert.AreEqual(4, sut.StaleMateCards.Count);
            Assert.AreEqual(1, sut.Players[0].CardCount);
            Assert.AreEqual(1, sut.Players[1].CardCount);

            sut.PlayOneRound(); //round 3
            //Player 1 (1 cards) : 3
            //Player 2 (1 cards) : 2
            //Player 1 wins this round

            Assert.AreEqual(0, sut.StaleMateCards.Count);
            Assert.AreEqual(6, sut.Players[0].CardCount);
            Assert.AreEqual(0, sut.Players[1].CardCount);
        }
    }
}
