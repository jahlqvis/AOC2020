using System;
using System.Collections.Generic;
using System.Collections; 
using System.Diagnostics;
using TreeCollections;

namespace Day7
{
    static class calculator
    {


        public static BagNodeRoot rootItem;
        


        static string[] testrules = new string[]
        {
            "light red bags contain 1 bright white bag, 2 muted yellow bags.",
            "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
            "bright white bags contain 1 shiny gold bag.",
            "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
            "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
            "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
            "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
            "faded blue bags contain no other bags.",
            "dotted black bags contain no other bags."
        };

        static public void populatetree()
        {

            rootItem = new BagNodeRoot();

            foreach (var rule in testrules)
            {
                parse(rule);

            }

        }

        static public void printtree()
        {
            rootItem.PrintTree();
        }

        static public void parse(string rule)
        {
            BagNode bag;
            
            string[] s = rule.Split(" bags contain ");

            string containerBagColor = s[0];

            string[] t = s[1].Split(", ");

            var current = rootItem.AddColor(containerBagColor);
                
            foreach (string c in t)
            {
                var d = c.Split(' ');
                int amt = 0;
                if(int.TryParse(d[0], out amt))
                {
                    string color = d[1] + " " + d[2];

                    for (int i = 0; i < amt; i++)
                        current.Add(color);

                }    
                    
            }

            
            
        }


    }


    class BagNodeRoot
    {
        BagNode _root = new BagNode("root");
        
        
        public BagNode AddColor(string value)
        {
            // Add nodes from string chars.
            BagNode current = this._root;
            current = current.Add(value);
            return current;
        }

        public bool ContainsColor(string value)
        {
            // Get existing nodes from string chars.
            BagNode current = this._root;
            
            current = current.Get(value);
            if (current == null)
            {
                return false;
            }
            
            // Return state.
            return current != null;
        }

        public void PrintTree()
        {
            _root.PrintBagNode(_root);
        }

        public void MoveColorsAtRoot()
        {
            for (int i = _root.Bags.Count - 1; i >= 0; i--)
            {
                var bag = _root.Bags[i];
                if (_root.MoveBagWithColorToNode(bag.Color, bag))
                    _root.Remove(bag.Color);
            }

        }
    }

    class BagNode
    {
        List<BagNode> _bags; 
        string _color;

        internal List<BagNode> Bags { get => _bags; }
        public string Color { get => _color; }

        public BagNode(string color)
        {
            _color = color;
        }

        public void Remove(string color)
        {
            
            for(int i = _bags.Count-1;i>=0;i--)
            {
                if (_bags[i]._color.Equals(color))
                    _bags.RemoveAt(i);
            }
            
        }

        public BagNode Add(string color)
        {
            // Add individual node as child.
            // ... Handle null field.
            if (this._bags == null)
            {
                this._bags = new List<BagNode>();
            }
            
            // Look up and return if possible.
            
            //foreach (var b in this._bags)
            //    if (b._color.Equals(color))
            //        return b;

            // Store.
            BagNode bag;
            bag = new BagNode(color);
            bag._color = color;
            this._bags.Add(bag);
            return bag;
        }

        public BagNode Get(string color)
        {
            // Get individual child node.
            if (this._bags == null)
            {
                return null;
            }
            
            foreach (var b in this._bags)
                if (b._color.Equals(color))
                    return b;
            
            return null;
        }

        
        public void PrintBagNode(BagNode bagnode)
        {
            Console.WriteLine($"Color: {bagnode._color}");

            if (bagnode._bags == null)
                return;

            foreach (var bag in bagnode._bags)
            {
                PrintBagNode(bag);
            }
        }

        public bool MoveBagWithColorToNode(string color, BagNode bagnode)
        {
            bool success = false;
            if (this._color.Equals(color))
            {
                if (this._bags == null)
                {
                    this._bags = new List<BagNode>();
                    this._bags.Add(bagnode);
                    success = true;
                }

                return success;
            }
            else
            {
                if(this._bags != null)
                {
                    
                    foreach (var bag in this._bags)
                    {
                        if (bag.MoveBagWithColorToNode(color, bagnode))
                            success=true;
                    }
                }
                return success;
            }
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            calculator.populatetree();
            calculator.rootItem.MoveColorsAtRoot();
            calculator.printtree();
        }
    }
}
