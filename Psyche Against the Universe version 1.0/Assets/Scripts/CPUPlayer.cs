using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the product for the CPU cuilder pattern. 
////This class acts as the model in the MVC architecture for all CPU players
//  Even if multiple CPU players exist, they act as one MVC structure
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 * 10/31/25 - refinements to the initial builder pattern. removed generalization and added
 * associations and dependency relations.
 * 11/5/25 - Added player hand object to hold answer card objects.
 * 11/14/25 - changed the personality field to an array
 */
public class CPUPlayer : IPlayerCommon
{
    //public string Name { get;  set; }
    public string[] Personality { get; set; } = new string[4];

    public string Avatar_Name { get; set; }

    public int score { get ; set ; }
    
    //Basic list structure to hold the CPU players answer cards.
    public List<ScriptableCard> Hand { get ; set ; } = new List<ScriptableCard>();
   // public GameLoop gL;
    public bool judge { get; set; }

    public PersonalityParse[] PersonalityPriority; // parsed enums

    /*
* This is a general debug method for this class, however it can be 
* adapted for extensibility.
*/
    public override string ToString()
    {
        //return $"Name: {Avatar_Name}, Personality: {Personality}";
        return $"Name: {Avatar_Name}, Personality: {string.Join(",", Personality)}";

    }
    //public AnswerCard RunStrategy(string[] personality, IReadOnlyList<AnswerCard> playedCards)
    public ScriptableCard RunStrategy(string[] personality, IReadOnlyList<ScriptableCard> playedCards)

    {
        Debug.Log("Judging based on: " + string.Join(",", personality));

        // Convert string[] personality into PersonalityParse[]
        PersonalityPriority = personality
            .Select(p => PersonalityParseextention.FromString(p))
            .ToArray();

        // Pick the first bias in the priority list (you can extend this later)
        PersonalityParse judgeBias = PersonalityPriority.FirstOrDefault();

        // Use StrategyCommon to judge the best card
        //AnswerCard winningCard = StrategyCommon.JudgeBest(playedCards, judgeBias);
        // Use StrategyCommon to judge the best card
        ScriptableCard winningCard = StrategyCommon.JudgeBest(playedCards, judgeBias);

        // if (winningCard != null)
        if (winningCard != null)
        {
            Debug.Log($"CPU {Avatar_Name} chose winning card: {winningCard.CardTitle}");
        }
        else
        {
            Debug.LogWarning("No winning card found.");
        }

        return winningCard;


    }

    public void DrawCard()
    {
        
    }
    /// <summary>
    /// No longer required in current context
    /// </summary>
    public void PlayCard()
    {
       /* if (Hand.Count > 0)
        {
            Debug.Log("Card Played " + Hand[0].title + "Persona " + Hand[0].personality + "weight " + Hand[0].weight);

            //add to the played cards list in the gameloop
            gL.PlayedCards.Add(Hand[0]);

            Hand.RemoveAt(0); //top card is sufficent
        }*/
    }

    public bool isJudge()
    {
        return this.judge;
    }

    public void PlayCard(GameLoop gameLoop)
    {
        if (Hand.Count > 0)
        {
            
            //Use CPU logic to determine which card to play from its hand based on dominant personality
            string DominantPersona = Personality[0];
            PersonalityParse persona = PersonalityParseextention.FromString(DominantPersona);

            PersonalityPriority = Personality.Select(p => PersonalityParseextention.FromString(p)).ToArray();

            //use the strategy logic to pick the best cards

            //AnswerCard choice = StrategyCommon.PickBestByPersonality(Hand, persona);
            ScriptableCard choice = StrategyCommon.PickBestByPersonality(Hand, persona);

            //other choices based on personality matrix
            if (choice == null)
            {
               choice = StrategyCommon.PickBestAcrossPriority(Hand, PersonalityPriority);
                Debug.Log("Choosing across priority");
            }

            //should not be needed, but a good catch all
            if (choice == null)
            {
                choice = Hand[0];
                Debug.Log("Default");
            }

            var data = choice;
            // Debug: determine strongest trait
            int maxWeight = Mathf.Max(
                data.CardSerious,
                data.CardScifi,
                data.CardFunny,
                data.CardChaos
            );

            //following is for logic testing and debug*************************************************************************
            //int maxWeight = Mathf.Max(choice.WeightSerious, choice.WeightSciFi, choice.WeightFunny, choice.WeightChaotic);

            string strongestTrait = "Unknown";
            if (maxWeight == data.CardSerious) strongestTrait = "Serious";
            else if (maxWeight == data.CardScifi) strongestTrait = "SciFi";
            else if (maxWeight == data.CardFunny) strongestTrait = "Funny";
            else if (maxWeight == data.CardChaos) strongestTrait = "Chaotic";

            //if (maxWeight == choice.WeightSerious) strongestTrait = "Serious";
            // else if (maxWeight == choice.WeightSciFi) strongestTrait = "SciFi";
            //else if (maxWeight == choice.WeightFunny) strongestTrait = "Funny";
            //else if (maxWeight == choice.WeightChaotic) strongestTrait = "Chaotic";

            Debug.Log(
                $"Card Played {data.CardTitle} " +
                $"serious {data.CardSerious} " +
                $"sci {data.CardScifi} " +
                $"fun {data.CardFunny} " +
                $"chao {data.CardChaos} " +
                $"--> strongest: {strongestTrait} ({maxWeight})"

           // Debug.Log(
                //$"Card Played {choice.title} " +
                //$"serious {choice.WeightSerious} " +
                //$"sci {choice.WeightSciFi} " +
                //$"fun {choice.WeightFunny} " +
                //$"chao {choice.WeightChaotic} " +
                //$"--> strongest: {strongestTrait} ({maxWeight})"
            );
            //**********************************************************************************************************************

            gameLoop.TestConsoleLog("Card Played " + choice.CardTitle + " serious  " + choice.CardSerious + " Sci  " +choice.CardScifi + " fun  " + choice.CardFunny + 
             " chao  " + choice.CardChaos);

            //choice.PlayedBy = this.Avatar_Name;  //this way we know who played the card
            var uiCard = CardSpawner.Instance.Spawn(choice);
            uiCard.PlayedBy = this.Avatar_Name;

            //add to the played cards list in the gameloop
            //gameLoop.RegisterPlayedCard(choice);
            gameLoop.RegisterPlayedCard(uiCard);
            Hand.Remove(choice); //top card is sufficent
        }
    }

    public void PLayCard(GameLoop gameLoop, int Index)
    {
        throw new NotImplementedException();
    }
    //Add additional game play methods below for judge and card play selection
    // such as judge()
    // playcard()
    //banter is controlled by the CPU view which interacts with this object and pulls
    //  from the applicable banter file
}
