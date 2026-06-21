using System;

Console.WriteLine("Welcome to the Multiplication Table Generator!\n");

Console.Write("Type the desired number\n>> ");
int n = int.Parse(Console.ReadLine());

Console.WriteLine();
for (int i = 0; i <= 10; i++) {
	Console.WriteLine($"{n} * {i} = {n * i}");

}