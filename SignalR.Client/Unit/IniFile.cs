using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace SignalR.Client.Unit
{
    /// <summary>
    /// Provides methods for reading and writing to an INI file.
    /// </summary>
    public class IniFile
    {
        /// <summary>
        /// The maximum size of a section in an ini file.
        /// </summary>
        /// <remarks>
        /// This property defines the maximum size of the buffers 
        /// used to retreive data from an ini file.  This value is 
        /// the maximum allowed by the win32 functions 
        /// GetPrivateProfileSectionNames() or 
        /// GetPrivateProfileString().
        /// </remarks>
        public const int MAX_SECTION_SIZE = 32767; // 32 KB

        //The path of the file we are operating on.
        private string m_strPath;

        #region P/Invoke declares

        /// <summary>
        /// A static class that provides the win32 P/Invoke signatures 
        /// used by this class.
        /// </summary>
        /// <remarks>
        /// Note:  In each of the declarations below, we explicitly set CharSet to 
        /// Auto.  By default in C#, CharSet is set to Ansi, which reduces 
        /// performance on windows 2000 and above due to needing to convert strings
        /// from Unicode (the native format for all .Net strings) to Ansi before 
        /// marshalling.  Using Auto lets the marshaller select the Unicode version of 
        /// these functions when available.
        /// </remarks>
        [System.Security.SuppressUnmanagedCodeSecurity]
        private static class NativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer,
                                                                   uint nSize,
                                                                   string lpFileName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern uint GetPrivateProfileString(string lpAppName,
                                                              string lpKeyName,
                                                              string lpDefault,
                                                              StringBuilder lpReturnedString,
                                                              int nSize,
                                                              string lpFileName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern uint GetPrivateProfileString(string lpAppName,
                                                              string lpKeyName,
                                                              string lpDefault,
                                                              [In, Out] char[] lpReturnedString,
                                                              int nSize,
                                                              string lpFileName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetPrivateProfileString(string lpAppName,
                                                             string lpKeyName,
                                                             string lpDefault,
                                                             IntPtr lpReturnedString,
                                                             uint nSize,
                                                             string lpFileName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetPrivateProfileInt(string lpAppName,
                                                          string lpKeyName,
                                                          int lpDefault,
                                                          string lpFileName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetPrivateProfileSection(string lpAppName,
                                                              IntPtr lpReturnedString,
                                                              uint nSize,
                                                              string lpFileName);

            //We explicitly enable the SetLastError attribute here because
            // WritePrivateProfileString returns errors via SetLastError.
            // Failure to set this can result in errors being lost during 
            // the marshal back to managed code.
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool WritePrivateProfileString(string lpAppName,
                                                                string lpKeyName,
                                                                string lpString,
                                                                string lpFileName);


        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFile"/> class.
        /// </summary>
        /// <param name="path">The ini file to read and write from.</param>
        public IniFile(string path)
        {
            //Convert to the full path.  Because of backward compatibility, 
            // the win32 functions tend to assume the path should be the 
            // root Windows directory if it is not specified.  By calling 
            // GetFullPath, we make sure we are always passing the full path
            // the win32 functions.
            m_strPath = System.IO.Path.GetFullPath(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFolderPath">文件路徑</param>
        /// <param name="strFileName">文件名稱</param>
        public IniFile(string strFolderPath, string strFileName)
        {
            if (strFolderPath[1] != System.IO.Path.VolumeSeparatorChar)
            {
                m_strPath = string.Format(@"{0}\{1}", System.Windows.Forms.Application.StartupPath, strFolderPath);
            }
            else
            {
                m_strPath = strFolderPath;
            }
            if (m_strPath[m_strPath.Length - 1] != '\\')
            {
                m_strPath = string.Format(@"{0}\", m_strPath);
            }
            if (!Directory.Exists(m_strPath))
            {
                Directory.CreateDirectory(m_strPath);
            }
            m_strPath = string.Format(@"{0}{1}{2}", m_strPath, strFileName, strFileName.Contains(".ini") ? string.Empty : ".ini");
        }

        /// <summary>
        /// Gets the full path of ini file this object instance is operating on.
        /// </summary>
        /// <value>A file path.</value>
        public string Path
        {
            get
            {
                return m_strPath;
            }
        }

        #region Get Value Methods

        /// <summary>
        /// Gets the value of a setting in an ini file as a <see cref="T:System.String"/>.
        /// </summary>
        /// <param name="strSectionName">The name of the section to read from.</param>
        /// <param name="strKeyName">The name of the key in section to read.</param>
        /// <param name="strDefaultValue">The default value to return if the key
        /// cannot be found.</param>
        /// <returns>The value of the key, if found.  Otherwise, returns 
        /// <paramref name="defaultValue"/></returns>
        /// <remarks>
        /// The retreived value must be less than 32KB in length.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> or <paramref name="keyName"/> are 
        /// a null reference  (Nothing in VB)
        /// </exception>
        public string getString(string strSectionName,
                                string strKeyName,
                                string strDefaultValue)
        {
            if (strSectionName == null)
                throw new ArgumentNullException("sectionName");

            if (strKeyName == null)
                throw new ArgumentNullException("keyName");

            StringBuilder retval = new StringBuilder(MAX_SECTION_SIZE);

            NativeMethods.GetPrivateProfileString(strSectionName,
                                                  strKeyName,
                                                  strDefaultValue,
                                                  retval,
                                                  MAX_SECTION_SIZE,
                                                  m_strPath);

            return retval.ToString();
        }

        /// <summary>
        /// 取得字串
        /// </summary>
        /// <param name="strSectionName">區域名稱</param>
        /// <param name="strKeyName">Key名稱</param>
        /// <returns>文字數值</returns>
        public string getString(string strSectionName, string strKeyName)
        {
            return getString(strSectionName, strKeyName, string.Empty);
        }

        /// <summary>
        /// Gets the value of a setting in an ini file as a <see cref="T:System.Int16"/>.
        /// </summary>
        /// <param name="sectionName">The name of the section to read from.</param>
        /// <param name="keyName">The name of the key in section to read.</param>
        /// <param name="defaultValue">The default value to return if the key
        /// cannot be found.</param>
        /// <returns>The value of the key, if found.  Otherwise, returns 
        /// <paramref name="defaultValue"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> or <paramref name="keyName"/> are 
        /// a null reference  (Nothing in VB)
        /// </exception>
        public Int16 getInt16(string sectionName,
                            string keyName,
                            short defaultValue)
        {
            int retval = getInt32(sectionName, keyName, defaultValue);

            return Convert.ToInt16(retval);
        }

        public Int16 getInt16(string sectionName, string keyName)
        {
            return getInt16(sectionName, keyName, 0);
        }

        /// <summary>
        /// Gets the value of a setting in an ini file as a <see cref="T:System.Int32"/>.
        /// </summary>
        /// <param name="sectionName">The name of the section to read from.</param>
        /// <param name="keyName">The name of the key in section to read.</param>
        /// <param name="defaultValue">The default value to return if the key
        /// cannot be found.</param>
        /// <returns>The value of the key, if found.  Otherwise, returns 
        /// <paramref name="defaultValue"/></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> or <paramref name="keyName"/> are 
        /// a null reference  (Nothing in VB)
        /// </exception>
        public Int32 getInt32(string strSectionName,
                            string strKeyName,
                            int iDefaultValue)
        {
            if (strSectionName == null)
                throw new ArgumentNullException("sectionName");

            if (strKeyName == null)
                throw new ArgumentNullException("keyName");


            return NativeMethods.GetPrivateProfileInt(strSectionName, strKeyName, iDefaultValue, m_strPath);
        }

        public Int32 getInt32(string strSectionName, string strKeyName)
        {
            return getInt32(strSectionName, strKeyName, 0);
        }

        public Int64 getInt64(string sectionName,
                        string keyName,
                        long defaultValue)
        {
            string retval = getString(sectionName, keyName, "");

            if (retval == null || retval.Length == 0)
            {
                return defaultValue;
            }

            return Convert.ToInt64(retval, CultureInfo.InvariantCulture);
        }

        public Int64 getInt64(string strSectionName, string strKeyName)
        {
            return getInt64(strSectionName, strKeyName, 0);
        }

        /// <summary>
        /// Gets the value of a setting in an ini file as a <see cref="T:System.Double"/>.
        /// </summary>
        /// <param name="strSectionName">The name of the section to read from.</param>
        /// <param name="strKeyName">The name of the key in section to read.</param>
        /// <param name="dDefaultValue">The default value to return if the key
        /// cannot be found.</param>
        /// <returns>The value of the key, if found.  Otherwise, returns 
        /// <paramref name="defaultValue"/></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> or <paramref name="keyName"/> are 
        /// a null reference  (Nothing in VB)
        /// </exception>
        public double getDouble(string strSectionName,
                                string strKeyName,
                                double dDefaultValue)
        {
            string strRetval = getString(strSectionName, strKeyName, "");

            if (strRetval == null || strRetval.Length == 0)
            {
                return dDefaultValue;
            }

            return Convert.ToDouble(strRetval, CultureInfo.InvariantCulture);
        }

        public double getDouble(string strSectionName, string strKeyName)
        {
            return getDouble(strSectionName, strKeyName, 0.0);
        }

        public Decimal getDecimal(string strSectionName,
                                string strKeyName,
                                decimal decDefaultValue)
        {
            string strRetval = getString(strSectionName, strKeyName, "");

            if (strRetval == null || strRetval.Length == 0)
            {
                return decDefaultValue;
            }

            return Convert.ToDecimal(strRetval, CultureInfo.InvariantCulture);
        }

        public Decimal getDecimal(string strSectionName, string strKeyName)
        {
            return getDecimal(strSectionName, strKeyName, 0);
        }

        public Boolean getBoolean(string strSectionName,
                                string strKeyName,
                                bool bDefaultValue)
        {
            string strRetval = getString(strSectionName, strKeyName, "");

            if (strRetval == null || strRetval.Length == 0)
            {
                return bDefaultValue;
            }

            return Convert.ToBoolean(strRetval, CultureInfo.InvariantCulture);
        }

        public Boolean getBoolean(string strSectionName, string strKeyName)
        {
            return getBoolean(strSectionName, strKeyName, false);
        }

        #endregion

        #region GetSectionValues Methods

        /// <summary>
        /// Gets all of the values in a section as a list.
        /// </summary>
        /// <param name="strSectionName">
        /// Name of the section to retrieve values from.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> containing <see cref="KeyValuePair{T1, T2}"/> objects 
        /// that describe this section.  Use this verison if a section may contain
        /// multiple items with the same key value.  If you know that a section 
        /// cannot contain multiple values with the same key name or you don't 
        /// care about the duplicates, use the more convenient 
        /// <see cref="GetSectionValues"/> function.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> is a null reference  (Nothing in VB)
        /// </exception>
        public List<KeyValuePair<string, string>> getSectionValuesAsList(string strSectionName)
        {
            List<KeyValuePair<string, string>> lstRetval;
            string[] keyValuePairs;
            string strKey, strValue;
            int iEqualSignPos;

            if (strSectionName == null)
                throw new ArgumentNullException("sectionName");

            //Allocate a buffer for the returned section names.
            IntPtr ptr = Marshal.AllocCoTaskMem(MAX_SECTION_SIZE);

            try
            {
                //Get the section key/value pairs into the buffer.
                int len = NativeMethods.GetPrivateProfileSection(strSectionName,
                                                                 ptr,
                                                                 MAX_SECTION_SIZE,
                                                                 m_strPath);

                keyValuePairs = convertNullSeperatedStringToStringArray(ptr, len);
            }
            finally
            {
                //Free the buffer
                Marshal.FreeCoTaskMem(ptr);
            }

            //Parse keyValue pairs and add them to the list.
            lstRetval = new List<KeyValuePair<string, string>>(keyValuePairs.Length);

            for (int i = 0; i < keyValuePairs.Length; ++i)
            {
                //Parse the "key=value" string into its constituent parts
                iEqualSignPos = keyValuePairs[i].IndexOf('=');

                strKey = keyValuePairs[i].Substring(0, iEqualSignPos);

                strValue = keyValuePairs[i].Substring(iEqualSignPos + 1,
                                                   keyValuePairs[i].Length - iEqualSignPos - 1);

                lstRetval.Add(new KeyValuePair<string, string>(strKey, strValue));
            }

            return lstRetval;
        }

        /// <summary>
        /// Gets all of the values in a section as a dictionary.
        /// </summary>
        /// <param name="strSectionName">
        /// Name of the section to retrieve values from.
        /// </param>
        /// <returns>
        /// A <see cref="Dictionary{T, T}"/> containing the key/value 
        /// pairs found in this section.  
        /// </returns>
        /// <remarks>
        /// If a section contains more than one key with the same name, 
        /// this function only returns the first instance.  If you need to 
        /// get all key/value pairs within a section even when keys have the 
        /// same name, use <see cref="GetSectionValuesAsList"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> is a null reference  (Nothing in VB)
        /// </exception>
        public Dictionary<string, string> getSectionValues(string strSectionName)
        {
            List<KeyValuePair<string, string>> lstKeyValuePairs;
            Dictionary<string, string> dicRetval;

            lstKeyValuePairs = getSectionValuesAsList(strSectionName);

            //Convert list into a dictionary.
            dicRetval = new Dictionary<string, string>(lstKeyValuePairs.Count);

            foreach (KeyValuePair<string, string> keyValuePair in lstKeyValuePairs)
            {
                //Skip any key we have already seen.
                if (!dicRetval.ContainsKey(keyValuePair.Key))
                {
                    dicRetval.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return dicRetval;
        }

        #endregion

        #region Get Key/Section Names
        public bool checkSectionInCollection(string strSectionName)
        {
            string[] astrSections = getSectionNames();
            foreach (string strEachSection in astrSections)
            {
                if (strEachSection == strSectionName)
                {
                    return true;
                }
            }

            return false;
        }

        public bool checkKeyInSection(string strSectionName, string strKey)
        {
            if (checkSectionInCollection(strSectionName) == false)
            {
                return false;
            }

            string[] astrKeys = getKeyNames(strSectionName);
            foreach (string strEachKey in astrKeys)
            {
                if (strEachKey == strKey)
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 檢查Key存不存在, 不存在則寫一次
        /// </summary>
        /// <param name="strSectionName"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public void initKeyValue(string strSectionName, string strKey, string strValue)
        {
            if (checkKeyInSection(strSectionName, strKey) == false)
            {
                writeValue(strSectionName, strKey, strValue);
            }
        }

        /// <summary>
        /// Gets the names of all keys under a specific section in the ini file.
        /// </summary>
        /// <param name="strSectionName">
        /// The name of the section to read key names from.
        /// </param>
        /// <returns>An array of key names.</returns>
        /// <remarks>
        /// The total length of all key names in the section must be 
        /// less than 32KB in length.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> is a null reference  (Nothing in VB)
        /// </exception>
        public string[] getKeyNames(string strSectionName)
        {
            int iLen;
            string[] astrRetval;

            if (strSectionName == null)
                throw new ArgumentNullException("sectionName");

            //Allocate a buffer for the returned section names.
            IntPtr ptr = Marshal.AllocCoTaskMem(MAX_SECTION_SIZE);

            try
            {
                //Get the section names into the buffer.
                iLen = NativeMethods.GetPrivateProfileString(strSectionName,
                                                            null,
                                                            null,
                                                            ptr,
                                                            MAX_SECTION_SIZE,
                                                            m_strPath);

                astrRetval = convertNullSeperatedStringToStringArray(ptr, iLen);
            }
            finally
            {
                //Free the buffer
                Marshal.FreeCoTaskMem(ptr);
            }

            return astrRetval;
        }

        /// <summary>
        /// Gets the names of all sections in the ini file.
        /// </summary>
        /// <returns>An array of section names.</returns>
        /// <remarks>
        /// The total length of all section names in the section must be 
        /// less than 32KB in length.
        /// </remarks>
        public string[] getSectionNames()
        {
            string[] astrRetval;
            int iLen;

            //Allocate a buffer for the returned section names.
            IntPtr ptr = Marshal.AllocCoTaskMem(MAX_SECTION_SIZE);

            try
            {
                //Get the section names into the buffer.
                iLen = NativeMethods.GetPrivateProfileSectionNames(ptr,
                    MAX_SECTION_SIZE, m_strPath);

                astrRetval = convertNullSeperatedStringToStringArray(ptr, iLen);
            }
            finally
            {
                //Free the buffer
                Marshal.FreeCoTaskMem(ptr);
            }

            return astrRetval;
        }

        /// <summary>
        /// Converts the null seperated pointer to a string into a string array.
        /// </summary>
        /// <param name="ptr">A pointer to string data.</param>
        /// <param name="iLength">
        /// Length of the data pointed to by <paramref name="ptr"/>.
        /// </param>
        /// <returns>
        /// An array of strings; one for each null found in the array of characters pointed
        /// at by <paramref name="ptr"/>.
        /// </returns>
        private static string[] convertNullSeperatedStringToStringArray(IntPtr ptr, int iLength)
        {
            string[] astrRetval;

            if (iLength == 0)
            {
                //Return an empty array.
                astrRetval = new string[0];
            }
            else
            {
                //Convert the buffer into a string.  Decrease the length 
                //by 1 so that we remove the second null off the end.
                string buff = Marshal.PtrToStringAuto(ptr, iLength - 1);

                //Parse the buffer into an array of strings by searching for nulls.
                astrRetval = buff.Split('\0');
            }

            return astrRetval;
        }

        #endregion

        #region Write Methods

        /// <summary>
        /// Writes a <see cref="T:System.String"/> value to the ini file.
        /// </summary>
        /// <param name="sectionName">The name of the section to write to .</param>
        /// <param name="keyName">The name of the key to write to.</param>
        /// <param name="value">The string value to write</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">
        /// The write failed.
        /// </exception>
        private void writeValueInternal(string strSectionName, string strKeyName, string strValue)
        {
            if (!NativeMethods.WritePrivateProfileString(strSectionName, strKeyName, strValue, m_strPath))
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }

        /// <summary>
        /// Writes a <see cref="T:System.String"/> value to the ini file.
        /// </summary>
        /// <param name="strSectionName">The name of the section to write to .</param>
        /// <param name="strKeyName">The name of the key to write to.</param>
        /// <param name="strValue">The string value to write</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">
        /// The write failed.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> or <paramref name="keyName"/> or 
        /// <paramref name="value"/>  are a null reference  (Nothing in VB)
        /// </exception>
        public void writeValue(string strSectionName, string strKeyName, string strValue)
        {
            if (strSectionName == null)
                throw new ArgumentNullException("sectionName");

            if (strKeyName == null)
                throw new ArgumentNullException("keyName");

            if (strValue == null)
                throw new ArgumentNullException("value");

            writeValueInternal(strSectionName, strKeyName, strValue);
        }

        /// <summary>
        /// Writes an <see cref="T:System.Int16"/> value to the ini file.
        /// </summary>
        /// <param name="strSectionName">The name of the section to write to .</param>
        /// <param name="strKeyName">The name of the key to write to.</param>
        /// <param name="sValue">The value to write</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">
        /// The write failed.
        /// </exception>
        public void writeValue(string strSectionName, string strKeyName, short sValue)
        {
            writeValue(strSectionName, strKeyName, (int)sValue);
        }

        /// <summary>
        /// Writes an <see cref="T:System.Int32"/> value to the ini file.
        /// </summary>
        /// <param name="strSectionName">The name of the section to write to .</param>
        /// <param name="strKeyName">The name of the key to write to.</param>
        /// <param name="iValue">The value to write</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">
        /// The write failed.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> or <paramref name="keyName"/> are 
        /// a null reference  (Nothing in VB)
        /// </exception>
        public void writeValue(string strSectionName, string strKeyName, int iValue)
        {
            writeValue(strSectionName, strKeyName, iValue.ToString(CultureInfo.InvariantCulture));
        }

        public void writeValue(string strSectionName, string strKeyName, long lValue)
        {
            writeValue(strSectionName, strKeyName, lValue.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes an <see cref="T:System.Single"/> value to the ini file.
        /// </summary>
        /// <param name="strSectionName">The name of the section to write to .</param>
        /// <param name="strKeyName">The name of the key to write to.</param>
        /// <param name="fValue">The value to write</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">
        /// The write failed.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="strSectionName"/> or <paramref name="strKeyName"/> are 
        /// a null reference  (Nothing in VB)
        /// </exception>
        public void writeValue(string strSectionName, string strKeyName, float fValue)
        {
            writeValue(strSectionName, strKeyName, fValue.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes an <see cref="T:System.Double"/> value to the ini file.
        /// </summary>
        /// <param name="strSectionName">The name of the section to write to .</param>
        /// <param name="strKeyName">The name of the key to write to.</param>
        /// <param name="dValue">The value to write</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">
        /// The write failed.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> or <paramref name="keyName"/> are 
        /// a null reference  (Nothing in VB)
        /// </exception>
        public void writeValue(string strSectionName, string strKeyName, double dValue)
        {
            writeValue(strSectionName, strKeyName, dValue.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 設定數值
        /// </summary>
        /// <param name="strSectionName">區塊名稱</param>
        /// <param name="strKeyName">Key Name</param>
        /// <param name="decValue">數值</param>
        public void writeValue(string strSectionName, string strKeyName, decimal decValue)
        {
            writeValue(strSectionName, strKeyName, decValue.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 設定數值
        /// </summary>
        /// <param name="strSectionName">區塊名稱</param>
        /// <param name="strKeyName">Key Name</param>
        /// <param name="decValue">數值</param>
        public void writeValue(string strSectionName, string strKeyName, bool decValue)
        {
            writeValue(strSectionName, strKeyName, decValue.ToString(CultureInfo.InvariantCulture));
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes the specified key from the specified section.
        /// </summary>
        /// <param name="strSectionName">
        /// Name of the section to remove the key from.
        /// </param>
        /// <param name="strKeyName">
        /// Name of the key to remove.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> or <paramref name="keyName"/> are 
        /// a null reference  (Nothing in VB)
        /// </exception>
        public void deleteKey(string strSectionName, string strKeyName)
        {
            if (strSectionName == null)
                throw new ArgumentNullException("sectionName");

            if (strKeyName == null)
                throw new ArgumentNullException("keyName");

            writeValueInternal(strSectionName, strKeyName, null);
        }

        /// <summary>
        /// Deletes a section from the ini file.
        /// </summary>
        /// <param name="strSectionName">
        /// Name of the section to delete.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sectionName"/> is a null reference (Nothing in VB)
        /// </exception>
        public void deleteSection(string strSectionName)
        {
            if (strSectionName == null)
                throw new ArgumentNullException("sectionName");

            writeValueInternal(strSectionName, null, null);
        }

        #endregion
    }
}
