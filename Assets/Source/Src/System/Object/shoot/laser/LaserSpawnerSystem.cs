using Entitas;
using UnityEngine;

public class LaserSpawnerSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _group;
	LayerMask _layerMask;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.LaserSpawner);
		pool.GetGroup(Matcher.LaserSpawner);
		_time = pool.GetGroup(Matcher.Time);
		_layerMask = 1 << LayerMask.NameToLayer("Enemy");
	}

	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _group.GetEntities()) {
			fuckThemWithLaser(e, deltaTime);
		}
	}
	
	void fuckThemWithLaser(Entity e, float deltaTime) {
		float laserHeight = 10.0f;
		LaserSpawnerComponent component = e.laserSpawner;
		GameObject go = e.gameObject.gameObject;
		Vector2 laserDirection = new Vector2(0.0f, 1.0f);
		RaycastHit2D hit = Physics2D.Raycast(go.transform.position,
		                                     laserDirection,
		                                     laserHeight,
		                                     _layerMask);
		Collider2D collider = hit.collider;
		if (collider != null) {
			CollisionScript collision = collider.GetComponentInParent<CollisionScript>();
			if (collision != null) {
				collision.queue.Enqueue("1_FU"); // 60dmg perSecond, wow
			}
			laserHeight = Vector2.Distance(hit.collider.transform.position, go.transform.position);
		} 
		if (component.laser == null) {
			Entity laser = _pool.CreateEntity()
				.AddLaser(laserHeight, e)
				.AddPosition(go.transform.position.x, go.transform.position.y)
				.AddResource(Resource.Laser);
			laser.isNonRemovable = true;
			component.laser = laser;
		}
		component.height = laserHeight;
	}
}