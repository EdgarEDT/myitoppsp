//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-8-26 9:57:09
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类PSPDEV 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PSPDEV
    {
        public PSPDEV()
        {
            SUID = Guid.NewGuid().ToString();
        }
        #region 字段
        private string _suid = "";
        private int _number;
        private string _name = "";
        private double _x1;
        private double _y1;
        private double _x2;
        private double _y2;
        private string _svguid = "";
        private int _firstnode;
        private int _lastnode;
        private string _eleid = "";
        private string _type = "";
        private double _outp;
        private double _outq;
        private double _inputp;
        private double _inputq;
        private double _voltr;
        private double _voltv;
        private double _liner;
        private double _linetq;
        private double _linegndc;
        private double _linechange;
        private int _flag;
        private string _nodetype;
        private decimal _burthen;
        private double _linelength;
        private string _linelevel;
        private string _linetype;
        private string _linestatus;
        private double _voltcapacity;
        private double _powerfactor;
        private double _standardvolt;
        private double _standardcurrent;
        private double _positiver;
        private double _positivetq;
        private double _zeror;
        private double _zerotq;
        private double _bigtq;
        private double _smalltq;
        private string _huganline1;
        private string _huganline2;
        private string _huganline3;
        private string _huganline4;
        private double _huganTQ1;
        private double _huganTQ2;
        private double _huganTQ3;
        private double _huganTQ4;
        private double _huganTQ5;
        private decimal _huganfirst;
        private string _lable;
        private double _bigP;
        private double _k;
        private double _g;
        private string _kname;
        private string _kswitchstatus;
        private double _referencevolt;
        private double _p0;
        private double _i0;
        private double _sin;
        private double _sjn;
        private double _skn;
        private double _iv;
        private double _jv;
        private double _kv;
        private double _pij;
        private double _pjk;
        private double _pik;
        private double _vij;
        private double _vjk;
        private double _vik;
        private double _vipos;
        private double _vistep;
        private double _vimax;
        private double _vimin;
        private double _vjpos;
        private double _vjstep;
        private double _vjmax;
        private double _vjmin;
        private double _vkpos;
        private double _vkstep;
        private double _vkmax;
        private double _vkmin;
        private double _vi0;
        private double _vj0;
        private double _vk0;
        private double _vib;
        private double _vjb;
        private double _vkb;
        private string _unitflag = "";
        private double _ratevolt;
        private string _substationeleid = "";
        private string _substationname = "";
        private string _operationyear = "";
        private string _iname = "";
        private string _jname = "";
        private string _iswitch = "";
        private string _jswitch = "";
        private double _zerogndc;
        private double _positivegndc;
        private string _projectid = "";
        private string _layerid = "";
        private string _jsfs = "";
        private string _dq = "";
        private string _areaid = "";
        private string _linetype2 = "";
        private string _llfs = "";
        private int _switchnum;
        private double _num1;
        private double _num2;
        private double _length2 ;
        private string _HgFlag;
        private double _FactI;

        private string _date1="";
        private string _date2 = "";

        #endregion 字段

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public string SUID
        {
            set { _suid = value; }
            get { return _suid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double X1
        {
            set { _x1 = value; }
            get { return _x1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Y1
        {
            set { _y1 = value; }
            get { return _y1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double X2
        {
            set { _x2 = value; }
            get { return _x2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Y2
        {
            set { _y2 = value; }
            get { return _y2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SvgUID
        {
            set { _svguid = value; }
            get { return _svguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int FirstNode
        {
            set { _firstnode = value; }
            get { return _firstnode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LastNode
        {
            set { _lastnode = value; }
            get { return _lastnode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EleID
        {
            set { _eleid = value; }
            get { return _eleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double OutP
        {
            set { _outp = value; }
            get { return _outp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double OutQ
        {
            set { _outq = value; }
            get { return _outq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double InPutP
        {
            set { _inputp = value; }
            get { return _inputp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double InPutQ
        {
            set { _inputq = value; }
            get { return _inputq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double VoltR
        {
            set { _voltr = value; }
            get { return _voltr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double VoltV
        {
            set { _voltv = value; }
            get { return _voltv; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double LineR
        {
            set { _liner = value; }
            get { return _liner; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double LineTQ
        {
            set { _linetq = value; }
            get { return _linetq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double LineGNDC
        {
            set { _linegndc = value; }
            get { return _linegndc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double LineChange
        {
            set { _linechange = value; }
            get { return _linechange; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NodeType
        {
            set { _nodetype = value; }
            get { return _nodetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Burthen
        {
            set { _burthen = value; }
            get { return _burthen; }
        }
        public double LineLength
        {
            set { _linelength = value; }
            get { return _linelength; }
        }
        public string LineLevel
        {
            set { _linelevel = value; }
            get { return _linelevel; }
        }
        public string LineType
        {
            set { _linetype = value; }
            get { return _linetype; }
        }
        public string LineStatus
        {
            set { _linestatus = value; }
            get { return _linestatus; }
        }
        public double VoltCapacity
        {
            set { _voltcapacity = value;}
            get { return _voltcapacity; }
        }
        public double PowerFactor
        {
            set { _powerfactor = value; }
            get { return _powerfactor; }
        }
        public double StandardVolt
        {
            set { _standardvolt = value; }
            get { return _standardvolt; }
        }
        public double StandardCurrent
        {
            set { _standardcurrent = value; }
            get { return _standardcurrent; }
        }
        public double PositiveR
        {
            set { _positiver = value; }
            get { return _positiver; }
        }
        public double PositiveTQ
        {
            set { _positivetq = value; }
            get { return _positivetq; }
        }
        public double ZeroR
        {
            set { _zeror = value; }
            get { return _zeror; }
        }
        public double ZeroTQ
        {
            set { _zerotq = value; }
            get { return _zerotq; }
        }
        public double BigTQ
        {
            set { _bigtq = value; }
            get { return _bigtq; }
        }
        public double SmallTQ
        {
            set { _smalltq = value; }
            get { return _smalltq; }
        }
        public string HuganLine1
        {
            set { _huganline1 = value; }
            get { return _huganline1; }
        }
        public string HuganLine2
        {
            set { _huganline2 = value; }
            get { return _huganline2; }
        }
        public string HuganLine3
        {
            set { _huganline3 = value; }
            get { return _huganline3; }
        }
        public string HuganLine4
        {
            set { _huganline4 = value; }
            get { return _huganline4; }
        }
        public double HuganTQ1
        {
            set { _huganTQ1 = value; }
            get { return _huganTQ1; }
        }
        public double HuganTQ2
        {
            set { _huganTQ2 = value; }
            get { return _huganTQ2; }
        }
        public double HuganTQ3
        {
            set { _huganTQ3 = value; }
            get { return _huganTQ3; }
        }
        public double HuganTQ4
        {
            set { _huganTQ4 = value; }
            get { return _huganTQ4; }

        }
        public double HuganTQ5
        {
            set { _huganTQ5 = value; }
            get { return _huganTQ5; }
        }
        public decimal HuganFirst
        {
            set { _huganfirst = value; }
            get { return _huganfirst; }
        }
        public string Lable
        {
            set { _lable = value; }
            get { return _lable; }
        }
        public double BigP
        {
            set {_bigP=value;}
            get { return _bigP; }
        }
        public double K
        {
            get { return _k; }
            set { _k = value; }
        }
        public double G
        {
            get { return _g; }
            set { _g = value; }
        }
        public string KSwitchStatus
        {
            get { return _kswitchstatus; }
            set { _kswitchstatus = value; }
        }

        public string KName
        {
            get { return _kname; }
            set { _kname = value; }
        }
        public double ReferenceVolt
        {
            set { _referencevolt = value; }
            get { return _referencevolt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double P0
        {
            set { _p0 = value; }
            get { return _p0; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double I0
        {
            set { _i0 = value; }
            get { return _i0; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double SiN
        {
            set { _sin = value; }
            get { return _sin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double SjN
        {
            set { _sjn = value; }
            get { return _sjn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double SkN
        {
            set { _skn = value; }
            get { return _skn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double iV
        {
            set { _iv = value; }
            get { return _iv; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double jV
        {
            set { _jv = value; }
            get { return _jv; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double kV
        {
            set { _kv = value; }
            get { return _kv; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Pij
        {
            set { _pij = value; }
            get { return _pij; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Pjk
        {
            set { _pjk = value; }
            get { return _pjk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Pik
        {
            set { _pik = value; }
            get { return _pik; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vij
        {
            set { _vij = value; }
            get { return _vij; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vjk
        {
            set { _vjk = value; }
            get { return _vjk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vik
        {
            set { _vik = value; }
            get { return _vik; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vipos
        {
            set { _vipos = value; }
            get { return _vipos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vistep
        {
            set { _vistep = value; }
            get { return _vistep; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vimax
        {
            set { _vimax = value; }
            get { return _vimax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vimin
        {
            set { _vimin = value; }
            get { return _vimin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vjpos
        {
            set { _vjpos = value; }
            get { return _vjpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vjstep
        {
            set { _vjstep = value; }
            get { return _vjstep; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vjmax
        {
            set { _vjmax = value; }
            get { return _vjmax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vjmin
        {
            set { _vjmin = value; }
            get { return _vjmin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vkpos
        {
            set { _vkpos = value; }
            get { return _vkpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vkstep
        {
            set { _vkstep = value; }
            get { return _vkstep; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vkmax
        {
            set { _vkmax = value; }
            get { return _vkmax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vkmin
        {
            set { _vkmin = value; }
            get { return _vkmin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vi0
        {
            set { _vi0 = value; }
            get { return _vi0; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vj0
        {
            set { _vj0 = value; }
            get { return _vj0; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vk0
        {
            set { _vk0 = value; }
            get { return _vk0; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vib
        {
            set { _vib = value; }
            get { return _vib; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vjb
        {
            set { _vjb = value; }
            get { return _vjb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vkb
        {
            set { _vkb = value; }
            get { return _vkb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnitFlag
        {
            set { _unitflag = value; }
            get { return _unitflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double RateVolt
        {
            set { _ratevolt = value; }
            get { return _ratevolt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubstationEleID
        {
            set { _substationeleid = value; }
            get { return _substationeleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubstationName
        {
            set { _substationname = value; }
            get { return _substationname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperationYear
        {
            set { _operationyear = value; }
            get { return _operationyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IName
        {
            set { _iname = value; }
            get { return _iname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JName
        {
            set { _jname = value; }
            get { return _jname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ISwitch
        {
            set { _iswitch = value; }
            get { return _iswitch; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JSwitch
        {
            set { _jswitch = value; }
            get { return _jswitch; }
        }
        public double ZeroGNDC
        {
            set { _zerogndc = value; }
            get { return _zerogndc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double PositiveGNDC
        {
            set { _positivegndc = value; }
            get { return _positivegndc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        public string LayerID
        {
            set { _layerid = value; }
            get { return _layerid; }
        }
        public string JXFS
        {
            set { _jsfs = value; }
            get { return _jsfs; }
        }
        public string DQ
        {
            set { _dq = value; }
            get { return _dq; }
        }
        public string AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }

        public string LineType2
        {
            get { return _linetype2; }
            set { _linetype2 = value; }
        }
        public double Length2
        {
            get { return _length2; }
            set { _length2 = value; }
        }
        public string LLFS
        {
            get { return _llfs; }
            set { _llfs = value; }
        }
        public int SwitchNum
        {
            get { return _switchnum; }
            set { _switchnum = value; }
        }
        public double Num1
        {
            get { return _num1; }
            set { _num1 = value; }
        }
        public double Num2
        {
            get { return _num2; }
            set { _num2 = value; }
        }
        public string HgFlag
        {
            get { return _HgFlag; }
            set { _HgFlag = value; }
        }
        public double FactI
        {
            get { return _FactI; }
            set { _FactI = value; }
        }
        public string Date1
        {
            get { return _date1; }
            set { _date1 = value; }
        }
        public string Date2
        {
            get { return _date2; }
            set { _date2 = value; }
        }
        #endregion 属性

    }
}

