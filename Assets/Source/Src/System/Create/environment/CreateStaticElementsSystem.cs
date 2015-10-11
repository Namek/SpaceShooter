using Entitas;
using UnityEngine;

public class CreateStaticElementsSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		Debug.Log("CreateStaticElementsSystem");
		_pool.CreateEntity()
			.AddPosition(-2.0f, 2.0f)
			.AddHealth(2000)
			.AddResource(Resource.Blockade)
			.AddCollision(CollisionTypes.Static)
			.isDestroyPosition = true;
	}
}