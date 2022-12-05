namespace homework3;

public class StackNode<T>
{
    public readonly T Value;
    public StackNode<T>? NextNode;
    public StackNode(T value, StackNode<T>? nextNode)
    {
        Value = value;
        NextNode = nextNode;
    }
}