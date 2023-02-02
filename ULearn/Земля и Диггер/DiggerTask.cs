﻿using System;
using System.Windows.Forms;
using System.Windows.Media;

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

            if (key == Keys.Left && Block.IsNoBorder(x, y, "Left"))
                act.DeltaX = -1;
            if (key == Keys.Right && Block.IsNoBorder(x, y, "Right") && !(Game.Map[x + 1, y] is Sack))
                act.DeltaX = 1;
            if (key == Keys.Up && Block.IsNoBorder(x, y, "Up"))
                act.DeltaY = -1;
            if (key == Keys.Down && Block.IsNoBorder(x, y, "Down"))
                act.DeltaY = 1;
            return act;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack)
            {
                return true;
            }
            return false;
        }
    }

    // Мешок с золотом

    public class Sack : ICreature
    {
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
            var act = new CreatureCommand()
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = null
            };

            if (Block.IsNoBorder(x, y, "Down") && Block.IsEmpty(x, y))
            {
                act.DeltaY = 1;
                if (Block.CanTransformToGold(x, y))
                {
                    act.TransformTo = new Gold();
                }
            }

            //Можно считать, что мешок падал достаточно для превращения в золото, если над нам 2 и более клеток пустые
            return act;
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
            var act = new CreatureCommand()
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = null
            };

            return act;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }


    //Обработка движений должна выехать отдельно
    // static Movements

    //Класс для падения мешка и хождения диггера
    public class Block
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
        public static bool IsEmpty(int x, int y)
        {
            return Game.Map[x, y + 1] is null;
        }

        public static bool CanTransformToGold(int x, int y)
        {
            if (y > 1)
            {
                if (Game.Map[x, y - 1] is Player )
                    return false;
                if (Game.Map[x, y - 1] is Gold)
                    return false;
                if (Game.Map[x, y - 1] is Terrain)
                    return false;
                if (Game.Map[x, y - 1] is Sack)
                    return false;
                if (Game.Map[x, y - 1] is Terrain)
                    return false;
                return true;
            }
            return false;
        }


        public static bool IsSomething(int x, int y, string some, string direction)
        {
            switch (direction)
            {
                case "Down":
                    return Game.Map[x, y + 1].ToString() == some;
                case "Up":
                    return Game.Map[x, y - 1].ToString() == some;
                case "Left":
                    return Game.Map[x - 1, y].ToString() == some;
                case "Right":
                    return Game.Map[x + 1, y].ToString() == some;
                default:
                    return false;
            }
        }
    }


//
    public class DeathRule
    {

    }
}