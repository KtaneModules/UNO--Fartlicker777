using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

/*
 * Hey blan when you read this the following sounds should happen that I already haven't done.
 * WrongSound should be played whenever you play a card that isn't correct.
 * YOUHAVEUNO should be played whenever you play the second to last card without pressing the UNO button.
 * IDONTHAVEUNO should be played when you play the second to last card without pressing the UNO button 2+ times.
 * UNO should be played when the second to last card has been played.
 */

public class UNO : MonoBehaviour {

   public KMBombInfo Bomb;
   public KMAudio Audio;

   public KMSelectable[] Buttons;

   List<string> InitialCardDistribution = new List<string> { "R0", "R1", "R1", "R2", "R2", "R3", "R3", "R4", "R5", "R5", "R6", "R6", "R7", "R7", "R8", "R8", "R9", "R9", "R+2", "R+2", "RS", "RS", "RR", "RR", "G0", "G1", "G1", "G2", "G2", "G3", "G3", "G4", "G5", "G5", "G6", "G6", "G7", "G7", "G8", "G8", "G9", "G9", "G+2", "G+2", "GS", "GS", "GR", "GR", "Y0", "Y1", "Y1", "Y2", "Y2", "Y3", "Y3", "Y4", "Y5", "Y5", "Y6", "Y6", "Y7", "Y7", "Y8", "Y8", "Y9", "Y9", "Y+2", "Y+2", "YS", "YS", "YR", "YR", "B0", "B1", "B1", "B2", "B2", "B3", "B3", "B4", "B5", "B5", "B6", "B6", "B7", "B7", "B8", "B8", "B9", "B9", "B+2", "B+2", "BS", "BS", "BR", "BR", "K*4", "K*4", "K*4", "K*4", "KW", "KW", "KW", "KW" };
   //Top is used for a reset.
   List<string> CardDistribution = new List<string> { "R0", "R1", "R1", "R2", "R2", "R3", "R3", "R4", "R5", "R5", "R6", "R6", "R7", "R7", "R8", "R8", "R9", "R9", "R+2", "R+2", "RS", "RS", "RR", "RR", "G0", "G1", "G1", "G2", "G2", "G3", "G3", "G4", "G5", "G5", "G6", "G6", "G7", "G7", "G8", "G8", "G9", "G9", "G+2", "G+2", "GS", "GS", "GR", "GR", "Y0", "Y1", "Y1", "Y2", "Y2", "Y3", "Y3", "Y4", "Y5", "Y5", "Y6", "Y6", "Y7", "Y7", "Y8", "Y8", "Y9", "Y9", "Y+2", "Y+2", "YS", "YS", "YR", "YR", "B0", "B1", "B1", "B2", "B2", "B3", "B3", "B4", "B5", "B5", "B6", "B6", "B7", "B7", "B8", "B8", "B9", "B9", "B+2", "B+2", "BS", "BS", "BR", "BR", "K*4", "K*4", "K*4", "K*4", "KW", "KW", "KW", "KW" };
   //This one gets shuffled.

   string InitialCard = "";
   string LastCard = "";
   List<string> Deck = new List<string> { };

   bool FollowColor;

   static int moduleIdCounter = 1;
   int moduleId;
   private bool moduleSolved;

   void Awake () {
      moduleId = moduleIdCounter++;

      foreach (KMSelectable Button in Buttons) {
         Button.OnInteract += delegate () { CardFlip(Button); return false; };
         Button.OnHighlight += delegate () { CardHover(Button); };
      }


      GetComponent<KMBombModule>().OnActivate += Activate;
   }

   void CardFlip (KMSelectable Button) {
      Audio.PlaySoundAtTransform("PlaceSound", Button.transform);
      //Debug.Log("L");
   }

   void CardHover (KMSelectable Button) {
      Audio.PlaySoundAtTransform("Hover", Button.transform);
      //Debug.Log("K");
   }

   void Activate () {
      Audio.PlaySoundAtTransform("StartNoiseFade", transform);
   }

   void Start () {
      GenerateCards();
   }

   void GenerateCards () {

   }

#pragma warning disable 414
   private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
   }

   IEnumerator TwitchHandleForcedSolve () {
      yield return null;
   }
}
