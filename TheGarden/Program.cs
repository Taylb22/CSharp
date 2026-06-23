using System.Drawing; // Para cores
using TheGarden; // Para usar o TheGarden

var garden = new Garden();

// Colocando 1 Drunk Person na posição (40, 40)
garden.Add("drunkperson", Color.Beige, 40, 40);

garden.Run();

public class DrunkPerson
{
    public void Act(Gardenkeeper keeper)
    {
		// Pede ao jardineiro que o mova aleatóriamente por ai
        keeper.Move(
            Random.Shared.Next(-1, 2),
            Random.Shared.Next(-1, 2)
        );
    }
}