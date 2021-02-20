using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DSDO.COMMON.UTIL.FILESYSTEM
{
    public class FolderHelper
    {
        /// <summary>
        /// 최대파일경로길이
        /// </summary>
        public const int MAX_PATH = 260;



        /// <summary>
        /// 선택한 폴더의 값 가져오기
        /// </summary>
        /// <param name="title"></param>
        /// <returns>선택한 폴더의 값</returns>
        public static String ChooseFolder(String title)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = title;
            fbd.ShowNewFolderButton = true;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return null;
        }

        /// <summary>
        /// 선택한 폴더 값 가져 오기(절대경로)
        /// </summary>
        /// <param name="title"></param>
        /// <param name="rootFolder"></param>
        /// <returns>선택한 폴더 값</returns>
        public static String ChooseFolder(String title, Environment.SpecialFolder rootFolder)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = title;
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = rootFolder;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return null;
        }

        /// <summary>
        /// Folder 생성
        /// </summary>
        /// <param name="path"></param>
        /// <returns>지정된경로에폴더생성</returns>
        public static DirectoryInfo CreateFolder(String path)
        {
            try
            {
                return Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " > " + ex.StackTrace);
                return null;
            }
        }

        public static void CreateFolderSecurity(string path)
        {
            DirectorySecurity directorySecurity = new DirectorySecurity();
            directorySecurity.SetAccessRuleProtection(true, false);

            //administrators
            IdentityReference adminId = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            directorySecurity.AddAccessRule(new FileSystemAccessRule(adminId, FileSystemRights.FullControl
                , InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));

            // set the owner and the group to admins
            directorySecurity.SetOwner(adminId);
            directorySecurity.SetGroup(adminId);

            string sDirPath;
            sDirPath = path;
            DirectoryInfo di = new DirectoryInfo(sDirPath);
            if (di.Exists == false)
            {
                di.Create(directorySecurity);
            }
        }

        /// <summary>
        /// Folder 삭제
        /// </summary>
        /// <param name="path">지정된경로에폴더및하위파일들을삭제</param>
        public static void DeleteFolder(String path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " > " + ex.StackTrace);
            }
        }

        /// <summary>
        /// 파일및폴더의경로여부
        /// </summary>
        /// <param name="path"></param>
        /// <returns>True or False</returns>
        public static bool IsPathExist(String path)
        {
            return Directory.Exists(path) || File.Exists(path);
        }

        /// <summary>
        /// 폴더경로여부
        /// </summary>
        /// <param name="path"></param>
        /// <returns>True or False</returns>
        public static bool IsDirectoryExist(String path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 파일존재여부
        /// </summary>
        /// <param name="path"></param>
        /// <returns>True or False</returns>
        public static bool IsFileExist(String path)
        {
            return File.Exists(path);
        }

        // 파일 크기 가져오기
        public static long GetFileSize(String path)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
                return fi.Length;
            return 0;
        }

        // 파일 크기 비교
        public static bool IsCompareFileSize(String path1, String path2)
        {
            if (IsFileExist(path1) == false || IsFileExist(path2) == false) //path2가 null 이어도 false 에러가 없다
                return false;

            FileInfo fi1 = new FileInfo(path1);
            FileInfo fi2 = new FileInfo(path2);
            return (fi1.Length == fi2.Length);
        }

        /// <summary>
        /// 대상파일옮기기
        /// </summary>
        /// <param name="fromFile"></param>
        /// <param name="toFile"></param>
        /// <param name="overWrite"></param>
        /// <returns>파일옮기기</returns>
        public static bool CopyFile(String fromFile, String toFile, bool overWrite = true)
        {
            try
            {
                File.Copy(fromFile, toFile, overWrite);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 폴더명만반환
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>폴더명만반환</returns>
        public static String GetPathOnly(String filePath)
        {
            String retString = filePath;
            int strLength = retString.Length;

            for (int stringTrav = strLength - 1; stringTrav >= 0; stringTrav--)
            {
                if (retString[stringTrav].CompareTo('\\') != 0)
                    retString = retString.Remove(stringTrav, 1);
                else
                    return retString;
            }
            return retString;
        }

        /// <summary>
        /// 지정된파일경로만반환
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>지정된파일경로만반환</returns>
        public static String GetFileExtension(String filePath)
        {
            String tmpString = filePath;
            //size_t strLength=System::TcsLen(filePath);
            int strLength = tmpString.Length;
            String retString = "";
            for (int stringTrav = strLength - 1; stringTrav >= 0; stringTrav--)
            {
                if (tmpString[stringTrav].CompareTo('.') == 0)
                {
                    retString = tmpString.Remove(0, stringTrav + 1);
                    break;
                }
            }
            return retString;
        }

        /// <summary>
        /// 파일이름만반환
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>파일이름만반환</returns>
        public static String GetFileTitle(String filePath)
        {
            String tmpString = filePath;
            String retString = "";
            int strLength = tmpString.Length;
            for (int stringTrav = strLength - 1; stringTrav >= 0; stringTrav--)
            {
                if (tmpString[stringTrav].CompareTo('.') == 0)
                {
                    tmpString = tmpString.Remove(stringTrav, tmpString.Length - stringTrav);
                    break;
                }
            }

            strLength = tmpString.Length;
            for (int stringTrav = strLength - 1; stringTrav >= 0; stringTrav--)
            {
                if (tmpString[stringTrav].CompareTo('\\') == 0)
                {
                    tmpString = tmpString.Remove(0, stringTrav + 1);
                    retString = tmpString;
                    break;
                }
            }
            return retString;
        }

        /// <summary>
        /// 파일이름과확장명반환
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>파일이름과확장명반환</returns>
        public static String GetFileName(String filePath)
        {
            return Path.GetFileName(filePath);
            //String tmpString = filePath;
            //String retString = "";
            //int strLength = tmpString.Length;
            //for (int stringTrav = strLength - 1; stringTrav >= 0; stringTrav--)
            //{
            //    if (tmpString[stringTrav].CompareTo('\\') == 0)
            //    {
            //        tmpString = tmpString.Remove(0, stringTrav + 1);
            //        retString = tmpString;
            //        break;
            //    }
            //}
            //return retString;
        }


        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

        /// <summary>
        /// 실행파일명과경로반환
        /// </summary>
        /// <returns>실행파일명과경로반환</returns>
        public static String GetModuleFileName()
        {

            return System.Windows.Forms.Application.ExecutablePath;
        }

        /// <summary>
        /// 실행파일명과전체경로를반환
        /// </summary>
        /// <returns>실행파일명과전체경로를반환</returns>
        public static String GetModuleFileDirectory()
        {
            String retString = GetModuleFileName();
            return GetPathOnly(retString);
        }

        /// <summary>
        /// 디렉토리와파일리스트반환
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns>디렉토리와파일리스트반환</returns>
        public static List<string> GetDirList(string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            List<string> dirList = new List<string>();
            try
            {

                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    dirList.Add(d.Name + "\\");
                }
                foreach (FileInfo f in dir.GetFiles())
                {
                    //Console.WriteLine("File {0}", f.FullName);
                    dirList.Add(f.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
            }
            return dirList;
        }

        public static void DirectoryCopy(string srcDirName, string dstDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(srcDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + srcDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(dstDirName))
            {
                Directory.CreateDirectory(dstDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(dstDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(dstDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }

    public class OpenFileDialogEx : IDisposable
    {
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private IWin32Window owner = null;


        public OpenFileDialogEx(String title = null, String defaultExt = null, String defaultDir = null, String filter = null, IWin32Window owner = null)
        {
            openFileDialog.Title = title;
            openFileDialog.DefaultExt = defaultExt;
            openFileDialog.InitialDirectory = defaultDir;
            openFileDialog.Filter = filter;
            openFileDialog.Multiselect = false;
            this.owner = owner;
        }

        /// <summary>
        /// 파일경로및파일명반환
        /// </summary>
        /// <returns>파일경로및파일명반환</returns>
        public String GetPathName()
        {
            return openFileDialog.FileName;
        }

        /// <summary>
        /// 파일명반환
        /// </summary>
        /// <returns>파일명반환</returns>
        public String GetFileName()
        {
            return FolderHelper.GetFileName(openFileDialog.FileName);
        }

        /// <summary>
        /// 지정된파일경로만반환
        /// </summary>
        /// <returns></returns>
        public String GetFileExt()
        {
            return FolderHelper.GetFileExtension(openFileDialog.FileName);
        }

        /// <summary>
        /// 파일이름만반환
        /// </summary>
        /// <returns></returns>
        public String GetFileTitle()
        {
            return FolderHelper.GetFileTitle(openFileDialog.FileName);
        }

        /// <summary>
        /// 다이얼로그박스와결과값반환
        /// </summary>
        /// <returns></returns>
        public virtual DialogResult ShowDialog()
        {
            if (owner != null)
                return openFileDialog.ShowDialog(owner);
            return openFileDialog.ShowDialog();
        }


        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    if (isDisposing)
                    {
                        // Free any other managed objects here.
                        if (openFileDialog != null)
                        {
                            openFileDialog.Dispose();
                            openFileDialog = null;
                        }
                    }
                    // Free any unmanaged objects here.
                }
            }
            finally
            {
                this.IsDisposed = true;
            }
        }

        ~OpenFileDialogEx() { Dispose(false); }
    }

    public class OpenMultiFileDialog : IDisposable
    {

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private IWin32Window owner = null;

        public OpenMultiFileDialog(String title = null, String defaultExt = null, String defaultDir = null, String filter = null, IWin32Window owner = null)
        {
            openFileDialog.Title = title;
            openFileDialog.DefaultExt = defaultExt;
            openFileDialog.InitialDirectory = defaultDir;
            openFileDialog.Filter = filter;
            openFileDialog.Multiselect = true;
            this.owner = owner;
        }

        /// <summary>
        ///  선택된파일이름리스트반환
        /// </summary>
        /// <returns>선택된파일이름리스트반환</returns>
        public String[] GetFileNames()
        {
            return openFileDialog.FileNames;
        }

        /// <summary>
        /// 다이얼로그박스와결과값반환
        /// </summary>
        /// <returns>다이얼로그박스와결과값반환</returns>
        public virtual DialogResult ShowDialog()
        {
            if (owner != null)
                return openFileDialog.ShowDialog(owner);
            return openFileDialog.ShowDialog();
        }


        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool isDisposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    if (isDisposing)
                    {
                        // Free any other managed objects here.
                        if (openFileDialog != null)
                        {
                            openFileDialog.Dispose();
                            openFileDialog = null;
                        }
                    }

                    // Free any unmanaged objects here.
                }
            }
            finally
            {
                this.IsDisposed = true;
            }
        }

        ~OpenMultiFileDialog() { Dispose(false); }

    }

    public class OpenFolderDialog : IDisposable
    {
        private FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        private IWin32Window owner = null;

        public OpenFolderDialog(String title = null, bool showNewFolderButton = true, IWin32Window owner = null)
        {

            folderBrowserDialog.Description = title;
            folderBrowserDialog.ShowNewFolderButton = showNewFolderButton;
            this.owner = owner;
        }



        /// <summary>
        /// 파일경로및파일명반환
        /// </summary>
        /// <returns>파일경로및파일명반환</returns>
		public String GetPathName()
        {
            return folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// 다이얼로그박스와결과값반환
        /// </summary>
        /// <returns>다이얼로그박스와결과값반환</returns>
        public virtual DialogResult ShowDialog()
        {
            if (owner != null)
                return folderBrowserDialog.ShowDialog(owner);
            return folderBrowserDialog.ShowDialog();
        }


        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    if (isDisposing)
                    {
                        // Free any other managed objects here.
                        if (folderBrowserDialog != null)
                        {
                            folderBrowserDialog.Dispose();
                            folderBrowserDialog = null;
                        }
                    }

                    // Free any unmanaged objects here.
                }
            }
            finally
            {
                this.IsDisposed = true;
            }
        }

        ~OpenFolderDialog() { Dispose(false); }
    }

    public class SaveFileDialogEx : IDisposable
    {
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        private IWin32Window owner = null;

        public SaveFileDialogEx(String title = null, String defaultExt = null, String defaultDir = null, String filter = null, bool overwritePrompt = true, IWin32Window owner = null)
        {
            saveFileDialog.Title = title;
            saveFileDialog.DefaultExt = defaultExt;
            saveFileDialog.InitialDirectory = defaultDir;
            saveFileDialog.Filter = filter;
            saveFileDialog.OverwritePrompt = overwritePrompt;
            this.owner = owner;
        }


        /// <summary>
        /// 파일경로및파일명반환
        /// </summary>
        /// <returns>파일경로및파일명반환</returns>
        public String GetPathName()
        {
            return saveFileDialog.FileName;
        }

        /// <summary>
        /// 파일명반환
        /// </summary>
        /// <returns>파일명반환</returns>
        public String GetFileName()
        {
            return FolderHelper.GetFileName(saveFileDialog.FileName);
        }


        /// <summary>
        /// 지정된파일경로만반환
        /// </summary>
        /// <returns>지정된파일경로만반환</returns>
        public String GetFileExt()
        {
            return FolderHelper.GetFileExtension(saveFileDialog.FileName);
        }

        /// <summary>
        /// 파일이름만반환
        /// </summary>
        /// <returns>파일이름만반환</returns>
        public String GetFileTitle()
        {
            return FolderHelper.GetFileTitle(saveFileDialog.FileName);
        }

        /// <summary>
        /// 다이얼로그박스와결과값반환
        /// </summary>
        /// <returns>다이얼로그박스와결과값반환</returns>
        public virtual DialogResult ShowDialog()
        {
            if (owner != null)
                return saveFileDialog.ShowDialog(owner);
            return saveFileDialog.ShowDialog();
        }


        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool isDisposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    if (isDisposing)
                    {
                        // Free any other managed objects here.
                        if (saveFileDialog != null)
                        {
                            saveFileDialog.Dispose();
                            saveFileDialog = null;
                        }
                    }

                    // Free any unmanaged objects here.
                }
            }
            finally
            {
                this.IsDisposed = true;
            }
        }

        ~SaveFileDialogEx() { Dispose(false); }

    };

}
