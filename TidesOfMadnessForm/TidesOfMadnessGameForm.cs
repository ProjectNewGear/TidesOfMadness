using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TidesOfMadness;

namespace TidesOfMadnessForm
{
    public partial class TidesOfMadnessForm : Form
    {
        GameDriver driver;

        public TidesOfMadnessForm()
        {
            InitializeComponent();
        }

        private void TidesOfMadnessForm_Load(object sender, EventArgs e)
        {
            driver = new GameDriver();
            driver.InitializeGame();
            UpdateGameLog(driver.GetGameLog());
            UpdateScoreDisplays();

            pbxCardImage.SizeMode = PictureBoxSizeMode.StretchImage;
            lbxHumanHand.DataSource = driver.GetHumanPlayer().GetCardsInHand();
            lbxHumanHand.DisplayMember = "CardNameDisplay";

            lbxHumanInPlay.DataSource = driver.GetHumanPlayer().GetCardsInPlay();
            lbxHumanInPlay.DisplayMember = "CardNameDisplay";

            lbxOppInPlay.DataSource = driver.GetAIPlayer().GetCardsInPlay();
            lbxOppInPlay.DisplayMember = "CardNameDisplay";

            pbxCardImage.ImageLocation = SetArtFromListBox(lbxHumanHand, 0);

            UpdatePlayerInstructions(driver.GetCurrentGameState(), lblPlayerInstructions);
            UpdateUISettings(driver.GetCurrentGameState());
        }

