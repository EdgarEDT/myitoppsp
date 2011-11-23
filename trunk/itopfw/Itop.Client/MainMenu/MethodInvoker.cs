
using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.Globalization;

namespace Itop.Client.MainMenu {
    /// <summary>
    /// 方法动态调用
    /// </summary>
    public static class MethodInvoker {
        /// <summary>
        /// 动态调用
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="paramValues"></param>
        /// <returns>调用返回的结果</returns>
        public static object Execute(string assemblyName, string className, string methodName, string[] paramValues) {
            if (assemblyName == null)
                assemblyName = string.Empty;
            if (string.Compare(assemblyName, "Itop.Client.dll", true) == 0)
                assemblyName = string.Empty;

            if (className == null || className == string.Empty)
                return null;

            object result = null;
            Assembly asm = (assemblyName == string.Empty) ? Assembly.GetExecutingAssembly() : Assembly.LoadFrom(assemblyName);
            Type type = asm.GetType(className, true);

            //目前还没有处理overload方法的调用
            MethodInfo method = type.GetMethod(methodName);
            if (method != null) {
                ParameterInfo[] paramInfos = method.GetParameters();
                if (paramInfos.Length == paramValues.Length) {
                    // 参数个数相同才会执行
                    object[] methodParams = new object[paramValues.Length];
                    for (int i = 0; i < paramValues.Length; i++)
                        methodParams[i] = Convert.ChangeType(paramValues[i], paramInfos[i].ParameterType, CultureInfo.InvariantCulture);

                    object instance = (method.IsStatic) ? null : Activator.CreateInstance(type);
                    result = method.Invoke(instance, methodParams);
                }
            }

            return result;
        }
    }
}