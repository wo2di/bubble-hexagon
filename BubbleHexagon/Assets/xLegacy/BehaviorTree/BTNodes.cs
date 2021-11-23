using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public enum NodeState
    {
        FAILURE,
        SUCCESS,
        RUNNING
    }

    public abstract class Node
    {
        public delegate NodeState NodeReturn();
        protected NodeState m_nodeState;
        public NodeState nodeState
        {
            get { return m_nodeState; }
        }
        public Node() { }
        public abstract NodeState Evaluate();

    }

    public class Selector : Node
    {
        protected List<Node> m_nodes = new List<Node>();
        public Selector(List<Node> nodes)
        {
            m_nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            foreach(Node node in m_nodes)
            {
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:
                        break;
                    case NodeState.SUCCESS:
                        m_nodeState = NodeState.SUCCESS;
                        return m_nodeState;
                    case NodeState.RUNNING:
                        m_nodeState = NodeState.RUNNING;
                        return m_nodeState;
                    default:
                        break;
                }
            }
            m_nodeState = NodeState.FAILURE;
            return m_nodeState;
        }
    }

    public class Sequence : Node
    {
        protected List<Node> m_nodes = new List<Node>();
        public Sequence(List<Node> nodes)
        {
            m_nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            bool anyChildRunning = false;
            foreach (Node node in m_nodes)
            { 
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:
                        m_nodeState = NodeState.FAILURE;
                        return m_nodeState;
                    case NodeState.SUCCESS:
                        break;
                    case NodeState.RUNNING:
                        anyChildRunning = true;
                        break;
                    default:
                        m_nodeState = NodeState.SUCCESS;
                        return m_nodeState;
                }
            }
            m_nodeState = anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return m_nodeState; 
        }

    }

    public class Inverter : Node
    {
        private Node m_node;
        public Node node
        {
            get { return m_node; }
        }
        public Inverter(Node node)
        {
            m_node = node;
        }

        public override NodeState Evaluate()
        {
            switch(m_node.Evaluate())
            {
                case NodeState.FAILURE:
                    m_nodeState = NodeState.SUCCESS;
                    return m_nodeState;
                case NodeState.SUCCESS:
                    m_nodeState = NodeState.FAILURE;
                    return m_nodeState;
                case NodeState.RUNNING:
                    m_nodeState = NodeState.RUNNING;
                    return m_nodeState;
                default:
                    m_nodeState = NodeState.SUCCESS;
                    return m_nodeState;
            }
        }
    }

    public class ActionNode : Node
    {
        public delegate NodeState ActionNodeDelegate();
        private ActionNodeDelegate m_action;
        public ActionNode(ActionNodeDelegate action)
        {
            m_action = action;
        }
        public override NodeState Evaluate()
        {
            switch (m_action())
            {
                case NodeState.FAILURE:
                    m_nodeState = NodeState.FAILURE;
                    return m_nodeState;
                case NodeState.SUCCESS:
                    m_nodeState = NodeState.SUCCESS;
                    return m_nodeState;
                case NodeState.RUNNING:
                    m_nodeState = NodeState.RUNNING;
                    return m_nodeState;
                default:
                    m_nodeState = NodeState.FAILURE;
                    return m_nodeState;
            }
        }
    }
}