        private void UpdatePlayerInstructions(GameStates currentState, Label playerInstructions)
        {
            string returnText = string.Empty;
            switch (currentState)
            {
                case GameStates.Setup:
                    {
                        returnText = "Something went wrong...";
                        break;
                    }
                case GameStates.PlayCards:
                    {
                        returnText = "Select a card in hand to play.";
                        break;
                    }
                case GameStates.ResolveMadnessBonus:
                    {
                        returnText = "You have the highest Madness total this round." + Environment.NewLine + "Would you like to remove 1 Madness token or gain 4 points?";
                        break;
                    }
                case GameStates.SetDreamlands:
                    {
                        returnText = "Pick a suit for Dreamlands.";
                        break;
                    }    
                case GameStates.Scoring:
                    {
                        returnText = "Something went wrong; we shouldn't be asking for input during Scoring";
                        break;
                    }
                case GameStates.PickUpCards:
                    {
                        returnText = "Choose 5 cards to return to your hand.";
                        break;
                    }
                case GameStates.ChooseCardToReplay:
                    {
                        returnText = "Choose a card to immediately replay.";
                        break;
                    }
                case GameStates.ChooseCardToDiscard:
                    {
                        returnText = "Choose a card to discard.";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            playerInstructions.Text = returnText;
        }

        private void UpdateUISettings(GameStates currentState)
        {
            switch (currentState)
            {
                case GameStates.Setup:
                    {
                        break;
                    }
                case GameStates.PlayCards:
                    {
                        cbxPlayerChoice.Visible = false;
                        lbxHumanInPlay.ClearSelected();
                        lbxHumanInPlay.SelectionMode = SelectionMode.One;
                        break;
                    }
                case GameStates.ResolveMadnessBonus:
                    {
                        cbxPlayerChoice.Visible = true;
                        cbxPlayerChoice.SelectedItem = null;
                        cbxPlayerChoice.Items.Clear();
                        foreach (ResolveMadnessOption option in driver.MadnessOptions)
                        {
                            cbxPlayerChoice.Items.Add(option);
                        }
                        cbxPlayerChoice.DisplayMember = "Text";
                        lbxHumanInPlay.ClearSelected();
                        lbxHumanInPlay.SelectionMode = SelectionMode.One;
                        break;
                    }
                case GameStates.SetDreamlands:
                    {
                        cbxPlayerChoice.Visible = true;
                        cbxPlayerChoice.SelectedItem = null;
                        cbxPlayerChoice.Items.Clear();
                        foreach (SuitOption option in driver.SuitOptions)
                        {
                            cbxPlayerChoice.Items.Add(option);
                        }
                        cbxPlayerChoice.DisplayMember = "Text";
                        lbxHumanInPlay.ClearSelected();
                        lbxHumanInPlay.SelectionMode = SelectionMode.One;
                        break;
                    }
                case GameStates.Scoring:
                    {
                        //THIS SHOULD BE TEMPORARY
                        cbxPlayerChoice.Visible = false;
                        lbxHumanInPlay.ClearSelected();
                        break;
                    }
                case GameStates.PickUpCards:
                    {
                        cbxPlayerChoice.Visible = false;
                        lbxHumanInPlay.ClearSelected();
                        lbxHumanInPlay.SelectionMode = SelectionMode.MultiSimple;
                        break;
                    }
                case GameStates.ChooseCardToReplay:
                    {
                        cbxPlayerChoice.Visible = false;
                        lbxHumanInPlay.ClearSelected();
                        lbxHumanInPlay.SelectionMode = SelectionMode.One;
                        break;
                    }
                case GameStates.ChooseCardToDiscard:
                    {
                        cbxPlayerChoice.Visible = false;
                        lbxHumanInPlay.ClearSelected();
                        lbxHumanInPlay.SelectionMode = SelectionMode.One;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            _tempGameStateLabel.Text = $"Gamestate = {currentState.ToString()}";
        }

        private void UpdateScoreDisplays()
        {
            lblOppPointsTotal.Text = $"Points Total: {driver.GetAIPlayer().Score}";
            lblOppMadnessTotal.Text = $"Madness Total: {driver.GetAIPlayer().MadnessTotal}";
            lblOppMadnessThisRound.Text = $"Madness This Round: {driver.GetAIPlayer().MadnessThisRound}";

            lblPlayerPointsTotal.Text = $"Points Total: {driver.GetHumanPlayer().Score}";
            lblPlayerMadnessTotal.Text = $"Madness Total: {driver.GetHumanPlayer().MadnessTotal}";
            lblPlayerMadnessThisRound.Text = $"Madness This Round: {driver.GetHumanPlayer().MadnessThisRound}";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            switch (driver.GetCurrentGameState())
            {
                case GameStates.Setup:
                    {
                        UpdateScoreDisplays();
                        break;
                    }
                case GameStates.PlayCards:
                case GameStates.ChooseCardToReplay:
                case GameStates.ChooseCardToDiscard:
                    {
                        Card selectedCard = (Card)lbxHumanHand.SelectedItem;

                        if(selectedCard != null)
                        {
                            PlayerInput input = new PlayerInput();
                            input.SelectedCards.Add(selectedCard);
                            driver.ActOnPlayerInput(input);
                            UpdateUISettings(driver.GetCurrentGameState());
                            UpdateGameLog(driver.GetGameLog());
                        }
                        break;
                    }
                case GameStates.ResolveMadnessBonus:
                    {
                        PlayerInput input = new PlayerInput();
                        ResolveMadnessOption chosenOption = (ResolveMadnessOption)cbxPlayerChoice.SelectedItem;
                        input.SelectedBonus = chosenOption.Bonus;
                        driver.ActOnPlayerInput(input);
                        UpdateUISettings(driver.GetCurrentGameState());
                        UpdateGameLog(driver.GetGameLog());
                        break;
                    }
                case GameStates.SetDreamlands:
                    {
                        PlayerInput input = new PlayerInput();
                        input.SelectedSuit = (SuitOption)cbxPlayerChoice.SelectedItem;
                        driver.ActOnPlayerInput(input);
                        UpdateUISettings(driver.GetCurrentGameState());
                        UpdateGameLog(driver.GetGameLog());
                        break;
                    }
                case GameStates.Scoring:
                    {
                        break;
                    }
                case GameStates.PickUpCards:
                    {
                        BindingList<Card> selectedCards = new BindingList<Card>();

                        foreach(Card card in lbxHumanInPlay.SelectedItems)
                        {
                            selectedCards.Add(card);
                        }

                        if (selectedCards.Count == 5)
                        {
                            PlayerInput input = new PlayerInput();
                            input.SelectedCards = selectedCards;
                            driver.ActOnPlayerInput(input);
                            UpdateUISettings(driver.GetCurrentGameState());
                            UpdateGameLog(driver.GetGameLog());
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            UpdatePlayerInstructions(driver.GetCurrentGameState(), lblPlayerInstructions);
            UpdateScoreDisplays();
        }

        private void UpdateGameLog(string result)
        {
            txtGameLog.Text = result;
        }

        private void lbxHumanHand_SelectedIndexChanged(object sender, EventArgs e)
        {
            pbxCardImage.ImageLocation = SetArtFromListBox(lbxHumanHand, lbxHumanHand.SelectedIndex);
        }

        private string SetArtFromListBox(ListBox box, int indexNum)
        {
            if (indexNum >= 0 && indexNum <= box.Items.Count)
            {
                Card currentCard = (Card)box.Items[indexNum];
                return currentCard.Art;
            }
            return null;    //TO DO - how can I get below 0???
        }

        private void lbxHumanInPlay_SelectedIndexChanged(object sender, EventArgs e)
        {
            pbxCardImage.ImageLocation = SetArtFromListBox(lbxHumanInPlay, lbxHumanInPlay.SelectedIndex);
        }

        private void lbxOppInPlay_SelectedIndexChanged(object sender, EventArgs e)
        {
            pbxCardImage.ImageLocation = SetArtFromListBox(lbxOppInPlay, lbxOppInPlay.SelectedIndex);
        }
    }
}
