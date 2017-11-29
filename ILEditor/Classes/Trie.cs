/*
 * A "trie" structure implementation as seen on:
 * https://visualstudiomagazine.com/articles/2015/10/20/text-pattern-search-trie-class-net.aspx
 * See https://en.wikipedia.org/wiki/Trie for more information.
 * 
 * In short is a prefix search tree usefull to find words for autocomplete.
 * 
 * Adapted for IDLE by Santiago Ottonello <sanotto@gmail.com>
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
	public class TrieNode
	{
		public char Value { get; set; }
		public List<TrieNode> Children { get; set; }
		public TrieNode Parent { get; set; }
		public int Depth { get; set; }

		public TrieNode(char value, int depth, TrieNode parent)
		{
			Value = value;
			Children = new List<TrieNode>();
			Depth = depth;
			Parent = parent;
		}

		public bool IsLeaf()
		{
			return Children.Count == 0;
		}

		public TrieNode FindChildTrieNode(char c)
		{
			foreach (var child in Children)
				if (child.Value == c)
					return child;

			return null;
		}

		public void DeleteChildTrieNode(char c)
		{
			for (var i = 0; i < Children.Count; i++)
				if (Children[i].Value == c)
					Children.RemoveAt(i);
		}
	}

	public class Trie
	{
		private readonly TrieNode _root;

		public Trie()
		{
			_root = new TrieNode('^', 0, null);
		}

		public TrieNode Prefix(string s)
		{
			var currentTrieNode = _root;
			var result = currentTrieNode;

			foreach (var c in s)
			{
				currentTrieNode = currentTrieNode.FindChildTrieNode(c);
				if (currentTrieNode == null)
					break;
				result = currentTrieNode;
			}

			return result;
		}

		public bool Search(string s)
		{
			var prefix = Prefix(s);
			return prefix.Depth == s.Length && prefix.FindChildTrieNode('$') != null;
		}

		public void InsertRange(List<string> items)
		{
			for (int i = 0; i < items.Count; i++)
				Insert(items[i]);
		}

		public void Insert(string s)
		{
			var commonPrefix = Prefix(s);
			var current = commonPrefix;

			for (var i = current.Depth; i < s.Length; i++)
			{
				var newTrieNode = new TrieNode(s[i], current.Depth + 1, current);
				current.Children.Add(newTrieNode);
				current = newTrieNode;
			}

			current.Children.Add(new TrieNode('$', current.Depth + 1, current));
		}

		public void Delete(string s)
		{
			if (Search(s))
			{
				var TrieNode = Prefix(s).FindChildTrieNode('$');

				while (TrieNode.IsLeaf())
				{
					var parent = TrieNode.Parent;
					parent.DeleteChildTrieNode(TrieNode.Value);
					TrieNode = parent;
				}
			}
		}

	}
}