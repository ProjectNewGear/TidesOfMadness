using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                        break;
                    }
                case GameStates.Scoring:
                    {
                        if (GameState.HumanPlayer.MadnessTotal >= 9 || GameState.AIPlayer.MadnessTotal >= 9 || GameState.CurrentRound >= 3)
                        {
                            GameState.CurrentGameState = GameStates.GameOver;
                        }
                        else
                        {
                            GameState.CurrentGameState = GameStates.PickUpCards;
                            GameState.RequirePlayerInput = true;
                        }
                        break;
                    }
                case GameStates.PickUpCards:
                    {
                        GameState.CurrentGameState = GameStates.ChooseCardToReplay;
                        GameState.RequirePlayerInput = true;
                        break;
                    }
                case GameStates.ChooseCardToReplay:
                    {
                        GameState.CurrentGameState = GameStates.ChooseCardToDiscard;
                        GameState.RequirePlayerInput = true;
                        break;
                    }
                case GameStates.ChooseCardToDiscard:
                    {
                        GameState.CurrentGameState = GameStates.PlayCards;
                        GameState.RequirePlayerInput = true;
                        break;
                    }
                case GameStates.GameOver:
                    {
                        GameState.RequirePlayerInput = true;
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
            GameState.DiscardPile = new CardCollection();
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

        private void EachPlayerDiscardsOneCard(Player humanPlayer, AIPlayer aiPlayer, Card playerCard, Card aiCard)
        {
            humanPlayer.CardsInHand.MoveCardToAnotherCollection(playerCard, GameState.DiscardPile);
            aiPlayer.CardsInHand.MoveCardToAnotherCollection(aiCard, GameState.DiscardPile);
            GameState.AppendToGameLog($"{humanPlayer.Name} discards {playerCard.CardNameDisplay} from hand");
            GameState.AppendToGameLog($"{aiPlayer.Name} discards {aiCard.CardNameDisplay} from hand");
        }

        private Player SeeWhoHasDreamlands(Player humanPlayer, AIPlayer aiPlayer)
        {
            if (humanPlayer.CheckForSpecificCardInPlay(CardNames.Dreamlands))
            {
                GameState.AppendToGameLog($"{humanPlayer.Name} has Dreamlands in play.");
                return humanPlayer;
            }
            else if (aiPlayer.CheckForSpecificCardInPlay(CardNames.Dreamlands))
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

        private void ResetDreamlandsSuit(BindingList<Card> cards)
        {
            foreach(Card card in cards)
            {
                if(card.CardNameEnum == CardNames.Dreamlands)
                {
                    card.Suit = Suits.None;
                }
            }
        }

        private void ScoreRound(Player firstPlayer, Player secondPlayer)
        {
            firstPlayer.Score += ScoreCalculator.CalculateScore(firstPlayer, secondPlayer);
            secondPlayer.Score += ScoreCalculator.CalculateScore(secondPlayer, firstPlayer);
        }

        private void CheckGameOverCondition(Player firstPlayer, Player secondPlayer)
        {
            if(firstPlayer.MadnessTotal >= 9 && secondPlayer.MadnessTotal >= 9)
            {
                GameState.AppendToGameLog("GAME OVER - Both players have 9 Madness tokens!");
                return;
            }

            if(firstPlayer.MadnessTotal >= 9)
            {
                GameState.AppendToGameLog("GAME OVER - Human has 9 Madness tokens; AI wins!");
                return;
            }

            if(secondPlayer.MadnessTotal >= 9)
            {
                GameState.AppendToGameLog("GAME OVER - AI has 9 Madness tokens; Human wins!");
                return;
            }

            if(firstPlayer.Score == secondPlayer.Score)
            {
                GameState.AppendToGameLog($"GAME OVER - Players tie with {firstPlayer.Score} points!");
                return;
            }

            if (firstPlayer.Score > secondPlayer.Score)
            {
                GameState.AppendToGameLog($"GAME OVER - Human wins with a score of {firstPlayer.Score} points!");
                return;
            }

            if (secondPlayer.Score > firstPlayer.Score)
            {
                GameState.AppendToGameLog($"GAME OVER - AI wins with a score of {secondPlayer.Score} points!");
                return;
            }
        }

        private void PickUpCards(Player player, BindingList<Card> selectedCards)
        {
            foreach (Card card in selectedCards)
            {
                player.ReturnCardToHand(card);
            }
        }

        private void SwapPlayerHands(Player firstPlayer, Player secondPlayer)
        {
            BindingList<Card> temp1 = new BindingList<Card>();
            BindingList<Card> temp2 = new BindingList<Card>();

            foreach (Card card in firstPlayer.GetCardsInHand())
            {
                temp1.Add(card);
            }

            foreach (Card card in secondPlayer.GetCardsInHand())
            {
                temp2.Add(card);
            }

            firstPlayer.CardsInHand.CardsInCollection.Clear();
            secondPlayer.CardsInHand.CardsInCollection.Clear();

            foreach (Card card in temp1)
            {
                secondPlayer.CardsInHand.AddCardToCollection(card);
            }

            foreach (Card card in temp2)
            {
                firstPlayer.CardsInHand.AddCardToCollection(card);
            }
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
                        if (this.GetHumanPlayer().CheckForSpecificCardInPlay(CardNames.Dreamlands))
                        {
                            SetDreamlandsSuit(this.GetHumanPlayer(), input.SelectedSuit);
                        }
                        else if (this.GetAIPlayer().CheckForSpecificCardInPlay(CardNames.Dreamlands))
                        {
                            SetDreamlandsSuit(this.GetAIPlayer(), this.GetAIPlayer().ChooseDreamlandsSuit(this.SuitOptions));
                        }
                        break;
                    case GameStates.Scoring:
                        {
                            ScoreRound(this.GetHumanPlayer(), this.GetAIPlayer());
                        }
                        break;
                    case GameStates.PickUpCards:
                        {
                            PickUpCards(this.GetHumanPlayer(), input.SelectedCards);
                            this.GetAIPlayer().ReturnCardsToHand();
                        }
                        break;
                    case GameStates.ChooseCardToReplay:
                        playerCard = input.SelectedCards[0];
                        aiCard = this.GetAIPlayer().ChooseCardToPlay();
                        EachPlayerPlaysOneCard(this.GetHumanPlayer(), this.GetAIPlayer(), playerCard, aiCard);
                        break;
                    case GameStates.ChooseCardToDiscard:
                        playerCard = input.SelectedCards[0];
                        aiCard = this.GetAIPlayer().ChooseCardToPlay();
                        EachPlayerDiscardsOneCard(this.GetHumanPlayer(), this.GetAIPlayer(), playerCard, aiCard);
                        GameState.CurrentRound++;
                        SetUpRound();
                        break;
                    case GameStates.GameOver:
                        CheckGameOverCondition(this.GetHumanPlayer(), this.GetAIPlayer());
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
