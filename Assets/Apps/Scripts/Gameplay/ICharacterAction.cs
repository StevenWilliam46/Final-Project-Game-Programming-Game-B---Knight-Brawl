namespace Apps.Scripts
{
    public interface ICharacterAction<T>
    {
        void Execute(T info);
    }
}