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
bool isGameOver = false;
byte player = 1;
byte rounds = 1;
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

    //Indexing user choice
    col = col - 1;
   
    //Veryfing if the column is full and getting the row placement
    bool IsValid = false;
    int row = 0;
    for (int i = 5; i >= 0; i--) {
        if (table[i, col] == 0) {
            IsValid = true;
            row = i;
            break;
        }
    }

    //Validating Column
    if (!IsValid) {
        Console.WriteLine("\nThe Column is full...");
        Console.WriteLine("Choose another column");
        Console.WriteLine("Press any key to try again...");
        Console.ReadKey(true);
        //continue;
        break;
    }

    table[row, col] = player;

    //Array of tuples for offsets
    (int r, int c)[] directions =  new[] {
        (1, 1), (1, 0), (1, -1), (0, 1)
    };
    
    //Validate Win
    foreach ((int r, int c) tuple in directions) {
        int count = 1;
        for (int i = 1; i <= 3; i++) {
            int Roffset = tuple.r * i;
            int Coffset = tuple.c * i;

            if (row + Roffset < 0 || row + Roffset > 5 ||
                col + Coffset < 0 || col + Coffset > 6) {
                break;
            }

            if (table[row + Roffset, col + Coffset] != player) {
                break;
            }

            ++count;
        }
        
        if (count == 4) {
            isGameOver = true;
            break;
        }

        //Opposite Direction
        for (int i = 1; i <= 3; i++) {
            int NRoffset = (-tuple.r) * i;
            int NCoffset = (-tuple.c) * i;

            if (row + NRoffset < 0 || row + NRoffset > 5 ||
                col + NCoffset < 0 || col + NCoffset > 6) {
                break;
            }

            if (table[row + NRoffset, col + NCoffset] != player) {
                break;
            }

            ++count;
        }

        if (count >= 4) {
            isGameOver = true;
            break;
        }
    }

    //End Game
    if(isGameOver) {
        Console.Clear();
        DisplayTable();
        Console.WriteLine($"Player {player} won!!!");
        break;
    }

    //Board is Full
    if (rounds == (6 * 7)) {
        Console.Clear();
        DisplayTable();
        Console.WriteLine("Draw!!!");
        break;
    }
    ++rounds;

    //Switch Players
    if (player == 1) {
        player = 2;
    } else {
        player = 1;
    }
}