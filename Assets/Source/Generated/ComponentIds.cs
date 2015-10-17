public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Bonus = 1;
    public const int BonusModel = 2;
    public const int BonusSpawner = 3;
    public const int Camera = 4;
    public const int CircleMissileSpawner = 5;
    public const int Collision = 6;
    public const int CollisionDeath = 7;
    public const int CreateLevel = 8;
    public const int Damage = 9;
    public const int DestroyEntity = 10;
    public const int DestroyEntityDelayed = 11;
    public const int DestroyPosition = 12;
    public const int Enemy = 13;
    public const int EnemySpawner = 14;
    public const int FaceDirection = 15;
    public const int FindTarget = 16;
    public const int FollowTarget = 17;
    public const int GameObject = 18;
    public const int Health = 19;
    public const int HomeMissile = 20;
    public const int HomeMissileSpawner = 21;
    public const int Input = 22;
    public const int Laser = 23;
    public const int LaserSpawner = 24;
    public const int LevelDimensions = 25;
    public const int MissileSpawner = 26;
    public const int MouseInput = 27;
    public const int NonRemovable = 28;
    public const int Player = 29;
    public const int PoolableGO = 30;
    public const int Position = 31;
    public const int RegularCamera = 32;
    public const int Resource = 33;
    public const int RestartGame = 34;
    public const int SmoothCamera = 35;
    public const int SnapPosition = 36;
    public const int SpeedBonus = 37;
    public const int Test = 38;
    public const int Time = 39;
    public const int Velocity = 40;
    public const int VelocityLimit = 41;
    public const int Weapon = 42;

    public const int TotalComponents = 43;

    static readonly string[] components = {
        "Acceleration",
        "Bonus",
        "BonusModel",
        "BonusSpawner",
        "Camera",
        "CircleMissileSpawner",
        "Collision",
        "CollisionDeath",
        "CreateLevel",
        "Damage",
        "DestroyEntity",
        "DestroyEntityDelayed",
        "DestroyPosition",
        "Enemy",
        "EnemySpawner",
        "FaceDirection",
        "FindTarget",
        "FollowTarget",
        "GameObject",
        "Health",
        "HomeMissile",
        "HomeMissileSpawner",
        "Input",
        "Laser",
        "LaserSpawner",
        "LevelDimensions",
        "MissileSpawner",
        "MouseInput",
        "NonRemovable",
        "Player",
        "PoolableGO",
        "Position",
        "RegularCamera",
        "Resource",
        "RestartGame",
        "SmoothCamera",
        "SnapPosition",
        "SpeedBonus",
        "Test",
        "Time",
        "Velocity",
        "VelocityLimit",
        "Weapon"
    };

    public static string IdToString(int componentId) {
        return components[componentId];
    }
}

namespace Entitas {
    public partial class Matcher : AllOfMatcher {
        public Matcher(int index) : base(new [] { index }) {
        }

        public override string ToString() {
            return ComponentIds.IdToString(indices[0]);
        }
    }
}