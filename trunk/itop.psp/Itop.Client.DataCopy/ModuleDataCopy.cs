using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;

namespace Itop.Client.DataCopy
{
    public class ModuleDataCopy
    {
        public bool CopyData(string pid,string pid1)
        {
            bool bl = false;
            FormModuleList fm=new FormModuleList();
            fm.PID = pid;
            fm.PID1 = pid1;

            if (fm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                bl = true;
            return bl;
        }

        public void CopyData(string oldId, string newId, string className, string istr, string sstr)
        {
            try
            {
                Assembly assem = Assembly.LoadFrom(Application.StartupPath + "\\Itop.Domain.Table.dll");
                Type type = assem.GetType("Itop.Domain.Table." + className, true);

                string conn =  "ProjectID = '" + oldId + "'";
                IList oldList = Common.Services.BaseService.GetList(sstr, conn);
                for (int i = 0; i < oldList.Count; i++)
                {
                    object newObj = Activator.CreateInstance(type);
                    newObj = oldList[i];
                    //string str = oldList[i].GetType().GetProperty("Title").GetValue(oldList[i], null).ToString();
                    IList<string> strList = GetNewID(type.GetProperty("ID").GetValue(newObj, null).ToString(),
                        type.GetProperty("ParentID").GetValue(newObj, null).ToString(), oldId, newId);
                    type.GetProperty("ID").SetValue(newObj, strList[0], null);
                    type.GetProperty("ParentID").SetValue(newObj, strList[1], null);
                    type.GetProperty("ProjectID").SetValue(newObj, newId, null);
                    Common.Services.BaseService.Create(istr, newObj);
                }
            }
            catch (Exception e) { string te = e.Message; }
        }
        public void delete(string oldId, string className, string istr, string sstr)
        {
            try
            {
                Assembly assem = Assembly.LoadFrom(Application.StartupPath + "\\Itop.Domain.Table.dll");
                Type type = assem.GetType("Itop.Domain.Table." + className, true);
                
                string conn = "ProjectID = '" + oldId + "'";
                IList oldList = Common.Services.BaseService.GetList(sstr, conn);
                for (int i = 0; i < oldList.Count; i++)
                {
                    object newObj = Activator.CreateInstance(type);
                    newObj = oldList[i];
                    Common.Services.BaseService.Delete<object>(oldList[i]);
                }
            }
            catch (Exception e) { string te = e.Message; }
        }
        public IList<string> GetNewID(string oldid, string oldparentid, string oldprojectid, string newprojectid)
        {
            IList<string> list = new List<string>();
            string newid = "", newparentid = oldparentid;
            if (oldid.IndexOf(oldprojectid) != -1)
            {
                newid = oldid.Replace(oldprojectid, newprojectid);
            }
            if (oldparentid.IndexOf(oldprojectid) != -1)
            {
                newparentid = oldparentid.Replace(oldprojectid, newprojectid);
            }
            list.Add(newid); list.Add(newparentid);
            return list;
        }
    }
}
