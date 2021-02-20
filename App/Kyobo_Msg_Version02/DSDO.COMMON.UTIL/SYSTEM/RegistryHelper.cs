using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Win32;

namespace DSDO.COMMON.UTIL.SYSTEM
{
    public class RegistryHelper
    {
        /// <summary>
        /// 주어진 레지스트리 문자열 데이터를 주어진 레지스트리 이름으로 설정
        /// </summary>
        /// <param name="key">the registry hive ex. (HKEY_LOCAL_MACHINE)</param>
        /// <param name="subKey">the subkey within the registry mode ex. ("SOFTWARE\\WINDOWS\\")</param>
        /// <param name="regName">the name of the registry to write the data</param>
        /// <param name="regData">the string data to write</param>
        /// <returns>true if successful, otherwise false</returns>
		public static bool SetRegistryData(RegistryHive key, String subKey, String regName, Object regData)
        {
            try
            {
                RegistryKey registry = RegistryKey.OpenBaseKey(key, RegistryView.Default);
                registry = registry.CreateSubKey(subKey);
                registry.SetValue(regName, regData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                return false;
            }
            return true;
        }

        /// <summary>
        ///  주어진 레지스트리 이름의 주어진 레지스트리 문자열 데이터를 가져옴
        /// </summary>
        /// <param name="key">the registry hive ex. (HKEY_LOCAL_MACHINE)</param>
        /// <param name="subKey">the subkey within the registry hive ex. ("SOFTWARE\\WINDOWS\\")</param>
        /// <param name="regName">the name of the registry to read the data</param>
        /// <param name="retRegData">the data read</param>
        /// <returns>true if successful, otherwise false</returns>
        public static bool GetRegistryData(RegistryHive key, String subKey, String regName, ref Object retRegData)
        {
            try
            {
                RegistryKey registry = RegistryKey.OpenBaseKey(key, RegistryView.Default);
                registry = registry.OpenSubKey(subKey);
                retRegData = registry.GetValue(regName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 주어진 레지스트리 값을 삭제
        /// </summary>
        /// <param name="key">the registry hive</param>
        /// <param name="subkey">the subkey within the registry hive</param>
        /// <param name="regName">the registry value to be deleted</param>
        public static void DeleteRegistryValue(RegistryHive key, String subkey, String regName)
        {
            try
            {
                RegistryKey registry = RegistryKey.OpenBaseKey(key, RegistryView.Default);
                registry = registry.OpenSubKey(subkey);
                registry.DeleteValue(regName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 주어진 레지스트리 키 삭제
        /// </summary>
        /// <param name="key">the registry hive</param>
        /// <param name="subkey">the subkey within the registry hive to be deleted</param>
        public static void DeleteRegistryKey(RegistryHive key, String subkey)
        {
            try
            {
                RegistryKey registry = RegistryKey.OpenBaseKey(key, RegistryView.Default);
                registry = registry.OpenSubKey(subkey);
                registry.DeleteSubKeyTree(subkey);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 지정된 레지스트리의 RegistryValueKind를 반환
        /// </summary>
        /// <param name="key">the registry hive</param>
        /// <param name="subkey">the subkey within the registry hive</param>
        /// <param name="regName">the name of the registry to get the RegistryValueType</param>
        /// <returns></returns>
        public static RegistryValueKind GetRegistryValueKind(RegistryHive key, String subkey, String regName)
        {
            try
            {
                RegistryKey registry = RegistryKey.OpenBaseKey(key, RegistryView.Default);
                registry = registry.OpenSubKey(subkey);
                return registry.GetValueKind(regName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                return RegistryValueKind.Unknown;
            }
        }
    }
}
