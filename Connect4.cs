using System;

byte[,] table = new byte[6,7];
void DisplayTable() {
    for (int i = 0; i < 6 * 7; i++) {
        int row = i / 7;
        int col = i % 7;

        //Printing the element according to the player color
        Console.Write("| ");
        char C = 'O';
        switch (table[row,col]) {
            case 0:
                C = ' ';
                break;
            case 1:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
        }
        Console.Write(C);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" |");
        
        if (col == 6) {
            Console.WriteLine();
        }
    }
    
    //Printing the column number
    for (int i = 0; i < 7; i++) {
        Console.Write($"  {i+1}  ");
    }
    Console.WriteLine();
}

//Entry Point
//Instructions
Console.WriteLine();
Console.WriteLine("Welcome to the Connect 4 game!");
Console.WriteLine("Player 1 starts with the blue pieces and player 2 will use the yellow ones.");
Console.WriteLine("Press any key to start...");
Console.ReadKey(true);


//Main Loop
bool isGameOver = true;
byte player = 1;
while (true) {
    Console.Clear();
    Console.WriteLine($"Player {player}");
    DisplayTable();

    //Getting user column choice
    Console.Write("Type the Column to play\n>> ");
    string choice = Console.ReadLine();
    
    //Validating user input
    int col;
    if (!int.TryParse(choice, out col)
        || col > 7 || col < 1) {

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nThe choice must be an integer number between 1 and 7...");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("Press any key to try again...");
        Console.ReadKey(true);
        continue;
    }

    if(isGameOver) {
        break;
    }


    if (player == 1) {
        player = 2;
    } else {
        player = 1;
    }
}