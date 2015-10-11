using Entitas;

public class CircleMissileSpawnerComponent : IComponent {
	public int amount;
	public float time;
	public float spawnDelay;
	public string resource;
	public float velocityX;
	public float velocityY;
	public int collisionType;
}