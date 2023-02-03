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

            if (key == Keys.Left && Block.IsNoBorder(x, y, "Left") && !(Game.Map[x - 1, y] is Sack))
                act.DeltaX = -1;
            if (key == Keys.Right && Block.IsNoBorder(x, y, "Right") && !(Game.Map[x + 1, y] is Sack))
                act.DeltaX = 1;
            if (key == Keys.Up && Block.IsNoBorder(x, y, "Up") && !(Game.Map[x, y - 1] is Sack))
                act.DeltaY = -1;
            if (key == Keys.Down && Block.IsNoBorder(x, y, "Down") && !(Game.Map[x, y + 1] is Sack))
                act.DeltaY = 1;
            return act;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack) return true;

            return false;
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
            if (Block.IsNoBorder(x, y, "Down"))
            {
                var nextPoint = Game.Map[x, y + 1];

                if (nextPoint is null || (nextPoint is Player && counter > 0))
                {
                    counter++;
                    return new CreatureCommand { DeltaY = 1 };
                }
            }

            if (counter > 1) return new CreatureCommand { TransformTo = new Gold() };

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
            return true;
        }
    }

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
    }
}