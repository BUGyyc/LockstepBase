using System.Collections.Generic;
using Entitas;
using Lockstep.Common.Logging;

namespace Lockstep.Core.Logic.Systems.Debugging
{

    /// <summary>
    /// ?? 核实没有备份的数据
    /// </summary>
    public class VerifyNoDuplicateBackups : IExecuteSystem, ISystem
    {
        private readonly IGroup<GameEntity> _backups;

        public VerifyNoDuplicateBackups(Contexts contexts)
        {
            _backups = (contexts.game).GetGroup(GameMatcher.Backup);
        }

        public void Execute()
        {
            //TODO:
            Dictionary<uint, List<uint>> dictionary = new Dictionary<uint, List<uint>>();
            foreach (GameEntity backup in _backups)
            {
                if (dictionary.ContainsKey(backup.backup.tick))
                {
                    if (dictionary[backup.backup.tick].Contains(backup.backup.localEntityId))
                    {
                        Log.Warn(this, "Backup duplicate: " + dictionary[backup.backup.tick].Count + " backups in tick " + backup.backup.tick + " are already pointing to " + backup.backup.localEntityId);
                    }
                }
                else
                {
                    dictionary.Add(backup.backup.tick, new List<uint>());
                }
                dictionary[backup.backup.tick].Add(backup.backup.localEntityId);
            }
        }
    }
}
