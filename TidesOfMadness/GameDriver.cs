using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TidesOfMadness
{
    public class GameDriver
    {
        public List<ResolveMadnessOption> MadnessOptions;  //TODO: This should probably go elsewhere

        public GameStateTracker GameState;
        

        private void RefreshStatesAndInputStatus()
        {
            //Update what GameState we are in, and whether or not we are waiting on the user for input
            switch(GameState.CurrentGameState)
            {
                case GameStates.Setup:
                    {
                        //Always go to PlayCards after Setup has been run
                        GameState.CurrentGameState = GameStates.PlayCards;
                        GameState.RequirePlayerInput = true;
                        break;
                    }
                case GameStates.PlayCards:
                    {
                        //If player hands are empty
                        if(this.GetHumanPlayer().CardsInHand.CardsInCollection.Count == 0)
                        {
                            GameState.CurrentGameState = GameStates.SetDreamlands;
                            if (this.GetHumanPlayer().CardsInPlay.CardsInCollection.Any(c => c.CardNameEnum == CardNames.Dreamlands))
                            {
                                GameState.RequirePlayerInput = true;
                            }
                            else
                            {
                                GameState.RequirePlayerInput = false;
                            }
                        }
                        else
                        {
                            GameState.RequirePlayerInput = true;
                        }
                        break;
                    }
                case GameStates.SetDreamlands:
                    {
                        GameState.CurrentGameState = GameStates.ResolveMadness;
                        GameState.RequirePlayerInput = true; //TEMP
                        break;
                    }
                case GameStates.ResolveMadness:
                    {
                        break;
                    }
                case GameStates.Scoring:
                    {
                        break;
                    }
                case GameStates.PickUpCards:
                    {
                        break;
                    }
                case GameStates.ChooseCardToReplay:
                    {
                        break;
                    }
                case GameStates.ChooseCardToDiscard:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public GameStateTracker InitializeGame()
        {
            GameState = new GameStateTracker();
            GameState.HumanPlayer = new Player();
            GameState.AIPlayer = new AIPlayer();
            GameState.AppendToGameLog("Welcome to Tides of Madness!");
            MadnessOptions = MadnessListOptionGenerator.GenerateMadnessListOptions();
            GameState.Deck =  DeckGenerator.GenerateDeck();
            GameState.Deck.Shuffle();
            GameState.CurrentRound = 1;
            GameState.CurrentGameState = GameStates.Setup;
            SetUpRound();
            RefreshStatesAndInputStatus();
            return GameState;
        }

        public Player GetHumanPlayer()
        {
            return this.GameState.HumanPlayer;
        }

        public AIPlayer GetAIPlayer()
        {
            return this.GameState.AIPlayer;
        }

        public CardCollection GetDeck()
        {
            return this.GameState.Deck;
        }

        public GameStates GetCurrentGameState()
        {
            return this.GameState.CurrentGameState;
        }

        public string GetGameLog()
        {
            return this.GameState.GameLog;
        }

        private void SetUpRound()
        {
            //If round 1, deal 5 cards to each player
            //Otherwise, deal two cards
            //Then move to PlayCards state

            this.GetHumanPlayer().MadnessThisRound = 0;
            this.GetAIPlayer().MadnessThisRound = 0;

            int cardsToDeal = (GameState.CurrentRound == 1) ? 5 : 2;

            GameState.AppendToGameLog($"Each player draws {cardsToDeal} new cards.");

            this.GetHumanPlayer().CardsInHand.AddCardToCollection(this.GetDeck().GetTopCards(cardsToDeal));
            this.GetAIPlayer().CardsInHand.AddCardToCollection(this.GetDeck().GetTopCards(cardsToDeal));
        }

        private void EachPlayerPlaysOneCard(Card playerCard, Card AICard)
        {
            this.GetHumanPlayer().PlaySelectedCard(playerCard);
            this.GetAIPlayer().PlaySelectedCard(AICard);
        }

        private void PlayIndividualCard(Player player, Card playedCard)
        {
            player.CardsInHand.CardsInCollection.Remove(playedCard);
            player.CardsInPlay.CardsInCollection.Add(playedCard);
        }

        private void SetDreamlands(Player humanPlayer, AIPlayer aiPlayer)
        {
            GameState.AppendToGameLog($"Still need to SetDreamlands");
            if (humanPlayer.CardsInPlay.CardsInCollection.Any(c => c.CardNameEnum == CardNames.Dreamlands))
            {
                GameState.AppendToGameLog($"Human player has Dreamlands");
            }
            else if (aiPlayer.CardsInPlay.CardsInCollection.Any(c => c.CardNameEnum == CardNames.Dreamlands))
            {
                GameState.AppendToGameLog($"AI player has Dreamlands");
            }
            else
            {
                GameState.AppendToGameLog($"Nobody has Dreamlands");
            }
        }

        private void ResetDoubleFlags()
        {
            //Reset the DoubleScore flag on all cards
        }

        private void ResolveMadness()
        {
            //Check to see which player got more madness this round.
            //If it wasn't a tie, one player gets the opportunity to remove one madness token or gain 4 points
            //Then move to the Scoring state
        }

        private void ScoreRound()
        {
            //Score all points on cards.
            //If this isn't the last round, move to PickUpCards state
            //Otherwise, end the game
        }

        private void PickUpCards()
        {
            //Reset the "double score" flag on all cards
            //Each player picks up 5 cards from play and returns them to their hand
            //Then move to the ChooseCardToReplay state
        }

        private void ReplayCard()
        {
            //Each player chooses a card to replay
            //Then move to the Setup state
        }

        private void SwapPlayerHands(Player firstPlayer, Player secondPlayer)
        {
            CardCollection temp = new CardCollection();
            temp = firstPlayer.CardsInHand;
            firstPlayer.CardsInHand = secondPlayer.CardsInHand;
            secondPlayer.CardsInHand = temp;
        }

        public void ActOnPlayerInput(PlayerInput input)
        {
            do
            {
                switch (GameState.CurrentGameState)
                {
                    case GameStates.PlayCards:
                        Card playerCard = input.SelectedCards[0];
                        Card aiCard = this.GetAIPlayer().ChooseCardToPlay();
                        EachPlayerPlaysOneCard(playerCard, aiCard);
                        SwapPlayerHands(this.GetHumanPlayer(), this.GetAIPlayer());
                        GameState.AppendToGameLog($"Player plays {playerCard.CardNameDisplay} from hand");
                        GameState.AppendToGameLog($"Opponent plays {aiCard.CardNameDisplay} from hand");
                        break;
                    case GameStates.SetDreamlands:
                        SetDreamlands(this.GetHumanPlayer(), this.GetAIPlayer());
                        break;
                    case GameStates.ChooseCardToReplay:
                        break;
                    case GameStates.ChooseCardToDiscard:
                        break;
                    default:
                        break;
                        //BVJ TO DO: Error handling
                }
 
                RefreshStatesAndInputStatus();
            }
            while (GameState.RequirePlayerInput == false);
        }

        
    }
}
