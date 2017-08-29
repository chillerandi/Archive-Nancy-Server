using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domla.Archive.Nancy.Infrastructure
{
    public class Node
    {
        public Node()
        {
            Nodes_ = new List<Node>();
        }

        List<Node> Nodes_;

        public string Text { get; set; }

        public Node[] Nodes => Nodes_.ToArray();

        public Node AddOrIgnore(string name)
        {
            var result = Nodes_.FirstOrDefault(node => node.Text == name);
            if (result != null) return result;
            result = new Node { Text = name };
            Nodes_.Add(result);
            return result;
        }

        public static void AddOrIgnore(string directory, Node root)
        {
            var parts = directory.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var child = root.AddOrIgnore(parts[0]);
            if (parts.Length == 1) return;
            AddOrIgnore(string.Join("/", parts.Skip(1)), child);
        }
    }
}
