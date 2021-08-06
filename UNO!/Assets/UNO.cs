using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class UNO : MonoBehaviour {

   public KMBombInfo Bomb;
   public KMAudio Audio;

   List<string> CardDistribution = new List<string> { "R0", "R1", "R1", "R2", "R2", "R3", "R3", "R4", "R5", "R5", "R6", "R6", "R7", "R7", "R8", "R8", "R9", "R9", "R+2", "R+2", "RS", "RS", "RR", "RR", "G0", "G1", "G1", "G2", "G2", "G3", "G3", "G4", "G5", "G5", "G6", "G6", "G7", "G7", "G8", "G8", "G9", "G9", "G+2", "G+2", "GS", "GS", "GR", "GR", "Y0", "Y1", "Y1", "Y2", "Y2", "Y3", "Y3", "Y4", "Y5", "Y5", "Y6", "Y6", "Y7", "Y7", "Y8", "Y8", "Y9", "Y9", "Y+2", "Y+2", "YS", "YS", "YR", "YR", "B0", "B1", "B1", "B2", "B2", "B3", "B3", "B4", "B5", "B5", "B6", "B6", "B7", "B7", "B8", "B8", "B9", "B9", "B+2", "B+2", "BS", "BS", "BR", "BR", "+", "+", "+", "+", "W", "W", "W", "W" };

   string InitialCard = "";
   List<string> Deck = new List<string> { };

   static int moduleIdCounter = 1;
   int moduleId;
   private bool moduleSolved;

   void Awake () {
      moduleId = moduleIdCounter++;
      /*
      foreach (KMSelectable object in keypad) {
          object.OnInteract += delegate () { keypadPress(object); return false; };
      }
      */

      //button.OnInteract += delegate () { buttonPress(); return false; };

   }

   void Start () {
      CardDistribution.Shuffle();
      int Temp = 0;
      while (CardDistribution[Temp][0] == 'W' || CardDistribution[Temp][0] == '+') {
         Temp++;
      }
      InitialCard = CardDistribution[Temp];
      CardDistribution.RemoveAt(Temp);
   }

   void Update () {

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
