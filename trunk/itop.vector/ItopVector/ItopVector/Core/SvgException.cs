namespace ItopVector.Core
{
    using System;

    public class SvgException : Exception
    {
        // Methods
        public SvgException()
        {
            this.message = string.Empty;
            this.lineNumber = 0;
            this.lineposition = 0;
        }

        public SvgException(string message)
        {
            this.message = string.Empty;
            this.lineNumber = 0;
            this.lineposition = 0;
            this.message = message;
        }

        public SvgException(string message, int linenumber, int lineposition)
        {
            this.message = string.Empty;
            this.lineNumber = 0;
            this.lineposition = 0;
            this.message = message;
            this.lineNumber = linenumber;
            this.lineposition = lineposition;
        }


        // Properties
        public string ExceptionMessage
        {
            get
            {
                return this.message;
            }
        }

        public int LineNumber
        {
            get
            {
                return this.lineNumber;
            }
        }

        public int LinePosition
        {
            get
            {
                return this.lineposition;
            }
        }


        // Fields
        private int lineNumber;
        private int lineposition;
        private string message;
    }
}

