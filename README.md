Psyche Against the Universe - ReadMe.md File

Welcome to Psyche Against the Universe. 
  An interactive card game based on the physical version of Psyche Against the Universe created by Nidah Shah.

Table of Contents:
  1.0: Creators
      1.0.1: General Information
      1.0.2: Development Information
    1.1: Version information
    1.2: Disclaimer Statement
  2.0: Current Bugs and Progress
  3.0: How to Build
  4.0: How to Play 
  5.0: Screenshots

Section 1.0: Creators
            Project designed and created by Team 24B from Software Engineering 480/481 Cohort, Pennslyvania State University
            2024-2025. Team members are Timothy Burke, Azaria Mehanovic, Rijul Sudesh, and Abdur Rhaman-Igram.

Section 1.0.1: General Information:
            The system was built using Unity version 6.2.10F1. The system was optimized to run on PC, but should be able to 
            scale to laptops, and tablets. 
            The system is scaled to 1366 X 768 and will run on all supported browser that support the Psyche Game portal.
            The Main Repo and Web repository will contain the most current build. The current distribution will be from the Web Repository.
            Updates to the system post publishing will occur on the Main Repo and then sent for hosting.

Section 1.0.2: Development Information
            Emulation of any game is always a challenge. Team 24B did thier best to capture the essence of playing a physical card game on 
            a computer system. As such the CPU players will do thier best to emulate a personality to get you, the player to predict how they will 
            judge and act. 
            However the true purpose of the game is to have fun and learn a bit about the Psyche Mission, so although a littel competition is nice, it is always 
            good to just relax and enjoy the show.
            To create a game that is open to all people, and to preserve online safety, no user data is required. Players are under a chosen alias, and all CPU players
            are given generated names, and a random avatar picture. 

Section 1.1: Version Information
           Current build of the program is 2.0.

Section 1.2: Disclaimer
            This work was created in partial fulfillment of The Pennslyvania State University Capstone Course “SWENG 480/481″. 
            The work is a result of the Psyche Student Collaborations component of NASA’s Psyche Mission (https://psyche.ssl.berkeley.edu). 
            “Psyche: A Journey to a Metal World” [Contract number NNM16AA09C] is part of the NASA Discovery Program mission to solar system targets. 
            Trade names and trademarks of ASU and NASA are used in this work for identification only. Their usage does not constitute an official endorsement, 
            either expressed or implied, by Arizona State University or National Aeronautics and Space Administration. 
            The content is solely the responsibility of the authors and does not necessarily represent the official views of ASU or NASA.

Section 2.0: Current Bugs
            As of 4/8/26 the current bugs exist in the system but do not critically impact gameplay:
            1) Random Card name displayed on human-machine interface when human player judges.
                      The card played is random and is not counted during the game. This is due to a redundant call while the game loop 
                      is transistion and changing state. The properly selected card is displayed immediately in the translator window.

            2) Double deal for human player. 
                      The human player is given two cards each turn vice one to replace the played card. All other cards not played by
                      the human player are preserved. This is suspected to be a redundant call and related to the random Card name bug described above.
                      No effect on game play as the cards are only shown on the players turn.
            3) Card Render bug.
                      The system renders cards and buffering makes the cards appear to be bouncing. This is due to graphical scaling and the system trying to 
                      replicate shuffling cards. 

Section 3.0: How to build

          As the system was developed in Unity, it has also been packaged as a web build using the editors build feature. This aspect also allows it to be hosted
          to a web server using the editor. As such the instructions and photos below detail the process using the uni
            
  
