using Entitas;
using UnityEngine;

public class LaserSpawnerSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _time;
	LayerMask _enemyLayerMask;
	LayerMask _playerLayerMask;

	const float EPSILON = 0.005f;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.LaserSpawner);
		_time = pool.GetGroup(Matcher.Time);
		_enemyLayerMask = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Static"));
		_playerLayerMask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Static"));
	}

	public void Execute() {
		TimeComponent time = _time.GetSingleEntity().time;
		if (time.isPaused) {
			return;
		}

		foreach (Entity e in _group.GetEntities()) {
			fuckThemWithLaser(e);
		}
	}
	
	void fuckThemWithLaser(Entity e) {
		float laserHeight = 10.0f;
		LaserSpawnerComponent component = e.laserSpawner;
		GameObject go = e.gameObject.gameObject;
		component.direction = Quaternion.Euler(0, 0, component.angle) * new Vector2(0.0f, 1.0f);
		RaycastHit2D hit = Physics2D.Raycast(go.transform.position,
		                                     component.direction,
		                                     laserHeight,
		                                     component.collisionType == CollisionTypes.Player ? _enemyLayerMask : _playerLayerMask);
		Collider2D collider = hit.collider;
		if (collider != null) {
			CollisionScript collision = collider.GetComponentInParent<CollisionScript>();
			Transform collidi = collider.transform;
			if (collision != null) {
				collision.queue.Enqueue("10_FU"); // 600dmg perSecond, wow
			}
			laserHeight = Vector2.Distance(collidi.position, go.transform.position);
		} 
		if (component.laser == null) {
			Entity laser = _pool.CreateEntity()
				.AddLaser(laserHeight, component.direction, e)
				.AddPosition(new Vector2().Set(e.position.pos))
				.AddResource(Resource.Laser);
			laser.isNonRemovable = true;
			component.laser = laser;
		}
		component.height = laserHeight;
	}
}