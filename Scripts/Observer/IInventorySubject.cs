public interface IInventorySubject
{
    void Subscribe(IInventoryObserver observer);
    void Unsubscribe(IInventoryObserver observer);
}
