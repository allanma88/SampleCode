<Query Kind="Program" />

void Main()
{
	char[,] board = new char[,]
	{
		{'5','3','.','.','7','.','.','.','.'},
		{'6','.','.','1','9','5','.','.','.'},
		{'.','9','8','.','.','.','.','6','.'},
		{'8','.','.','.','6','.','.','.','3'},
		{'4','.','.','8','.','3','.','.','1'},
		{'7','.','.','.','2','.','.','.','6'},
		{'.','6','.','.','.','.','2','8','.'},
		{'.','.','.','4','1','9','.','.','5'},
		{'.','.','.','.','8','.','.','7','9'}
	};
	Sudoku s = new Sudoku();
	s.Solve(board);
	board.Dump();
}

// Define other methods and classes here
public class Sudoku
{
	public void Solve(char[,] board)
	{
		Dictionary<int, RowHeader> headers = new Dictionary<int, RowHeader>();
		int[,] matrix = ConvertToMatrix(board, headers);
		DanceLink danceLink = new DanceLink();
		var rows = danceLink.Dance(matrix);
		FillCells(board, rows, headers);
	}
	
	public int[,] ConvertToMatrix(char[,] board, Dictionary<int, RowHeader> headers)
	{
		var len = board.GetLength(0);
		int[,] matrix = new int[len * len * len, 4 * len * len];
		for (int row = 0; row < len; row++)
		{
			for (int col = 0; col < len; col++)
			{
				if (board[row, col] == '.')
				{
					for (int n = 1; n <= len; n++)
					{
						var positions = GetPosition(row + 1, col + 1, n, len);
						int r = positions.Key;
						foreach (var c in positions.Value)
						{
							matrix[r, c] = 1;
						}
						headers[r] = new RowHeader(row, col, n);
					}
				}
				else
				{
					int n = int.Parse(board[row, col].ToString());
					var positions = GetPosition(row + 1, col + 1, n, len);
					int r = positions.Key;
					foreach (var c in positions.Value)
					{
						matrix[r, c] = 1;
					}
				}
			}
		}
		return matrix;
	}
	
	private KeyValuePair<int, List<int>> GetPosition(int row, int col, int n, int len)
	{
		int r = (row - 1) * len * len + (col - 1) * len + n;
		List<int> cols = new List<int>();
		//cells
		int c = (row - 1) * len + col;
		cols.Add(c - 1);
		//rows
		c = len * len + (row - 1) * len + n;
		cols.Add(c - 1);
		//columns
		c = 2 * len * len + (col - 1) * len + n;
		cols.Add(c - 1);
		//regions
		int sqrt = (int)Math.Sqrt(len);
		c = 3 * len * len + ((col - 1) / sqrt) * len + ((row - 1) / sqrt) * sqrt * len + n;
		cols.Add(c - 1);
		var positions = new KeyValuePair<int, List<int>>(r - 1, cols);
		return positions;
	}
	
	private void FillCells(char[,] board, List<int> rows, Dictionary<int, RowHeader> headers)
	{
		foreach (var row in rows)
		{
			if (headers.ContainsKey(row))
			{
				var rowHeader = headers[row];
				board[rowHeader.Row, rowHeader.Col] = rowHeader.N.ToString()[0];
			}
		}
	}
}

public class RowHeader
{
	public RowHeader(int row, int col, int n)
	{
		Row = row;
		Col = col;
		N = n;
	}

	public int Row { get; set; }

	public int Col { get; set; }

	public int N { get; set; }
}

