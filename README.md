# MobileCheckers

## Overview
  For our project, we decided to focus on the Android platform for gaming. To do this, we decided to work specifically with Unity. Unity is an excellent real-time development platform in which the types of games the user can build are numerous. Unity also provides compatibility on over 25 different platform, including on both mobile and on desktop. For our project, we decided to create a game of checkers to show off what we learned about Unity. Unity allows us to use C# scripts to code checker pieces, moves, and the game mechanics in general to fit how we want. One big positive about Unity is how quickly and easy it is to create and modify Game Objects, as that is extremely helpful in loading up games and the moves the pieces make.
## Getting Started
  In order to work on Unity for this project, we're first going to need to install it. Head over to https://unity3d.com/get-unity/download and click on the button that says "Download Unity Hub". Open the installer, agree to the terms of service, select a proper installation folder, and let the program install Unity on your computer. In your first time using Unity Hub, you may be prompted to create an account and go through and example project. Feel free to create your account and explore the options they provide you. Once Unity Hub is installed, opened, and past the tutorial phase, there should be a few tabs on the left. The user should click on the "Installs" Tab. This will bring up a page where you can install different versions of Unity. Click the blue button near the top that says "ADD".
  
![](/images/add.jpg) 
  
  Inside ADD, they will prompt you to select an option of recommended releases or official releases. For our current project, it is not listed since they've made updates. In order to get our version of Unity, you're going to want to click the blue link to visit our "download archive." Inside there, you're going to want to click the tab that says Unity 2020, and you're going to want to locate the version "Unity 2020.3.0". To the right of that label should be a green button that says Unity, click on this button. This will prompt another installer. In here you're going to want to ensure that the installer runs and that you download the android libraries as well as the default checkmarked installs. Wait for this to install, then bring Unity Hub back up. 

  With the projects tab open on the left side of Unity, we're going to want to click the blue new button in the top right of the screen. For templates, we're going to want to download the Mobile 2D template provided. For the project name, feel free to call it what you like and create a path that you will be able to find the project in the future. When opening the project in the future from the Unity Hub, you'll want to select your platform as "Android" rather than "Current Platform", to ensure it stays in an Android format.
  
  If you are planning to follow along and create a checkers project, we recommend that you also download a checkerboard picture, as well as different colored checkers pieces.
## Step-By-Step Coding

### Main Camera

One key feature of Unity is the ability to have a Main Camera. The main camera acts as what the user currently sees in this scenario, and for our project we want it to focus on the checkers board.

![](/images/camera.JPG) 

This main camera, by default, will not be the portrait size of an Android device.[To change the size of your camera to specific Android standards, Hellium at Unity Answers gives great general advice as to how to change this camera size to fit port sizes for both iOS and Android phones.](https://answers.unity.com/questions/1273713/how-to-set-up-unity-for-portrait-mobile-developmen.html) 

### Creating Sprites to Place in the Main Camera

Now that we have our main camera all set up and ready to go, we will need to place sprites within this main camera. For this project, we'll need to import a checkers board, so we can play checkers on top of it. To do this, we'll need to look at the bottom box of the screen, and on the left side there should be a folder called Assets. Right click on the assets folder, and select "Import New Asset."

![](/images/Import.png) 

From here, we will choose the checkerboard file already downloaded. You should now see the file appear in the bottom of the screen. We'll create a folder to store these sprites and we will call it "Sprites" within the Assets folder. In order to get the image to appear on screen, click and drag your sprite from the bottom box to the hierarchy box where the Scene and the Main Camera exist, and place it in there. You can either utilize the move tool at the top of the screen or the transform tool under inspector to change the position of the sprite and center it in the camera.

![](/images/transform.JPG) 

### Creating a Game Controller

For coding the scripts to make the game function, we'll use C#. We're going to need to create a Controller to handle the events of the game. To do this, right click under the hierarchy and select Create Empty. This will create a new GameObject, which serve several different purposes in Unity. For this GameObject, we're going to go on the inspector on the right side, and hit the dropdown menu which says "Tag" and click on the GameController option. This will indicate that this class is designated to be the main controller for the game. 

With the controller still inspected, look furthest down the available options until you find the "Add Component" button. In here, we're going to want to select the scripts option, then type in the name you want your script to be, and click the option for "New Script" and that should pop up. We'll select our name as "Game", and then click the Create and Add option. You now have a game controller script created!

### Coding the Game Controller

To access your Game Controller script, there should now be a folder on the bottom box labeled as "Scripts". Inside of there should be a C# file containing the filename that you wanted for that script. We're going to utilize this script for some practical coding and understanding about Unity. When you first open up the C# file, there should be already two methods created for you: Start() and Update(). Start() is the code that will be played at the start of the game and can be used to create initial objects, such as our initial checker pieces. Let's take a look at the start of our Checkers program: 

```
    public GameObject Checker;

    
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[12];
    private GameObject[] playerWhite = new GameObject[12];

    private string currentPlayer = "Black";
    private bool hasJump = false;

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //Load in white pieces
        for (int i = 0; i < 8; i++)
        {
            if(i % 2 == 0)
            {
                playerWhite[i] = Create("singleWhite", i, 0);
                playerWhite[11 - i / 2] = Create("singleWhite", i, 2);
            } 
            else
            {
               playerWhite[i] = Create("singleWhite", i, 1);
            }
        }

        //Load in Black pieces
        for (int i = 0; i < 8; i++)
        {
            if (i % 2 == 0)
            {
                playerBlack[i] = Create("singleBlack", i, 6);
                
            }
            else
            {
                playerBlack[i] = Create("singleBlack", i, 7);
                playerBlack[11 - i / 2] = Create("singleBlack", i, 5);
            }
        }

        for (int i = 0; i < playerWhite.Length; i++)
        {
            SetPosition(playerWhite[i]);
            SetPosition(playerBlack[i]);
        }
    }
```

Take note of the first line where we call public GameObject Checker. If we go back to our UI and double click on the controller in the hierarchy on the left side, we now see under our original game script is a Checker GameObject. Now, we're going to need to import our checker piece images into the sprite folder through the similar method we used above. For this checker piece, we're going to want to also create a script. Have the checker piece sprite selected, head to the bottom of the right menu and click the Add Component like before. Click on the option that says Scripts and this time we're going to want to create a Checkers_Piece script. This will control the actions of the checker pieces specifically.

![](/images/script.png)

## Further Discussions/Conclusions
  This was our tutorial on how to utilize Unity and how our group utilized it to create a Checkers project. Unity is becoming an increasingly popular development tool for creating games, so this was a great experience on our part to see what goes into coding Unity games. We recommend that you all take a look into Unity if you are interested and have the free time. One tool that is useful for Android development specifically is in the Unity Package Manager called "Device Simulator". This allows you to get a look at your game from the perspective of users on different sized phones. Here's an excellent tutorial on its uses and how to install it: https://www.youtube.com/watch?v=uokF9CmUs9c .
  We really enjoyed creating this project, and we hope you found this tutorial helpful. Hopefully it sparked some interest from you all to test out Unity!
