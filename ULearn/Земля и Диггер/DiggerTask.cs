using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Terrian : ICreature
    {
        public string GetImageFileName()
        {
            throw new NotImplementedException();
        }

        public int GetDrawingPriority()
        {
            throw new NotImplementedException();
        }

        public CreatureCommand Act(int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            throw new NotImplementedException();
        }
    }

    public class Player : ICreature
    {
        public string GetImageFileName()
        {
            throw new NotImplementedException();
        }

        public int GetDrawingPriority()
        {
            throw new NotImplementedException();
        }

        public CreatureCommand Act(int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            throw new NotImplementedException();
        }
    }
    
    //Напишите здесь классы Player, Terrain и другие.

   
}
