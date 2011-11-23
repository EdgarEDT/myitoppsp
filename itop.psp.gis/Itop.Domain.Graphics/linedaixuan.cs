using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Domain.Graphics
{
    public class linedaixuan : IComparable
    {
        private int _linenum;
        private string _suid;
        private string _linename;
        private double _linepij;
        private double _linevalue;
        private double _linetouzilv;
        public int linenum
        {
            get { return _linenum; }
            set { _linenum = value; }
        }
        public string Suid
        {
            get { return _suid; }
            set { _suid = value; }
        }
        public string linename
        {
            get { return _linename; }
            set { _linename = value; }
        }
        public double linepij
        {
            get { return _linepij; }
            set { _linepij = value; }
        }

        public double linevalue
        {
            get { return _linevalue; }
            set { _linevalue = value; }
        }
        public double linetouzilv
        {
            get { return _linetouzilv; }
            set { _linetouzilv = value; }
        }
        public linedaixuan(int _linenum, string suid, string _linename)
        {
            this.linenum = _linenum;
            Suid = suid;
            this.linename = _linename;
        }
        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                linedaixuan sObj = (linedaixuan)obj;
                if (this.linetouzilv > sObj.linetouzilv)
                {
                    res = 1;
                }
                else if (this.linetouzilv < sObj.linetouzilv)
                {
                    res = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("±È½ÏÒì³£", ex.InnerException);
            }
            return res;

        }
    }
}
