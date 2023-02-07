using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Digger
{
    //Земля
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

    // Игрок, он же диггер
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
            return conflictedObject is Sack;
        }
    }

    // Мешок с золотом

    public class Sack : ICreature
    {
        private int counter;

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

                if (nextPoint is null || (nextPoint is Player && counter > 0) || (nextPoint is Monster && counter > 0))
                {
                    counter++;
                    return new CreatureCommand { DeltaY = 1 };
                }
            }
            if (counter > 1)
            {
                counter = 0;
                return new CreatureCommand() { TransformTo = new Gold() };
            }
            counter = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }


    // Золото

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
            if (conflictedObject is Player)
            {
                Game.Scores = Game.Scores + 10;
            }
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

        int [] playerPoint = PlayerLocation.PlayerPoint();
        public CreatureCommand Act(int x, int y)
        {
            var act = new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = null
            };

            if (playerPoint[0] == -1 && playerPoint[1] == -1)
            {
                return act;
            }

            //todo тут логика того как монстр движется к человеку

            var direction = MonsterWalking.MonsterDirection(x, y, playerPoint[0], playerPoint[1]);

            switch (direction)
            {
                case "Left": act.DeltaX = -1;
                    break;
                case "Right": act.DeltaX = 1;
                    break;
                case "Up": act.DeltaY = -1;
                    break;
                case "Down": act.DeltaY = 1;
                    break;
            }
            return act;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }

    //Класс для падения мешка и хождения диггера
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

    public class PlayerLocation
    {
        public static int [] PlayerPoint()
        {
            for (int i = 0; i < Game.MapHeight; i++)
            {
                for (int j = 0; j < Game.MapWidth; j++)
                {
                    if (Game.Map[i, j] is Player)
                    {
                        return new[] { i, j };
                    }
                }
            }
            return new[] { -1, -1 };
        }
    }

    public class MonsterWalking
    {
        public static bool CanMonsterGo(int x, int y)
        {
            if ((Game.Map[x, y] is Terrain) || (Game.Map[x, y] is Sack))
            {
                return false;
            }

            return true;
        }

        public static string MonsterDirection(int xMonster, int yMonster, int xPlayer, int yPlayer)
        {
            var distance = new Dictionary<double, string>();

            //Todo надо разобраться с x и y. Кажется, что тут не то передается, что должно. Путаются x и y монстра и человека
            if (Border.IsNoBorder(xMonster, yMonster, "Left") && CanMonsterGo(xMonster - 1, yMonster))
            {
                distance.Add(1000, "Left");
            }
            if (Border.IsNoBorder(xMonster, yMonster, "Right") && CanMonsterGo(xMonster + 1, yMonster))
            {
                distance.Add(1001, "Right");
            }
            if (Border.IsNoBorder(xMonster, yMonster, "Up") && CanMonsterGo(xMonster, yMonster - 1))
            {
                distance.Add(1002, "Up");
            }
            if (Border.IsNoBorder(xMonster, yMonster, "Down") && CanMonsterGo(xMonster, yMonster + 1))
            {
                distance.Add(1003, "Down");
            }



            foreach (var e in distance)
            {
                var oldValue = distance[e.Key];
                distance.Remove(e.Key);
                distance.Add(Math.Sqrt(Math.Pow(xPlayer - xMonster, 2) + Math.Pow(yPlayer - yMonster, 2)), oldValue);
            }

            return distance[distance.Keys.Min()];
        }

    }
}