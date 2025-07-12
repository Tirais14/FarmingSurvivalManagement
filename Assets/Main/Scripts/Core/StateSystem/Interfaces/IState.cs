#nullable enable
namespace Core
{
    public interface IState : IStateBase
    {
        void Execute();
    }
}
