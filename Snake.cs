using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

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

var StopWatch = Stopwatch.StartNew();
double LastTime = StopWatch.Elapsed.TotalSeconds;
double Accumulator = 0;

const double TICKRATE = 0.25;

//First Apple
Random Generator = new Random();
int apple = Generator.Next(0, ROWS*COLUMNS);
Table[apple] = 2;

//Start Snake
List<int> Snake = [7];
Table[7] = 1;

int direction = COLUMNS;

bool IsGameOver = false;
while (!IsGameOver) {
	Console.SetCursorPosition(0, 0);
	Console.WriteLine("\n\nPress Q to Exit");
	DisplayTable();

	double CurrentTime = StopWatch.Elapsed.TotalSeconds;
	double DeltaTime = CurrentTime - LastTime;
	LastTime = CurrentTime;

	Accumulator += DeltaTime;
	
	if (Console.KeyAvailable) {
		var KeyInfo = Console.ReadKey(intercept: true);

		switch (KeyInfo.Key) {
			case ConsoleKey.Q:
				IsGameOver = true;
				break;
			case ConsoleKey.W:
				direction = -COLUMNS;
				break;
			case ConsoleKey.S:
				direction = COLUMNS;
				break;
			case ConsoleKey.D:
				direction = 1;
				break;
			case ConsoleKey.A:
				direction = -1;
				break;
		}
	}

	if (Table[Snake[Snake[0] + direction] == 2) {
		Snake.add(Snake[Snake.Count-1]);
		int last = Snake[0];
		for (int i = Snake.Count-2; i > 0; i--) {
			Snake[i] = last;
			last = Snake[i];
		}
	}

	while (Accumulator >= TICKRATE) {
		Accumulator -= TICKRATE;
		for (int i = 0; i < Snake.Count-1 ; i++) {
			Table[Snake[i]] = 0;
			Snake[i] += direction;

			Table[Snake[i]] = 1;
		}
	}
}
Console.CursorVisible = true;