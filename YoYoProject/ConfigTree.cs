using System;
using System.Collections.Generic;
using System.Text;

namespace YoYoProject
{
    public sealed class ConfigTree
    {
        public const string DefaultName = "default";

        public Node Root => nodes[DefaultName];

        public Dictionary<string, Node>.ValueCollection Configs => nodes.Values;

        private readonly Dictionary<string, Node> nodes;

        public ConfigTree()
        {
            nodes = new Dictionary<string, Node>();
            Add(DefaultName, null);
        }

        public Node Add(string name, Node parent)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            
            // TODO Validate name

            if (nodes.ContainsKey(name))
            {
                throw new InvalidOperationException(
                    $"Cannot add a new config with the name '{name}' because one already exists."
                );
            }

            var node = new Node(name, parent);
            parent?.Children.Add(node);

            nodes[name] = node;

            return node;
        }

        public Node Get(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return nodes[name];
        }

        public void Remove(string name)
        {
            throw new NotImplementedException();
        }

        internal List<string> Serialize()
        {
            var configs = Configs;
            var list = new List<string>(configs.Count);

            foreach (var config in configs)
            {
                var builder = new StringBuilder();
                var node = config;
                while (true)
                {
                    builder.Insert(0, node.Name);
                    if (node.Parent != null)
                    {
                        builder.Insert(0, ';');
                        node = node.Parent;
                    }
                    else
                        break;
                }

                list.Add(builder.ToString());
            }

            return list;
        }

        public sealed class Node
        {
            public string Name { get; set; }

            public Node Parent { get; }

            public List<Node> Children { get; }

            public Node(string name, Node parent)
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));

                if (parent == null && name != DefaultName)
                    throw new ArgumentNullException(nameof(parent));

                Name = name;
                Parent = parent;
                Children = new List<Node>();
            }
        }
    }
}