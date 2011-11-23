
using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.Globalization;
using System.Windows.Forms;

namespace Itop.Common {
    /// <summary>
    /// ������̬����
    /// </summary>
    public static class MethodInvoker {
        /// <summary>
        /// ��̬����
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="paramValues"></param>
        /// <returns>���÷��صĽ��</returns>
        public static object Execute(string assemblyName, string className, string methodName, object[] paramValues) {
            if (assemblyName == null)
                assemblyName = string.Empty;
           
            if (className == null || className == string.Empty)
                return null;

            object result = null;
            Assembly asm = (assemblyName == string.Empty) ? Assembly.GetExecutingAssembly() : Assembly.LoadFrom(Application.StartupPath+"\\"+assemblyName);
            Type type = asm.GetType(className, true);

            //
            Type[] ptypes=new Type[paramValues.Length];
            for (int i = 0; i < paramValues.Length; i++)
                ptypes[i] = paramValues[i].GetType();

            MethodInfo method = type.GetMethod(methodName,ptypes);
            if (method != null) {
                ParameterInfo[] paramInfos = method.GetParameters();
                if (paramInfos.Length == paramValues.Length) {
                    // ����������ͬ�Ż�ִ��
                    object[] methodParams = new object[paramValues.Length];
                    
                    for (int i = 0; i < paramValues.Length; i++)
                        methodParams[i] = Convert.ChangeType(paramValues[i], paramInfos[i].ParameterType, CultureInfo.InvariantCulture);

                    object instance = (method.IsStatic) ? null : Activator.CreateInstance(type);
                   
                    result = method.Invoke(instance, methodParams);
                }
            }

            return result;
        }
        /// <summary>
        /// ��̬����
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="paramValues"></param>
        /// <returns>���÷��صĽ��</returns>
        public static object Execute(string assemblyName, string className, string methodName, object[] paramValues, ref object classInstance) {
            int ii = 1;
            ii++;
            
            
            if (assemblyName == null)
                assemblyName = string.Empty;

            if (className == null || className == string.Empty)
                return null;

            object result = null;
            Assembly asm = (assemblyName == string.Empty) ? Assembly.GetExecutingAssembly() : Assembly.LoadFrom(Application.StartupPath + "\\" + assemblyName);
            Type type = asm.GetType(className, true);

            //
            Type[] ptypes = new Type[paramValues.Length];
            for (int i = 0; i < paramValues.Length; i++)
                ptypes[i] = paramValues[i].GetType();

            MethodInfo method = type.GetMethod(methodName, ptypes);
            if (method != null) {
                ParameterInfo[] paramInfos = method.GetParameters();
                if (paramInfos.Length == paramValues.Length) {
                    // ����������ͬ�Ż�ִ��
                    object[] methodParams = new object[paramValues.Length];

                    for (int i = 0; i < paramValues.Length; i++)
                        methodParams[i] = Convert.ChangeType(paramValues[i], paramInfos[i].ParameterType, CultureInfo.InvariantCulture);
                    if (classInstance == null) {
                        classInstance = (method.IsStatic) ? null : Activator.CreateInstance(type);
                    }
                    result = method.Invoke(classInstance, methodParams);
                }
            }

            return result;
        }
    }
}