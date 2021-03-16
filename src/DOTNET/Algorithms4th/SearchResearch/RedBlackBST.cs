using System;

namespace SearchResearch
{
    public class RedBlackBST<TKey, TValue> : BinarySearchTree_Recur<TKey, TValue> where TKey : IComparable
    {
        private static readonly bool RED = true;
        private static readonly bool BLACK = false;

        public override void Put(TKey key, TValue value)
        {
            _root = Put(_root, key, value);
            _root.Color = BLACK;
        }

        private Node<TKey, TValue> Put(Node<TKey, TValue> h, TKey key, TValue value)
        {
            if (h == null)
            {
                return new Node<TKey, TValue>(key, value, 1, RED);
            }

            int cmp = key.CompareTo(h.Key);
            if (cmp < 0)
            {
                h.Left = Put(h.Left, key, value);
            }
            else if (cmp > 0)
            {
                h.Right = Put(h.Right, key, value);
            }
            else
            {
                h.Value = value;
            }

            if (IsRed(h.Right) && !IsRed(h.Left))
            {
                h = RotateLeft(h);
            }
            if (h.Left != null && IsRed(h.Left) && IsRed(h.Left.Left))
            {
                h = RotateRight(h);
            }
            if (IsRed(h.Left) && IsRed(h.Right))
            {
                FlipColors(h);
            }

            h.N = Size(h.Left) + Size(h.Right) + 1;

            return h;
        }

        public override void DeleteMin()
        {
            _root = DeleteMin(_root);
            if (!IsEmpty())
            {
                _root.Color = BLACK;
            }
        }

        private Node<TKey, TValue> DeleteMin(Node<TKey, TValue> h)
        {
            if (h.Left == null)
            {
                return null;
            }
            if (!IsRed(h.Left) && !IsRed(h.Left.Left))
            {
                h = MoveRedLeft(h);
            }
            h.Left = DeleteMin(h.Left);
            //删除完之后可能存在4-Node的情况，需要重新从下向上进行balance
            return Balance(h);
        }

        public override void DeleteMax()
        {
            _root = DeleteMax(_root);
            if (!IsEmpty())
            {
                _root.Color = BLACK;
            }
        }

        private Node<TKey, TValue> DeleteMax(Node<TKey, TValue> h)
        {
            //如果当前节点的左孩子是红色的话
            if (IsRed(h.Left))
            {
                //最下层节点是3节点，当前节点是黑色，左孩子是红色，通过一次RotateRight变换
                //将当前节点和左孩子调换，使当前节点变为左孩子的右孩子
                //这样直接删除右孩子就完成最大值的删除，同时保持树的平衡
                h = RotateRight(h);
            }
            if (h.Right == null)
            {
                return null;
            }
            //如果当前节点的右孩子不是红节点，且当前节点的右孩子的左孩子也不是红节点
            //如果当前节点的右孩子是2-node，需要通过MoveRedRight从父节点或者左节点借一个节点
            if (!IsRed(h.Right) && !IsRed(h.Right.Left))
            {
                h = MoveRedRight(h);
            }
            h.Right = DeleteMax(h.Right);
            //删除完之后可能存在4-Node的情况，需要重新从下向上进行balance
            return Balance(h);
        }

        public override void Delete(TKey key)
        {
            if (_root == null)
            {
                return;
            }
            if (!IsRed(_root.Left) && !IsRed(_root.Right))
            {
                _root.Color = RED;
            }
            _root = Delete(_root, key);
        }

        private Node<TKey, TValue> Delete(Node<TKey, TValue> h, TKey key)
        {
            if (h == null)
            {
                return null;
            }
            if (key.CompareTo(h.Key) < 0)
            {
                if (h.Left != null && !IsRed(h.Left) && !IsRed(h.Left.Left))
                {
                    h = MoveRedLeft(h);
                }
                h.Left = Delete(h.Left, key);
            }
            else
            {
                if (IsRed(h.Left))
                {
                    h = RotateRight(h);
                }
                if (key.CompareTo(h.Key) == 0 && h.Right == null)
                {
                    return null;
                }
                if (key.CompareTo(h.Key) == 0)
                {
                    Node<TKey, TValue> min = Min(h.Right);
                    h.Key = min.Key;
                    h.Value = min.Value;
                    h.Right = DeleteMin(h.Right);
                }
                else
                {
                    if (h.Right != null && !IsRed(h.Right) && !IsRed(h.Right.Left))
                    {
                        h = MoveRedRight(h);
                    }
                    h.Right = Delete(h.Right, key);
                }
            }
            return Balance(h);
        }

        private Node<TKey, TValue> MoveRedLeft(Node<TKey, TValue> h)
        {
            //通过颜色变换，将当前节点或者其子节点下沉
            //可以实现将3个2-node转换为1个4-node(Parent Node下沉)
            //或者实现其中一个Node下沉
            FlipColors(h);
            if (h.Right != null && IsRed(h.Right.Left))
            {
                //通过RotateRight和RotateLeft实现从右节点借一个节点过来的需求
                //实际上是将右节点的一个节点先借到父节点，然后将父节点的一个节点借到左节点
                h.Right = RotateRight(h.Right);
                h = RotateLeft(h);
            }
            return h;
        }

        private Node<TKey, TValue> MoveRedRight(Node<TKey, TValue> h)
        {
            //通过颜色变换，将当前节点或者其子节点下沉
            //可以实现将3个2-node转换为1个4-node(Parent Node下沉)
            //或者实现其中一个Node下沉
            FlipColors(h);
            //这里如果为true的话，一定是出现了当前节点的左孩子和左孩子的左孩子都为红节点，出现5-node
            if (h.Left != null && IsRed(h.Left.Left))
            {
                //通过RotateRight将左孩子和当前节点调换，使当前节点变为左孩子的右孩子
                h = RotateRight(h);
            }
            return h;
        }

        private Node<TKey, TValue> Balance(Node<TKey, TValue> h)
        {
            if (IsRed(h.Right) && !IsRed(h.Left))
            {
                h = RotateLeft(h);
            }
            if (h.Left != null && IsRed(h.Left) && IsRed(h.Left.Left))
            {
                h = RotateRight(h);
            }
            if (IsRed(h.Left) && IsRed(h.Right))
            {
                FlipColors(h);
            }

            return h;
        }

        private Node<TKey, TValue> RotateLeft(Node<TKey, TValue> h)
        {
            Node<TKey, TValue> x = h.Right;
            h.Right = x.Left;
            x.Left = h;
            x.Color = h.Color;
            h.Color = RED;
            x.N = h.N;
            h.N = 1 + Size(h.Left) + Size(h.Right);
            return x;
        }

        private Node<TKey, TValue> RotateRight(Node<TKey, TValue> h)
        {
            Node<TKey, TValue> x = h.Left;
            h.Left = x.Right;
            x.Right = h;
            x.Color = h.Color;
            h.Color = RED;
            x.N = h.N;
            h.N = 1 + Size(h.Left) + Size(h.Right);
            return x;
        }

        private void FlipColors(Node<TKey, TValue> h)
        {
            h.Color = !h.Color;
            if (h.Left != null)
            {
                h.Left.Color = !h.Left.Color;
            }
            if (h.Right != null)
            {
                h.Right.Color = !h.Right.Color;
            }
        }

        private int Size(Node<TKey, TValue> node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.N;
        }

        private bool IsRed(Node<TKey, TValue> x)
        {
            if (x == null)
            {
                return false;
            }
            return x.Color == RED;
        }
    }
}