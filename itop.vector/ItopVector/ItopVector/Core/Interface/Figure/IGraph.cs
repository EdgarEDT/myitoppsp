namespace ItopVector.Core.Interface.Figure
{
    using ItopVector.Core.ClipAndMask;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Types;
    using System;
    using System.Drawing;
	using System.ComponentModel;
	using ItopVector.Core;

	/// <summary>
	/// ���ƽӿ�
	/// </summary>
    public interface IGraph : ISvgElement, IPath
    {
        // Methods
		/// <summary>
		/// ���Ʒ���
		/// </summary>
		/// <param name="g"></param>
		/// <param name="time"></param>
        void Draw(Graphics g, int time);

		void DrawConnect(Graphics g);

		/// <summary>
		/// ��Χ����
		/// </summary>
		/// <returns></returns>
        RectangleF GetBounds();

		void NotifyChange();

		bool IsChanged{ get; set; }
        /// <summary>
        /// �Ƿ��ͷ��Ԫ��
        /// </summary>
        bool IsMarkerChild { get;set;}
        // Properties
		/// <summary>
		/// ���ɫ
		/// </summary>
        Color BoundColor { get; set; }

        ItopVector.Core.ClipAndMask.ClipPath ClipPath { get; set; }

        bool DrawVisible { get; set; }

        Transf GraphTransform { get; set; }

		/// <summary>
		/// �Ƿ����
		/// </summary>
        bool IsLock { get; set; }

		bool LimitSize{get;set;}
        bool ShowBound { get; set; }

        bool ShowClip { set; }

        float TempFillOpacity { get; set; }

        float TempOpacity { get; set; }

        float TempStrokeOpacity { get; set; }

        Transf Transform { get; set; }

        bool Visible { get; set; }

		void AddConnectLine(ItopVector.Core.Figure.ConnectLine connect);

		void RemoveAllConnectLines();

		bool ShowConnectPoints{get;set;}

		PointF[] ConnectPoints{get;}

		bool CanSelect{get;set;}

		PointF CenterPoint{get;set;}

		SvgElementCollection ConnectLines{get;set;}
		ILayer Layer{get;set;}
    }
}

