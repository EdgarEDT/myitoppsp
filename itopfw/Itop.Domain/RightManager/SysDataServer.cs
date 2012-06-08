//********************************************************************************/
//
//�˴�����TONLI.NET�����������Զ�����.
//����ʱ��:2012-5-30 9:34:54
//
//********************************************************************************/
using System;
namespace Itop.Domain
{
    /// <summary>
    /// ʵ����SysDataServer ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class SysDataServer
    {
        public SysDataServer()
        { }
        #region �ֶ�
        private string _id =Guid.NewGuid().ToString();
        private int _sort;
        private string _serveraddress = "";
        private string _servername = "";
        private string _serveruser = "";
        private string _serverpwd = "";
        private string _cityname = "";
        private double _cityjd;
        private double _citywd;
        private double _citypyjd;
        private double _citypywd;
        private double _citypyarea;
        private string _citydesc = "";
        private string _remark = "";
        private string _bycol1 = "";
        private string _bycol2 = "";
        private string _bycol3 = "";
        private string _bycol4 = "";
        private string _bycol5 = "";
        private string _bycol6 = "";
        private string _bycol7 = "";
        private string _bycol8 = "";
        private string _bycol9 = "";
        private string _bycol10 = "";
        #endregion �ֶ�

        #region ����
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// ��������ַ
        /// </summary>
        public string ServerAddress
        {
            set { _serveraddress = value; }
            get { return _serveraddress; }
        }
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public string ServerName
        {
            set { _servername = value; }
            get { return _servername; }
        }
        /// <summary>
        /// ���ݿ��¼�û���
        /// </summary>
        public string ServerUser
        {
            set { _serveruser = value; }
            get { return _serveruser; }
        }
        /// <summary>
        /// ���ݿ��¼����
        /// </summary>
        public string ServerPwd
        {
            set { _serverpwd = value; }
            get { return _serverpwd; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string CityName
        {
            set { _cityname = value; }
            get { return _cityname; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public double CityJD
        {
            set { _cityjd = value; }
            get { return _cityjd; }
        }
        /// <summary>
        /// γ��
        /// </summary>
        public double CityWD
        {
            set { _citywd = value; }
            get { return _citywd; }
        }
        /// <summary>
        /// ƫ�ƾ���
        /// </summary>
        public double CityPYJD
        {
            set { _citypyjd = value; }
            get { return _citypyjd; }
        }
        /// <summary>
        /// ƫ��γ��
        /// </summary>
        public double CityPYWD
        {
            set { _citypywd = value; }
            get { return _citypywd; }
        }
        /// <summary>
        /// ����ؿ��������
        /// </summary>
        public double CityPYArea
        {
            set { _citypyarea = value; }
            get { return _citypyarea; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string CityDesc
        {
            set { _citydesc = value; }
            get { return _citydesc; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// ����1
        /// </summary>
        public string ByCol1
        {
            set { _bycol1 = value; }
            get { return _bycol1; }
        }
        /// <summary>
        /// ����2
        /// </summary>
        public string ByCol2
        {
            set { _bycol2 = value; }
            get { return _bycol2; }
        }
        /// <summary>
        /// ����3
        /// </summary>
        public string ByCol3
        {
            set { _bycol3 = value; }
            get { return _bycol3; }
        }
        /// <summary>
        /// ����4
        /// </summary>
        public string ByCol4
        {
            set { _bycol4 = value; }
            get { return _bycol4; }
        }
        /// <summary>
        /// ����5
        /// </summary>
        public string ByCol5
        {
            set { _bycol5 = value; }
            get { return _bycol5; }
        }
        /// <summary>
        /// ����6
        /// </summary>
        public string ByCol6
        {
            set { _bycol6 = value; }
            get { return _bycol6; }
        }
        /// <summary>
        /// ����7
        /// </summary>
        public string ByCol7
        {
            set { _bycol7 = value; }
            get { return _bycol7; }
        }
        /// <summary>
        /// ����8
        /// </summary>
        public string ByCol8
        {
            set { _bycol8 = value; }
            get { return _bycol8; }
        }
        /// <summary>
        /// ����9
        /// </summary>
        public string ByCol9
        {
            set { _bycol9 = value; }
            get { return _bycol9; }
        }
        /// <summary>
        /// ����10
        /// </summary>
        public string ByCol10
        {
            set { _bycol10 = value; }
            get { return _bycol10; }
        }
        #endregion ����
    }
}

