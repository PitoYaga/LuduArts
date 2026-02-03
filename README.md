# LuduArts

This project was created for a job application to Ludu Arts.



\# Interaction System - Efe Canoğlu



\## Installation

\- Unity version: Unity 6000.0.23f1

\- How to open/run - After cloning the project with GitHub Desktop, you can locate and open the project from within Unity Hub.



\## How to Test

\- Everything is within the starting map. 

\- Controls: \[WASD, E, TAB]

\- Test scenarios - The keys and door colors match. The chest contains the key for the blue door. The yellow door opens with lever.



\## Code Design Decisions

\- I used interfaces for the interaction of characters and objects. This prevents them from referencing each other and saves RAM space. 

\- What were the alternatives? - A base interactive object could be created, and then different objects could be obtained by changing the details. 

\- Trade-offs - The design phase needs to be predetermined for variable and object variety. Adding them later can cause some time loss.



\## Compliance with Ludu Arts Standards

\- Which standards did I apply? - I paid attention to naming conventions. I increased code clarity by using regions and providing descriptions. 

\- Difficulty points - I hadn't used ScriptableObjects before because I had done small projects. Understanding its logic was a bit challenging for me.



\## Known Limitations

\- Features I couldn't complete - I didn't create a save system. 

\- Known bugs - There are no bugs that break the game. Perhaps very fast, consecutive interactions might cause problems, but I don't think it will. 

\- Improvement suggestions - The inventory system could definitely be improved. UI animations could be improved.



