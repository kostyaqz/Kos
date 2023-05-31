using System.Windows.Forms;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public CreatureCommand Act(int x, int y)
        {
            var creatureCommand = new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = null
            };
            return creatureCommand;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    public class Player : ICreature
    {
        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            var act = new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = null
            };
            var key = Game.KeyPressed;

            switch (key)
            {
                case Keys.Left when Border.IsNoBorder(x, y, "Left") && !(Game.Map[x - 1, y] is Sack):
                    act.DeltaX = -1;
                    break;
                case Keys.Right when Border.IsNoBorder(x, y, "Right") && !(Game.Map[x + 1, y] is Sack):
                    act.DeltaX = 1;
                    break;
                case Keys.Up when Border.IsNoBorder(x, y, "Up") && !(Game.Map[x, y - 1] is Sack):
                    act.DeltaY = -1;
                    break;
                case Keys.Down when Border.IsNoBorder(x, y, "Down") && !(Game.Map[x, y + 1] is Sack):
                    act.DeltaY = 1;
                    break;
            }

            return act;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }
    }

    public abstract class Sack : ICreature
    {
        private int _counter;

        public string GetImageFileName()
        {
            return "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 10;
        }

        public CreatureCommand Act(int x, int y)
        {
            if (Border.IsNoBorder(x, y, "Down"))
            {
                var nextPoint = Game.Map[x, y + 1];

                if (nextPoint is null || (nextPoint is Player && _counter > 0) || (nextPoint is Monster && _counter > 0))
                {
                    _counter++;
                    return new CreatureCommand { DeltaY = 1 };
                }
            }

            if (_counter > 1)
            {
                _counter = 0;
                return new CreatureCommand { TransformTo = new Gold() };
            }

            _counter = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public CreatureCommand Act(int x, int y)
        {
            var act = new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = null
            };

            return act;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player) Game.Scores = Game.Scores + 10;
            return true;
        }
    }

    public class Monster : ICreature
    {
        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public CreatureCommand Act(int x, int y)
        {
            var playerPoint = PlayerLocation.GetPlayerPoint();
            var act = new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = null };

            if (playerPoint.x == -1 && playerPoint.y == -1) return act;

            var direction = MonsterWalking.GetMonsterDirection(x, y, playerPoint.x, playerPoint.y);

            switch (direction)
            {
                case "Left":
                    act.DeltaX = -1;
                    break;
                case "Right":
                    act.DeltaX = 1;
                    break;
                case "Up":
                    act.DeltaY = -1;
                    break;
                case "Down":
                    act.DeltaY = 1;
                    break;
                case "Stop":
                    break;
            }

            return act;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }
    }

    public class Border
    {
        public static bool IsNoBorder(int x, int y, string direction)
        {
            switch (direction)
            {
                case "Down":
                    return y < Game.MapHeight - 1;
                case "Up":
                    return y > 0;
                case "Left":
                    return x > 0;
                case "Right":
                    return x < Game.MapWidth - 1;
                default:
                    return false;
            }
        }
    }

    public abstract class PlayerLocation
    {

        public static (int x, int y) GetPlayerPoint()
        {
            for (var i = 0; i < Game.MapWidth; i++)
            for (var j = 0; j < Game.MapHeight; j++)
                if (Game.Map[i, j] is Player)
                    return (i, j);
            return (-1, -1 );
        }
    }

    public static class MonsterWalking
    {
        public static bool CanMonsterGo(int x, int y)
        {
            return !(Game.Map[x, y] is Terrain) && !(Game.Map[x, y] is Sack) && !(Game.Map[x, y] is Monster);
        }

        public static string GetMonsterDirection(int xMonster, int yMonster, int xPlayer, int yPlayer)
        {
            var direction = "";
            if (xPlayer == xMonster)
            {
                if (yPlayer < yMonster && CanMonsterGo(xMonster, yMonster - 1))
                    direction = "Up";
                else if (yPlayer > yMonster && CanMonsterGo(xMonster, yMonster + 1))
                    direction = "Down";
            }
            else
            {
                if (xPlayer < xMonster && CanMonsterGo(xMonster - 1, yMonster))
                    direction = "Left";
                else if (xPlayer > xMonster && CanMonsterGo(xMonster + 1, yMonster))
                    direction = "Right";
            }

            if (Border.IsNoBorder(xMonster, yMonster, direction) && direction != "")
                return direction;
            return "Stop";
        }
    }
}