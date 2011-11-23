
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Itop.Server.Interface.Tree;

namespace Itop.Client.Tree {
    /// <summary>
    /// �ڵ㵥���¼�Delegate
    /// </summary>
    /// <typeparam name="NodeType">�ڵ�����</typeparam>
    /// <param name="item">�ڵ�</param>
    /// <param name="tagData">�ڵ㸽������</param>
    public delegate void AddClickEventHandler<NodeType>(NodeType item, TagData tagData);

    /// <summary>
    /// ���ͽṹ������
    /// </summary>
    /// <typeparam name="NodeType">�ڵ�����</typeparam>
    /// <typeparam name="RootType">���ڵ�����</typeparam>
    public abstract class TreeGenerator<NodeType, RootType> where NodeType : new() {
        private RootType m_root;

        private string m_textName;

        private string m_levelName;

        private bool m_genSeparator;

        private bool m_addClickEvent;

        /// <summary>
        /// ���ýڵ�ĸ�������
        /// </summary>
        /// <param name="item">�ڵ�</param>
        /// <param name="tagData">��������</param>
        protected abstract void SetTag(NodeType item, TagData tagData);
        
        /// <summary>
        /// ��ȡ�ڵ�ĸ�������
        /// </summary>
        /// <param name="item">�ڵ�</param>
        /// <returns>TagData</returns>
        protected abstract TagData GetTag(NodeType item);
        
        /// <summary>
        /// �����ӽڵ�
        /// </summary>
        /// <param name="parent">���ڵ�</param>
        /// <param name="child">�ӽ��</param>
        protected abstract void AddNode(NodeType parent, NodeType child);
        
        /// <summary>
        /// ���Ӹ��ڵ�
        /// </summary>
        /// <param name="child">Ҫ���ӵ����ڵ���ȥ���ӽ��</param>
        protected abstract void AddRoot(NodeType child);
        
        /// <summary>
        /// ���ӷָ���
        /// </summary>
        /// <param name="parent">���ڵ�</param>
        protected abstract void AddSeparator(NodeType parent);

        /// <summary>
        /// �ж��Ƿ���ֱ�����¼���ϵ
        /// </summary>
        /// <param name="parent">�ϼ�</param>
        /// <param name="child">�¼�</param>
        /// <returns>true�������¼���false��û��</returns>
        protected virtual bool IsSubLevel(string parent, string child) {
            return (child.Length - parent.Length == 2) && child.StartsWith(parent);
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="root">���ڵ�</param>
        /// <param name="textName">�ڵ�Text��Ӧ���ֶ���</param>
        /// <param name="levelName">�ڵ㼶�ζ�Ӧ���ֶ���</param>
        public TreeGenerator(RootType root, string textName, string levelName) {
            m_root = root;
            m_textName = textName;
            m_levelName = levelName;
        }

        /// <summary>
        /// ����Tree
        /// </summary>
        /// <param name="dataTable">����Դ</param>
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
                            //��ʱstack�Ѿ�Ϊ�գ�����Ϊ����obj
                            AddRoot(item);
                            stack.Push(item);
                        }
                    }
                } //end of while
            } //end of using
        }

        /// <summary>
        /// ���ڵ�
        /// </summary>
        public RootType Root {
            get { return m_root; }
        }

        /// <summary>
        /// �ڵ㼶�ζ�Ӧ���ֶ���
        /// </summary>
        public string LevelName {
            get { return m_levelName; }
        }

        /// <summary>
        /// �ڵ�Text��Ӧ���ֶ���
        /// </summary>
        public string TextName {
            get { return m_textName; }
        }

        /// <summary>
        /// �Ƿ����ɷָ�����true�����ɣ�false�������ɡ�
        /// ��Ҫ���ڹ���Menu��ʱ��
        /// </summary>
        public bool GenSeparator {
            get { return m_genSeparator; }
            set { m_genSeparator = value; }
        }

        /// <summary>
        /// �Ƿ񴴽�Click�¼���true��������false����������
        /// ��Ϊtrue��ʱ�򣬻���AddClickEventHandler�Ƿ�Ϊnull��
        /// AddClickEventHandler��Ϊnull�������ø�delegate��
        /// </summary>
        public bool AddClickEvent {
            get { return m_addClickEvent; }
            set { m_addClickEvent = value; }
        }

        /// <summary>
        /// ����Click�¼���Delegate
        /// </summary>
        public AddClickEventHandler<NodeType> AddClickEventHandler;
    }
}