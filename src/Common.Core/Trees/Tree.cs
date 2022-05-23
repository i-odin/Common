namespace Common.Core.Trees
{
    public class Tree<TNode>
    {
        public TNode Root { get; set; }
    }

    public class BinaryTree : Tree<BinaryNode<int>>
    {
        /// <summary>
        /// Обход в глубину: сверху-вниз
        /// </summary>
        void PreOrder(BinaryNode<int> node)
        {
            if (node != null)
            {
                Procedyre(node);
                PreOrder(node.Left);
                PreOrder(node.Right);
            }
        }

        /// <summary>
        /// Обход в глубину: слева-направо
        /// </summary>
        void InOrder(BinaryNode<int> node)
        {
            if (node != null)
            {
                PreOrder(node.Left);
                Procedyre(node);
                PreOrder(node.Right);
            }
        }

        /// <summary>
        /// Обход в глубину: снизу-вверх
        /// </summary>
        void PostOrder(BinaryNode<int> node)
        {
            if (node != null)
            {
                PreOrder(node.Left);
                PreOrder(node.Right);
                Procedyre(node);
            }
        }

        /// <summary>
        /// Обработка дерева
        /// </summary>
        /// <param name="node"></param>
        void Procedyre(BinaryNode<int> node)
        {
        }

        /// <summary>
        /// Вставить элемент в упорядочное дерево
        /// </summary>
        void Insert(ref BinaryNode<int> current, BinaryNode<int> node)
        {
            if (current == null)
                current = node;
            else if (current.Data <= node.Data)
            {
                var left = current.Left;
                Insert(ref left, node);
            }
            else
            {
                var rigth = current.Right;
                Insert(ref rigth, node);
            }
        }

        void Delete(int data, ref BinaryNode<int> tree)
        {
            if (tree != null)
            {
                if (data < Convert.ToInt32(tree.Data))
                {
                    var left = tree.Left;
                    Delete(data, ref left);
                }
                else if (data > Convert.ToInt32(tree.Data))
                {
                    var right = tree.Right;
                    Delete(data, ref right);
                }
                else
                {
                    var q = tree;
                    if (q.Right == null)
                        tree = q.Left;
                    else if (q.Left == null)
                        tree = q.Right;
                    else
                    {
                        var left = q.Left;
                        Del(ref left, ref left);
                    }
                }
            }
            void Del(ref BinaryNode<int> r, ref BinaryNode<int> parent)
            {
                if (r.Right != null)
                {
                    var right = r.Right;
                    var q = parent;
                    Del(ref right, ref q);
                }
                else
                {
                    parent.Data = r.Data; parent = r; r = r.Left;
                }
            }
        }
    }
}