using System;
using System.Threading;
using System.Threading.Tasks;

const int ROWS = 20;
const int COLUMNS = 20;
const int MIDDLE = (ROWS * COLUMNS) / 2;

byte[] Table = new byte[ROWS*COLUMNS];

void DisplayTable () {
	for (int i = 0; i < ROWS * COLUMNS; i++) {
		int row = i / COLUMNS;
		int col = i % COLUMNS;

		Console.Write(" ");
		switch (Table[row * COLUMNS + col]) {
			case 0:
				Console.Write(" ");
				break;
			case 1:
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("O");
				break;
			case 2:
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("O");
				break;
		}
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(" ");

		if (col == COLUMNS-1) {
			Console.WriteLine();
		}
	}
}

//Configuring
Console.ForegroundColor = ConsoleColor.White;
Console.CursorVisible = false;

Random Generator = new Random();
int apple = Generator.Next(0, ROWS*COLUMNS);
Table[apple] = 2;

bool IsGameOver = false;
while (!IsGameOver) {
	Console.SetCursorPosition(0, 0);
	Console.WriteLine("\n\nPress Q to Exit");
	DisplayTable();
	
	if (Console.KeyAvailable) {
		var KeyInfo = Console.ReadKey(intercept: true);

		switch (KeyInfo.Key) {
			case ConsoleKey.Q:
				IsGameOver = true;
				break;
			case ConsoleKey.W:
				break;
			case ConsoleKey.S:
				break;
			case ConsoleKey.D:
				break;
			case ConsoleKey.A:
				break;
		}
	}

	Thread.Sleep(50);
}
Console.CursorVisible = true;