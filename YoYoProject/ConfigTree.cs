using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoYoProject
{
    public sealed class ConfigTree
    {
        public const string DefaultName = "default";

        public Dictionary<string, Node> Nodes { get; }

        private Node active;
        public Node Active
        {
            get { return active ?? Default; }
            set { active = value; }
        }

        public Node Default => Nodes[DefaultName];

        public ConfigTree()
        {
            Nodes = new Dictionary<string, Node>();
            
            Add(DefaultName, null);
        }

        public Node Add(string name, Node parent)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            
            // TODO Validate name

            if (Nodes.ContainsKey(name))
            {
                throw new InvalidOperationException(
                    $"Cannot add a new config with the name '{name}' because one already exists."
                );
            }

            var node = new Node(name, parent);
            parent?.Children.Add(node);

            Nodes[name] = node;

            return node;
        }

        public Node Get(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return Nodes[name];
        }

        public IReadOnlyList<Node> GetForResource(Guid id)
        {
            return Nodes.Values.Where(x => x.Deltas.ContainsKey(id)).ToList();
        }

        public void Remove(string name)
        {
            throw new NotImplementedException();
        }

        // TODO Split
        internal List<string> Serialize()
        {
            var configs = Nodes.Values;
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

            public Dictionary<Guid, Dictionary<string, object>> Deltas { get; }

            public bool IsDefault { get; }

            public Node(string name, Node parent)
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));

                if (parent == null && name != DefaultName)
                    throw new ArgumentNullException(nameof(parent));

                Name = name;
                Parent = parent;
                Children = new List<Node>();
                Deltas = new Dictionary<Guid, Dictionary<string, object>>();

                // NOTE Cached instead of auto-property because this sits on a hotpath
                IsDefault = name == DefaultName;
            }
        }
    }
}