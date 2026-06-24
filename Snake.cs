using System;
using System.Threading;
//using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

//Table dimensions
const int ROWS = 20;
const int COLUMNS = 20;
const int MIDDLE = (ROWS / 2) * COLUMNS + (COLUMNS / 2);

//Entities
const byte EMPTY = 0;
const byte SNAKE = 1;
const byte APPLE = 2;

//Directions
const int UP = -COLUMNS;
const int DOWN = COLUMNS;
const int RIGHT = 1;
const int LEFT = -1;

//Game Table
byte[] Table = new byte[ROWS*COLUMNS];
int Score = 0;

//Renderer
void RenderTable () {
	Console.SetCursorPosition(0, 0);
	for (int i = 0; i < ROWS * COLUMNS; i++) {
		int row = i / COLUMNS;
		int col = i % COLUMNS;

		switch (Table[row * COLUMNS + col]) {
			case EMPTY:
				Console.BackgroundColor = ConsoleColor.DarkGray;
				break;
			case SNAKE:
				Console.BackgroundColor = ConsoleColor.Green;
				break;
			case APPLE:
				Console.BackgroundColor = ConsoleColor.Red;
				break;
		}
		Console.Write("  ");

		Console.BackgroundColor = ConsoleColor.Black;
		if (col == COLUMNS-1) {
			Console.WriteLine();
		}
	}
}

//Spawner
void SpawnApple() {
	Random Generator = new Random();
	int val;
	do {
		val = Generator.Next(0, ROWS*COLUMNS);
	} while (Table[val] != EMPTY);

	Table[val] = APPLE;
}

//Configuring
Console.CursorVisible = false;

//Stop Watch
var StopWatch = Stopwatch.StartNew();
double LastTime = StopWatch.Elapsed.TotalSeconds;
double Accumulator = 0;
const double TICKRATE = 0.25;

//Start Snake
List<int> Snake = [MIDDLE];
Table[MIDDLE] = SNAKE;

//First Apple
SpawnApple();

//Start Direction
int direction = UP;

bool IsGameOver = false;
while (!IsGameOver) {
	Console.BackgroundColor = ConsoleColor.Black;
	Console.WriteLine($"SCORE: {Score}");
	Console.WriteLine("Press Q to Exit");
	RenderTable();

	double CurrentTime = StopWatch.Elapsed.TotalSeconds;
	double DeltaTime = CurrentTime - LastTime;
	LastTime = CurrentTime;

	Accumulator += DeltaTime;
	
	//Get's the pressed Key and defines action
	if (Console.KeyAvailable) {
		var KeyInfo = Console.ReadKey(intercept: true);
		
		while (Console.KeyAvailable) {
			Console.ReadKey(intercept: true);
		}

		//WASD to change direction and Q to quit
		int NewDirection = direction;
		switch (KeyInfo.Key) {
			case ConsoleKey.Q:
				IsGameOver = true;
				break;
			case ConsoleKey.W:
				NewDirection = UP;
				break;
			case ConsoleKey.S:
				NewDirection = DOWN;
				break;
			case ConsoleKey.D:
				NewDirection = RIGHT;
				break;
			case ConsoleKey.A:
				NewDirection = LEFT;
				break;
		}

		//Avoids rotation by 180°
		if (NewDirection != (-direction)) {
			direction = NewDirection;
		}
	}
	Thread.Sleep(100);

	//Delta time implementation
	//Validates the Snake new postion and updates it
	while (Accumulator >= TICKRATE) {
		Accumulator -= TICKRATE; 
		
		int NewHead = Snake[0] + direction;
		int tail = Snake[Snake.Count - 1];

		//Collisions
		bool upWall = (NewHead < 0);
		bool downWall = (NewHead > ROWS * COLUMNS);
		bool rigthWall = ((direction == 1) && (NewHead % COLUMNS < Snake[0] % COLUMNS));
		bool leftWall = ((direction == -1) && (NewHead % COLUMNS > Snake[0] % COLUMNS));

		//Ends after collision with wall
		bool wallCollision = (upWall || downWall || rigthWall || leftWall);
		if (wallCollision) {
			IsGameOver = true;
			break;
		}

		//Ends after self Collision
		if (Table[NewHead] == SNAKE) {
			IsGameOver = true;
			break;
		}

		//Validates if the snake consumed an Apple and grows accordingly
		Snake.Insert(0, NewHead);
		if (Table[NewHead] != APPLE) {
			Table[tail] = EMPTY;
			Snake.RemoveAt(Snake.Count - 1);
		} else {
			Score++;
			SpawnApple();
		}
		Table[NewHead] = SNAKE;

		if (Snake.Count == ROWS * COLUMNS) {
			IsGameOver = true;
			RenderTable();
			break;
		}
	}
}
Console.CursorVisible = true;