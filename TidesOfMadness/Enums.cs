using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public enum Suits
    {
        Races,
        Locations,
        OuterGods,
        GreaterOldOnes,
        Manuscripts,
        None
    }

    public enum CardNames
    {
        Azathoth,
        Cthulhu,
        Dagon,
        Deep_Ones,
        Dreamlands,
        Elder_Things,
        Great_Race_Of_Yith,
        Hastur,
        Innsmouth,
        Miskatonic_University,
        Mountains_Of_Madness,
        Necronomicon,
        Nyarlathotep,
        Pnakotic_Manuscripts,
        Rlyeh,
        Shub_Niggurath,
        Unaussprechlichen_Kulten,
        Yog_Sothoth
    }

    public enum ScoreConditions
    {
        ScoreOneMajority,
        ScoreEachMajority,
        ScoreBySet,
        ScoreMissingSuits,
        ScoreByMadness,
        NoScore
    }

    public enum MadnessBonus
    {
        GainPoints,
        RemoveMadness
    }

    public enum GameStates
    {
        Setup,
        PlayCards,
        SetDreamlands,
        ResolveMadness,
        Scoring,
        PickUpCards,
        ChooseCardToReplay,
        ChooseCardToDiscard
    }
}
