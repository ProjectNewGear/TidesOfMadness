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
        GameDriver Driver;

        public TidesOfMadnessForm()
        {
            InitializeComponent();
        }

        private void TidesOfMadnessForm_Load(object sender, EventArgs e)
        {
            Driver = new GameDriver();
            Driver.InitializeGame();
            UpdateGameLog(Driver.GetGameLog());

            pbxCardImage.SizeMode = PictureBoxSizeMode.StretchImage;
            lbxHumanHand.DisplayMember = "CardNameDisplay";
            lbxHumanInPlay.DisplayMember = "CardNameDisplay";
            lbxOppInPlay.DisplayMember = "CardNameDisplay";

            

            UpdateCardDisplays();
            pbxCardImage.ImageLocation = SetArtFromListBox(lbxHumanHand, 0);

            UpdatePlayerInstructions(Driver.GetCurrentGameState(), lblPlayerInstructions);
            UpdateUISettings(Driver.GetCurrentGameState());
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
                case GameStates.SetDreamlands:
                    {
                        returnText = "Pick a suit for Dreamlands.";
                        break;
                    }
                case GameStates.ResolveMadness:
                    {
                        returnText = "You have the highest Madness total this round." + Environment.NewLine + "Would you like to remove 1 Madness token or gain 4 points?";
                        break;
                    }
                case GameStates.Scoring:
                    {
                        returnText = "Something went wrong...";
                        break;
                    }
                case GameStates.PickUpCards:
                    {
                        returnText = "Choose 5 cards to return to your hand.";
                        break;
                    }
                case GameStates.ChooseCardToReplay:
                    {
                        returnText = "Choose a card to replay immediately.";
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
                case GameStates.SetDreamlands:
                    {
                        cbxPlayerChoice.Visible = true;
                        UpdateDropdownWithSuits();
                        lbxHumanInPlay.ClearSelected();
                        lbxHumanInPlay.SelectionMode = SelectionMode.One;
                        break;
                    }
                case GameStates.ResolveMadness:
                    {
                        cbxPlayerChoice.Visible = true;
                        cbxPlayerChoice.Items.Clear();
                        foreach(ResolveMadnessOption option in Driver.MadnessOptions)
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

        private void UpdateCardDisplays()
        {
            lbxHumanHand.Items.Clear();

            foreach (Card card in Driver.GetHumanPlayer().CardsInHand.CardsInCollection)
            {
                lbxHumanHand.Items.Add(card);
            }

            lbxHumanInPlay.Items.Clear();

            foreach (Card card in Driver.GetHumanPlayer().CardsInPlay.CardsInCollection)
            {
                lbxHumanInPlay.Items.Add(card);
            }

            lbxOppInPlay.Items.Clear();

            foreach (Card card in Driver.GetAIPlayer().CardsInPlay.CardsInCollection)
            {
                lbxOppInPlay.Items.Add(card);
            }
        }

        private void UpdateDropdownWithMadnessOptions()
        {
            cbxPlayerChoice.Items.Clear();
            cbxPlayerChoice.Items.Add(MadnessBonus.GainPoints);
            cbxPlayerChoice.Items.Add(MadnessBonus.RemoveMadness);
        }

        private void UpdateDropdownWithSuits()
        {
            cbxPlayerChoice.Items.Clear();
            cbxPlayerChoice.Items.Add(Suits.GreaterOldOnes);
            cbxPlayerChoice.Items.Add(Suits.Locations);
            cbxPlayerChoice.Items.Add(Suits.Manuscripts);
            cbxPlayerChoice.Items.Add(Suits.OuterGods);
            cbxPlayerChoice.Items.Add(Suits.Races);
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            switch (Driver.GetCurrentGameState())
            {
                case GameStates.Setup:
                    {
                        break;
                    }
                case GameStates.PlayCards:
                    {
                        Card selectedCard = (Card)lbxHumanHand.SelectedItem;

                        if(selectedCard != null)
                        {
                            PlayerInput input = new PlayerInput();
                            input.SelectedCards.Add(selectedCard);
                            Driver.ActOnPlayerInput(input);
                            UpdateCardDisplays();
                            UpdateUISettings(Driver.GetCurrentGameState());
                            UpdateGameLog(Driver.GetGameLog());
                            UpdatePlayerInstructions(Driver.GetCurrentGameState(), lblPlayerInstructions);
                        }
                        break;
                    }
                case GameStates.SetDreamlands:
                    {
                        PlayerInput input = new PlayerInput();
                        input.SelectedSuit = (Suits)cbxPlayerChoice.SelectedItem;
                        Driver.ActOnPlayerInput(input);
                        UpdateCardDisplays();
                        UpdateUISettings(Driver.GetCurrentGameState());
                        UpdateGameLog(Driver.GetGameLog());
                        UpdatePlayerInstructions(Driver.GetCurrentGameState(), lblPlayerInstructions);
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
