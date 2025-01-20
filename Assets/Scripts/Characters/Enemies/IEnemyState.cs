namespace Enemies
{
    public interface IEnemyState
    {
        void OnActivate();
        void OnDeactivate();

        void OnUpdate();
    }
}
