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
   public Sprite[] Cards;
   public SpriteRenderer[] Sprites;
   public KMSelectable BigIfSquare;
   public Sprite Bill;

   List<string> InitialCardDistribution = new List<string> { "R0", "R1", "R1", "R2", "R2", "R3", "R3", "R4", "R4", "R5", "R5", "R6", "R6", "R7", "R7", "R8", "R8", "R9", "R9", "R+2", "R+2", "RS", "RS", "RR", "RR", "G0", "G1", "G1", "G2", "G2", "G3", "G3", "G4", "G4", "G5", "G5", "G6", "G6", "G7", "G7", "G8", "G8", "G9", "G9", "G+2", "G+2", "GS", "GS", "GR", "GR", "Y0", "Y1", "Y1", "Y2", "Y2", "Y3", "Y3", "Y4", "Y4", "Y5", "Y5", "Y6", "Y6", "Y7", "Y7", "Y8", "Y8", "Y9", "Y9", "Y+2", "Y+2", "YS", "YS", "YR", "YR", "B0", "B1", "B1", "B2", "B2", "B3", "B3", "B4", "B4", "B5", "B5", "B6", "B6", "B7", "B7", "B8", "B8", "B9", "B9", "B+2", "B+2", "BS", "BS", "BR", "BR", "K*4", "K*4", "K*4", "K*4", "KW", "KW", "KW", "KW" };
   //Top is used for a reset.
   List<string> CardDistribution = new List<string> { "R0", "R1", "R1", "R2", "R2", "R3", "R3", "R4", "R4", "R5", "R5", "R6", "R6", "R7", "R7", "R8", "R8", "R9", "R9", "R+2", "R+2", "RS", "RS", "RR", "RR", "G0", "G1", "G1", "G2", "G2", "G3", "G3", "G4", "G4", "G5", "G5", "G6", "G6", "G7", "G7", "G8", "G8", "G9", "G9", "G+2", "G+2", "GS", "GS", "GR", "GR", "Y0", "Y1", "Y1", "Y2", "Y2", "Y3", "Y3", "Y4", "Y4", "Y5", "Y5", "Y6", "Y6", "Y7", "Y7", "Y8", "Y8", "Y9", "Y9", "Y+2", "Y+2", "YS", "YS", "YR", "YR", "B0", "B1", "B1", "B2", "B2", "B3", "B3", "B4", "B4", "B5", "B5", "B6", "B6", "B7", "B7", "B8", "B8", "B9", "B9", "B+2", "B+2", "BS", "BS", "BR", "BR", "K*4", "K*4", "K*4", "K*4", "KW", "KW", "KW", "KW" };
   //This one gets shuffled.

   List<string> nicerNames = new List<string> { "Red 0", "Red 1", "Red 1", "Red 2", "Red 2", "Red 3", "Red 3", "Red 4", "Red 4", "Red 5", "Red 5", "Red 6", "Red 6", "Red 7", "Red 7", "Red 8", "Red 8", "Red 9", "Red 9", "Red +2", "Red +2", "Red Skip", "Red Skip", "Red Reverse", "Red Reverse", "Green 0", "Green 1", "Green 1", "Green 2", "Green 2", "Green 3", "Green 3", "Green 4", "Green 4", "Green 5", "Green 5", "Green 6", "Green 6", "Green 7", "Green 7", "Green 8", "Green 8", "Green 9", "Green 9", "Green +2", "Green +2", "Green Skip", "Green Skip", "Green Reverse", "Green Reverse", "Yellow 0", "Yellow 1", "Yellow 1", "Yellow 2", "Yellow 2", "Yellow 3", "Yellow 3", "Yellow 4", "Yellow 4", "Yellow 5", "Yellow 5", "Yellow 6", "Yellow 6", "Yellow 7", "Yellow 7", "Yellow 8", "Yellow 8", "Yellow 9", "Yellow 9", "Yellow +2", "Yellow +2", "Yellow Skip", "Yellow Skip", "Yellow Reverse", "Yellow Reverse", "Blue 0", "Blue 1", "Blue 1", "Blue 2", "Blue 2", "Blue 3", "Blue 3", "Blue 4", "Blue 4", "Blue 5", "Blue 5", "Blue 6", "Blue 6", "Blue 7", "Blue 7", "Blue 8", "Blue 8", "Blue 9", "Blue 9", "Blue +2", "Blue +2", "Blue Skip", "Blue Skip", "Blue Reverse", "Blue Reverse", "+4", "+4", "+4", "+4", "Wild", "Wild", "Wild", "Wild" };

   string InitialCard = "";
   string LastCard = "";
   List<string> Deck = new List<string> { };
   List<int> illegalCards = new List<int> { };
   List<string> kekDeck = new List<string> {};
   List<int> played = new List<int> {};

   int pointer = 0;
   string numbers = "0123456789+SR"; //rules changed halfway through, just roll with it...
   bool whichFirst; //false = color, true = number
   string notnumbers = "*W";
   int bullshitCard = 0;
   bool plusFour = false;
   string cardPriorToPlusFour = "";
   int debugPointer = 0;
   bool youGotIntoALoop = false;
   string firstInDeck = "";
   //string goodDeck = "";
   //string heckedDeck = "";
   bool hmmm;
   bool currentRuling; //same as whichFirst basically
   int cardsSubmitted = 0;
   bool hasUnoed = false;
   int failuresToUno = 0;
   string whatYouPlayedLast = "";
   string aaaaaaaaaaaaaaaaaaaa;

   static int moduleIdCounter = 1;
   int moduleId;
   private bool moduleSolved;

   void Awake () {
      moduleId = moduleIdCounter++;

      foreach (KMSelectable Button in Buttons) {
         Button.OnInteract += delegate () { CardFlip(Button); return false; };
         Button.OnHighlight += delegate () { CardHover(Button); };
         //Button.OnHighlightEnded += delegate () { CardDehover(Button); };
      }
      BigIfSquare.OnInteract += delegate () { CommitSquare(); return false; };

      GetComponent<KMBombModule>().OnActivate += Activate;
   }

   void CardFlip (KMSelectable Button) {
      for (int c = 0; c < 7; c++) {
         if (Button == Buttons[c]) {
            Audio.PlaySoundAtTransform("PlaceSound", Button.transform);
            //Debug.Log("L");
            hmmm = WasThisCardValid(c, cardsSubmitted);
            if (hmmm) {
               //do the cool shit later
               cardsSubmitted += 1;
               whatYouPlayedLast = Deck[c];
               Sprites[c+1].sprite = Bill; //Obviously when you make it look better you have to murder Bill
               Sprites[0].sprite = Cards[InitialCardDistribution.IndexOf(whatYouPlayedLast)];
               played.Add(c);
               Debug.LogFormat("[UNO! #{0}] You played a {1}, which is valid.", moduleId, better(whatYouPlayedLast));
               if (cardsSubmitted == 7) {
                  GetComponent<KMBombModule>().HandlePass();
                  moduleSolved = true;
               }
            } else {
               whatYouPlayedLast = Deck[c];
               Debug.LogFormat("[UNO! #{0}] You played a {1}, which is not valid. Strike!", moduleId, better(whatYouPlayedLast));
               cardsSubmitted = 0;
               hasUnoed = false;
               played.Clear();
               GetComponent<KMBombModule>().HandleStrike();
               GenerateCards();
               ShowCards();
            }
         }
      }
   }

   void CardHover (KMSelectable Button) {
      Audio.PlaySoundAtTransform("Hover", Button.transform); //make it so the card goes up a bit when hovered
      //Debug.Log("K");
   }

   void Activate () {
      Audio.PlaySoundAtTransform("StartNoiseFade", transform);
   }

   void Start () {
      GenerateCards();
      ShowCards();
   }

   void GenerateCards () {
      startAllOver:
      Deck.Clear();
      illegalCards.Clear();
      youGotIntoALoop = false;
      CardDistribution.Shuffle();
      if (UnityEngine.Random.Range(0, 7) != 0) { // 1 in 7 of card being bullshit, if it's not, do this

         while (numbers.IndexOf(CardDistribution[pointer][1]) == -1) {
            pointer = (pointer + 1) % InitialCardDistribution.Count();
         }

         Deck.Add(CardDistribution[pointer]);
         illegalCards.Add(pointer);
         pointer = (pointer + 1) % InitialCardDistribution.Count();

         whichFirst = UnityEngine.Random.Range(0, 2) == 0;
         
         for (int m = 0; m < 7; m++) {
            if (m != 0) {
               whichFirst = !whichFirst;
            }
            if (whichFirst) {
               while (CardDistribution[pointer][1] != Deck[m][1] || CardDistribution[pointer] == Deck[m] || notnumbers.Contains(CardDistribution[pointer][1]) || illegalCards.Contains(pointer)) {
                  pointer = (pointer + 1) % InitialCardDistribution.Count();
               }
            } else {
               while (CardDistribution[pointer][0] != Deck[m][0] || CardDistribution[pointer] == Deck[m] || notnumbers.Contains(CardDistribution[pointer][1]) || illegalCards.Contains(pointer)) {
                  pointer = (pointer + 1) % InitialCardDistribution.Count();
               }
            }
            Deck.Add(CardDistribution[pointer]);
            illegalCards.Add(pointer);
            pointer = (pointer + 1) % InitialCardDistribution.Count();
         }

      } else { //if it is bullshit
         bullshitCard = UnityEngine.Random.Range(1,7);

         while (numbers.IndexOf(CardDistribution[pointer][1]) == -1) { //pick first card same as before
            pointer = (pointer + 1) % InitialCardDistribution.Count();
         }
         Deck.Add(CardDistribution[pointer]);
         illegalCards.Add(pointer);
         pointer = (pointer + 1) % InitialCardDistribution.Count();

         whichFirst = bullshitCard % 2 == 0;
         for (int m = 0; m < 7; m++) {
            if (m != 0) {
               whichFirst = !whichFirst;
            }
            if (1+m == bullshitCard) { //if bullshit card
                       //the ^ here and all prior to that in the below line isn't that necessary anymore as colored cards are always not bs but eh
               while (!(CardDistribution[pointer][0] != Deck[m][0] ^ CardDistribution[pointer][0] != 'K') || CardDistribution[pointer] == Deck[m] || numbers.Contains(CardDistribution[pointer][1]) || illegalCards.Contains(pointer)) {
                  pointer = (pointer + 1) % InitialCardDistribution.Count();
               }
               if (CardDistribution[pointer][1] == '*') {
                  plusFour = true;
                  cardPriorToPlusFour = Deck[m];
               }
            } else if (1+m == bullshitCard+1) { //if one after the bullshit card
               while (illegalCards.Contains(pointer)) {
                  pointer = (pointer + 1) % InitialCardDistribution.Count();
               }
            } else { //otherwise just continue as normal
               if (plusFour) { //unless it's plus four :weary:
                  if (whichFirst) {
                     while (!youGotIntoALoop && (CardDistribution[pointer][1] != Deck[m][1] || CardDistribution[pointer] == Deck[m] || notnumbers.Contains(CardDistribution[pointer][1]) || illegalCards.Contains(pointer) || cardPriorToPlusFour[0] == CardDistribution[pointer][0] || cardPriorToPlusFour[1] == CardDistribution[pointer][1])) {
                        pointer = (pointer + 1) % InitialCardDistribution.Count();
                        if (debugPointer == pointer) {
                           youGotIntoALoop = true;
                           pointer = 0;
                        }
                     }
                  } else {
                     while (!youGotIntoALoop && (CardDistribution[pointer][0] != Deck[m][0] || CardDistribution[pointer] == Deck[m] || notnumbers.Contains(CardDistribution[pointer][1]) || illegalCards.Contains(pointer) || cardPriorToPlusFour[0] == CardDistribution[pointer][0] || cardPriorToPlusFour[1] == CardDistribution[pointer][1])) {
                        pointer = (pointer + 1) % InitialCardDistribution.Count();
                        if (debugPointer == pointer) {
                           youGotIntoALoop = true;
                           pointer = 0;
                        }
                     }
                  }
               } else {
                  if (whichFirst) {
                     while (!youGotIntoALoop && (CardDistribution[pointer][1] != Deck[m][1] || CardDistribution[pointer] == Deck[m] || notnumbers.Contains(CardDistribution[pointer][1]) || illegalCards.Contains(pointer))) {
                        pointer = (pointer + 1) % InitialCardDistribution.Count();
                        if (debugPointer == pointer) {
                           youGotIntoALoop = true;
                           pointer = 0;
                        }
                     }
                  } else {
                     while (!youGotIntoALoop && (CardDistribution[pointer][0] != Deck[m][0] || CardDistribution[pointer] == Deck[m] || notnumbers.Contains(CardDistribution[pointer][1]) || illegalCards.Contains(pointer))) {
                        pointer = (pointer + 1) % InitialCardDistribution.Count();
                        if (debugPointer == pointer) {
                           youGotIntoALoop = true;
                           pointer = 0;
                        }
                     }
                  }
               }
            }
            if (youGotIntoALoop) { //I don't think there's much else I can do if this occurs.
               goto startAllOver;
            }
            Deck.Add(CardDistribution[pointer]);
            illegalCards.Add(pointer);
            debugPointer = pointer;
            pointer = (pointer + 1) % InitialCardDistribution.Count();
            youGotIntoALoop = false;
         }
      }
      firstInDeck = Deck[0];
      Deck.RemoveAt(0);
      aaaaaaaaaaaaaaaaaaaa = String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", better(Deck[0]), better(Deck[1]), better(Deck[2]), better(Deck[3]), better(Deck[4]), better(Deck[5]), better(Deck[6]));
      kekDeck = Deck;
      Deck = Deck.Shuffle();
      Debug.LogFormat("[UNO! #{0}] Cards: {1} / {2}, {3}, {4}, {5}, {6}, {7}, {8}", moduleId, better(firstInDeck), better(kekDeck[0]), better(kekDeck[1]), better(kekDeck[2]), better(kekDeck[3]), better(kekDeck[4]), better(kekDeck[5]), better(kekDeck[6]));
      Debug.LogFormat("[UNO! #{0}] A valid order: {1}", moduleId, aaaaaaaaaaaaaaaaaaaa);
   }

   string better (string torture) {
      return nicerNames[InitialCardDistribution.IndexOf(torture)];
   }

   void ShowCards () {
      Sprites[0].sprite = Cards[InitialCardDistribution.IndexOf(firstInDeck)];

      for (int n = 0; n < 7; n++) {
         Sprites[1+n].sprite = Cards[InitialCardDistribution.IndexOf(Deck[n])];
      }
   }

   bool WasThisCardValid (int k, int i) {
      string previousCard = " ";
      string currentCard;

      currentCard = Deck[k]; 

      if (played.IndexOf(k) != -1) {
         return false;
      }

      if (i == 0 || previousCard[0] == 'K') {
         if (i == 0) { //do you think I care that I'm making the same check twice here?
            previousCard = firstInDeck;
         } else {
            previousCard = whatYouPlayedLast;
         }
         if (previousCard == currentCard) {
            return false;
         } else if (previousCard[0] == currentCard[0]) {
            currentRuling = false;
            return true;
         } else if (previousCard[1] == currentCard[1]) {
            currentRuling = true;
            return true;
         } else if (currentCard[0] == 'K') {
            return true;
         } else {
            return false;
         }
      } else {
         currentRuling = !currentRuling;
         if (i == 5 && !hasUnoed) {
            failuresToUno += 1;
            Yell(failuresToUno);
            return false;
         }
         previousCard = whatYouPlayedLast;
         if (previousCard[0] == 'K') {
            return true;
         }
         if (currentCard == "KW") {
            return true;
         }
         if (currentCard == "K*4") {
            for (int s = 0; s < 7; s++) {
               if (s == k) {
                  continue;
               }
               if (played.IndexOf(s) == -1) {
                  if (WasThisCardValid(s, i)) {
                     return false;
                  }
               }
            }
            return true;
         }
         if (currentRuling) {
            return previousCard[1] == currentCard[1];
         } else {
            return previousCard[0] == currentCard[0];
         }
      }
   }

   void CommitSquare () {
      if (hasUnoed) {
         return;
      }
      if (cardsSubmitted == 5) {
         Debug.LogFormat("[UNO! #{0}] UNO! pressed.", moduleId);
         Audio.PlaySoundAtTransform("UNO", BigIfSquare.transform);
         hasUnoed = true;
      }
   }

   void Yell (int piss) {
      if (piss == 1) {
         Audio.PlaySoundAtTransform("YOUHAVEUNO", BigIfSquare.transform);
      } else {
         Audio.PlaySoundAtTransform("IDONTHAVETWO", BigIfSquare.transform);
      }
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
