using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public FindTargetComponent findTarget { get { return (FindTargetComponent)GetComponent(ComponentIds.FindTarget); } }

        public bool hasFindTarget { get { return HasComponent(ComponentIds.FindTarget); } }

        static readonly Stack<FindTargetComponent> _findTargetComponentPool = new Stack<FindTargetComponent>();

        public static void ClearFindTargetComponentPool() {
            _findTargetComponentPool.Clear();
        }

        public Entity AddFindTarget(int newCollisionType) {
            var component = _findTargetComponentPool.Count > 0 ? _findTargetComponentPool.Pop() : new FindTargetComponent();
            component.collisionType = newCollisionType;
            return AddComponent(ComponentIds.FindTarget, component);
        }

        public Entity ReplaceFindTarget(int newCollisionType) {
            var previousComponent = hasFindTarget ? findTarget : null;
            var component = _findTargetComponentPool.Count > 0 ? _findTargetComponentPool.Pop() : new FindTargetComponent();
            component.collisionType = newCollisionType;
            ReplaceComponent(ComponentIds.FindTarget, component);
            if (previousComponent != null) {
                _findTargetComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveFindTarget() {
            var component = findTarget;
            RemoveComponent(ComponentIds.FindTarget);
            _findTargetComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherFindTarget;

        public static AllOfMatcher FindTarget {
            get {
                if (_matcherFindTarget == null) {
                    _matcherFindTarget = new Matcher(ComponentIds.FindTarget);
                }

                return _matcherFindTarget;
            }
        }
    }
}
