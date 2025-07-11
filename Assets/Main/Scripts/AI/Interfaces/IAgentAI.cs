using Game.AI;

namespace Game.Core{
    public interface IAgentAI
    {
        public LocationNavigator Navigator { get; }
    }
}