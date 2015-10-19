using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public FirstBossComponent firstBoss { get { return (FirstBossComponent)GetComponent(ComponentIds.FirstBoss); } }

        public bool hasFirstBoss { get { return HasComponent(ComponentIds.FirstBoss); } }

        static readonly Stack<FirstBossComponent> _firstBossComponentPool = new Stack<FirstBossComponent>();

        public static void ClearFirstBossComponentPool() {
            _firstBossComponentPool.Clear();
        }

        public Entity AddFirstBoss(float newRandom) {
            var component = _firstBossComponentPool.Count > 0 ? _firstBossComponentPool.Pop() : new FirstBossComponent();
            component.random = newRandom;
            return AddComponent(ComponentIds.FirstBoss, component);
        }

        public Entity ReplaceFirstBoss(float newRandom) {
            var previousComponent = hasFirstBoss ? firstBoss : null;
            var component = _firstBossComponentPool.Count > 0 ? _firstBossComponentPool.Pop() : new FirstBossComponent();
            component.random = newRandom;
            ReplaceComponent(ComponentIds.FirstBoss, component);
            if (previousComponent != null) {
                _firstBossComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveFirstBoss() {
            var component = firstBoss;
            RemoveComponent(ComponentIds.FirstBoss);
            _firstBossComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherFirstBoss;

        public static AllOfMatcher FirstBoss {
            get {
                if (_matcherFirstBoss == null) {
                    _matcherFirstBoss = new Matcher(ComponentIds.FirstBoss);
                }

                return _matcherFirstBoss;
            }
        }
    }
}