public class DanceLink
	{
		Dictionary<int, Node> _cache = new Dictionary<int, Node>();
		// List<List<Node>> _rowList = new List<List<Node>>();
		List<Node> _rows = null;

		public List<int> Dance(int[,] matrix)
		{
			Node head = ConvertToNode(matrix);
			// Assert(head);
			Stack<Node> rowNodes = new Stack<Node>();
			Search(head, 0, rowNodes);
			List<int> rows = new List<int>();
			foreach (var n in _rows)
			{
				rows.Add(n.Row);
			}
			return rows;
		}

		private void Search(Node head, int k, Stack<Node> rowNodes)
		{
			if (_rows != null)
			{
				return;
			}
			if (head.Right == head)
			{
				List<Node> rows = new List<Node>();
				foreach (var node in rowNodes)
				{
					rows.Add(node);
				}
				_rows = rows;
				return;
			}
			var column = ChooseColumn(head);
			Cover(column);
			for (Node down = column.Down; down != column; down = down.Down)
			{
				rowNodes.Push(down);
				for (Node right = down.Right; right != down; right = right.Right)
				{
					Cover(right.Header);
				}
				Search(head, k + 1, rowNodes);
				var row = rowNodes.Pop();
				for (Node left = row.Left; left != row; left = left.Left)
				{
					Uncover(left.Header);
				}
			}
			Uncover(column);
			// return nodes;
		}

		private void Cover(Node column)
		{
			column.Right.Left = column.Left;
			column.Left.Right = column.Right;
			for (Node down = column.Down; down != column; down = down.Down)
			{
				for (Node right = down.Right; right != down; right = right.Right)
				{
					right.Up.Down = right.Down;
					right.Down.Up = right.Up;

					right.Header.Count--;
				}
			}
		}

		private void Uncover(Node column)
		{
			for (Node up = column.Up; up != column; up = up.Up)
			{
				for (Node left = up.Left; left != up; left = left.Left)
				{
					left.Up.Down = left;
					left.Down.Up = left;

					left.Header.Count++;
				}
			}
			column.Right.Left = column;
			column.Left.Right = column;
		}

		private Node ChooseColumn(Node head)
		{
			int count = int.MaxValue;
			Node min = head;
			for (var node = head.Right; node != head; node = node.Right)
			{
				if (node.Count < count)
				{
					count = node.Count;
					min = node;
				}
			}
			return min;
		}

		private Node ConvertToNode(int[,] matrix)
		{
			Node head = new Node(-1, -1);
			Node hLink = head;
			int rowCount = matrix.GetLength(0);
			int columnCount = matrix.GetLength(1);
			HashSet<int> rowSet = new HashSet<int>();
			for (int column = 0; column < columnCount; column++)
			{
				Node headerNode = GetOrAdd(-1, column, columnCount);
				hLink.Right = headerNode;
				headerNode.Left = hLink;

				head.Left = headerNode;
				headerNode.Right = head;

				var vLink = headerNode;
				int count = 0;
				for (int row = 0; row < rowCount; row++)
				{
					if (matrix[row, column] == 1)
					{
						Node downNode = GetOrAdd(row, column, columnCount);
						downNode.Header = headerNode;

						vLink.Down = downNode;
						downNode.Up = vLink;

						headerNode.Up = downNode;
						downNode.Down = headerNode;

						if (!rowSet.Contains(row))
						{
							var rLink = downNode;
							int c = column;
							while (true)
							{
								while (++c < columnCount && matrix[row, c] != 1) { }
								if (c >= columnCount)
								{
									break;
								}
								Node rightNode = GetOrAdd(row, c, columnCount);
								rLink.Right = rightNode;
								rightNode.Left = rLink;

								downNode.Left = rightNode;
								rightNode.Right = downNode;

								rLink = rLink.Right;
							}
							rowSet.Add(row);
						}
						vLink = vLink.Down;
						count++;
					}
				}
				headerNode.Count = count;
				hLink = hLink.Right;
			}
			return head;
		}

		private Node GetOrAdd(int row, int column, int columnCount)
		{
			int index = row * columnCount + column;
			if (!_cache.ContainsKey(index))
			{
				Node n = new Node(row, column);
				n.Left = n;
				n.Right = n;
				n.Up = n;
				n.Down = n;
				_cache.Add(index, n);
			}
			return _cache[index];
		}

		private void Assert(Node head)
		{
			Node link = head;
			var node = link.Right;
			Assert(node.Count == 2);
			Assert(node.Column == 0 && node.Row == -1);
			Assert(node.Up.Column == 0 && node.Up.Row == 4);
			node = node.Down;
			Assert(node.Column == 0 && node.Row == 0);
			Assert(node.Left.Column == 3 && node.Left.Row == 0);
			node = node.Right;
			Assert(node.Column == 3 && node.Row == 0);
			Assert(node.Right.Column == 0 && node.Right.Row == 0);
			node = node.Down;
			Assert(node.Column == 3 && node.Row == 4);
			node = node.Left;
			Assert(node.Column == 0 && node.Row == 4);
			Assert(node.Down.Column == 0 && node.Down.Row == -1);

			link = link.Right;
			node = link.Right;
			Assert(node.Count == 1);
			Assert(node.Column == 1 && node.Row == -1);
			Assert(node.Up.Column == 1 && node.Up.Row == 1);
			node = node.Down;
			Assert(node.Column == 1 && node.Row == 1);
			Assert(node.Left.Column == 5 && node.Left.Row == 1);
			Assert(node.Down.Column == 1 && node.Down.Row == -1);
			node = node.Right;
			Assert(node.Column == 5 && node.Row == 1);
			Assert(node.Right.Column == 1 && node.Right.Row == 1);

			link = link.Right;
			node = link.Right;
			Assert(node.Count == 1);
			Assert(node.Column == 2 && node.Row == -1);
			node = node.Down;
			Assert(node.Column == 2 && node.Row == 2);

			link = link.Right;
			node = link.Right;
			Assert(node.Count == 2);
			Assert(node.Column == 3 && node.Row == -1);
			node = node.Down;
			Assert(node.Column == 3 && node.Row == 0);
			node = node.Left;
			Assert(node.Column == 0 && node.Row == 0);
			node = node.Down;
			Assert(node.Column == 0 && node.Row == 4);
			node = node.Right;
			Assert(node.Column == 3 && node.Row == 4);

			link = link.Right;
			node = link.Right;
			Assert(node.Count == 1);
			Assert(node.Column == 4 && node.Row == -1);
			node = node.Down;
			Assert(node.Column == 4 && node.Row == 3);

			link = link.Right;
			node = link.Right;
			Assert(node.Count == 1);
			Assert(node.Column == 5 && node.Row == -1);
			node = node.Down;
			Assert(node.Column == 5 && node.Row == 1);

			System.Console.WriteLine("success");
		}

		private void Assert(bool b)
		{
			if (!b)
			{
				throw new Exception();
			}
		}
	}

	public class Node
	{
		public Node(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public Node Up { get; set; }

		public Node Down { get; set; }

		public Node Left { get; set; }

		public Node Right { get; set; }

		public Node Header { get; set; }

		public int Column { get; set; }

		public int Row { get; set; }

		public int Count { get; set; }
	}