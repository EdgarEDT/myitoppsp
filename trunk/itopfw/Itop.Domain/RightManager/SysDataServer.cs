//********************************************************************************/
//
//此代码由TONLI.NET代码生成器自动生成.
//生成时间:2012-5-30 9:34:54
//
//********************************************************************************/
using System;
namespace Itop.Domain
{
    /// <summary>
    /// 实体类SysDataServer 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SysDataServer
    {
        public SysDataServer()
        { }
        #region 字段
        private string _id = "";
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
        #endregion 字段

        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAddress
        {
            set { _serveraddress = value; }
            get { return _serveraddress; }
        }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string ServerName
        {
            set { _servername = value; }
            get { return _servername; }
        }
        /// <summary>
        /// 数据库登录用户名
        /// </summary>
        public string ServerUser
        {
            set { _serveruser = value; }
            get { return _serveruser; }
        }
        /// <summary>
        /// 数据库登录密码
        /// </summary>
        public string ServerPwd
        {
            set { _serverpwd = value; }
            get { return _serverpwd; }
        }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName
        {
            set { _cityname = value; }
            get { return _cityname; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public double CityJD
        {
            set { _cityjd = value; }
            get { return _cityjd; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public double CityWD
        {
            set { _citywd = value; }
            get { return _citywd; }
        }
        /// <summary>
        /// 偏移经度
        /// </summary>
        public double CityPYJD
        {
            set { _citypyjd = value; }
            get { return _citypyjd; }
        }
        /// <summary>
        /// 偏移纬度
        /// </summary>
        public double CityPYWD
        {
            set { _citypywd = value; }
            get { return _citypywd; }
        }
        /// <summary>
        /// 计算地块面积变量
        /// </summary>
        public double CityPYArea
        {
            set { _citypyarea = value; }
            get { return _citypyarea; }
        }
        /// <summary>
        /// 城市描述
        /// </summary>
        public string CityDesc
        {
            set { _citydesc = value; }
            get { return _citydesc; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 备用1
        /// </summary>
        public string ByCol1
        {
            set { _bycol1 = value; }
            get { return _bycol1; }
        }
        /// <summary>
        /// 备用2
        /// </summary>
        public string ByCol2
        {
            set { _bycol2 = value; }
            get { return _bycol2; }
        }
        /// <summary>
        /// 备用3
        /// </summary>
        public string ByCol3
        {
            set { _bycol3 = value; }
            get { return _bycol3; }
        }
        /// <summary>
        /// 备用4
        /// </summary>
        public string ByCol4
        {
            set { _bycol4 = value; }
            get { return _bycol4; }
        }
        /// <summary>
        /// 备用5
        /// </summary>
        public string ByCol5
        {
            set { _bycol5 = value; }
            get { return _bycol5; }
        }
        /// <summary>
        /// 备用6
        /// </summary>
        public string ByCol6
        {
            set { _bycol6 = value; }
            get { return _bycol6; }
        }
        /// <summary>
        /// 备用7
        /// </summary>
        public string ByCol7
        {
            set { _bycol7 = value; }
            get { return _bycol7; }
        }
        /// <summary>
        /// 备用8
        /// </summary>
        public string ByCol8
        {
            set { _bycol8 = value; }
            get { return _bycol8; }
        }
        /// <summary>
        /// 备用9
        /// </summary>
        public string ByCol9
        {
            set { _bycol9 = value; }
            get { return _bycol9; }
        }
        /// <summary>
        /// 备用10
        /// </summary>
        public string ByCol10
        {
            set { _bycol10 = value; }
            get { return _bycol10; }
        }
        #endregion 属性
    }
}

