# PrecannonWarn
This is a simple tool to warn about a precannon wave is arriving to line on League Of Legends.

(Warn: this tool has been tested only in training/practice tool)

## Features  
- Warn to the player the precannon wave is arriving to line.

# How does it work
  This app request to the **League Client API** the basic data of the game to the endpoint "https://127.0.0.1:2999/liveclientdata/gamestats".

  After the API returns, a timer starts synchronized with the game time. When the timer matches a value from a pre-defined list
  the app play a sound waning you about the pre cannon wave is arriving.

  Current minutes the app warns are:
  
    * 2:00
    * 3:30
    * 5:00
    * 6:30
    * 8:00
    * 9:30
    * 11:0
    * 12:3
    * 14:0
    * 15:3
    * 17:0
    * 18:3
    * 20:0

 
## A look to the app  
![](https://github.com/chechoXR/PrecannonWarn/blob/main/PreCannon/Assets/img/demo/demo.png)  

 
## Badges  
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)  
 
 

