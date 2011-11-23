namespace ItopVector.Core.Interface
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IControl
    {
        // Events
        event OnTipEventHandler OnTipEvent;
        event TrackPopupEventHandler OnTrackPopup;
    }
}

