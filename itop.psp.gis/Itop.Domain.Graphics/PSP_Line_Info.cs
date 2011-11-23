//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2009-10-13 14:36:42
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// ʵ����Line_Info ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class PSP_Line_Info
    {
        public PSP_Line_Info() { }
        #region �ֶ�
        private string _uid = Guid.NewGuid().ToString().Substring(24);
        private string _areaid = "";
        private string _areaname = "";
        private string _substationid = "";
        private string _substationname = "";
        private string _title = "";
        private int _dy;
        private int _l1;
        private string _l2 = "";
        private string _l3 = "";
        private string _l4 = "";
        private double _l5;
        private string _l6 = "";
        private int _k1;
        private string _k2 = "";
        private string _k3 = "";
        private double _k4;
        private double _k5;
        private double _k6;
        private double _k7;
        private int _k8;
        private int _k9;
        private int _k10;
        private int _k11;
        private int _k12;
        private int _k13;
        private int _k14;
        private int _k15;
        private double _k16;
        private double _k17;
        private double _k18;
        private double _k19;
        private string _s1 = "";
        private string _s2 = "";
        private string _s3 = "";
        private string _s4 = "";
        private string _s5 = "";
        private string _s6 = "";
        private string _s7 = "";
        private string _s8 = "";
        private string _s9 = "";
        private string _s10 = "";
        private string _flag = "";
        private string _code = "";
        private string _isconn = "";
        private double _k20;
        private double _k21;
        private string _k22 = "";
        private DateTime _createdate = DateTime.Now;
        private string _k23 = "";
        private string _k24 = "";
        private string _k25 = "";
        private string _k26 = "";
        private string _k27 = "";
        private string _k28 = "";
        private string _k29 = "";
        private string _k30 = "";
        private double _m1;
        private double _m2;
        private double _m3;
        private double _m4;
        private double _m5;
        private double _m6;
        private double _m7;
        private double _m8;
        private double _m9;
        private double _m10;
        private double _m11;
        private double _m12;
        private double _m13;
        private double _m14;
        private double _m15;
        private double _m16;
        private double _m17;
        private double _m18;
        private double _m19;
        private double _m20;
        #endregion �ֶ�

        #region ����
        /// <summary>
        /// 
        /// </summary>
        public string UID {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaID {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaName {
            set { _areaname = value; }
            get { return _areaname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubstationID {
            set { _substationid = value; }
            get { return _substationid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubstationName {
            set { _substationname = value; }
            get { return _substationname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DY {
            set { _dy = value; }
            get { return _dy; }
        }
        /// <summary>
        /// ��ѹ�ȼ�(kV)
        /// </summary>
        public int L1 {
            set { _l1 = value; }
            get { return _l1; }
        }
        /// <summary>
        /// �յ���վ
        /// </summary>
        public string L2 {
            set { _l2 = value; }
            get { return _l2; }
        }
        /// <summary>
        /// �����վ
        /// </summary>
        public string L3 {
            set { _l3 = value; }
            get { return _l3; }
        }
        /// <summary>
        /// �����ͺ�
        /// </summary>
        public string L4 {
            set { _l4 = value; }
            get { return _l4; }
        }
        /// <summary>
        /// ��·����(km)
        /// </summary>
        public double L5 {
            set { _l5 = value; }
            get { return _l5; }
        }
        /// <summary>
        /// Ͷ��ʱ��
        /// </summary>
        public string L6 {
            set { _l6 = value; }
            get { return _l6; }
        }
        /// <summary>
        /// ��ѹ�ȼ�(kV)
        /// </summary>
        public int K1 {
            set { _k1 = value; }
            get { return _k1; }
        }
        /// <summary>
        /// ���ڵ����ͺ�
        /// </summary>
        public string K2 {
            set { _k2 = value; }
            get { return _k2; }
        }
        /// <summary>
        /// ��·����
        /// </summary>
        public string K3 {
            set { _k3 = value; }
            get { return _k3; }
        }
        /// <summary>
        /// �����߳���(km)
        /// </summary>
        public double K4 {
            set { _k4 = value; }
            get { return _k4; }
        }
        /// <summary>
        /// ��·�ܳ���(km)
        /// </summary>
        public double K5 {
            set { _k5 = value; }
            get { return _k5; }
        }
        /// <summary>
        /// �����߳�(km)
        /// </summary>
        public double K6 {
            set { _k6 = value; }
            get { return _k6; }
        }
        /// <summary>
        /// �ܿ��߳�(km)
        /// </summary>
        public double K7 {
            set { _k7 = value; }
            get { return _k7; }
        }
        /// <summary>
        /// ��ȫ����(A)
        /// </summary>
        public int K8 {
            set { _k8 = value; }
            get { return _k8; }
        }
        /// <summary>
        /// ����̨��
        /// </summary>
        public int K9 {
            set { _k9 = value; }
            get { return _k9; }
        }
        /// <summary>
        /// ��������(kVA)
        /// </summary>
        public int K10 {
            set { _k10 = value; }
            get { return _k10; }
        }
        /// <summary>
        /// ר��̨��
        /// </summary>
        public int K11 {
            set { _k11 = value; }
            get { return _k11; }
        }
        /// <summary>
        /// ר������(kVA)
        /// </summary>
        public int K12 {
            set { _k12 = value; }
            get { return _k12; }
        }
        /// <summary>
        /// ���������(kVA)
        /// </summary>
        public int K13 {
            set { _k13 = value; }
            get { return _k13; }
        }
        /// <summary>
        /// �޹�������̨��(̨)
        /// </summary>
        public int K14 {
            set { _k14 = value; }
            get { return _k14; }
        }
        /// <summary>
        /// �޹�����������(Kvar)
        /// </summary>
        public int K15 {
            set { _k15 = value; }
            get { return _k15; }
        }
        /// <summary>
        /// ������(A)
        /// </summary>
        public double K16 {
            set { _k16 = value; }
            get { return _k16; }
        }
        /// <summary>
        /// ��为����(%)
        /// </summary>
        public double K17 {
            set { _k17 = value; }
            get { return _k17; }
        }
        /// <summary>
        /// ��λ�߳��������(kVA/km)
        /// </summary>
        public double K18 {
            set { _k18 = value; }
            get { return _k18; }
        }
        /// <summary>
        /// ��·������(%)
        /// </summary>
        public double K19 {
            set { _k19 = value; }
            get { return _k19; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S1 {
            set { _s1 = value; }
            get { return _s1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S2 {
            set { _s2 = value; }
            get { return _s2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S3 {
            set { _s3 = value; }
            get { return _s3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S4 {
            set { _s4 = value; }
            get { return _s4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S5 {
            set { _s5 = value; }
            get { return _s5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S6 {
            set { _s6 = value; }
            get { return _s6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S7 {
            set { _s7 = value; }
            get { return _s7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S8 {
            set { _s8 = value; }
            get { return _s8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S9 {
            set { _s9 = value; }
            get { return _s9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string S10 {
            set { _s10 = value; }
            get { return _s10; }
        }
        /// <summary>
        /// �Ƿ�Ϊ��״ 1Ϊ��״��2Ϊ�滮
        /// </summary>
        public string Flag {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// �����滮�豸ID
        /// </summary>
        public string Code {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public string IsConn {
            set { _isconn = value; }
            get { return _isconn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double K20 {
            set { _k20 = value; }
            get { return _k20; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double k21 {
            set { _k21 = value; }
            get { return _k21; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string K22 {
            set { _k22 = value; }
            get { return _k22; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string K23 {
            set { _k23 = value; }
            get { return _k23; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string K24 {
            set { _k24 = value; }
            get { return _k24; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string K25 {
            set { _k25 = value; }
            get { return _k25; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string K26 {
            set { _k26 = value; }
            get { return _k26; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string K27 {
            set { _k27 = value; }
            get { return _k27; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string K28 {
            set { _k28 = value; }
            get { return _k28; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string K29 {
            set { _k29 = value; }
            get { return _k29; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string K30 {
            set { _k30 = value; }
            get { return _k30; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M1 {
            set { _m1 = value; }
            get { return _m1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M2 {
            set { _m2 = value; }
            get { return _m2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M3 {
            set { _m3 = value; }
            get { return _m3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M4 {
            set { _m4 = value; }
            get { return _m4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M5 {
            set { _m5 = value; }
            get { return _m5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M6 {
            set { _m6 = value; }
            get { return _m6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M7 {
            set { _m7 = value; }
            get { return _m7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M8 {
            set { _m8 = value; }
            get { return _m8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M9 {
            set { _m9 = value; }
            get { return _m9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M10 {
            set { _m10 = value; }
            get { return _m10; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M11 {
            set { _m11 = value; }
            get { return _m11; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M12 {
            set { _m12 = value; }
            get { return _m12; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M13 {
            set { _m13 = value; }
            get { return _m13; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M14 {
            set { _m14 = value; }
            get { return _m14; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M15 {
            set { _m15 = value; }
            get { return _m15; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M16 {
            set { _m16 = value; }
            get { return _m16; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M17 {
            set { _m17 = value; }
            get { return _m17; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M18 {
            set { _m18 = value; }
            get { return _m18; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M19 {
            set { _m19 = value; }
            get { return _m19; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double M20 {
            set { _m20 = value; }
            get { return _m20; }
        }
        #endregion ����
    }
}

