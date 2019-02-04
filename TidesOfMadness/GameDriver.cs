using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TidesOfMadness
{
    public class GameDriver
    {
        public List<ResolveMadnessOption> MadnessOptions;   //TODO: This should probably go elsewhere
        public List<SuitOption> SuitOptions;                //TODO: This too

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
                        if (this.GetHumanPlayer().GetCardsInHand().Count == 0)
                        {
                            GameState.CurrentGameState = GameStates.ResolveMadness;
                        }
                        else
                        {
                            GameState.RequirePlayerInput = true;
                        }
                        break;
                    }
                case GameStates.ResolveMadness:
                    {
                        GameState.CurrentGameState = GameStates.ResolveMadnessBonus;
                        if (GameState.HumanPlayer.MadnessThisRound > GameState.AIPlayer.MadnessThisRound)
                        {
                            GameState.RequirePlayerInput = true;
                        }
                        break;
                    }
                case GameStates.ResolveMadnessBonus:
                    {
                        GameState.CurrentGameState = GameStates.SetDreamlands;
                        Player tempPlayer = SeeWhoHasDreamlands(this.GetHumanPlayer(), this.GetAIPlayer());
                        if (tempPlayer != null && tempPlayer == GetHumanPlayer())
                        {
                            GameState.RequirePlayerInput = true;
                        }
                        break;
                    }
                case GameStates.SetDreamlands:
                    {
                        GameState.CurrentGameState = GameStates.Scoring;
                        GameState.RequirePlayerInput = true;    //TEMP
                        break;
                    }
                case GameStates.Scoring:
                    {
                        GameState.RequirePlayerInput = true;    //TEMP
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
            GameState.HumanPlayer = new Player("Player");
            GameState.AIPlayer = new AIPlayer("Opponent");
            GameState.AppendToGameLog("Welcome to Tides of Madness!");
            GameState.Deck =  DeckGenerator.GenerateDeck();
            GameState.Deck.Shuffle();
            GameState.CurrentRound = 1;
            GameState.CurrentGameState = GameStates.Setup;
            SuitOptions = ListOptionGenerator.GenerateSuitOptions();
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
            //Reset MadnessThisRound and CardsPlayedThisRound to 0
            //Reset doubling flag on all cards
            ResetRoundBasedValues();

            //If round 1, deal 5 cards to each player
            //Otherwise, deal two cards
            //Then move to PlayCards state
            int cardsToDeal = (GameState.CurrentRound == 1) ? 5 : 2;

            GameState.AppendToGameLog($"Each player draws {cardsToDeal} new cards.");

            this.GetHumanPlayer().CardsInHand.AddCardToCollection(this.GetDeck().GetTopCards(cardsToDeal));
            this.GetAIPlayer().CardsInHand.AddCardToCollection(this.GetDeck().GetTopCards(cardsToDeal));
        }

        private void EachPlayerPlaysOneCard(Player humanPlayer, AIPlayer aiPlayer, Card playerCard, Card aiCard)
        {
            humanPlayer.PlaySelectedCard(playerCard);
            aiPlayer.PlaySelectedCard(aiCard);
            GameState.AppendToGameLog($"{humanPlayer.Name} plays {playerCard.CardNameDisplay} from hand");
            GameState.AppendToGameLog($"{aiPlayer.Name} plays {aiCard.CardNameDisplay} from hand");
        }

        private Player SeeWhoHasDreamlands(Player humanPlayer, AIPlayer aiPlayer)
        {
            if (humanPlayer.CheckForSpecificCard(CardNames.Dreamlands))
            {
                GameState.AppendToGameLog($"{humanPlayer.Name} has Dreamlands in play.");
                return humanPlayer;
            }
            else if (aiPlayer.CheckForSpecificCard(CardNames.Dreamlands))
            {
                GameState.AppendToGameLog($"{aiPlayer.Name} has Dreamlands in play.");
                return aiPlayer;
            }
            else
            {
                GameState.AppendToGameLog("Nobody has Dreamlands in play.");
                return null;
            }
        }

        private void SetDreamlandsSuit(Player player, SuitOption option)
        {
            Card dreamlands = player.GetCardsInPlay().FirstOrDefault(n => n.CardNameEnum == CardNames.Dreamlands);
            if (dreamlands == null)
            {
                GameState.AppendToGameLog($"ERROR: How did we get here? {player.Name} does not have Dreamlands in play!");
            }
            else
            {
                dreamlands.Suit = option.Suit;
                GameState.AppendToGameLog($"{player.Name} has chosen {option.Text} for the Dreamlands Suit.");
            }
        }

        private void ResetRoundBasedValues()
        {
            this.GetHumanPlayer().MadnessThisRound = 0;
            this.GetAIPlayer().MadnessThisRound = 0;
            this.GetHumanPlayer().CardsPlayedThisRound = 0;
            this.GetAIPlayer().CardsPlayedThisRound = 0;

            GameState.PlayerWithMostMadnessThisRound = null;

            foreach (Card card in this.GetHumanPlayer().GetCardsInHand())
            {
                card.DoubleScore = false;
            }

            foreach (Card card in this.GetAIPlayer().GetCardsInHand())
            {
                card.DoubleScore = false;
            }

            ResetDreamlandsSuit();
        }

        private void ResolveMadnessBothPlayers(Player humanPlayer, AIPlayer aiPlayer)
        {
            ResolveMadnessOnePlayer(humanPlayer);
            ResolveMadnessOnePlayer(aiPlayer);
            if (humanPlayer.MadnessThisRound > aiPlayer.MadnessThisRound)
            {
                GameState.PlayerWithMostMadnessThisRound = humanPlayer;
                GameState.AppendToGameLog($"{humanPlayer.Name} had the highest Madness total this round, and gets to select a bonus.");
                MadnessOptions = ListOptionGenerator.GenerateMadnessListOptions(GameState.HumanPlayer); //There is DEFINITELY a better way and place for this
            }
            else if(aiPlayer.MadnessThisRound > humanPlayer.MadnessThisRound)
            {
                GameState.PlayerWithMostMadnessThisRound = aiPlayer;
                GameState.AppendToGameLog($"{aiPlayer.Name} had the highest Madness total this round, and gets to select a bonus.");
            }
            else
            {
                GameState.PlayerWithMostMadnessThisRound = null;
                GameState.AppendToGameLog("Both players had the same Madness total.");
            }
        }

        private void ResolveMadnessOnePlayer(Player player)
        {
            foreach (Card card in player.GetCardsInPlay())
            {
                if (card.HasMadness)
                {
                    player.MadnessTotal++;

                }
            }
            GameState.AppendToGameLog($"{player.Name} gained {player.MadnessThisRound} Madess this round.");
            GameState.AppendToGameLog($"{player.Name} had a Madness total of {player.MadnessThisRound}.");
        }

        private void ApplyMadnessBonus(Player player, MadnessBonus selectedBonus)
        {
            if (selectedBonus.Equals(MadnessBonus.GainPoints))
            {
                player.Score = player.Score + 4;
                GameState.AppendToGameLog($"{player.Name} chose to score an additional four points.");
            }
            else if(selectedBonus.Equals(MadnessBonus.RemoveMadness))
            {
                if(player.MadnessTotal <= 0)
                {
                    GameState.AppendToGameLog("ERROR: Player should not have been able to remove madness if the total was 0");
                }
                else
                {
                    player.MadnessTotal--;
                    GameState.AppendToGameLog($"{player.Name} chose to heal 1 Madness.");
                }
            }
        }

        private void ResetDreamlandsSuit()
        {
            ResetDreamlandsSuit(GetHumanPlayer().GetCardsInHand());
            ResetDreamlandsSuit(GetHumanPlayer().GetCardsInPlay());
            ResetDreamlandsSuit(GetAIPlayer().GetCardsInHand());
            ResetDreamlandsSuit(GetAIPlayer().GetCardsInPlay());
        }

        private void ResetDreamlandsSuit(List<Card> cards)
        {
            foreach(Card card in cards)
            {
                if(card.CardNameEnum == CardNames.Dreamlands)
                {
                    card.Suit = Suits.None;
                }
            }
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
            GameState.RequirePlayerInput = false;
            do
            {
                switch (GameState.CurrentGameState)
                {
                    case GameStates.PlayCards:
                        Card playerCard = input.SelectedCards[0];
                        Card aiCard = this.GetAIPlayer().ChooseCardToPlay();
                        EachPlayerPlaysOneCard(this.GetHumanPlayer(), this.GetAIPlayer(), playerCard, aiCard);
                        SwapPlayerHands(this.GetHumanPlayer(), this.GetAIPlayer());
                        break;
                    case GameStates.ResolveMadness:
                        ResolveMadnessBothPlayers(this.GetHumanPlayer(), this.GetAIPlayer());
                        break;
                    case GameStates.ResolveMadnessBonus:
                        if (GameState.PlayerWithMostMadnessThisRound == this.GetHumanPlayer())
                        {
                            ApplyMadnessBonus(this.GetHumanPlayer(), input.SelectedBonus);
                        }
                        else if (GameState.PlayerWithMostMadnessThisRound == this.GetAIPlayer())
                        {
                            ApplyMadnessBonus(this.GetAIPlayer(), this.GetAIPlayer().ChooseMadnessBonus());
                        }
                        break;
                    case GameStates.SetDreamlands:
                        if (this.GetHumanPlayer().CheckForSpecificCard(CardNames.Dreamlands))
                        {
                            SetDreamlandsSuit(this.GetHumanPlayer(), input.SelectedSuit);
                        }
                        else if (this.GetAIPlayer().CheckForSpecificCard(CardNames.Dreamlands))
                        {
                            SetDreamlandsSuit(this.GetAIPlayer(), this.GetAIPlayer().ChooseDreamlandsSuit(this.SuitOptions));
                        }
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
