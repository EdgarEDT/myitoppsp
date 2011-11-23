
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Itop.Server.Interface.Tree;

namespace Itop.Client.Tree {
    /// <summary>
    /// 节点单击事件Delegate
    /// </summary>
    /// <typeparam name="NodeType">节点类型</typeparam>
    /// <param name="item">节点</param>
    /// <param name="tagData">节点附加数据</param>
    public delegate void AddClickEventHandler<NodeType>(NodeType item, TagData tagData);

    /// <summary>
    /// 树型结构生成器
    /// </summary>
    /// <typeparam name="NodeType">节点类型</typeparam>
    /// <typeparam name="RootType">根节点类型</typeparam>
    public abstract class TreeGenerator<NodeType, RootType> where NodeType : new() {
        private RootType m_root;

        private string m_textName;

        private string m_levelName;

        private bool m_genSeparator;

        private bool m_addClickEvent;

        /// <summary>
        /// 设置节点的附加数据
        /// </summary>
        /// <param name="item">节点</param>
        /// <param name="tagData">附加数据</param>
        protected abstract void SetTag(NodeType item, TagData tagData);
        
        /// <summary>
        /// 获取节点的附加数据
        /// </summary>
        /// <param name="item">节点</param>
        /// <returns>TagData</returns>
        protected abstract TagData GetTag(NodeType item);
        
        /// <summary>
        /// 增加子节点
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="child">子结点</param>
        protected abstract void AddNode(NodeType parent, NodeType child);
        
        /// <summary>
        /// 增加根节点
        /// </summary>
        /// <param name="child">要增加到根节点上去的子结点</param>
        protected abstract void AddRoot(NodeType child);
        
        /// <summary>
        /// 增加分隔符
        /// </summary>
        /// <param name="parent">父节点</param>
        protected abstract void AddSeparator(NodeType parent);

        /// <summary>
        /// 判断是否有直接上下级关系
        /// </summary>
        /// <param name="parent">上级</param>
        /// <param name="child">下级</param>
        /// <returns>true：有上下级，false：没有</returns>
        protected virtual bool IsSubLevel(string parent, string child) {
            return (child.Length - parent.Length == 2) && child.StartsWith(parent);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="root">根节点</param>
        /// <param name="textName">节点Text对应的字段名</param>
        /// <param name="levelName">节点级次对应的字段名</param>
        public TreeGenerator(RootType root, string textName, string levelName) {
            m_root = root;
            m_textName = textName;
            m_levelName = levelName;
        }

        /// <summary>
        /// 构建Tree
        /// </summary>
        /// <param name="dataTable">数据源</param>
        public void Execute(DataTable dataTable) {
            Stack<NodeType> stack = new Stack<NodeType>();
            using (DataTableReader reader = dataTable.CreateDataReader()) {
                while (reader.Read()) {
                    NodeType item = new NodeType();
                    TagData tagData = new TagData();
                    for (int i = 0; i < reader.FieldCount; i++)
                        tagData[reader.GetName(i)] = reader.GetValue(i);

                    SetTag(item, tagData);
                    bool isSeparator = tagData[TextName].ToString() == "-";
                    NodeType parentNode = (stack.Count != 0) ? stack.Peek() : item;
                    if (IsSubLevel(GetTag(parentNode)[m_levelName].ToString(), GetTag(item)[m_levelName].ToString())) {
                        if (m_genSeparator && isSeparator)
                            AddSeparator(parentNode);
                        else {
                            AddNode(parentNode, item);
                            if (m_addClickEvent && AddClickEventHandler != null) {
                                AddClickEventHandler(item, tagData);
                            }
                        }
                        stack.Push(item);
                    } else {
                        bool added = false;
                        while (stack.Count != 0) {
                            parentNode = stack.Peek();
                            if (IsSubLevel(GetTag(parentNode)[m_levelName].ToString(), GetTag(item)[m_levelName].ToString())) {
                                if (m_genSeparator && isSeparator)
                                    AddSeparator(parentNode);
                                else {
                                    AddNode(parentNode, item);
                                    if (m_addClickEvent && AddClickEventHandler != null) {
                                        AddClickEventHandler(item, tagData);
                                    }
                                }
                                stack.Push(item);
                                added = true;
                                break;
                            } else
                                stack.Pop();
                        }
                        if (!added) {
                            //此时stack已经为空，故视为顶层obj
                            AddRoot(item);
                            stack.Push(item);
                        }
                    }
                } //end of while
            } //end of using
        }

        /// <summary>
        /// 根节点
        /// </summary>
        public RootType Root {
            get { return m_root; }
        }

        /// <summary>
        /// 节点级次对应的字段名
        /// </summary>
        public string LevelName {
            get { return m_levelName; }
        }

        /// <summary>
        /// 节点Text对应的字段名
        /// </summary>
        public string TextName {
            get { return m_textName; }
        }

        /// <summary>
        /// 是否生成分隔符。true：生成，false：不生成。
        /// 主要用于构建Menu的时候
        /// </summary>
        public bool GenSeparator {
            get { return m_genSeparator; }
            set { m_genSeparator = value; }
        }

        /// <summary>
        /// 是否创建Click事件。true：创建，false：不创建。
        /// 当为true的时候，会检测AddClickEventHandler是否为null，
        /// AddClickEventHandler不为null，则会调用该delegate。
        /// </summary>
        public bool AddClickEvent {
            get { return m_addClickEvent; }
            set { m_addClickEvent = value; }
        }

        /// <summary>
        /// 增加Click事件的Delegate
        /// </summary>
        public AddClickEventHandler<NodeType> AddClickEventHandler;
    }
}