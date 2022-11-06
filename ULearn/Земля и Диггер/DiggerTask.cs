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

            if (key == Keys.Left && x > 0)
                act.DeltaX = -1;
            if (key == Keys.Right && x < Game.MapWidth - 1)
                act.DeltaX = 1;
            if (key == Keys.Up && y > 0)
                act.DeltaY = -1;
            if (key == Keys.Down && y < Game.MapHeight - 1)
                act.DeltaY = 1;
            return act;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
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

            //Game.Map[x, y].toString() == "<имя файла>.png" или Game.Map (is или ==) <имя объекта>

            var nextPosition = act;
            //var map = Game.Map[x, y + 1];

            if (Game.KeyPressed == Keys.Up && !Block.IsYBorder(y) && !Block.IsBarrier(x, y))
                act.DeltaY = 1;

            return act;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
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

    //Класс для падения мешка
    public class Block
    {
        public static bool IsYBorder(int y)
        {
            return y < Game.MapHeight - 1;
        }
        public static bool IsBarrier(int x, int y)
        {
            if (Game.Map[x, y + 1] is Player)
                return true;
            if (Game.Map[x, y + 1] is Gold)
                return true;
            if (Game.Map[x, y + 1] is Terrain)
                return true;
            return false;
        }
    }
}