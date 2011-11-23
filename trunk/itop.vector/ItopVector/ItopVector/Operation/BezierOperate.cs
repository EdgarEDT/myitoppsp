namespace ItopVector.DrawArea
{
    using System;

    public enum BezierOperate
    {
        // Fields
        AddAnchor = 1,
        CenterPoint = 13,
        ChangeAnchor = 3,
        ChangeEndAnchor = 6,
        CloseFigure = 7,
        ConvertAnchor = 5,
        CreateAnchor = 10,
        DelAnchor = 2,
        Draw = 0,
        MoveAnchor = 4,
        MoveControl = 8,
        MovePath = 9,
        None = 14,
        Select = 12,
        SelectPath = 11
    }
}

