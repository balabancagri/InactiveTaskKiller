# InactiveTaskKiller
 This program detects when the computer is not in use and waits as long as you want and closes the programs you want. The program runs in the background and is only visible in the task manager. It consumes an average of 3 to 7 mb of ram.
 
 Program has "parametreler" file. Please open file with text editor. 
 First line should be int value. This line is requested minute. 
 Second and other lines are program names which you want to close.
 
 For example;
 ---parametreler file inside----
10
notepad
Spotify
 ---parametreler file inside----
 
This file says that if the mouse is not moved and a key is not pressed on the keyboard, it closes the notepad and spotify application after 10 minutes.

Note: app names should be without .exe 
