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

Section 1.0.1: General Information

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
          to a web server using the editor. As such the instructions and photos below detail the process using the unity editor to perform a build and run procedure.
          This will build the system from the provided build profile. If desired a clean build can be performed but will take about 7 minutes to perform.

          3.1: Link to repository in Unity

                1) If not done, download and install the Unity Hub. This will also download the Unity Editor if not done.
                2) Clone the Web Repository in Github
                3) Add the project to the Unity Hub by:
                    a. Projects Tab
                    b. Click Add
                    c. Browse to the folder that was cloned
                    d. Select it
                4) Unity Hub should now show the project.
                5) The system was built using Unity Editor 6000.2.10F1. Install this version. 
                  (NOTE: Unity may require you to update do version F2. At this time, do not update past version 2.10. 
                  The errors listed have had no impact on system deployment and gameplay).
                  
            
          3.2: Build Program (Clean and Regular)

              1) Open the project in the editor
              2) Go to File -> Build Profiles-> on Left side, click on Web
              3) In the top Right, click on Player Settings
              4) In Player Settings on left -> Player Menu
                  a. Under Resolution and Presentation, verify that settings are 1366X768
                  b. Under Publisher Settings, verify that Compression Format is disabled. 
                  (You can enable it, but this will lengthen the build process and may limit browser compatibilty)
                  c. Close Project Settings Window.
              5) On the bottom you have the options to publish, build, or build and run.
                  a. Choose the option and the build process will proceed. For publish, you will need to provide the 
                    neccessary server information.
                  b. For an initial download, you can perform a clean build by selecting Build-> clean build. This will 
                      start the build process. (This can take up to 10 minutes). 
                        1) When selected you will be asked to select a folder. If this is a first time build, you will need
                          to create a folder (name it what you wish). Otherwise you can use the created build profile folder.
                        2) When finished, the file browser window will be open highlighting your build package.
                  c. Click Build And Run
                        1) In the File Browser window, select your desired build package and hit select folder.
                        2) The system will perform a build process and then run in the default browser.
              6) If you have already created build profile packages, you can simply press Build And Run and select the desired package.

  4.0: How to play

      4.1: Goal 
      
              The goal of the game is to guide the player through the solar system
              who is on its way to asteroid Physche-16. However the journey is not as empty as space may
              seem. The player will encounter unusual scenerios, and the solution they provide may provide them 
              the boost the need to succeed.
              
      4.2: Set-Up
              The user (You) selects an avatar to be you on this journey. Afterwhich, determine which 
              game mode you want to play. Select the number of CPU players whom you wish to be challenged against.At this time
              also select any quality of life features you may want to use. When ready to begin your journey,
              press Start.
      4.3: Gameplay
              Each player is dealt 5 answer cards containing a possible solution to a problem.
              Each turn the game will present a situation (Prompt Card) that each player will need to provide a solution too. 
              Once player each turn will act as the judge and evaluate which players chosen solution (Answer Card) is the best.
              Play rotates to the left of the judge, with each player playing an answer card facedown.
              Once the judge has chosen a card, the player who played it scores a point.
              Afterwards, each player who played a card gets a new answer card for thier hand, and the 
              judge rotates to the next player to the left. A new prompt card is played begining the new round.

     4.4:  Win
             Play continues until a player reaches 7 points (normal) or 3 points (Sudden Win). 
             Normal Mode: At 7 points a winner is declared. Play continues for one additional round to allow
             the current judge to get an even amount of play rounds. If a tie is not reached, the game ends with the 
             winner declared. In the case of a tie, an additional round is played to break the tie.

        4.4.1: Sudden Win
                  Extra booster rockets have accelerated the journey to Pysche 16. First player to 3 points, 
                  regardless of ties or rounds played wins.

    4.5: Strategy
            As the human player, you need to convince the judge that you deserve the best. This means 
            you need to read into the other players personalites to determine how they are going to play. Wach Answer card
            contains a list of 4 personality traits. Through observation, you may be able to determine which type of 
            card personality a player will use, and then play a card of that type to influence them when they are the judge.
            Do the same when it is your turn as judge to ensure you don't accidently give another player a winning chance.

  5.0: Game Screenshots
