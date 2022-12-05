namespace homework3;

internal class MyConcurrentStack<T>: IStack<T>
{
    private StackNode<T>? _head;
    private int _count;
    
    public void Push(T item)
    { 
        var newHeadNode = new StackNode<T>(item, _head);
        var spinWait = new SpinWait();

        while (true)
        {
            var headNode = _head;
            newHeadNode.NextNode = headNode;
            if (Interlocked.CompareExchange(ref _head, newHeadNode, headNode) == headNode)
            {
                break;
            }
            
            spinWait.SpinOnce();
        }
        Interlocked.Increment(ref _count);
    }

    public bool TryPop(out T? item) 
    {
        var spinWait = new SpinWait();
        while (true)
        {
            var headNode = _head;
            if (headNode is null)
            {
                item = default;
                return false;
            }
 
            if (Interlocked.CompareExchange(ref _head, headNode.NextNode, headNode) == headNode)
            {
                item = headNode.Value;
                Interlocked.Decrement(ref _count);
                return true;
            }
            spinWait.SpinOnce();
        }
    }

    public int Count => _count;
}