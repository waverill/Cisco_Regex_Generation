using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cisco_Regex_Generation
{
    class Node
    {
        public object data;
        public List<Node> children;
        public List<char> child_list;
        public Node(object data)
        {
            this.data = data;
            this.children = new List<Node>();
            this.child_list = new List<char>();
        }

        public void addChild(Node n)
        {
            this.children.Add(n);
        }
    }

    class Trie2
    {
        Node root;
        Node current;

        public Trie2(List<string> strings)
        {
            this.root = new Node(null);
            this.current = this.root;
            foreach (string s in strings)
            {
                foreach (char c in s)
                {
                    if (!this.current.child_list.Contains(c))
                    {
                        
                    }
                }
            }
        }
    }
}
