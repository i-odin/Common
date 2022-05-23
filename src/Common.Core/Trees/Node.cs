namespace Common.Core.Trees
{
    public class Node<TData>
    {
        public TData Data { get; set; }
    }

    public class TreeNode<TData> : Node<TData>
    {
        public ICollection<TreeNode<TData>> Nodes { get; set; }
    }

    public class BinaryNode<TData> : Node<TData>
    {
        public BinaryNode<TData> Left { get; set; }
        public BinaryNode<TData> Right { get; set; }
    }
}
