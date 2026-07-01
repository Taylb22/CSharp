using System;

World world = new World(new Cooperative(), new Cooperative());
world.Run();

public class World {
	private Individual First;
	private Individual Second;

	public World (Individual f, Individual s) {
		this.First = f;
		this.Second = s;
	}

	public void Run () {
		Console.WriteLine("Initial Values:");
		Console.WriteLine($"First: {First.coins}");
		Console.WriteLine($"Second: {Second.coins}");
		Console.Write("Press any key to continue...");
		Console.ReadKey(intercept: true);

		for (int i = 0; i < 10; i++) {
			Console.Clear();
			
			Simulate();
			
			Console.WriteLine($"First: {First.coins}");
			Console.WriteLine($"Second: {Second.coins}");
			Console.Write("Press any key to continue...");
			Console.ReadKey(intercept: true);
		}
	}

	private void Simulate() {
		bool f = First.Act();
		bool s = Second.Act();

		if (f && s) {
			First.Deposit(1);
			Second.Deposit(1);
		} else if (f) {
			First.Withdraw();
			Second.Deposit(3);
		} else if (s) {
			Second.Withdraw();
			First.Deposit(3);
		}
	}


}

public class Individual {
	public int coins {
		get {
			return this.coins;
		} set;
	}

	public Individual () {
		this.coins = 10;
	}

	public bool Act () {
		return true;
	}
	public void Withdraw () {
		this.coins--;
	}
	public void Deposit (int amount) {
		this.coins += amount;
	}
}

public class Cooperative : Individual {	
	public Cooperative() : base() {}
	
	public bool Act () {
		return true;
	}
}

public class Cheater : Individual {	
	public Cheater() : base() {}
	
	public bool Act () {
		return false;
	}
}