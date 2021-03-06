using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public GameObjectComponent gameObject { get { return (GameObjectComponent)GetComponent(ComponentIds.GameObject); } }

        public bool hasGameObject { get { return HasComponent(ComponentIds.GameObject); } }

        static readonly Stack<GameObjectComponent> _gameObjectComponentPool = new Stack<GameObjectComponent>();

        public static void ClearGameObjectComponentPool() {
            _gameObjectComponentPool.Clear();
        }

        public Entity AddGameObject(UnityEngine.GameObject newGameObject, string newPath) {
            var component = _gameObjectComponentPool.Count > 0 ? _gameObjectComponentPool.Pop() : new GameObjectComponent();
            component.gameObject = newGameObject;
            component.path = newPath;
            return AddComponent(ComponentIds.GameObject, component);
        }

        public Entity ReplaceGameObject(UnityEngine.GameObject newGameObject, string newPath) {
            var previousComponent = hasGameObject ? gameObject : null;
            var component = _gameObjectComponentPool.Count > 0 ? _gameObjectComponentPool.Pop() : new GameObjectComponent();
            component.gameObject = newGameObject;
            component.path = newPath;
            ReplaceComponent(ComponentIds.GameObject, component);
            if (previousComponent != null) {
                _gameObjectComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveGameObject() {
            var component = gameObject;
            RemoveComponent(ComponentIds.GameObject);
            _gameObjectComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherGameObject;

        public static IMatcher GameObject {
            get {
                if (_matcherGameObject == null) {
                    _matcherGameObject = Matcher.AllOf(ComponentIds.GameObject);
                }

                return _matcherGameObject;
            }
        }
    }
}
