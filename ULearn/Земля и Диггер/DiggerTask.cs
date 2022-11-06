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

			if (key == Keys.Left && x > 0)
				act.DeltaX = -1;
			else if (key == Keys.Right && x < Game.MapWidth - 1)
				act.DeltaX = 1;
			else if (key == Keys.Up && y > 0)
				act.DeltaY = -1;
			else if (key == Keys.Down && y < Game.MapHeight - 1) 
				act.DeltaY = 1;
			return act;
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{
			return false;
		}
	}
}