#nullable enable
using Core.Map;

namespace Core
{
    public class BuildMode : IGameMode
    {
        private readonly ILocation location;

        public BuildMode(ILocation location)
        {
            this.location = location;
        }

        public void Execute() => throw new System.NotImplementedException();
    }
}
