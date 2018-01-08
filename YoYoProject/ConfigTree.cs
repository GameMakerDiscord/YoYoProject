using System;
using System.Collections.Generic;
using System.Text;

namespace YoYoProject
{
    // TODO God Object - Needs responsibilities split
    public sealed class ConfigTree
    {
        public const string DefaultName = "default";

        public Node Default => nodes[DefaultName];

        public Dictionary<string, Node>.ValueCollection Configs => nodes.Values;

        private readonly Dictionary<string, Node> nodes;

        private readonly Dictionary<Node, ConfigDeltaLayer> layers;
        private readonly Stack<ConfigDeltaLayer> stack;

        public ConfigTree()
        {
            nodes = new Dictionary<string, Node>();
            layers = new Dictionary<Node, ConfigDeltaLayer>();
            stack = new Stack<ConfigDeltaLayer>();

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

        public T GetProperty<T>(Guid id, string propertyName, T value)
        {
            foreach (var config in stack)
            {
                Dictionary<string, object> deltas;
                if (!config.Deltas.TryGetValue(id, out deltas))
                    continue;

                object deltaValue;
                if (deltas.TryGetValue(propertyName, out deltaValue))
                    return (T)deltaValue;
            }

            return value;
        }

        public bool SetProperty<T>(Guid id, string propertyName, T value)
        {
            if (stack.Count <= 0)
                return false;

            var config = stack.Peek();

            Dictionary<string, object> deltas;
            if (!config.Deltas.TryGetValue(id, out deltas))
            {
                deltas = new Dictionary<string, object>();
                config.Deltas.Add(id, deltas);
            }

            deltas[propertyName] = value;

            return true;
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

        public void SetConfig(Node node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            stack.Clear();

            if (node == Default)
                return;
        
            // TODO Optimize
            var configChain = new List<Node>();
            for (var x = node; x != null; x = x.Parent)
                configChain.Insert(0, x);
            
            foreach (var config in configChain)
            {
                ConfigDeltaLayer layer;
                if (!layers.TryGetValue(config, out layer))
                {
                    layer = new ConfigDeltaLayer();
                    layers.Add(config, layer);
                }

                stack.Push(layer);
            }
        }

        public IReadOnlyList<Node> GetConfigDeltasForResource(Guid id)
        {
            var list = new List<Node>();

            foreach (var kvp in layers)
            {
                if (kvp.Value.Deltas.ContainsKey(id))
                    list.Add(kvp.Key);
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

        private class ConfigDeltaLayer
        {
            public readonly Dictionary<Guid, Dictionary<string, object>> Deltas;

            public ConfigDeltaLayer()
            {
                Deltas = new Dictionary<Guid, Dictionary<string, object>>();
            }
        }
    }
}