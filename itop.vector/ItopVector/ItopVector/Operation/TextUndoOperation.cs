namespace ItopVector.DrawArea
{
    using ItopVector.Core.UnDo;
    using System;

    internal class TextUndoOperation : IUndoOperation
    {
        // Methods
        internal TextUndoOperation(TextAction action, int index, CharInfo[] infos, ItopVector.DrawArea.TextOperation textoperation)
        {
            this.Action = action;
            this.CharIndex = index;
            this.CharInfos = infos;
            this.TextOperation = textoperation;
        }

        public void Redo()
        {
            CharInfo[] infoArray2;
            int num3;
            switch (this.Action)
            {
                case TextAction.Insert:
                {
                    int num1 = this.CharIndex;
                    CharInfo[] infoArray1 = this.CharInfos;
                    for (int num2 = 0; num2 < infoArray1.Length; num2++)
                    {
                        CharInfo info1 = infoArray1[num2];
                        if (num1 >= this.TextOperation.chars.Count)
                        {
                            this.TextOperation.chars.Add(info1);
                        }
                        else
                        {
                            this.TextOperation.chars.Insert(num1, info1);
                        }
                        if (info1.TextNode.ParentNode != info1.TextSvgElement)
                        {
                            if (info1.PreSibling == null)
                            {
                                info1.TextSvgElement.PrependChild(info1.TextNode);
                            }
                            else
                            {
                                info1.TextSvgElement.InsertAfter(info1.TextNode, info1.PreSibling);
                            }
                        }
                        num1++;
                    }
                    this.TextOperation.caretindex = num1;
                    return;
                }
                case TextAction.Delete:
                {
                    infoArray2 = this.CharInfos;
                    num3 = 0;
                    goto Label_0128;
                }
                default:
                {
                    return;
                }
            }
        Label_0128:
            if (num3 < infoArray2.Length)
            {
                CharInfo info2 = infoArray2[num3];
                this.TextOperation.chars.Remove(info2);
                if (info2.TextNode.ParentNode != null)
                {
                    info2.TextNode.ParentNode.RemoveChild(info2.TextNode);
                }
                num3++;
                goto Label_0128;
            }
        }

        public void Undo()
        {
            switch (this.Action)
            {
                case TextAction.Insert:
                {
                    CharInfo[] infoArray1 = this.CharInfos;
                    for (int num2 = 0; num2 < infoArray1.Length; num2++)
                    {
                        CharInfo info1 = infoArray1[num2];
                        this.TextOperation.chars.Remove(info1);
                        if (info1.TextNode.ParentNode != null)
                        {
                            info1.TextNode.ParentNode.RemoveChild(info1.TextNode);
                        }
                    }
                    this.TextOperation.caretindex = this.CharIndex;
                    return;
                }
                case TextAction.Delete:
                {
                    int num1 = this.CharIndex;
                    CharInfo[] infoArray2 = this.CharInfos;
                    for (int num3 = 0; num3 < infoArray2.Length; num3++)
                    {
                        CharInfo info2 = infoArray2[num3];
                        if (num1 >= this.TextOperation.chars.Count)
                        {
                            this.TextOperation.chars.Add(info2);
                        }
                        else
                        {
                            this.TextOperation.chars.Insert(num1, info2);
                        }
                        if (info2.PreSibling != null)
                        {
                            info2.TextSvgElement.InsertAfter(info2.TextNode, info2.PreSibling);
                        }
                        else
                        {
                            info2.TextSvgElement.AppendChild(info2.TextNode);
                        }
                        num1++;
                    }
                    this.TextOperation.caretindex = num1;
                    return;
                }
            }
        }


        // Fields
        public TextAction Action;
        public int CharIndex;
        public CharInfo[] CharInfos;
        public ItopVector.DrawArea.TextOperation TextOperation;
    }
}

