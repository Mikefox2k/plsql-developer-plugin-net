using System;
using RGiesecke.DllExport;
using System.Runtime.InteropServices;

namespace PLSQLDevWrapper
{
    //public interface IPLSQLDevWrapper
    //{
    //    void OnMenuClick(int Index);

    //}


    //public abstract class PLSQLProxy
    //{  
    //    virtual public void OnMenuClick(int Index)
    //    {
    //    }
    //    virtual public void OnCreate()
    //    {
    //        return;
    //    }
    //    virtual public void OnActivate()
    //    {
    //    }
    //    virtual public void OnDeactivate()
    //    {
    //    }
    //    virtual public bool CanClose()
    //    {
    //        return true;
    //    }
    //    virtual public void AfterStart()
    //    {
    //    }
    //    virtual public void OnBrowserChange()
    //    {
    //    }
    //    virtual public void OnWindowChange()
    //    {
    //    }
    //    virtual public void OnWindowCreate(int WindowType)
    //    {
    //    }
    //    virtual public void OnWindowCreated(int WindowType)
    //    {
    //    }
    //    virtual public int OnWindowClose(int WindowType, bool Changed)
    //    {
    //        return -1;
    //    }
    //    virtual public bool BeforeExecuteWindow(int WindowType)
    //    {
    //        return false;
    //    }
    //    virtual public void AfterExecuteWindow(int WindowType, int Result)
    //    {
    //    }
    //    virtual public void OnConnectionChange()
    //    {
    //    }
    //    virtual public void OnWindowConnectionChange()
    //    {
    //    }
    //    virtual public void OnPopup(string ObjectType, string ObjectName)
    //    {
    //    }
    //    virtual public void OnMainMenu(string MenuName)
    //    {
    //    }
    //    virtual public bool OnTemplate(string Filename, out string Data)
    //    {
    //        Data = "";
    //        return false;
    //    }
    //    virtual public void OnFileLoaded(int WindowType, int Mode)
    //    {
    //    }
    //    virtual public void OnFileSaved(int WindowType, int Mode)
    //    {
    //    }
    //    virtual public string About()
    //    {
    //        return "Proxy DLL für PLSQL Developer";
    //    }
    //    virtual public void Configure()
    //    {
    //    }
    //    virtual public void CommandLine(int FeedbackHandle, string Command, string Params)
    //    {
    //    }
    //    virtual public string PlugInName()
    //    {
    //        return "PlugInName";
    //    }
    //    virtual public string PlugInSubName()
    //    {
    //        return PlugInName();
    //    }
    //    virtual public string PlugInShortName()
    //    {
    //        return "PlugInShortName";
    //    }
    //    virtual public string DirectFileLoad()
    //    {
    //        return "";
    //    }
    //    virtual public bool DirectFileSave()
    //    {
    //        return false;
    //    }
    //    virtual public string RegisterExport()
    //    {
    //        return "";
    //    }
    //    virtual public bool ExportInit()
    //    {
    //        return false;
    //    }
    //    virtual public void ExportFinished()
    //    {
    //    }
    //    virtual public bool ExportPrepare()
    //    {
    //        return false;
    //    }
    //    virtual public bool ExportData(string Value)
    //    {
    //        return false;
    //    }
    //}

    public class CSharpPLugIn
    {
        private SYS _SysFunc;
        private IDE _IdeFunc;
        private SQL _SqlFunc;
        private string _pluginname;
            
        public SYS SysFunc
        {
            get { return this._SysFunc; }            
        }
        public IDE IdeFunc
        {
            get { return this._IdeFunc; }            
        }
        public SQL SqlFunc
        {
            get { return this.SqlFunc; }
            
        }

        public string PlugInName
        {
            get { return this._pluginname; }
           
        }        

        public CSharpPLugIn(string PlugInName)
        {
            this._pluginname = PlugInName;
            this._SysFunc = new SYS();
            this._IdeFunc = new IDE();
            this._SqlFunc = new SQL();
        }

        [DllExport("RegisterCallback", CallingConvention = CallingConvention.Cdecl)]
        public static void RegisterCallback(int index, IntPtr function)
        {            

            if (Enum.IsDefined(typeof(SYS.SysType), index))
            {
                SYS.RegisterCallback((SYS.SysType)index, function);
            }
            else if (Enum.IsDefined(typeof(SQL.SqlType), index))
            {
                SQL.RegisterCallback((SQL.SqlType)index, function);
            }
            else if (Enum.IsDefined(typeof(IDE.IdeType), index))
            {
                IDE.RegisterCallback((IDE.IdeType)index, function);
            }
        }

        public class SYS
        {
            public enum SysType
            {
                VERSION = 1,
                REGISTRY = 2,
                ROOTDIR = 3,
                ORACLEHOME = 4,
                OCIDLL = 5,
                OCI8MODE = 6,
                XPSTYLE = 7,
                TNSNAMES = 8,
                DELPHIVERSION = 9
            };

            #region Delegates
            private delegate int VERSION_DELEGATE();
            private delegate IntPtr REGISTRY_DELEGATE();
            private delegate IntPtr ROOTDIR_DELEGATE();
            private delegate IntPtr ORACLEHOME_DELEGATE();
            private delegate IntPtr OCIDLL_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool OCI8MODE_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool XPSTYLE_DELEGATE();
            private delegate IntPtr TNSNAMES_DELEGATE(string Param);
            private delegate int DELPHIVERSION_DELEGATE();
            #endregion

            #region var
            private static VERSION_DELEGATE _SYS_VERSION;
            private static REGISTRY_DELEGATE _SYS_REGISTRY;
            private static ROOTDIR_DELEGATE _SYS_ROOTDIR;
            private static ORACLEHOME_DELEGATE _SYS_ORACLEHOME;
            private static OCIDLL_DELEGATE _SYS_OCIDLL;
            private static OCI8MODE_DELEGATE _SYS_OCI8MODE;
            private static XPSTYLE_DELEGATE _SYS_XPSTYLE;
            private static TNSNAMES_DELEGATE _SYS_TNSNAMES;
            private static DELPHIVERSION_DELEGATE _SYS_DELPHIVERSION;

            #endregion

            #region  methods
            /// <summary>       
            ///  Returns the PL/SQL Developer main and subversion, for example 210 for version 2.1.0. 
            ///  This might be useful if you want to use functions that are not available in all versions.  
            /// </summary>
            public int Version()
            {
                return _SYS_VERSION();
            }

            /// <summary>        
            ///  Returns the registry root name of PL/SQL Developer in HKEY_CURRENT_USER (usually  “Software\PL/SQL Developer”). 
            ///  If you want to save your settings in the registry, you can  create a section within the PL/SQL Developer section.   
            ///  Note: In PL/SQL Developer 3.1, the registry section is moved to: (“Software\Allround  Automations\PL/SQL Developer”)  
            /// </summary>
            public string Registry()
            {
                return Marshal.PtrToStringAnsi(_SYS_REGISTRY());
            }

            /// <summary>        
            ///  The directory where PL/SQL Developer is installed, for example “C:\Program Files\PLSQL Developer”.  
            /// </summary>
            public string RootDir()
            {
                return Marshal.PtrToStringAnsi(_SYS_ROOTDIR());
            }

            /// <summary>
            ///  The Oracle directory, for example “C:\Orawin95”  
            /// </summary>    
            public string OracleHome()
            {
                return Marshal.PtrToStringAnsi(_SYS_ORACLEHOME());
            }

            /// <summary>
            ///  Returns the path of the OCI DLL that is used by PL/SQL Developer. If you want to initialize  a new session, 
            ///  you might want to use this value if you want to make sure you’re using the  same OCI version.  
            ///  Available in version 300  
            /// </summary> 
            public string OCIDLL()
            {
                return Marshal.PtrToStringAnsi(_SYS_OCIDLL());
            }

            /// <summary>
            ///  Returns True if PL/SQL Developer is currently connected in OCI8 Mode (Net8).  Available in version 300  
            /// </summary>
            public bool OCI8Mode()
            {
                return _SYS_OCI8MODE();
            }

            /// <summary>
            ///  Returns if PL/SQL Developer is currently using the visual XP style.  Available in version 700  
            /// </summary>
            public bool XPStyle()
            {
                return _SYS_XPSTYLE();
            }

            /// <summary>
            ///  If Param is empty, the function will return the full tnsnames filename.  
            ///  If Param has a value, the connection details of the alias as specified by  Param is returned. 
            ///  If Param is *, the connection details of the current  connection are returned).     
            /// </summary>
            public string TNSNAMES(string Param)
            {
                return Marshal.PtrToStringAnsi(_SYS_TNSNAMES(Param));
            }

            /// <summary>
            ///  Returns the Delphi version used to build PL/SQL Developer. Only useful for very specific functions.   
            /// </summary>
            public int DelphiVersion()
            {
                return _SYS_DELPHIVERSION();
            }
            #endregion

            /// <summary>
            ///  Call Back Funktionen für SYS  
            /// </summary>  <param name="index"></param>  <param name="function"></param>
            public static void RegisterCallback(SysType index, IntPtr function)
            {
                switch (index)
                {
                    case SysType.VERSION:
                        _SYS_VERSION = (VERSION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(VERSION_DELEGATE));
                        break;
                    case SysType.REGISTRY:
                        _SYS_REGISTRY = (REGISTRY_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(REGISTRY_DELEGATE));
                        break;
                    case SysType.ROOTDIR:
                        _SYS_ROOTDIR = (ROOTDIR_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(ROOTDIR_DELEGATE));
                        break;
                    case SysType.ORACLEHOME:
                        _SYS_ORACLEHOME = (ORACLEHOME_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(ORACLEHOME_DELEGATE));
                        break;
                    case SysType.OCIDLL:
                        _SYS_OCIDLL = (OCIDLL_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(OCIDLL_DELEGATE));
                        break;
                    case SysType.OCI8MODE:
                        _SYS_OCI8MODE = (OCI8MODE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(OCI8MODE_DELEGATE));
                        break;
                    case SysType.XPSTYLE:
                        _SYS_XPSTYLE = (XPSTYLE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(XPSTYLE_DELEGATE));
                        break;
                    case SysType.TNSNAMES:
                        _SYS_TNSNAMES = (TNSNAMES_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(TNSNAMES_DELEGATE));
                        break;
                    case SysType.DELPHIVERSION:
                        _SYS_DELPHIVERSION = (DELPHIVERSION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(DELPHIVERSION_DELEGATE));
                        break;
                }
            }
        }

        public class IDE
        {
            public enum IdeType
            {
                MENUSTATE = 10,
                CONNECTED = 11,
                GETCONNECTIONINFO = 12,
                GETBROWSERINFO = 13,
                GETWINDOWTYPE = 14,
                GETAPPHANDLE = 15,
                GETWINDOWHANDLE = 16,
                GETCLIENTHANDLE = 17,
                GETCHILDHANDLE = 18,
                REFRESH = 19,
                CREATEWINDOW = 20,
                OPENFILE = 21,
                SAVEFILE = 22,
                FILENAME = 23,
                CLOSEFILE = 24,
                SETREADONLY = 25,
                GETREADONLY = 26,
                EXECUTESQLREPORT = 27,
                RELOADFILE = 28,
                SETFILENAME = 29,
                GETTEXT = 30,
                GETSELECTEDTEXT = 31,
                GETCURSORWORD = 32,
                GETEDITORHANDLE = 33,
                SETTEXT = 34,
                SETSTATUSMESSAGE = 35,
                SETERRORPOSITION = 36,
                CLEARERRORPOSITIONS = 37,
                GETCURSORWORDPOSITION = 38,
                PERFORM = 39,
                GETCUSTOMKEYWORDS = 60,
                SETCUSTOMKEYWORDS = 61,
                SETKEYWORDS = 62,
                ACTIVATEKEYWORDS = 63,
                REFRESHMENUS = 64,
                SETMENUNAME = 65,
                SETMENUCHECK = 66,
                SETMENUVISIBLE = 67,
                GETMENULAYOUT = 68,
                CREATEPOPUPITEM = 69,
                SETCONNECTION = 70,
                GETOBJECTINFO = 71,
                GETBROWSERITEMS = 72,
                REFRESHBROWSER = 73,
                GETPOPUPOBJECT = 74,
                GETPOPUPBROWSERROOT = 75,
                REFRESHOBJECT = 76,
                FIRSTSELECTEDOBJECT = 77,
                NEXTSELECTEDOBJECT = 78,
                GETOBJECTSOURCE = 79,
                GETWINDOWCOUNT = 80,
                SELECTWINDOW = 81,
                ACTIVATEWINDOW = 82,
                WINDOWISMODIFIED = 83,
                WINDOWISRUNNING = 84,
                WINDOWPIN = 85,
                SPLASHCREATE = 90,
                SPLASHHIDE = 91,
                SPLASHWRITE = 92,
                SPLASHWRITELN = 93,
                SPLASHPROGRESS = 94,
                TEMPLATEPATH = 95,
                EXECUTETEMPLATE = 96,
                GETCONNECTAS = 97,
                SETCONNECTIONAS = 98,
                GETFILEOPENMENU = 100,
                CANSAVEWINDOW = 101,
                OPENFILEEXTERNAL = 102,
                GETFILETYPES = 103,
                GETDEFAULTEXTENSION = 104,
                GETFILEDATA = 105,
                FILESAVED = 106,
                SHOWHTML = 107,
                REFRESHHTML = 108,
                GETPROCEDITEXTENSION = 109,
                GETWINDOWOBJECT = 110,
                FIRSTSELECTEDFILE = 111,
                NEXTSELECTEDFILE = 112,
                REFRESHFILEBROWSER = 113,
                KEYPRESS = 120,
                GETMENUITEM = 121,
                SELECTMENU = 122,
                TRANSLATIONFILE = 130,
                TRANSLATIONLANGUAGE = 131,
                GETTRANSLATEDMENULAYOUT = 132,
                MAINFONT = 133,
                TRANSLATEITEMS = 134,
                TRANSLATESTRING = 135,
                SAVERECOVERYFILES = 140,
                GETCURSORX = 141,
                GETCURSORY = 142,
                SETCURSOR = 143,
                SETBOOKMARK = 144,
                CLEARBOOKMARK = 145,
                GOTOBOOKMARK = 146,
                GETBOOKMARK = 147,
                TABINFO = 148,
                TABINDEX = 149,
                CREATETOOLBUTTON = 150,
                WINDOWHASEDITOR = 153,
                BEAUTIFIEROPTIONS = 160,
                BEAUTIFYWINDOW = 161,
                BEAUTIFYTEXT = 162,
                OBJECTACTION = 165,
                SHOWDIALOG = 166,
                DEBUGLOG = 173,
                GETPARAMSTRING = 174,
                GETPARAMBOOL = 175,
                GETBROWSERFILTER = 176,
                COMMANDFEEDBACK = 180,
                RESULTGRIDROWCOUNT = 190,
                RESULTGRIDCOLCOUNT = 191,
                RESULTGRIDCELL = 192,
                AUTHORIZED = 200,
                WINDOWALLOWED = 201,
                AUTHORIZATION = 202,
                AUTHORIZATIONITEMS = 203,
                ADDAUTHORIZATIONITEM = 204,
                GETPERSONALPREFSETS = 210,
                GETDEFAULTPREFSETS = 211,
                GETPREFASSTRING = 212,
                GETPREFASINTEGER = 213,
                GETPREFASBOOL = 214,
                SETPREFASSTRING = 215,
                SETPREFASINTEGER = 216,
                SETPREFASBOOL = 217,
                GETGENERALPREF = 218,
                PLUGINSETTING = 219,
                GETPROCOVERLOADCOUNT = 220,
                SELECTPROCOVERLOADING = 221,
                GETSESSIONVALUE = 230,
                CHECKDBVERSION = 231,
                GETCONNECTIONINFOEX = 240,
                FINDCONNECTION = 241,
                ADDCONNECTION = 242,
                CONNECTCONNECTION = 243,
                SETMAINCONNECTION = 244,
                GETWINDOWCONNECTION = 245,
                SETWINDOWCONNECTION = 246,
                GETCONNECTIONTREE = 247,
                GETCONNECTIONINFOEX10 = 250,
                FINDCONNECTIONEX10 = 251,
                ADDCONNECTIONEX10 = 252,
                GETCONNECTIONTREEEX10 = 253
            }

            #region Delegates
            private delegate void MENUSTATE_DELEGATE(int ID, int Index, [MarshalAs(UnmanagedType.Bool)] bool Enabled);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool CONNECTED_DELEGATE();
            private delegate void GETCONNECTIONINFO_DELEGATE(out IntPtr Username, out IntPtr Password, out IntPtr Database);
            private delegate void GETBROWSERINFO_DELEGATE(out IntPtr ObjectType, out IntPtr ObjectOwner, out IntPtr ObjectName);
            private delegate int GETWINDOWTYPE_DELEGATE();
            private delegate int GETAPPHANDLE_DELEGATE();
            private delegate int GETWINDOWHANDLE_DELEGATE();
            private delegate int GETCLIENTHANDLE_DELEGATE();
            private delegate int GETCHILDHANDLE_DELEGATE();
            private delegate void REFRESH_DELEGATE();
            private delegate void CREATEWINDOW_DELEGATE(int WindowType, string Text, [MarshalAs(UnmanagedType.Bool)] bool Execute);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool OPENFILE_DELEGATE(int WindowType, string Filename);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SAVEFILE_DELEGATE();
            private delegate IntPtr FILENAME_DELEGATE();
            private delegate void CLOSEFILE_DELEGATE();
            private delegate void SETREADONLY_DELEGATE([MarshalAs(UnmanagedType.Bool)] bool ReadOnly);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETREADONLY_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool EXECUTESQLREPORT_DELEGATE(string SQL, string Title, [MarshalAs(UnmanagedType.Bool)] bool Updateable);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool RELOADFILE_DELEGATE();
            private delegate void SETFILENAME_DELEGATE(string Filename);
            private delegate IntPtr GETTEXT_DELEGATE();
            private delegate IntPtr GETSELECTEDTEXT_DELEGATE();
            private delegate IntPtr GETCURSORWORD_DELEGATE();
            private delegate int GETEDITORHANDLE_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETTEXT_DELEGATE(string Text);
            [return: MarshalAs(UnmanagedType.Bool)]            
            private delegate bool SETSTATUSMESSAGE_DELEGATE(string Text);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETERRORPOSITION_DELEGATE(int Line, int Col);
            private delegate void CLEARERRORPOSITIONS_DELEGATE();
            private delegate int GETCURSORWORDPOSITION_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool PERFORM_DELEGATE(int Param);
            private delegate IntPtr GETCUSTOMKEYWORDS_DELEGATE();
            private delegate void SETCUSTOMKEYWORDS_DELEGATE(string Keywords);
            private delegate void SETKEYWORDS_DELEGATE(int ID, int Style, string Keywords);
            private delegate void ACTIVATEKEYWORDS_DELEGATE();
            private delegate void REFRESHMENUS_DELEGATE(int ID);
            private delegate void SETMENUNAME_DELEGATE(int ID, int Index, string Name);
            private delegate void SETMENUCHECK_DELEGATE(int ID, int Index, [MarshalAs(UnmanagedType.Bool)] bool Enabled);
            private delegate void SETMENUVISIBLE_DELEGATE(int ID, int Index, [MarshalAs(UnmanagedType.Bool)] bool Enabled);
            private delegate IntPtr GETMENULAYOUT_DELEGATE();
            private delegate void CREATEPOPUPITEM_DELEGATE(int ID, int Index, string Name, string ObjectType);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETCONNECTION_DELEGATE(string Username, string Password, string Database);
            private delegate int GETOBJECTINFO_DELEGATE(string AnObject, out IntPtr ObjectType, out IntPtr ObjectOwner, out IntPtr ObjectName, out IntPtr SubObject);
            private delegate IntPtr GETBROWSERITEMS_DELEGATE(string Node, [MarshalAs(UnmanagedType.Bool)] bool GetItems);
            private delegate void REFRESHBROWSER_DELEGATE(string Node);
            private delegate int GETPOPUPOBJECT_DELEGATE(out IntPtr ObjectType, out IntPtr ObjectOwner, out IntPtr ObjectName, out IntPtr SubObject);
            private delegate IntPtr GETPOPUPBROWSERROOT_DELEGATE();
            private delegate void REFRESHOBJECT_DELEGATE(string ObjectType, string ObjectOwner, string ObjectName, int Action);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool FIRSTSELECTEDOBJECT_DELEGATE(string ObjectType, string ObjectOwner, string ObjectName, string SubObject);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool NEXTSELECTEDOBJECT_DELEGATE(string ObjectType, string ObjectOwner, string ObjectName, string SubObject);
            private delegate IntPtr GETOBJECTSOURCE_DELEGATE(string ObjectType, string ObjectOwner, string ObjectName);
            private delegate int GETWINDOWCOUNT_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SELECTWINDOW_DELEGATE(int Index);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool ACTIVATEWINDOW_DELEGATE(int Index);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool WINDOWISMODIFIED_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool WINDOWISRUNNING_DELEGATE();
            private delegate void SPLASHCREATE_DELEGATE(int ProgressMax);
            private delegate void SPLASHHDELEGATE();
            private delegate void SPLASHWRITE_DELEGATE(string s);
            private delegate void SPLASHWRITELN_DELEGATE(string s);
            private delegate void SPLASHPROGRESS_DELEGATE(int Progress);
            private delegate IntPtr TEMPLATEPATH_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool EXECUTETEMPLATE_DELEGATE(string Template, [MarshalAs(UnmanagedType.Bool)] bool NewWindow);
            private delegate IntPtr GETCONNECTAS_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETCONNECTIONAS_DELEGATE(string Username, string Password, string Database, string ConnectAs);
            private delegate IntPtr GETFILEOPENMENU_DELEGATE(int MenuIndex, out int WindowType);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool CANSAVEWINDOW_DELEGATE();
            private delegate void OPENFILEEXTERNAL_DELEGATE(int WindowType, string Data, string FileSystem, string Tag, string Filename);
            private delegate IntPtr GETFILETYPES_DELEGATE(int WindowType);
            private delegate IntPtr GETDEFAULTEXTENSION_DELEGATE(int WindowType);
            private delegate IntPtr GETFILEDATA_DELEGATE();
            private delegate void FILESAVED_DELEGATE(string FileSystem, string FileTag, string Filename);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SHOWHTML_DELEGATE(string Url, string Hash, string Title, string ID);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool REFRESHHTML_DELEGATE(string Url, string ID, [MarshalAs(UnmanagedType.Bool)] bool BringToFront);
            private delegate IntPtr GETPROCEDITEXTENSION_DELEGATE(string oType);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETWINDOWOBJECT_DELEGATE(out IntPtr ObjectType, out IntPtr ObjectOwner, out IntPtr ObjectName, out IntPtr SubObject);
            private delegate void KEYPRESS_DELEGATE(int Key, int Shift);
            private delegate int GETMENUITEM_DELEGATE(string MenuName);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SELECTMENU_DELEGATE(int MenuItem);
            private delegate IntPtr TRANSLATIONFILE_DELEGATE();
            private delegate IntPtr TRANSLATIONLANGUAGE_DELEGATE();
            private delegate IntPtr GETTRANSLATEDMENULAYOUT_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SAVERECOVERYFILES_DELEGATE();
            private delegate int GETCURSORX_DELEGATE();
            private delegate int GETCURSORY_DELEGATE();
            private delegate void SETCURSOR_DELEGATE(int X, int Y);
            private delegate int SETBOOKMARK_DELEGATE(int Index, int X, int Y);
            private delegate void CLEARBOOKMARK_DELEGATE(int Index);
            private delegate void GOTOBOOKMARK_DELEGATE(int Index);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETBOOKMARK_DELEGATE(int Index, int X, int Y);
            private delegate IntPtr TABINFO_DELEGATE(int Index);
            private delegate int TABINDEX_DELEGATE(int Index);
            private delegate void CREATETOOLBUTTON_DELEGATE(int ID, int Index, string Name, string BitmapFile, int BitmapHandle);
            private delegate int BEAUTIFIEROPTIONS_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool BEAUTIFYWINDOW_DELEGATE();
            private delegate IntPtr BEAUTIFYTEXT_DELEGATE(string S);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool OBJECTACTION_DELEGATE(string Action, string ObjectType, string ObjectOwner, string ObjectName);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SHOWDIALOG_DELEGATE(string Dialog, string Param);
            private delegate void DEBUGLOG_DELEGATE(string Msg);
            private delegate IntPtr GETPARAMSTRING_DELEGATE(string Name);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETPARAMBOOL_DELEGATE(string Name);
            private delegate void COMMANDFEEDBACK_DELEGATE(int FeedbackHandle, string S);
            private delegate int RESULTGRIDROWCOUNT_DELEGATE();
            private delegate int RESULTGRIDCOLCOUNT_DELEGATE();
            private delegate IntPtr RESULTGRIDCELL_DELEGATE(int Col, int Row);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool AUTHORIZED_DELEGATE(string Category, string Name, string SubName);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool WINDOWALLOWED_DELEGATE(int WindowType, [MarshalAs(UnmanagedType.Bool)] bool ShowErrorMessage);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool AUTHORIZATION_DELEGATE();
            private delegate IntPtr AUTHORIZATIONITEMS_DELEGATE(string Category);
            private delegate void ADDAUTHORIZATIONITEM_DELEGATE(int PlugInID, string Name);
            private delegate IntPtr GETPERSONALPREFSETS_DELEGATE();
            private delegate IntPtr GETDEFAULTPREFSETS_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETPREFASSTRING_DELEGATE(int PlugInID, string PrefSet, string Name, string Default);
            private delegate int GETPREFASINTEGER_DELEGATE(int PlugInID, string PrefSet, string Name, [MarshalAs(UnmanagedType.Bool)] bool Default);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETPREFASBOOL_DELEGATE(int PlugInID, string PrefSet, string Name, [MarshalAs(UnmanagedType.Bool)] bool Default);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETPREFASSTRING_DELEGATE(int PlugInID, string PrefSet, string Name, string Value);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETPREFASINTEGER_DELEGATE(int PlugInID, string PrefSet, string Name, int Value);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETPREFASBOOL_DELEGATE(int PlugInID, string PrefSet, string Name, [MarshalAs(UnmanagedType.Bool)] bool Value);
            private delegate IntPtr GETGENERALPREF_DELEGATE(string Name);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool PLUGINSETTING_DELEGATE(int PlugInID, string Setting, string Value);
            private delegate int GETPROCOVERLOADCOUNT_DELEGATE(string Owner, string PackageName, string ProcedureName);
            private delegate int SELECTPROCOVERLOADING_DELEGATE(string Owner, string PackageName, string ProcedureName);
            private delegate IntPtr GETSESSIONVALUE_DELEGATE(string Name);
            private delegate int ADDCONNECTION_DELEGATE(string Username, string Password, string Database, string ConnectAs);
            private delegate int WINDOWPIN_DELEGATE(int Pin);
            private delegate IntPtr FIRSTSELECTEDFILE_DELEGATE([MarshalAs(UnmanagedType.Bool)] bool Files, [MarshalAs(UnmanagedType.Bool)] bool Directories);
            private delegate IntPtr NEXTSELECTEDFILE_DELEGATE();
            private delegate void REFRESHFILEBROWSER_DELEGATE();
            private delegate IntPtr MAINFONT_DELEGATE();
            private delegate IntPtr TRANSLATEITEMS_DELEGATE(string Group);
            private delegate IntPtr TRANSLATESTRING_DELEGATE(string ID, string Default, string Param1, string Param2);            
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool WINDOWHASEDITOR_DELEGATE([MarshalAs(UnmanagedType.Bool)] bool CodeEditor);
            private delegate void GETBROWSERFILTER_DELEGATE(int Index, out IntPtr Name, out IntPtr WhereClause, out IntPtr OrderByClause, out IntPtr User, [MarshalAs(UnmanagedType.Bool)] bool Active);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool CHECKDBVERSION_DELEGATE(string Version);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETCONNECTIONINFOEX_DELEGATE(int ix, out IntPtr Username, out IntPtr Password, out IntPtr Database, out IntPtr ConnectAs);
            private delegate int FINDCONNECTION_DELEGATE(string Username, string Database);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool CONNECTCONNECTION_DELEGATE(int ix);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETMAINCONNECTION_DELEGATE(int ix);
            private delegate int GETWINDOWCONNECTION_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETWINDOWCONNECTION_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETCONNECTIONTREE_DELEGATE(int ix, out IntPtr Description, out IntPtr Username, out IntPtr stringPassword, out IntPtr stringDatabase, out IntPtr stringConnectAs, int ID, int ParentID);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETCONNECTIONINFOEX10_DELEGATE(int ix, out IntPtr Username, out IntPtr Password, out IntPtr Database, out IntPtr ConnectAs, out IntPtr Edition, out IntPtr Workspace);
            private delegate int FINDCONNECTIONEX10_DELEGATE(string Username, string Database, string Edition, string Workspace);
            private delegate int ADDCONNECTIONEX10_DELEGATE(string Username, string Password, string Database, string ConnectAs, string Edition, string Workspace);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool GETCONNECTIONTREEEX10_DELEGATE(int ix, out IntPtr Description, out IntPtr Username, out IntPtr Password, out IntPtr Database, out IntPtr ConnectAs, out IntPtr Edition, out IntPtr Workspace, int ID, int ParentID);
            #endregion

            #region vars
            private static MENUSTATE_DELEGATE _MENUSTATE;
            private static CONNECTED_DELEGATE _CONNECTED;
            private static GETCONNECTIONINFO_DELEGATE _GETCONNECTIONINFO;
            private static GETBROWSERINFO_DELEGATE _GETBROWSERINFO;
            private static GETWINDOWTYPE_DELEGATE _GETWINDOWTYPE;
            private static GETAPPHANDLE_DELEGATE _GETAPPHANDLE;
            private static GETWINDOWHANDLE_DELEGATE _GETWINDOWHANDLE;
            private static GETCLIENTHANDLE_DELEGATE _GETCLIENTHANDLE;
            private static GETCHILDHANDLE_DELEGATE _GETCHILDHANDLE;
            private static REFRESH_DELEGATE _REFRESH;
            private static CREATEWINDOW_DELEGATE _CREATEWINDOW;
            private static OPENFILE_DELEGATE _OPENFILE;
            private static SAVEFILE_DELEGATE _SAVEFILE;
            private static FILENAME_DELEGATE _FILENAME;
            private static CLOSEFILE_DELEGATE _CLOSEFILE;
            private static SETREADONLY_DELEGATE _SETREADONLY;
            private static GETREADONLY_DELEGATE _GETREADONLY;
            private static EXECUTESQLREPORT_DELEGATE _EXECUTESQLREPORT;
            private static RELOADFILE_DELEGATE _RELOADFILE;
            private static SETFILENAME_DELEGATE _SETFILENAME;
            private static GETTEXT_DELEGATE _GETTEXT;
            private static GETSELECTEDTEXT_DELEGATE _GETSELECTEDTEXT;
            private static GETCURSORWORD_DELEGATE _GETCURSORWORD;
            private static GETEDITORHANDLE_DELEGATE _GETEDITORHANDLE;
            private static SETTEXT_DELEGATE _SETTEXT;
            private static SETSTATUSMESSAGE_DELEGATE _SETSTATUSMESSAGE;
            private static SETERRORPOSITION_DELEGATE _SETERRORPOSITION;
            private static CLEARERRORPOSITIONS_DELEGATE _CLEARERRORPOSITIONS;
            private static GETCURSORWORDPOSITION_DELEGATE _GETCURSORWORDPOSITION;
            private static PERFORM_DELEGATE _PERFORM;
            private static GETCUSTOMKEYWORDS_DELEGATE _GETCUSTOMKEYWORDS;
            private static SETCUSTOMKEYWORDS_DELEGATE _SETCUSTOMKEYWORDS;
            private static SETKEYWORDS_DELEGATE _SETKEYWORDS;
            private static ACTIVATEKEYWORDS_DELEGATE _ACTIVATEKEYWORDS;
            private static REFRESHMENUS_DELEGATE _REFRESHMENUS;
            private static SETMENUNAME_DELEGATE _SETMENUNAME;
            private static SETMENUCHECK_DELEGATE _SETMENUCHECK;
            private static SETMENUVISIBLE_DELEGATE _SETMENUVISIBLE;
            private static GETMENULAYOUT_DELEGATE _GETMENULAYOUT;
            private static CREATEPOPUPITEM_DELEGATE _CREATEPOPUPITEM;
            private static SETCONNECTION_DELEGATE _SETCONNECTION;
            private static GETOBJECTINFO_DELEGATE _GETOBJECTINFO;
            private static GETBROWSERITEMS_DELEGATE _GETBROWSERITEMS;
            private static REFRESHBROWSER_DELEGATE _REFRESHBROWSER;
            private static GETPOPUPOBJECT_DELEGATE _GETPOPUPOBJECT;
            private static GETPOPUPBROWSERROOT_DELEGATE _GETPOPUPBROWSERROOT;
            private static REFRESHOBJECT_DELEGATE _REFRESHOBJECT;
            private static FIRSTSELECTEDOBJECT_DELEGATE _FIRSTSELECTEDOBJECT;
            private static NEXTSELECTEDOBJECT_DELEGATE _NEXTSELECTEDOBJECT;
            private static GETOBJECTSOURCE_DELEGATE _GETOBJECTSOURCE;
            private static GETWINDOWCOUNT_DELEGATE _GETWINDOWCOUNT;
            private static SELECTWINDOW_DELEGATE _SELECTWINDOW;
            private static ACTIVATEWINDOW_DELEGATE _ACTIVATEWINDOW;
            private static WINDOWISMODIFIED_DELEGATE _WINDOWISMODIFIED;
            private static WINDOWISRUNNING_DELEGATE _WINDOWISRUNNING;
            private static SPLASHCREATE_DELEGATE _SPLASHCREATE;
            private static SPLASHHDELEGATE _SPLASHHIDE;
            private static SPLASHWRITE_DELEGATE _SPLASHWRITE;
            private static SPLASHWRITELN_DELEGATE _SPLASHWRITELN;
            private static SPLASHPROGRESS_DELEGATE _SPLASHPROGRESS;
            private static TEMPLATEPATH_DELEGATE _TEMPLATEPATH;
            private static EXECUTETEMPLATE_DELEGATE _EXECUTETEMPLATE;
            private static GETCONNECTAS_DELEGATE _GETCONNECTAS;
            private static SETCONNECTIONAS_DELEGATE _SETCONNECTIONAS;
            private static GETFILEOPENMENU_DELEGATE _GETFILEOPENMENU;
            private static CANSAVEWINDOW_DELEGATE _CANSAVEWINDOW;
            private static OPENFILEEXTERNAL_DELEGATE _OPENFILEEXTERNAL;
            private static GETFILETYPES_DELEGATE _GETFILETYPES;
            private static GETDEFAULTEXTENSION_DELEGATE _GETDEFAULTEXTENSION;
            private static GETFILEDATA_DELEGATE _GETFILEDATA;
            private static FILESAVED_DELEGATE _FILESAVED;
            private static SHOWHTML_DELEGATE _SHOWHTML;
            private static REFRESHHTML_DELEGATE _REFRESHHTML;
            private static GETPROCEDITEXTENSION_DELEGATE _GETPROCEDITEXTENSION;
            private static GETWINDOWOBJECT_DELEGATE _GETWINDOWOBJECT;
            private static KEYPRESS_DELEGATE _KEYPRESS;
            private static GETMENUITEM_DELEGATE _GETMENUITEM;
            private static SELECTMENU_DELEGATE _SELECTMENU;
            private static TRANSLATIONFILE_DELEGATE _TRANSLATIONFILE;
            private static TRANSLATIONLANGUAGE_DELEGATE _TRANSLATIONLANGUAGE;
            private static GETTRANSLATEDMENULAYOUT_DELEGATE _GETTRANSLATEDMENULAYOUT;
            private static SAVERECOVERYFILES_DELEGATE _SAVERECOVERYFILES;
            private static GETCURSORX_DELEGATE _GETCURSORX;
            private static GETCURSORY_DELEGATE _GETCURSORY;
            private static SETCURSOR_DELEGATE _SETCURSOR;
            private static SETBOOKMARK_DELEGATE _SETBOOKMARK;
            private static CLEARBOOKMARK_DELEGATE _CLEARBOOKMARK;
            private static GOTOBOOKMARK_DELEGATE _GOTOBOOKMARK;
            private static GETBOOKMARK_DELEGATE _GETBOOKMARK;
            private static TABINFO_DELEGATE _TABINFO;
            private static TABINDEX_DELEGATE _TABINDEX;
            private static CREATETOOLBUTTON_DELEGATE _CREATETOOLBUTTON;
            private static BEAUTIFIEROPTIONS_DELEGATE _BEAUTIFIEROPTIONS;
            private static BEAUTIFYWINDOW_DELEGATE _BEAUTIFYWINDOW;
            private static BEAUTIFYTEXT_DELEGATE _BEAUTIFYTEXT;
            private static OBJECTACTION_DELEGATE _OBJECTACTION;
            private static SHOWDIALOG_DELEGATE _SHOWDIALOG;
            private static DEBUGLOG_DELEGATE _DEBUGLOG;
            private static GETPARAMSTRING_DELEGATE _GETPARAMSTRING;
            private static GETPARAMBOOL_DELEGATE _GETPARAMBOOL;
            private static COMMANDFEEDBACK_DELEGATE _COMMANDFEEDBACK;
            private static RESULTGRIDROWCOUNT_DELEGATE _RESULTGRIDROWCOUNT;
            private static RESULTGRIDCOLCOUNT_DELEGATE _RESULTGRIDCOLCOUNT;
            private static RESULTGRIDCELL_DELEGATE _RESULTGRIDCELL;
            private static AUTHORIZED_DELEGATE _AUTHORIZED;
            private static WINDOWALLOWED_DELEGATE _WINDOWALLOWED;
            private static AUTHORIZATION_DELEGATE _AUTHORIZATION;
            private static AUTHORIZATIONITEMS_DELEGATE _AUTHORIZATIONITEMS;
            private static ADDAUTHORIZATIONITEM_DELEGATE _ADDAUTHORIZATIONITEM;
            private static GETPERSONALPREFSETS_DELEGATE _GETPERSONALPREFSETS;
            private static GETDEFAULTPREFSETS_DELEGATE _GETDEFAULTPREFSETS;
            private static GETPREFASSTRING_DELEGATE _GETPREFASSTRING;
            private static GETPREFASINTEGER_DELEGATE _GETPREFASINTEGER;
            private static GETPREFASBOOL_DELEGATE _GETPREFASBOOL;
            private static SETPREFASSTRING_DELEGATE _SETPREFASSTRING;
            private static SETPREFASINTEGER_DELEGATE _SETPREFASINTEGER;
            private static SETPREFASBOOL_DELEGATE _SETPREFASBOOL;
            private static GETGENERALPREF_DELEGATE _GETGENERALPREF;
            private static PLUGINSETTING_DELEGATE _PLUGINSETTING;
            private static GETPROCOVERLOADCOUNT_DELEGATE _GETPROCOVERLOADCOUNT;
            private static SELECTPROCOVERLOADING_DELEGATE _SELECTPROCOVERLOADING;
            private static GETSESSIONVALUE_DELEGATE _GETSESSIONVALUE;
            private static ADDCONNECTION_DELEGATE _ADDCONNECTION;
            private static WINDOWPIN_DELEGATE _WINDOWPIN;
            private static FIRSTSELECTEDFILE_DELEGATE _FIRSTSELECTEDFILE;
            private static NEXTSELECTEDFILE_DELEGATE _NEXTSELECTEDFILE;
            private static REFRESHFILEBROWSER_DELEGATE _REFRESHFILEBROWSER;
            private static MAINFONT_DELEGATE _MAINFONT;
            private static TRANSLATEITEMS_DELEGATE _TRANSLATEITEMS;
            private static TRANSLATESTRING_DELEGATE _TRANSLATESTRING;
            private static WINDOWHASEDITOR_DELEGATE _WINDOWHASEDITOR;
            private static GETBROWSERFILTER_DELEGATE _GETBROWSERFILTER;
            private static CHECKDBVERSION_DELEGATE _CHECKDBVERSION;
            private static GETCONNECTIONINFOEX_DELEGATE _GETCONNECTIONINFOEX;
            private static FINDCONNECTION_DELEGATE _FINDCONNECTION;
            private static CONNECTCONNECTION_DELEGATE _CONNECTCONNECTION;
            private static SETMAINCONNECTION_DELEGATE _SETMAINCONNECTION;
            private static GETWINDOWCONNECTION_DELEGATE _GETWINDOWCONNECTION;
            private static SETWINDOWCONNECTION_DELEGATE _SETWINDOWCONNECTION;
            private static GETCONNECTIONTREE_DELEGATE _GETCONNECTIONTREE;
            private static GETCONNECTIONINFOEX10_DELEGATE _GETCONNECTIONINFOEX10;
            private static FINDCONNECTIONEX10_DELEGATE _FINDCONNECTIONEX10;
            private static ADDCONNECTIONEX10_DELEGATE _ADDCONNECTIONEX10;
            private static GETCONNECTIONTREEEX10_DELEGATE _GETCONNECTIONTREEEX10;
            #endregion

            #region methods
            /// <summary>
            ///  Use this function to enable or disable a menu. The ID is the Plug-In ID, which is given by  the IdentifyPlugIn function. The Index is the menu index, which the menu was related to by  the CreateMenuItem function. The Enabled boolean determines if the menu item is enabled or  grayed.  
            /// </summary>
            public void MenuState(int ID, int Index, bool Enabled)
            {
                _MENUSTATE(ID, Index, Enabled);
            }

            /// <summary>
            ///  Returns a boolean that indicates if PL/SQL Developer is currently connected to a database.  
            /// </summary>
            public bool Connected()
            {
                return _CONNECTED();
            }

            /// <summary>
            ///  Returns the username, password and database of the current connection.  
            /// </summary>
            public void GetConnectionInfo(out string Username, out string Password, out string Database)
            {
                IntPtr pUsername, pPassword, pDatabase;
                _GETCONNECTIONINFO(out pUsername, out pPassword, out pDatabase);

                Username = Marshal.PtrToStringAnsi(pUsername);
                Password = Marshal.PtrToStringAnsi(pPassword);
                Database = Marshal.PtrToStringAnsi(pDatabase);
            }

            /// <summary>
            ///  Returns information about the selected item in the Browser. If no item is selected, all items are empty.  
            /// </summary>
            public void GetBrowserInfo(out string ObjectType, out string ObjectOwner, out string ObjectName)
            {
                IntPtr pObjectType, pObjectOwner, pObjectName;
                _GETBROWSERINFO(out pObjectType, out pObjectOwner, out pObjectName);

                ObjectType = Marshal.PtrToStringAnsi(pObjectType);
                ObjectOwner = Marshal.PtrToStringAnsi(pObjectOwner);
                ObjectName = Marshal.PtrToStringAnsi(pObjectName);
            }

            /// <summary>
            /// Returns the type of the current window.  1 = SQL Window 2 = Test Window 3 = Procedure Window 4 = Command Window 5 = Plan Window 6 = Report Window 0 = None of the above
            /// </summary>    
            public int GetWindowType()
            {
                return _GETWINDOWTYPE();
            }

            /// <summary>
            ///  Returns the Application handle of PL/SQL Developer  
            /// </summary>
            public int GetAppHandle()
            {
                return _GETAPPHANDLE();
            }

            /// <summary>
            ///  Returns the handle of PL/SQL Developers main window  
            /// </summary>
            public int GetWindowHandle()
            {
                return _GETWINDOWHANDLE();
            }

            /// <summary>
            ///  Returns the handle of PL/SQL Developers client window  
            /// </summary>
            public int GetClientHandle()
            {
                return _GETCLIENTHANDLE();
            }

            /// <summary>
            ///  Returns the handle of the active child form  
            /// </summary>
            public int GetChildHandle()
            {
                return _GETCHILDHANDLE();
            }

            /// <summary>
            ///  Resets the state of the menus, buttons and the active window.  You can call this function if  you made some changes that affect the state of a menu or window which are unnoticed by PL/SQL  Developer.   Available in version 213  
            /// </summary>
            public void Refresh()
            {
                _REFRESH();
            }

            /// <summary>
            ///  Creates a new window. The Text parameter contains text that is placed in the window. If the Execute boolean is true, the Window will be executed.  WindowType can be one of the following values:  1 = SQL Window  2 = Test Window  3 = Procedure Window  4 = Command Window  5 = Plan Window  6 = Report Window  
            /// </summary>  

            public void CreateWindow(int WindowType, string Text, bool Execute)
            {
                _CREATEWINDOW(WindowType, Text, Execute);
            }

            /// <summary>
            ///  Creates a window of type WindowType and loads the specified file.  WindowType can be one of the following values:  1 = SQL Window  2 = Test Window  3 = Procedure Window  4 = Command Window  The function returns True if successful.   Version 301 and higher  If you pass 0 as WindowType, PL/SQL Developer will try to determine the actual WindowType on the extension of the filename.  
            /// </summary>


            public bool OpenFile(int WindowType, string Filename)
            {
                return _OPENFILE(WindowType, Filename);
            }

            /// <summary>
            ///  This function saves the current window. It returns True if successful.  
            /// </summary>
            public bool SaveFile()
            {
                return _SAVEFILE();
            }

            /// <summary>
            ///  Return the filename of the current child window.  See also SetFilename()  
            /// </summary>
            public string Filename()
            {
                return Marshal.PtrToStringAnsi(_FILENAME());
            }

            /// <summary>
            ///  Closes the current child window  
            /// </summary>
            public void CloseFile()
            {
                _CLOSEFILE();
            }

            /// <summary>
            ///  Set the ReadOnly status of the current Window  
            /// </summary>
            public void SetReadOnly(bool ReadOnly)
            {
                _SETREADONLY(ReadOnly);
            }

            /// <summary>
            ///  Get the ReadOnly status of the current Window  Available in version 213  
            /// </summary>
            public bool GetReadOnly()
            {
                return _GETREADONLY();
            }

            /// <summary>
            ///  This function will execute a query (SQL parameter) and display the result in a ‘result only’  SQL Window. Title will be used as the window name and the Updateable parameter determines if  the results are updateable.   Available in version 300  
            /// </summary>
            public bool ExecuteSQLReport(string SQL, string Title, bool Updateable)
            {
                return _EXECUTESQLREPORT(SQL, Title, Updateable);
            }

            /// <summary>
            ///  Forces the active child window to reload its file from disk.  Note: In PL/SQL Developer 4  there will no longer be a warning message when modifications were made.  Available in version 301  
            /// </summary>
            public bool ReloadFile()
            {
                return _RELOADFILE();
            }

            /// <summary>
            ///  Set the filename of the active child window. The filename should contain a valid path, but  the file does not need to exist. The new filename will be used when the file is saved.  If  the Filename parameter is an empty string, the Window will behave as a new created Window.   Available in version 303  
            /// </summary>
            public void SetFilename(string Filename)
            {
                _SETFILENAME(Filename);
            }

            /// <summary>
            ///  Retrieves the text from the current child window.  
            /// </summary>
            public string GetText()
            {
                return Marshal.PtrToStringAnsi(_GETTEXT());
            }

            /// <summary>
            ///  Retrieves the selected text from the current child window.  
            /// </summary>
            public string GetSelectedText()
            {
                return Marshal.PtrToStringAnsi(_GETSELECTEDTEXT());
            }

            /// <summary>
            ///  Retrieves the word the cursor is on in the current child window.  
            /// </summary>
            public string GetCursorWord()
            {
                return Marshal.PtrToStringAnsi(_GETCURSORWORD());
            }

            /// <summary>
            ///  Returns the handle of the editor of the current child window.  
            /// </summary>
            public int GetEditorHandle()
            {
                return _GETEDITORHANDLE();
            }

            /// <summary>
            ///  Sets the text in the editor of current window. If this failed for some reason (ReadOnly?),  the function returns false.   Available in version 213  
            /// </summary>
            public bool SetText(string Text)
            {
                return _SETTEXT(Text);
            }

            /// <summary>
            ///  Places a message in the status bar of the current window, returns false if the window did not  have a status bar.   Available in version 213  
            /// </summary>
            public bool SetStatusMessage(string Text)
            {
                return _SETSTATUSMESSAGE(Text);
            }

            /// <summary>
            ///  Highlights the given line and places the cursor at the given position.  This will only work  when the active window is a procedure window, if not, the function returns false.   Available in version 213  
            /// </summary>
            public bool SetErrorPosition(int Line, int Col)
            {
                return _SETERRORPOSITION(Line, Col);
            }

            /// <summary>
            ///  Resets the highlighted lines.  Available in version 213  
            /// </summary>
            public void ClearErrorPositions()
            {
                _CLEARERRORPOSITIONS();
            }

            /// <summary>
            ///  This function returns the location of the cursor in the word after a call to  GetCursorWord. Possible return values:     0: Unknown    1: Cursor was at start of word    2: Cursor was somewhere in the middle    3: Cursor was at the end   Available in version 400  
            /// </summary>
            public int GetCursorWordPosition()
            {
                return _GETCURSORWORDPOSITION();
            }

            /// <summary>
            ///  This function allows you to perform a specific action as if the menu item as specified in  Param was selected. The following values are supported:    1: Execute    2: Break    3: Kill    4: Commit    5: Rollback    6: Print   Available in version 400  
            /// </summary>
            public bool Perform(int Param)
            {
                return _PERFORM(Param);
            }

            /// <summary>
            ///  Returns a list of all keywords as entered in the ‘custom keywords’ option in the Editor  preference.   Available in version 300  
            /// </summary>
            public string GetCustomKeywords()
            {
                return Marshal.PtrToStringAnsi(_GETCUSTOMKEYWORDS());
            }

            /// <summary>
            ///  Fills the custom keywords with the words in the Keywords parameter. Words should be  separated by cr/lf. The currently used keywords will be overwritten.   Available in version 300  
            /// </summary>    
            public void SetCustomKeywords(string Keywords)
            {
                _SETCUSTOMKEYWORDS(Keywords);
            }

            /// <summary>
            ///  Adds a number of keywords with a specific style.  This function is more specific then  SetCustomKeywords because this one can set multiple sets of keywords for different  highlighting styles.  ID should be the PlugIn ID as returned by the IdentifyPlugIn function.  Style can be one of the following values:  10: Custom  11: Keywords  12: Comment  13: Strings  14: Numbers  15: Symbols  Keywords is a cr/lf separated list of words. You can define one list per style.  Available in version 300  
            /// </summary>    
            public void SetKeywords(int ID, int Style, string Keywords)
            {
                _SETKEYWORDS(ID, Style, Keywords);
            }

            /// <summary>
            ///  Activates the keywords as defined by the SetKeywords function.  Available in version 300  
            /// </summary>
            public void ActivateKeywords()
            {
                _ACTIVATEKEYWORDS();
            }

            /// <summary>
            ///  When this function is called, all menus for this Plug-In are removed and CreateMenuItem will  be called to build a new set of menus. This only makes sense if you supply a different set  of menu-items.   Available in version 300  
            /// </summary>
            public void RefreshMenus(int ID)
            {
                _REFRESHMENUS(ID);
            }

            /// <summary>
            ///  This function allows you to rename a certain menu-item.  ID is the Plug-In ID, Index is the Menu number and name is the new menu name.  Available in version 300  
            /// </summary>
            public void SetMenuName(int ID, int Index, string Name)
            {
                _SETMENUNAME(ID, Index, Name);
            }

            /// <summary>
            ///  You can display or remove a check mark for a menu-item.  Available in version 300  
            /// </summary>
            public void SetMenuCheck(int ID, int Index, bool Enabled)
            {
                _SETMENUCHECK(ID, Index, Enabled);
            }

            /// <summary>
            ///  With this function you can hide or show a specific menu. You can use this instead of  MenuState.  Available in version 300  
            /// </summary>
            public void SetMenuVisible(int ID, int Index, bool Enabled)
            {
                _SETMENUVISIBLE(ID, Index, Enabled);
            }

            /// <summary>
            ///  Returns a list of all standard PL/SQL Developer menu items. Items are separated by cr/lf and  child menu level is indicated by a number of spaces.  You can use this function to build an  advanced user configuration dialog where the user could be able to select place where he  wants to insert the Plug-In menus.   Available in version 300  
            /// </summary>
            public string GetMenulayout()
            {
                return Marshal.PtrToStringAnsi(_GETMENULAYOUT());
            }

            /// <summary>
            ///  With this function you can add items to certain popup menus. The ID is the Plug-In ID and  the index is the menu index. You can pass any number as the menu index, it can be an  existing menu (as used by CreateMenuItem) or anything else. If the popup menu gets selected,  OnMenuClick is called with the corresponding index.  The Name is the menu name as it will be  displayed. The ObjectType determines in which popup menus this item will be displayed. Some  possible values are: ‘TABLE’, ‘VIEW’, ‘PACKAGE’, etc.   Version 301 and higher  If you pass one of the following values as ObjectType, you can add items to specific Windows.    PROGRAMWINDOW    SQLWINDOW    TESTWINDOW    COMMANDWINDOW   Version 400 and higher  You can add popup items to Object Browser items like Tables, Views, etc. by passing their  name as ObjectType.   Version 510 and higher  If you want to create popup menus for multiple selected items (of the same object type), you  can add a + to the ObjectType parameter like ‘TABLE+’, ‘VIEW+’, etc. The OnMenuClick will be  called for every selected item, and the GetPopupObject will return the correct details.   Available in version 300  
            /// </summary>
            /// 
            public void CreatePopupItem(int ID, int Index, string Name, string ObjectType)
            {
                _CREATEPOPUPITEM(ID, Index, Name, ObjectType);
            }

            /// <summary>
            ///  This function allows you to reconnect PL/SQL Developer as another user. The return value  indicates if the connection was successful.  The function will fail if there is a childwindow  with an active query.  Also see SetConnectionAs  Available in version 301  
            /// </summary>
            public bool SetConnection(string Username, string Password, string Database)
            {
                return _SETCONNECTION(Username, Password, Database);
            }

            /// <summary>
            ///  This function returns Oracle information about the item in the AnObject parameter. The  SubObject returns the name of the procedure if the Object is a packaged procedure.   Available in version 400  
            ///  /// </summary>
            public int GetObjectInfo(string AnObject, out string ObjectType, out string ObjectOwner, out string ObjectName, out string SubObject)
            {
                IntPtr pObjectType, pObjectOwner, pObjectName, pSubObject;
                int result = _GETOBJECTINFO(AnObject, out  pObjectType, out pObjectOwner, out pObjectName, out pSubObject);

                ObjectType = Marshal.PtrToStringAnsi(pObjectType);
                ObjectOwner = Marshal.PtrToStringAnsi(pObjectOwner);
                ObjectName = Marshal.PtrToStringAnsi(pObjectName);
                SubObject = Marshal.PtrToStringAnsi(pSubObject);

                return result;
            }

            /// <summary>
            ///  Returns a cr/lf separated list of items from the Object Browser. The Node parameter  determines which items are returned. This can be one of the main items like TABLES, but you  can also us a slash to get more specific items like TABLES/DEPT/COLUMNS.  The GetItems  [MarshalAs(UnmanagedType.Bool)] boolean determines if PL/SQL Developer will fetch these values from the database if the item  has not been opened yet in the Browser.   Available in version 400  
            /// </summary>
            public string GetBrowserItems(string Node, bool GetItems)
            {
                return Marshal.PtrToStringAnsi(_GETBROWSERITEMS(Node, GetItems));
            }

            /// <summary>
            ///  Force a refresh to the Object Browser. If Node is empty, all items are refreshed. 
            ///  To refresh a specific item you can enter the name in the Node parameter.   
            ///  Note: Version 500 allows you to pass a * to refresh the current selected browser item.   
            ///  Available in version 400  
            ///  Note:  Version 500 allows you to pass a * to refresh the current selected  browser item. 
            ///  Note:  Version 600 allows you to pass a ** to refresh to parent of the current  browser item, and you can pass *** to refresh to root item.  
            /// </summary>
            public void RefreshBrowser(string Node)
            {
                _REFRESHBROWSER(Node);
            }

            /// <summary>
            ///  This function returns information about the item for which a popup menu (created with  CreatePopupItem) was activated.  
            ///  If the item is a Browser folder, the name of the folder  will be returned in ObjectName and ObjectType will return ‘FOLDER’  
            ///  Available in version 400  
            /// </summary>
            public int GetPopupObject(out string ObjectType, out string ObjectOwner, out string ObjectName, out string SubObject)
            {
                IntPtr pObjectType, pObjectOwner, pObjectName, pSubObject;
                int result = _GETPOPUPOBJECT(out pObjectType, out pObjectOwner, out  pObjectName, out  pSubObject);

                ObjectType = Marshal.PtrToStringAnsi(pObjectType);
                ObjectOwner = Marshal.PtrToStringAnsi(pObjectOwner);
                ObjectName = Marshal.PtrToStringAnsi(pObjectName);
                SubObject = Marshal.PtrToStringAnsi(pSubObject);

                return result;
            }

            /// <summary>
            ///  This function returns the name of browser root item for which a popup menu (created with  CreatePopupItem) was activated.   Available in version 400  
            /// </summary>
            public string GetPopupBrowserRoot()
            {
                return Marshal.PtrToStringAnsi(_GETPOPUPBROWSERROOT());
            }

            /// <summary>
            ///  If you modify database objects in your Plug-In and you want to update PL/SQL Developer to  reflect these changes, you can do so by calling this function. You should pass the object  type, owner, name and the action that you performed on the object. The action can be one of  the following:     1 = Object created    2 = Object modified    3 = Object deleted   PL/SQL Developer will update the browser and all windows that might use the object.   Available in version 400  
            /// </summary>
            public void RefreshObject(string ObjectType, string ObjectOwner, string ObjectName, int Action)
            {
                _REFRESHOBJECT(ObjectType, ObjectOwner, ObjectName, Action);
            }

            /// <summary>
            ///  This function will return the details of the first selected in the Browser. The function  will return false if no items are selected.  Use in combination with NextSelectedObject  to determine all selected items.   Available in version 500  
            /// </summary>
            public bool FirstSelectedObject(string ObjectType, string ObjectOwner, string ObjectName, string SubObject)
            {
                return _FIRSTSELECTEDOBJECT(ObjectType, ObjectOwner, ObjectName, SubObject);
            }

            /// <summary>
            ///  This function can be called after a call to FirstSelectedObject to determine all selected  objects. You can keep calling this function until it returns false.   Available in version 500  
            /// </summary>
            public bool NextSelectedObject(string ObjectType, string ObjectOwner, string ObjectName, string SubObject)
            {
                return _NEXTSELECTEDOBJECT(ObjectType, ObjectOwner, ObjectName, SubObject);
            }

            /// <summary>
            ///  Returns the source for the specified object. This function will only return source for  objects that actually have source (packages, views, …).   Available in version 511  
            /// </summary>
            public string GetObjectSource(string ObjectType, string ObjectOwner, string ObjectName)
            {
                return Marshal.PtrToStringAnsi(_GETOBJECTSOURCE(ObjectType, ObjectOwner, ObjectName));
            }

            /// <summary>
            ///  Returns the number of child windows in PL/SQL Developer. In combination with  SelectWindow you can communicate with all child windows.   Available in version 301  
            /// </summary>
            public int GetWindowCount()
            {
                return _GETWINDOWCOUNT();
            }

            /// <summary>
            ///  This function will ‘select’ one of PL/SQL Developers child Windows. 
            ///  Index is the window  number where 0 is the top child window. The return value will indicate if the window  existed.   
            ///  Normally all window related functions communicate with the active child window. With this  function you can select any window 
            ///  and all window-related IDE functions will refer to the  selected window.   
            ///  Note: SelectWindow does not actually bring the window to front, you need  ActivateWindow to do that.   
            ///  Available in version 301  
            /// </summary>
            public bool SelectWindow(int Index)
            {
                return _SELECTWINDOW(Index);
            }

            /// <summary>
            ///  Brings the Indexth child window with to front.  Available in version 301  
            /// </summary>
            /// </summary>
            public bool ActivateWindow(int Index)
            {
                return _ACTIVATEWINDOW(Index);
            }

            /// <summary>
            ///  Returns if the contents of the window is modified.  Available in version 301  
            /// </summary>
            public bool WindowIsModified()
            {
                return _WINDOWISMODIFIED();
            }

            /// <summary>
            ///  Returns if there is anything running in the current window.  Available in version 301  
            /// </summary>
            public bool WindowIsRunning()
            {
                return _WINDOWISRUNNING();
            }
            /// <summary>
            ///  Creates an empty splash screen (the one you see when PL/SQL Developer is starting or  printing) which allows you to show some kind of progress on lengthy operations.  If the  ProgressMax parameter is larger then 0, a progress bar is displayed which you can advance  with the SplashProgress function.   Note: There can only be one splash screen active at a  time. If a splash screen is created while one was active, the first one will get re-used.   Available in version 303  
            /// </summary>
            /// 
            public void SplashCreate(int ProgressMax)
            {
                _SPLASHCREATE(ProgressMax);
            }

            /// <summary>
            ///  Hides the splash screen. This function will work on any splash screen, you can even hide the  one created by PL/SQL Developer.   Available in version 303  
            /// </summary>
            public void SplashHide()
            {
                _SPLASHHIDE();
            }

            /// <summary>
            ///  Add text to the splash screen.  Available in version 303  
            /// </summary>
            public void SplashWrite(string s)
            {
                _SPLASHWRITE(s);
            }

            /// <summary>
            ///  Add text to the splash screen beginning on the next line.  Available in version 303  
            /// </summary>
            public void SplashWriteLn(string s)
            {
                _SPLASHWRITELN(s);
            }

            /// <summary>
            ///  If the splash screen was created with a progress bar, you can indicate progress with this function.  Available in version 303  
            /// </summary>
            public void SplashProgress(int Progress)
            {
                _SPLASHPROGRESS(Progress);
            }

            /// <summary>
            ///  This function returns the path where the templates are located.  Available in version 400  
            /// </summary>
            public string TemplatePath()
            {
                return Marshal.PtrToStringAnsi(_TEMPLATEPATH());
            }

            /// <summary>
            ///  If you want to execute a template from within your PlugIn you can do so with this function.  The NewWindow parameter indicates if a new window should be created or that the result of the  template should be pasted at the current cursor position in the active window. The template  parameter should contain the template name. If the template is located in one or more  folders, the folder name(s) should be prefixed to the template name separated by a backslash.   Available in version 400  
            /// </summary>
            public bool ExecuteTemplate(string Template, bool NewWindow)
            {
                return _EXECUTETEMPLATE(Template, NewWindow);
            }

            /// <summary>
            ///  Use this function to determine if the current connection has a specific ‘Connect As’.  Possible return values are: '', 'SYSDBA' and 'SYSOPER'  Available in version 500  
            /// </summary>
            public string GetConnectAs()
            {
                return Marshal.PtrToStringAnsi(_GETCONNECTAS());
            }

            /// <summary>
            ///  Identical to SetConnection, but with an option to specify a ConnectAs parameter. You can  pass 'SYSDBA' or 'SYSOPER', all other values will be handled as 'NORMAL'.   Available in version 500  
            /// </summary>
            public bool SetConnectionAs(string Username, string Password, string Database, string ConnectAs)
            {
                return _SETCONNECTIONAS(Username, Password, Database, ConnectAs);
            }

            /// <summary>
            ///          If you want to create a new ‘File Open’ menu with the same items as the standard menu, you  can use this function to determine the standard items. You can call this function in a loop  while incrementing MenuIndex (starting with 0) until the return value is an empty string.  The return values are the menu names in the File Open menu and the WindowType is the  corresponding window type.   Available in version 400  
            /// </summary>
            public string GetFileOpenMenu(int MenuIndex, out int WindowType)
            {
                return Marshal.PtrToStringAnsi(_GETFILEOPENMENU(MenuIndex, out WindowType));
            }

            /// <summary>
            ///  Returns True if the active child window can be saved. (which are the SQL, Test, Program and  Command windows).   Available in version 400  
            /// </summary>
            public bool CanSaveWindow()
            {
                return _CANSAVEWINDOW();
            }

            /// <summary>
            ///  Creates a new Window (of type WindowType) for the specified (and registered) FileSystem, Tag  and Filename.   Available in version 400  
            /// </summary>
            public void OpenFileExternal(int WindowType, string Data, string FileSystem, string Tag, string Filename)
            {
                _OPENFILEEXTERNAL(WindowType, Data, FileSystem, Tag, Filename);
            }

            /// <summary>
            ///  Returns the defined filetypes for a specific WindowType.   Available in version 400  
            /// </summary>
            public string GetFileTypes(int WindowType)
            {
                return Marshal.PtrToStringAnsi(_GETFILETYPES(WindowType));
            }

            /// <summary>
            ///  Returns the default extension (without period) for a specific window type.  Available in version 400  
            /// </summary>
            public string GetDefaultExtension(int WindowType)
            {
                return Marshal.PtrToStringAnsi(_GETDEFAULTEXTENSION(WindowType));
            }

            /// <summary>
            ///  Returns the data of a window. You can use this function to get the data and save it.   Available in version 400  
            /// </summary>
            public string GetFiledata()
            {
                return Marshal.PtrToStringAnsi(_GETFILEDATA());
            }

            /// <summary>
            ///  You can call this function when a file is saved successfully. The filename will be set in  the Window caption and the status will display that the file is ‘saved successfully’.  FileSystem and FileTag can be nil.   Available in version 400  
            /// </summary>

            public void FileSaved(string FileSystem, string FileTag, string Filename)
            {
                _FILESAVED(FileSystem, FileTag, Filename);
            }

            /// <summary>
            /// This function displays a html file in a child window. The url parameter identifies the file and the hash parameter allows you to jump to a specific location. The title parameter will be used as window title. You can refresh the contents of an already opened window by specifying an ID. If ID is not empty, and a window exists with the same ID, this will be used, otherwise a new window will be created.  
            /// </summary>
            /// <param name="Url"></param>
            /// <param name="Hash"></param>
            /// <param name="Title"></param>
            /// <param name="ID"></param>
            /// <returns></returns>
            public bool ShowHTML(string Url, string Hash, string Title, string ID)
            {
                return _SHOWHTML(Url, Hash, Title, ID);
            }

            /// <summary>
            ///  Refresh the contents of a HTML Window. You can pass a url to refress all windows that show a  specific url, or you can pass an ID to refresh a specific Window.   Available in version 512  
            /// </summary>
            public bool RefreshHTML(string Url, string ID, bool BringToFront)
            {
                return _REFRESHHTML(Url, ID, BringToFront);
            }

            /// <summary>
            /// Returns the define file extension of a specific object type. The oType parameter can hold one of the following valies:    
            /// </summary>
            /// <param name="oType"></param>
            /// <returns>FUNCTION, PROCEDURE, TRIGGER, PACKAGE, PACKAGE BODY, PACKAGE SPEC AND BODY, TYPE, TYPE BODY, TYPE SPEC AND BODY, JAVA SOURCE</returns>
            public string GetProcEditExtension(string oType)
            {
                return Marshal.PtrToStringAnsi(_GETPROCEDITEXTENSION(oType));
            }

            /// <summary>
            ///  Get info about the object opened in a Window. This will only work for Program Windows.   Available in version 512  
            /// </summary>
            public bool GetWindowObject(out string ObjectType, out string ObjectOwner, out string ObjectName, out string SubObject)
            {
                IntPtr pObjectType, pObjectOwner, pObjectName, pSubObject;
                bool result = _GETWINDOWOBJECT(out pObjectType, out pObjectOwner, out pObjectName, out pSubObject);

                ObjectType = Marshal.PtrToStringAnsi(pObjectType);
                ObjectOwner = Marshal.PtrToStringAnsi(pObjectOwner);
                ObjectName = Marshal.PtrToStringAnsi(pObjectName);
                SubObject = Marshal.PtrToStringAnsi(pSubObject);

                return result;
            }

            /// <summary>
            ///  Simulates a key press. You can use this function to do the things you can also do with the  keyboard. The Key parameter is the virtual key code of the key, and the Shift parameter  holds the status of the Shift Ctrl and Alt keys. You can combine the following values:   1 = Shift   2 = Alt   3 = Ctrl  Available in version 510  
            /// </summary>
            public void KeyPress(int Key, int Shift)
            {
                _KEYPRESS(Key, Shift);
            }

            /// <summary>
            ///  This function will return an ‘index’ of a specific menu item. The MenuName parameter must  specify the menu path separated by a slash, for example ‘edit / selection / uppercase’. The  menu name is not case sensitive. If the function returns zero, the menu did not exist.  You  can use the return value with SelectMenu  Available in version 510  
            /// </summary>
            public int GetMenuItem(string MenuName)
            {
                return _GETMENUITEM(MenuName);
            }

            /// <summary>
            ///  You can execute a menu item with this function. The MenuItem parameter has to be determined  by the SelectMenu function. If this function returns false, the menu did not exist, or  it was disabled.  Available in version 510  
            /// </summary>
            public bool SelectMenu(int MenuItem)
            {
                return _SELECTMENU(MenuItem);
            }

            /// <summary>
            ///  Returns the currently used translation file. If the return value is empty, no translation is  used.  Available in version 510  
            /// </summary>
            public string TranslationFile()
            {
                return Marshal.PtrToStringAnsi(_TRANSLATIONFILE());
            }

            /// <summary>
            ///  Returns the language of the currently used translation file. If the return value is empty,  no translation is used.  Available in version 510  
            /// </summary>
            public string TranslationLanguage()
            {
                return Marshal.PtrToStringAnsi(_TRANSLATIONLANGUAGE());
            }

            /// <summary>
            ///  Returns a list of all standard PL/SQL Developer menu items like GetMenuLayout, but this  function will return the translated menus.  Available in version 510  
            /// </summary>
            public string GetTranslatedMenuLayout()
            {
                return Marshal.PtrToStringAnsi(_GETTRANSLATEDMENULAYOUT());
            }

            /// <summary>
            ///  PL/SQL Developer has a preference to save all opened files on a time interval, and/or when an  Execute is performed. In case of a crash (from the system, Oracle or PL/SQL Dev), the user  will be able to recover the edited files.  If the Plug-In can do things that have a possible  risk of causing a crash, you can call this function to protect the user’s work.   Available in version 510  
            /// </summary>
            public bool SaveRecoveryFiles()
            {
                return _SAVERECOVERYFILES();
            }

            /// <summary>
            ///  Returns the (1 based) character position of the cursor in the current editor.  Available in version 510  
            /// </summary>
            public int GetCursorX()
            {
                return _GETCURSORX();
            }

            /// <summary>
            ///  Returns the (1 based) line position of the cusror in the current editor.  Available in version 510  
            /// </summary>
            public int GetCursorY()
            {
                return _GETCURSORY();
            }

            /// <summary>
            ///  Set the cursor in the current editor. If the X or Y parameter is 0, the position will not change.  This function will also update the position display in the statusbar.  Available in version 510  
            /// </summary>
            public void SetCursor(int X, int Y)
            {
                _SETCURSOR(X, Y);
            }

            /// <summary>
            ///  Create a bookmark at position X (character), Y (line). Index is the bookmark (0..9) you want  to set. If you pass –1 as bookmark, the first free bookmark will be used. The returned  value is the used bookmark.   Normally, from within PL/SQL Developer. Bookmarks can only be used for windows with a gutter  (Test window and Program editor), but the Plug-In interface allows you to use bookmarks for  all windows.   Available in version 510  
            /// </summary>
            public int SetBookmark(int Index, int X, int Y)
            {
                return _SETBOOKMARK(Index, X, Y);
            }

            /// <summary>
            ///  Clears the specified bookmark  Available in version 510  
            /// </summary>
            public void ClearBookmark(int Index)
            {
                _CLEARBOOKMARK(Index);
            }

            /// <summary>
            ///  Jumps to a bookmark  Available in version 510  
            /// </summary>
            public void GotoBookmark(int Index)
            {
                _GOTOBOOKMARK(Index);
            }

            /// <summary>
            ///  Get the cursor position for a specific bookmark  Available in version 510  
            /// </summary>
            public bool GetBookmark(int Index, int X, int Y)
            {
                return _GETBOOKMARK(Index, X, Y);
            }

            /// <summary>
            ///  Returns the description tab page Index (zero based). The return value is empty if the tab  page does not exist. This function allows you to determine which tab pages (if any) are  available for the current window.   Available in version 511  
            /// </summary>
            public string TabInfo(int Index)
            {
                return Marshal.PtrToStringAnsi(_TABINFO(Index));
            }

            /// <summary>
            /// This function allows you to read or set the active tab page. To set a specific page, pass a zero based value to the Index parameter. The return value is the actual selected page. To determine the active page (without setting it) pass a value of –1 to the Index parameter
            /// </summary>
            /// <param name="Index"></param>
            /// <returns></returns>
            public int TabIndex(int Index)
            {
                return _TABINDEX(Index);
            }

            /// <summary>
            ///  This function allows you to add Toolbuttons to your Plug-In, similar to CreatePopupItem.  The ID is the Plug-In ID and the index is the menu index. When a button is selected,  OnMenuClick is called with the corresponding index.   The Name will appear as hint for the button, and as name in the preferences dialog.   The button can be enabled and disabled with MenuState.   The image for the button can be set by passing a filename to a bmp file in the BitmapFile  parameter, or as a handle to a bitmap in memory.  The bmp image can have any number of  colors, but should approximately be 20 x 20 pixels in size.   The button will only be visible if it is selected in the Toolbar preference.   Available in version 510  
            /// </summary>
            public void CreateToolButton(int ID, int Index, string Name, string BitmapFile, int BitmapHandle)
            {
                _CREATETOOLBUTTON(ID, Index, Name, BitmapFile, BitmapHandle);
            }

            /// <summary>
            /// Returns the PL/SQL Beautifier options.  The result is a value where the following values are  or-ed together:    1 AfterCreating enabled    2 AfterLoading enabled    4 BeforeCompiling enabled    8 BeforeSaving enabled  You can use this to determine if you need to call the beautifier.   Available in version 510  
            /// </summary>
            public int BeautifierOptions()
            {
                return _BEAUTIFIEROPTIONS();
            }

            /// <summary>
            ///  Calls the PL/SQL Beautifier for the current Window. The result indicates if the operations  succeeded.  Available in version 510  
            /// </summary>
            public bool BeautifyWindow()
            {
                return _BEAUTIFYWINDOW();
            }

            /// <summary>
            ///  Calls the PL/SQL Beautifier to beautify the text in the S parameter. The result is the  beautified text or it is empty if the function failed   Available in version 510  
            /// </summary>
            public string BeautifyText(string S)
            {
                return Marshal.PtrToStringAnsi(_BEAUTIFYTEXT(S));
            }

            /// <summary>
            ///  This function allows you to do a specific action for the object specified.  The following actions are available:  VIEW, VIEWSPECANDBODY, EDIT, EDITSPECANDBODY, EDITDATA,  QUERYDATA, TEST  Available in version 514  
            /// </summary>
            public bool ObjectAction(string Action, string ObjectType, string ObjectOwner, string ObjectName)
            {
                return _OBJECTACTION(Action, ObjectType, ObjectOwner, ObjectName);
            }

            /// <summary>
            ///  This allows you to start a specific PL/SQL Developer dialog. The  following are supported:  AUTHORIZATIONS  PROJECTITEMS  BREAKPOINTS  PREFERENCES  CONFIG PLUGINS  CONFIG TOOLS  CONFIG DOCUMENTS  CONFIG REPORTS  CONFIG MACROS  CONFIG AUTOREFRESH  The Param parameter is for future use.  Available in version 700  
            /// </summary>
            public bool ShowDialog(string Dialog, string Param)
            {
                return _SHOWDIALOG(Dialog, Param);
            }

            /// <summary>
            ///  When debuggin is on, this function allows you to add messages in the  debug.txt file generated.  Available in version 700  
            /// </summary>
            public void DebugLog(string Msg)
            {
                _DEBUGLOG(Msg);
            }

            /// <summary>
            ///  This function returns a command-line parameter, or a parameter  specified in the params.ini file.  Available in version 700  
            /// </summary>
            public string GetParamString(string Name)
            {
                return Marshal.PtrToStringAnsi(_GETPARAMSTRING(Name));
            }

            /// <summary>
            ///  This function returns a command-line parameter, or a parameter  specified in the params.ini file.  Available in version 700  
            /// </summary>
            public bool GetParambool(string Name)
            {
                return _GETPARAMBOOL(Name);
            }

            /// <summary>
            ///  This function allows you to return feedback to the command window. The  description S will be displayed in the window identified by the  FeedbackHandle. See the CommandLine Plug-In function for details.  Available in version 513  
            /// </summary>    
            public void CommandFeedback(int FeedbackHandle, string S)
            {
                _COMMANDFEEDBACK(FeedbackHandle, S);
            }

            /// <summary>
            ///  Returns the number of rows in the result grid of a SQL or Test Window.  Available in version 516  
            /// </summary>
            public int ResultGridRowCount()
            {
                return _RESULTGRIDROWCOUNT();
            }

            /// <summary>
            ///  Returns the number of cols in the result grid of a SQL or Test Window.  Available in version 516  
            /// </summary>
            public int ResultGridColCount()
            {
                return _RESULTGRIDCOLCOUNT();
            }

            /// <summary>
            ///  This function allows you to access the results of a query in a SQL or Test  Window. Use the above two functions to determine the number of rows  and cols.  Available in version 516  
            /// </summary>
            public string ResultGridCell(int Col, int Row)
            {
                return Marshal.PtrToStringAnsi(_RESULTGRIDCELL(Col, Row));
            }

            /// <summary>
            ///  In PL/SQL Developer 6 we introduced the concept of Authorization. You  should test if a specific feature is allowed for the current user with this  function. In the Category parameter you can specify one of the main  categories (objects, menus, system). The name parameter specifies the  item (session.kill or objects.drop). Some items have a subname, like  objects.drop with the different objects.  Available in version 600  
            /// </summary>
            public bool Authorized(string Category, string Name, string SubName)
            {
                return _AUTHORIZED(Category, Name, SubName);
            }

            /// <summary>
            ///  For a quick check if authorization allows the Plug-In to create a specific  function, you can use this function.  Available in version 600  
            /// </summary>
            public bool WindowAllowed(int WindowType, bool ShowErrorMessage)
            {
                return _WINDOWALLOWED(WindowType, ShowErrorMessage);
            }

            /// <summary>
            ///  Returns if authorization is enabled or not.  Available in version 600  
            /// </summary>
            public bool Authorization()
            {
                return _AUTHORIZATION();
            }

            /// <summary>
            ///  If you want a list off all available authorization items, you can call this  function. It will return a cr/lf separated list.  Available in version 600  
            /// </summary>
            public string AuthorizationItems(string Category)
            {
                return Marshal.PtrToStringAnsi(_AUTHORIZATIONITEMS(Category));
            }

            /// <summary>
            ///  If you want to add items to the authorization list to allow them to be  managed through the authorization option, you can use this function.  Pass the PlugInID to identify your Plug-In, and pass the Name  parameter with the item you want to add. The name should be unique,  so you should prefix it with the name the Plug-In, for example:  MyPlugIn.Create New Command  All items will be added in the PlugIns category, so if you want to  test if this feature is allowed you should call:  Authorized('PlugIns ', ' MyPlugIn.Create New Command')  Available in version 600  
            /// </summary>
            public void AddAuthorizationItem(int PlugInID, string Name)
            {
                _ADDAUTHORIZATIONITEM(PlugInID, Name);
            }

            /// <summary>
            ///  Returns a list of all personal preference sets.  If you to have the Plug-In to use different preferences depending on the  current connection, you can use this function to build a list of possible  preference sets.  Available in version 600  
            /// </summary>
            public string GetPersonalPrefSets()
            {
                return Marshal.PtrToStringAnsi(_GETPERSONALPREFSETS());
            }

            /// <summary>
            ///  Returns a list of all default preference sets.  Available in version 600  
            /// </summary>
            public string GetDefaultPrefSets()
            {
                return Marshal.PtrToStringAnsi(_GETDEFAULTPREFSETS());
            }

            /// <summary>
            ///  Read a Plug-In preference from the preferences. In PL/SQL Developer 6, personal preferences  are stored in files, not in the registry. You can still use the registry, but if you want to  store your preferences in a shared location, you can use this function.  Pass the PlugInID  you received with the IdentifyPlugIn call. The PrefSet parameter can be empty to retrieve  default preferences, or you can specify one of the existing preference sets.  Available in version 600  
            /// </summary>
            public bool GetPrefAsString(int PlugInID, string PrefSet, string Name, string Default)
            {
                return _GETPREFASSTRING(PlugInID, PrefSet, Name, Default);
            }

            /// <summary>
            ///  As GetPrefAsString, but for integers.  Available in version 600  
            /// </summary>
            public int GetPrefAsInteger(int PlugInID, string PrefSet, string Name, bool Default)
            {
                return _GETPREFASINTEGER(PlugInID, PrefSet, Name, Default);
            }

            /// <summary>
            ///  As GetPrefAsString, but for booleans.  Available in version 600  
            /// </summary>
            public bool GetPrefAsbool(int PlugInID, string PrefSet, string Name, bool Default)
            {
                return _GETPREFASBOOL(PlugInID, PrefSet, Name, Default);
            }

            /// <summary>
            ///  Set a Plug-In preference. Pass the PlugInID you received with the  IdentifyPlugIn call. The PrefSet parameter can be empty to set default  preferences, or you can specify one of the existing preference sets. The  return value indicates if the function succeeded.  Available in version 600  
            /// </summary>
            public bool SetPrefAsString(int PlugInID, string PrefSet, string Name, string Value)
            {
                return _SETPREFASSTRING(PlugInID, PrefSet, Name, Value);
            }

            /// <summary>
            ///  As SetPrefAsString, but for integers.  Available in version 600  
            /// </summary>
            public bool SetPrefAsInteger(int PlugInID, string PrefSet, string Name, int Value)
            {
                return _SETPREFASINTEGER(PlugInID, PrefSet, Name, Value);
            }

            /// <summary>
            ///  As SetPrefAsString, but for booleans.  Available in version 600  
            /// </summary>
            /// 
            public bool SetPrefAsbool(int PlugInID, string PrefSet, string Name, bool Value)
            {
                return _SETPREFASBOOL(PlugInID, PrefSet, Name, Value);
            }

            /// <summary>
            ///  Returns the value of a preference. The names can be found in the  preference ini file under the [Preferences] section.  Available in version 700  
            /// </summary>
            public string GetGeneralPref(string Name)
            {
                return Marshal.PtrToStringAnsi(_GETGENERALPREF(Name));
            }

            /// <summary>
            ///  This will overrule default app behavior.  Currently only the setting "NOFILEDATECHECK" is  supported where you can pass "TRUE" or "FALSE"  
            /// </summary>

            public bool PlugInSetting(int PlugInID, string Setting, string Value)
            {
                return _PLUGINSETTING(PlugInID, Setting, Value);
            }
            /// <summary>
            ///  Returns the number of overloads for a specific procedure.  Result < 0 = Procedure doesn`t exist  Result > 0 = overload count  Available in version 700  
            /// </summary>

            public int GetProcOverloadCount(string Owner, string PackageName, string ProcedureName)
            {
                return _GETPROCOVERLOADCOUNT(Owner, PackageName, ProcedureName);
            }
            /// <summary>
            ///  Shows a dialog to allow the user to select an overloaded procedure.  Result < 0 = Cancel  Result 0 = No overloadings  Result > 0 = Overload index  Available in version 700  
            /// </summary>

            public int SelectProcOverloading(string Owner, string PackageName, string ProcedureName)
            {
                return _SELECTPROCOVERLOADING(Owner, PackageName, ProcedureName);
            }

            /// <summary>
            ///  This function will return one of the Session parameters as you see in the  grid of the session tool. You will only get a result if the Session Window  is active, so this will only work from a Popup menu created for the  SESSIONWINDOW object.  Available in version 700  
            /// </summary>
            public string GetSessionValue(string Name)
            {
                return Marshal.PtrToStringAnsi(_GETSESSIONVALUE(Name));
            }
            /// <summary>
            ///  This functions allows you to add a connection. If it already exists it won’t   be added twice. The result will be the new or existing index.   See also AddConnectionEx10().  
            /// </summary>

            public int AddConnection(string Username, string Password, string Database, string ConnectAs)
            {
                return _ADDCONNECTION(Username, Password, Database, ConnectAs);
            }

            /// <summary>
            ///  This function can be used to get or set the status of a the current  windows connection pinning.    As Pin parameter you can use 0 to set pinning off, or 1 to pin the connection. Value 2 will not change the pinned status.           
            /// </summary>
            public int WindowPin(int Pin)
            {
                return _WINDOWPIN(Pin);
            }

            /// <summary>
            /// Returns the first selected item in the file browser. Use NextSelectedFile for multiple selected items. The Files and Directories parameters allow you to specify if you do or don’t want selected files and/or directories. 
            /// </summary>
            /// <param name="Files"></param>
            /// <param name="Directories"></param>    
            public string FirstSelectedFile(bool Files, bool Directories)
            {
                return Marshal.PtrToStringAnsi(_FIRSTSELECTEDFILE(Files, Directories));
            }

            /// <summary>
            ///  Returns the next selected item. See the previous function. Returns empty value when no more items.   
            /// </summary>
            public string NextSelectedFile()
            {
                return Marshal.PtrToStringAnsi(_NEXTSELECTEDFILE());
            }

            /// <summary>
            ///  Forces the file browser to refresh the contents. Normally the browser will autodetect changes.   
            /// </summary>
            public void RefreshFileBrowser()
            {
                _REFRESHFILEBROWSER();
            }

            /// <summary>
            ///  Forces the file browser to refresh the contents. Normally the browser will autodetect changes.   
            /// </summary>
            public string MainFont()
            {
                return Marshal.PtrToStringAnsi(_MAINFONT());
            }

            /// <summary>
            ///  Return the PL/SQL Developer main font in the format: “Name”, size, color, charset, “style”  
            /// </summary>
            public string TranslateItems(string Group)
            {
                return Marshal.PtrToStringAnsi(_TRANSLATEITEMS(Group));
            }

            /// <summary>
            ///    Function for translating items.
            /// </summary>
            public string TranslateString(string ID, string Default, string Param1, string Param2)
            {
                return Marshal.PtrToStringAnsi(_TRANSLATESTRING(ID, Default, Param1, Param2));
            }

            /// <summary>
            /// Returns true if the current  Window has an Editor. If the CodeEditor parameter is true, it returns false for editors like the output editor. 
            /// </summary>
            public bool WindowHasEditor(bool CodeEditor)
            {
                return _WINDOWHASEDITOR(CodeEditor);
            }

            /// <summary>
            /// This function returns the defined browser filters. You can use this if the Plug-in has a similar requirement. Index = 0 and higher, and the returned values are empty if the filter does not exist.
            /// </summary>
            public void GetBrowserFilter(int Index, out string Name, out string WhereClause, out string OrderByClause, out string User, [MarshalAs(UnmanagedType.Bool)] bool Active)
            {
                IntPtr pName, pWhereClause, pOrderByClause, pUser;
                _GETBROWSERFILTER(Index, out pName, out pWhereClause, out pOrderByClause, out pUser, Active);

                Name = Marshal.PtrToStringAnsi(pName);
                WhereClause = Marshal.PtrToStringAnsi(pWhereClause);
                OrderByClause = Marshal.PtrToStringAnsi(pOrderByClause);
                User = Marshal.PtrToStringAnsi(pUser);
            }

            /// <summary>
            ///  You can use this function to check if the database is equal or higher then the specified version. The parameter should be in the format aa.bb, like 09.02 or 10.00.  
            /// </summary>
            public bool CheckDBVersion(string Version)
            {
                return _CHECKDBVERSION(Version);
            }

            /// <summary>
            ///    In version 9.0, multiple connections are introduced. This function will iterate through all available (recent) connections. You can start with ix = 0 and continue until you receive a false as result. The other parameters return the details of each connection.
            /// </summary>  
            public bool GetConnectionInfoEx(int ix, out string Username, out string Password, out string Database, out string ConnectAs)
            {
                IntPtr pUsername, pPassword, pConnectAs,  pDatabase;
                bool result = _GETCONNECTIONINFOEX(ix, out pUsername, out pPassword, out pDatabase, out pConnectAs);

                Username = Marshal.PtrToStringAnsi(pUsername);
                Password = Marshal.PtrToStringAnsi(pPassword);
                Database = Marshal.PtrToStringAnsi(pDatabase);
                ConnectAs = Marshal.PtrToStringAnsi(pConnectAs);

                return result;
            }

            /// <summary>
            /// Search in the available connections for a specific connection.
            /// </summary>
            /// <param name="Username"></param>
            /// <param name="Database"></param>
            /// <returns> Result will return -1 if not found, otherwise an index in the array as retrieved by GetConnectionInfoEx(). </returns>
            public int FindConnection(string Username, string Database)
            {
                return _FINDCONNECTION(Username, Database);
                /// <summary>
                ///    
                /// </summary>
            }

            /// <summary>
            ///  This will connect the specified connection to the database. A logon dialog will appear if necessary for a password.  
            /// </summary>
            public bool ConnectConnection(int ix)
            {
                return _CONNECTCONNECTION(ix);
            }

            /// <summary>
            /// Makes the specified connection the main connection. The main connection is used by PL/SQL Developer for the object browser and as default connection for new windows.
            /// </summary>
            /// <param name="ix"></param>    
            public bool SetMainConnection(int ix)
            {
                return _SETMAINCONNECTION(ix);
            }

            /// <summary>
            ///  Retrieves the connection used by the current window. Use GetConnectionInfoEx() to get details.  
            /// </summary>    
            public int GetWindowConnection()
            {
                return _GETWINDOWCONNECTION();
            }

            /// <summary>
            ///  Sets the connection for the current window.
            /// </summary>
            public bool SetWindowConnection()
            {
                return _SETWINDOWCONNECTION();
            }

            /// <summary>
            ///    Returns available connections. Use the ix parameter from 0 onwards to determine the connection until the function returns false. 
            ///    The ID and ParentID determine the parent child relation.
            /// </summary>
            public bool GetConnectionTree(int ix, out string Description, out string Username, out string Password, out string Database, out string ConnectAs, int ID, int ParentID)
            {
                IntPtr pDescription, pUsername, pPassword, pConnectAs, pDatabase;
                bool result = _GETCONNECTIONTREE(ix, out pDescription, out pUsername, out pPassword, out pDatabase, out pConnectAs, ID, ParentID);

                Description = Marshal.PtrToStringAnsi(pDescription);
                Username = Marshal.PtrToStringAnsi(pUsername);
                Password = Marshal.PtrToStringAnsi(pPassword);
                Database = Marshal.PtrToStringAnsi(pDatabase);
                ConnectAs = Marshal.PtrToStringAnsi(pConnectAs);

                return result;
            }

            /// <summary>
            ///  Extents function GetConnectionInfoEx for use with Edition and Workspace introduced in Version 10.  
            /// </summary>
            public bool GetConnectionInfoEx10(int ix, out string Username, out string Password, out string Database, out string ConnectAs, out string Edition, out string Workspace)
            {
                IntPtr pUsername, pPassword, pConnectAs, pEdition, pDatabase, pWorkspace;
                bool result = _GETCONNECTIONINFOEX10(ix, out pUsername, out pPassword, out pDatabase, out pConnectAs, out pEdition, out pWorkspace);

                Edition = Marshal.PtrToStringAnsi(pEdition);
                Username = Marshal.PtrToStringAnsi(pUsername);
                Password = Marshal.PtrToStringAnsi(pPassword);
                Database = Marshal.PtrToStringAnsi(pDatabase);
                ConnectAs = Marshal.PtrToStringAnsi(pConnectAs);
                Workspace = Marshal.PtrToStringAnsi(pWorkspace);

                return result;
            }

            /// <summary>
            /// Extents function FindConnection for use with Edition and Workspace introduced in Version 10.
            /// </summary>
            /// <param name="Username"></param>
            /// <param name="Database"></param>
            /// <param name="Edition"></param>
            /// <param name="Workspace"></param>
            /// <returns></returns>
            public int FindConnectionEx10(string Username, string Database, string Edition, string Workspace)
            {
                return _FINDCONNECTIONEX10(Username, Database, Edition, Workspace);
            }

            /// <summary>
            ///  Extents function AddConnection for use with Edition and Workspace introduced in Version 10.  
            /// </summary>
            public int AddConnectionEx10(string Username, string Password, string Database, string ConnectAs, string Edition, string Workspace)
            {
                return _ADDCONNECTIONEX10(Username, Password, Database, ConnectAs, Edition, Workspace);
            }

            /// <summary>
            /// Extents function GetConnectionTree for use with Edition and Workspace introduced in Version 10.
            /// </summary>
            /// <param name="ix"></param>
            /// <param name="Description"></param>
            /// <param name="Username"></param>
            /// <param name="Password"></param>
            /// <param name="Database"></param>
            /// <param name="ConnectAs"></param>
            /// <param name="Edition"></param>
            /// <param name="Workspace"></param>
            /// <param name="ID"></param>
            /// <param name="ParentID"></param>
            /// <returns></returns>        
            public bool GetConnectionTreeEx10(int ix, out string Description, out string Username, out string Password, out string Database, out string ConnectAs, out string Edition, out string Workspace, int ID, int ParentID)
            {
                IntPtr pDescription, pUsername, pPassword, pConnectAs, pEdition, pDatabase, pWorkspace;

                bool result = _GETCONNECTIONTREEEX10(ix, out pDescription, out pUsername, out pPassword, out pDatabase, out pConnectAs, out pEdition, out pWorkspace, ID, ParentID);

                Edition = Marshal.PtrToStringAnsi(pEdition);
                Username = Marshal.PtrToStringAnsi(pUsername);
                Password = Marshal.PtrToStringAnsi(pPassword);
                Database = Marshal.PtrToStringAnsi(pDatabase);
                ConnectAs = Marshal.PtrToStringAnsi(pConnectAs);
                Workspace = Marshal.PtrToStringAnsi(pWorkspace);
                Description = Marshal.PtrToStringAnsi(pDescription);

                return result;
            }
            #endregion

            /// <summary>
            ///  Call Back Funktionen für IDE  
            /// </summary>  <param name="index"></param>  <param name="function"></param>
            public static void RegisterCallback(IdeType index, IntPtr function)
            {
                switch (index)
                {
                    case IdeType.MENUSTATE:
                        _MENUSTATE = (MENUSTATE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(MENUSTATE_DELEGATE));
                        break;
                    case IdeType.CONNECTED:
                        _CONNECTED = (CONNECTED_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CONNECTED_DELEGATE));
                        break;
                    case IdeType.GETCONNECTIONINFO:
                        _GETCONNECTIONINFO = (GETCONNECTIONINFO_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCONNECTIONINFO_DELEGATE));
                        break;
                    case IdeType.GETBROWSERINFO:
                        _GETBROWSERINFO = (GETBROWSERINFO_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETBROWSERINFO_DELEGATE));
                        break;
                    case IdeType.GETWINDOWTYPE:
                        _GETWINDOWTYPE = (GETWINDOWTYPE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETWINDOWTYPE_DELEGATE));
                        break;
                    case IdeType.GETAPPHANDLE:
                        _GETAPPHANDLE = (GETAPPHANDLE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETAPPHANDLE_DELEGATE));
                        break;
                    case IdeType.GETWINDOWHANDLE:
                        _GETWINDOWHANDLE = (GETWINDOWHANDLE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETWINDOWHANDLE_DELEGATE));
                        break;
                    case IdeType.GETCLIENTHANDLE:
                        _GETCLIENTHANDLE = (GETCLIENTHANDLE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCLIENTHANDLE_DELEGATE));
                        break;
                    case IdeType.GETCHILDHANDLE:
                        _GETCHILDHANDLE = (GETCHILDHANDLE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCHILDHANDLE_DELEGATE));
                        break;
                    case IdeType.REFRESH:
                        _REFRESH = (REFRESH_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(REFRESH_DELEGATE));
                        break;
                    case IdeType.CREATEWINDOW:
                        _CREATEWINDOW = (CREATEWINDOW_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CREATEWINDOW_DELEGATE));
                        break;
                    case IdeType.OPENFILE:
                        _OPENFILE = (OPENFILE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(OPENFILE_DELEGATE));
                        break;
                    case IdeType.SAVEFILE:
                        _SAVEFILE = (SAVEFILE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SAVEFILE_DELEGATE));
                        break;
                    case IdeType.FILENAME:
                        _FILENAME = (FILENAME_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FILENAME_DELEGATE));
                        break;
                    case IdeType.CLOSEFILE:
                        _CLOSEFILE = (CLOSEFILE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CLOSEFILE_DELEGATE));
                        break;
                    case IdeType.SETREADONLY:
                        _SETREADONLY = (SETREADONLY_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETREADONLY_DELEGATE));
                        break;
                    case IdeType.GETREADONLY:
                        _GETREADONLY = (GETREADONLY_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETREADONLY_DELEGATE));
                        break;
                    case IdeType.EXECUTESQLREPORT:
                        _EXECUTESQLREPORT = (EXECUTESQLREPORT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(EXECUTESQLREPORT_DELEGATE));
                        break;
                    case IdeType.RELOADFILE:
                        _RELOADFILE = (RELOADFILE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(RELOADFILE_DELEGATE));
                        break;
                    case IdeType.SETFILENAME:
                        _SETFILENAME = (SETFILENAME_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETFILENAME_DELEGATE));
                        break;
                    case IdeType.GETTEXT:
                        _GETTEXT = (GETTEXT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETTEXT_DELEGATE));
                        break;
                    case IdeType.GETSELECTEDTEXT:
                        _GETSELECTEDTEXT = (GETSELECTEDTEXT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETSELECTEDTEXT_DELEGATE));
                        break;
                    case IdeType.GETCURSORWORD:
                        _GETCURSORWORD = (GETCURSORWORD_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCURSORWORD_DELEGATE));
                        break;
                    case IdeType.GETEDITORHANDLE:
                        _GETEDITORHANDLE = (GETEDITORHANDLE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETEDITORHANDLE_DELEGATE));
                        break;
                    case IdeType.SETTEXT:
                        _SETTEXT = (SETTEXT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETTEXT_DELEGATE));
                        break;
                    case IdeType.SETSTATUSMESSAGE:
                        _SETSTATUSMESSAGE = (SETSTATUSMESSAGE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETSTATUSMESSAGE_DELEGATE));
                        break;
                    case IdeType.SETERRORPOSITION:
                        _SETERRORPOSITION = (SETERRORPOSITION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETERRORPOSITION_DELEGATE));
                        break;
                    case IdeType.CLEARERRORPOSITIONS:
                        _CLEARERRORPOSITIONS = (CLEARERRORPOSITIONS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CLEARERRORPOSITIONS_DELEGATE));
                        break;
                    case IdeType.GETCURSORWORDPOSITION:
                        _GETCURSORWORDPOSITION = (GETCURSORWORDPOSITION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCURSORWORDPOSITION_DELEGATE));
                        break;
                    case IdeType.PERFORM:
                        _PERFORM = (PERFORM_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(PERFORM_DELEGATE));
                        break;
                    case IdeType.GETCUSTOMKEYWORDS:
                        _GETCUSTOMKEYWORDS = (GETCUSTOMKEYWORDS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCUSTOMKEYWORDS_DELEGATE));
                        break;
                    case IdeType.SETCUSTOMKEYWORDS:
                        _SETCUSTOMKEYWORDS = (SETCUSTOMKEYWORDS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETCUSTOMKEYWORDS_DELEGATE));
                        break;
                    case IdeType.SETKEYWORDS:
                        _SETKEYWORDS = (SETKEYWORDS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETKEYWORDS_DELEGATE));
                        break;
                    case IdeType.ACTIVATEKEYWORDS:
                        _ACTIVATEKEYWORDS = (ACTIVATEKEYWORDS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(ACTIVATEKEYWORDS_DELEGATE));
                        break;
                    case IdeType.REFRESHMENUS:
                        _REFRESHMENUS = (REFRESHMENUS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(REFRESHMENUS_DELEGATE));
                        break;
                    case IdeType.SETMENUNAME:
                        _SETMENUNAME = (SETMENUNAME_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETMENUNAME_DELEGATE));
                        break;
                    case IdeType.SETMENUCHECK:
                        _SETMENUCHECK = (SETMENUCHECK_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETMENUCHECK_DELEGATE));
                        break;
                    case IdeType.SETMENUVISIBLE:
                        _SETMENUVISIBLE = (SETMENUVISIBLE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETMENUVISIBLE_DELEGATE));
                        break;
                    case IdeType.GETMENULAYOUT:
                        _GETMENULAYOUT = (GETMENULAYOUT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETMENULAYOUT_DELEGATE));
                        break;
                    case IdeType.CREATEPOPUPITEM:
                        _CREATEPOPUPITEM = (CREATEPOPUPITEM_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CREATEPOPUPITEM_DELEGATE));
                        break;
                    case IdeType.SETCONNECTION:
                        _SETCONNECTION = (SETCONNECTION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETCONNECTION_DELEGATE));
                        break;
                    case IdeType.GETOBJECTINFO:
                        _GETOBJECTINFO = (GETOBJECTINFO_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETOBJECTINFO_DELEGATE));
                        break;
                    case IdeType.GETBROWSERITEMS:
                        _GETBROWSERITEMS = (GETBROWSERITEMS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETBROWSERITEMS_DELEGATE));
                        break;
                    case IdeType.REFRESHBROWSER:
                        _REFRESHBROWSER = (REFRESHBROWSER_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(REFRESHBROWSER_DELEGATE));
                        break;
                    case IdeType.GETPOPUPOBJECT:
                        _GETPOPUPOBJECT = (GETPOPUPOBJECT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPOPUPOBJECT_DELEGATE));
                        break;
                    case IdeType.GETPOPUPBROWSERROOT:
                        _GETPOPUPBROWSERROOT = (GETPOPUPBROWSERROOT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPOPUPBROWSERROOT_DELEGATE));
                        break;
                    case IdeType.REFRESHOBJECT:
                        _REFRESHOBJECT = (REFRESHOBJECT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(REFRESHOBJECT_DELEGATE));
                        break;
                    case IdeType.FIRSTSELECTEDOBJECT:
                        _FIRSTSELECTEDOBJECT = (FIRSTSELECTEDOBJECT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FIRSTSELECTEDOBJECT_DELEGATE));
                        break;
                    case IdeType.NEXTSELECTEDOBJECT:
                        _NEXTSELECTEDOBJECT = (NEXTSELECTEDOBJECT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(NEXTSELECTEDOBJECT_DELEGATE));
                        break;
                    case IdeType.GETOBJECTSOURCE:
                        _GETOBJECTSOURCE = (GETOBJECTSOURCE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETOBJECTSOURCE_DELEGATE));
                        break;
                    case IdeType.GETWINDOWCOUNT:
                        _GETWINDOWCOUNT = (GETWINDOWCOUNT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETWINDOWCOUNT_DELEGATE));
                        break;
                    case IdeType.SELECTWINDOW:
                        _SELECTWINDOW = (SELECTWINDOW_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SELECTWINDOW_DELEGATE));
                        break;
                    case IdeType.ACTIVATEWINDOW:
                        _ACTIVATEWINDOW = (ACTIVATEWINDOW_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(ACTIVATEWINDOW_DELEGATE));
                        break;
                    case IdeType.WINDOWISMODIFIED:
                        _WINDOWISMODIFIED = (WINDOWISMODIFIED_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(WINDOWISMODIFIED_DELEGATE));
                        break;
                    case IdeType.WINDOWISRUNNING:
                        _WINDOWISRUNNING = (WINDOWISRUNNING_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(WINDOWISRUNNING_DELEGATE));
                        break;
                    case IdeType.WINDOWPIN:
                        _WINDOWPIN = (WINDOWPIN_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(WINDOWPIN_DELEGATE));
                        break;
                    case IdeType.SPLASHCREATE:
                        _SPLASHCREATE = (SPLASHCREATE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SPLASHCREATE_DELEGATE));
                        break;
                    case IdeType.SPLASHHIDE:
                        _SPLASHHIDE = (SPLASHHDELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SPLASHHDELEGATE));
                        break;
                    case IdeType.SPLASHWRITE:
                        _SPLASHWRITE = (SPLASHWRITE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SPLASHWRITE_DELEGATE));
                        break;
                    case IdeType.SPLASHWRITELN:
                        _SPLASHWRITELN = (SPLASHWRITELN_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SPLASHWRITELN_DELEGATE));
                        break;
                    case IdeType.SPLASHPROGRESS:
                        _SPLASHPROGRESS = (SPLASHPROGRESS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SPLASHPROGRESS_DELEGATE));
                        break;
                    case IdeType.TEMPLATEPATH:
                        _TEMPLATEPATH = (TEMPLATEPATH_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(TEMPLATEPATH_DELEGATE));
                        break;
                    case IdeType.EXECUTETEMPLATE:
                        _EXECUTETEMPLATE = (EXECUTETEMPLATE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(EXECUTETEMPLATE_DELEGATE));
                        break;
                    case IdeType.GETCONNECTAS:
                        _GETCONNECTAS = (GETCONNECTAS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCONNECTAS_DELEGATE));
                        break;
                    case IdeType.SETCONNECTIONAS:
                        _SETCONNECTIONAS = (SETCONNECTIONAS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETCONNECTIONAS_DELEGATE));
                        break;
                    case IdeType.GETFILEOPENMENU:
                        _GETFILEOPENMENU = (GETFILEOPENMENU_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETFILEOPENMENU_DELEGATE));
                        break;
                    case IdeType.CANSAVEWINDOW:
                        _CANSAVEWINDOW = (CANSAVEWINDOW_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CANSAVEWINDOW_DELEGATE));
                        break;
                    case IdeType.OPENFILEEXTERNAL:
                        _OPENFILEEXTERNAL = (OPENFILEEXTERNAL_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(OPENFILEEXTERNAL_DELEGATE));
                        break;
                    case IdeType.GETFILETYPES:
                        _GETFILETYPES = (GETFILETYPES_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETFILETYPES_DELEGATE));
                        break;
                    case IdeType.GETDEFAULTEXTENSION:
                        _GETDEFAULTEXTENSION = (GETDEFAULTEXTENSION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETDEFAULTEXTENSION_DELEGATE));
                        break;
                    case IdeType.GETFILEDATA:
                        _GETFILEDATA = (GETFILEDATA_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETFILEDATA_DELEGATE));
                        break;
                    case IdeType.FILESAVED:
                        _FILESAVED = (FILESAVED_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FILESAVED_DELEGATE));
                        break;
                    case IdeType.SHOWHTML:
                        _SHOWHTML = (SHOWHTML_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SHOWHTML_DELEGATE));
                        break;
                    case IdeType.REFRESHHTML:
                        _REFRESHHTML = (REFRESHHTML_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(REFRESHHTML_DELEGATE));
                        break;
                    case IdeType.GETPROCEDITEXTENSION:
                        _GETPROCEDITEXTENSION = (GETPROCEDITEXTENSION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPROCEDITEXTENSION_DELEGATE));
                        break;
                    case IdeType.GETWINDOWOBJECT:
                        _GETWINDOWOBJECT = (GETWINDOWOBJECT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETWINDOWOBJECT_DELEGATE));
                        break;
                    case IdeType.FIRSTSELECTEDFILE:
                        _FIRSTSELECTEDFILE = (FIRSTSELECTEDFILE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FIRSTSELECTEDFILE_DELEGATE));
                        break;
                    case IdeType.NEXTSELECTEDFILE:
                        _NEXTSELECTEDFILE = (NEXTSELECTEDFILE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(NEXTSELECTEDFILE_DELEGATE));
                        break;
                    case IdeType.REFRESHFILEBROWSER:
                        _REFRESHFILEBROWSER = (REFRESHFILEBROWSER_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(REFRESHFILEBROWSER_DELEGATE));
                        break;
                    case IdeType.KEYPRESS:
                        _KEYPRESS = (KEYPRESS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(KEYPRESS_DELEGATE));
                        break;
                    case IdeType.GETMENUITEM:
                        _GETMENUITEM = (GETMENUITEM_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETMENUITEM_DELEGATE));
                        break;
                    case IdeType.SELECTMENU:
                        _SELECTMENU = (SELECTMENU_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SELECTMENU_DELEGATE));
                        break;
                    case IdeType.TRANSLATIONFILE:
                        _TRANSLATIONFILE = (TRANSLATIONFILE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(TRANSLATIONFILE_DELEGATE));
                        break;
                    case IdeType.TRANSLATIONLANGUAGE:
                        _TRANSLATIONLANGUAGE = (TRANSLATIONLANGUAGE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(TRANSLATIONLANGUAGE_DELEGATE));
                        break;
                    case IdeType.GETTRANSLATEDMENULAYOUT:
                        _GETTRANSLATEDMENULAYOUT = (GETTRANSLATEDMENULAYOUT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETTRANSLATEDMENULAYOUT_DELEGATE));
                        break;
                    case IdeType.MAINFONT:
                        _MAINFONT = (MAINFONT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(MAINFONT_DELEGATE));
                        break;
                    case IdeType.TRANSLATEITEMS:
                        _TRANSLATEITEMS = (TRANSLATEITEMS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(TRANSLATEITEMS_DELEGATE));
                        break;
                    case IdeType.TRANSLATESTRING:
                        _TRANSLATESTRING = (TRANSLATESTRING_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(TRANSLATESTRING_DELEGATE));
                        break;
                    case IdeType.SAVERECOVERYFILES:
                        _SAVERECOVERYFILES = (SAVERECOVERYFILES_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SAVERECOVERYFILES_DELEGATE));
                        break;
                    case IdeType.GETCURSORX:
                        _GETCURSORX = (GETCURSORX_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCURSORX_DELEGATE));
                        break;
                    case IdeType.GETCURSORY:
                        _GETCURSORY = (GETCURSORY_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCURSORY_DELEGATE));
                        break;
                    case IdeType.SETCURSOR:
                        _SETCURSOR = (SETCURSOR_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETCURSOR_DELEGATE));
                        break;
                    case IdeType.SETBOOKMARK:
                        _SETBOOKMARK = (SETBOOKMARK_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETBOOKMARK_DELEGATE));
                        break;
                    case IdeType.CLEARBOOKMARK:
                        _CLEARBOOKMARK = (CLEARBOOKMARK_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CLEARBOOKMARK_DELEGATE));
                        break;
                    case IdeType.GOTOBOOKMARK:
                        _GOTOBOOKMARK = (GOTOBOOKMARK_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GOTOBOOKMARK_DELEGATE));
                        break;
                    case IdeType.GETBOOKMARK:
                        _GETBOOKMARK = (GETBOOKMARK_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETBOOKMARK_DELEGATE));
                        break;
                    case IdeType.TABINFO:
                        _TABINFO = (TABINFO_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(TABINFO_DELEGATE));
                        break;
                    case IdeType.TABINDEX:
                        _TABINDEX = (TABINDEX_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(TABINDEX_DELEGATE));
                        break;
                    case IdeType.CREATETOOLBUTTON:
                        _CREATETOOLBUTTON = (CREATETOOLBUTTON_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CREATETOOLBUTTON_DELEGATE));
                        break;
                    case IdeType.WINDOWHASEDITOR:
                        _WINDOWHASEDITOR = (WINDOWHASEDITOR_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(WINDOWHASEDITOR_DELEGATE));
                        break;
                    case IdeType.BEAUTIFIEROPTIONS:
                        _BEAUTIFIEROPTIONS = (BEAUTIFIEROPTIONS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(BEAUTIFIEROPTIONS_DELEGATE));
                        break;
                    case IdeType.BEAUTIFYWINDOW:
                        _BEAUTIFYWINDOW = (BEAUTIFYWINDOW_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(BEAUTIFYWINDOW_DELEGATE));
                        break;
                    case IdeType.BEAUTIFYTEXT:
                        _BEAUTIFYTEXT = (BEAUTIFYTEXT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(BEAUTIFYTEXT_DELEGATE));
                        break;
                    case IdeType.OBJECTACTION:
                        _OBJECTACTION = (OBJECTACTION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(OBJECTACTION_DELEGATE));
                        break;
                    case IdeType.SHOWDIALOG:
                        _SHOWDIALOG = (SHOWDIALOG_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SHOWDIALOG_DELEGATE));
                        break;
                    case IdeType.DEBUGLOG:
                        _DEBUGLOG = (DEBUGLOG_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(DEBUGLOG_DELEGATE));
                        break;
                    case IdeType.GETPARAMSTRING:
                        _GETPARAMSTRING = (GETPARAMSTRING_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPARAMSTRING_DELEGATE));
                        break;
                    case IdeType.GETPARAMBOOL:
                        _GETPARAMBOOL = (GETPARAMBOOL_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPARAMBOOL_DELEGATE));
                        break;
                    case IdeType.GETBROWSERFILTER:
                        _GETBROWSERFILTER = (GETBROWSERFILTER_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETBROWSERFILTER_DELEGATE));
                        break;
                    case IdeType.COMMANDFEEDBACK:
                        _COMMANDFEEDBACK = (COMMANDFEEDBACK_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(COMMANDFEEDBACK_DELEGATE));
                        break;
                    case IdeType.RESULTGRIDROWCOUNT:
                        _RESULTGRIDROWCOUNT = (RESULTGRIDROWCOUNT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(RESULTGRIDROWCOUNT_DELEGATE));
                        break;
                    case IdeType.RESULTGRIDCOLCOUNT:
                        _RESULTGRIDCOLCOUNT = (RESULTGRIDCOLCOUNT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(RESULTGRIDCOLCOUNT_DELEGATE));
                        break;
                    case IdeType.RESULTGRIDCELL:
                        _RESULTGRIDCELL = (RESULTGRIDCELL_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(RESULTGRIDCELL_DELEGATE));
                        break;
                    case IdeType.AUTHORIZED:
                        _AUTHORIZED = (AUTHORIZED_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(AUTHORIZED_DELEGATE));
                        break;
                    case IdeType.WINDOWALLOWED:
                        _WINDOWALLOWED = (WINDOWALLOWED_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(WINDOWALLOWED_DELEGATE));
                        break;
                    case IdeType.AUTHORIZATION:
                        _AUTHORIZATION = (AUTHORIZATION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(AUTHORIZATION_DELEGATE));
                        break;
                    case IdeType.AUTHORIZATIONITEMS:
                        _AUTHORIZATIONITEMS = (AUTHORIZATIONITEMS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(AUTHORIZATIONITEMS_DELEGATE));
                        break;
                    case IdeType.ADDAUTHORIZATIONITEM:
                        _ADDAUTHORIZATIONITEM = (ADDAUTHORIZATIONITEM_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(ADDAUTHORIZATIONITEM_DELEGATE));
                        break;
                    case IdeType.GETPERSONALPREFSETS:
                        _GETPERSONALPREFSETS = (GETPERSONALPREFSETS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPERSONALPREFSETS_DELEGATE));
                        break;
                    case IdeType.GETDEFAULTPREFSETS:
                        _GETDEFAULTPREFSETS = (GETDEFAULTPREFSETS_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETDEFAULTPREFSETS_DELEGATE));
                        break;
                    case IdeType.GETPREFASSTRING:
                        _GETPREFASSTRING = (GETPREFASSTRING_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPREFASSTRING_DELEGATE));
                        break;
                    case IdeType.GETPREFASINTEGER:
                        _GETPREFASINTEGER = (GETPREFASINTEGER_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPREFASINTEGER_DELEGATE));
                        break;
                    case IdeType.GETPREFASBOOL:
                        _GETPREFASBOOL = (GETPREFASBOOL_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPREFASBOOL_DELEGATE));
                        break;
                    case IdeType.SETPREFASSTRING:
                        _SETPREFASSTRING = (SETPREFASSTRING_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETPREFASSTRING_DELEGATE));
                        break;
                    case IdeType.SETPREFASINTEGER:
                        _SETPREFASINTEGER = (SETPREFASINTEGER_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETPREFASINTEGER_DELEGATE));
                        break;
                    case IdeType.SETPREFASBOOL:
                        _SETPREFASBOOL = (SETPREFASBOOL_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETPREFASBOOL_DELEGATE));
                        break;
                    case IdeType.GETGENERALPREF:
                        _GETGENERALPREF = (GETGENERALPREF_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETGENERALPREF_DELEGATE));
                        break;
                    case IdeType.PLUGINSETTING:
                        _PLUGINSETTING = (PLUGINSETTING_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(PLUGINSETTING_DELEGATE));
                        break;
                    case IdeType.GETPROCOVERLOADCOUNT:
                        _GETPROCOVERLOADCOUNT = (GETPROCOVERLOADCOUNT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETPROCOVERLOADCOUNT_DELEGATE));
                        break;
                    case IdeType.SELECTPROCOVERLOADING:
                        _SELECTPROCOVERLOADING = (SELECTPROCOVERLOADING_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SELECTPROCOVERLOADING_DELEGATE));
                        break;
                    case IdeType.GETSESSIONVALUE:
                        _GETSESSIONVALUE = (GETSESSIONVALUE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETSESSIONVALUE_DELEGATE));
                        break;
                    case IdeType.CHECKDBVERSION:
                        _CHECKDBVERSION = (CHECKDBVERSION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CHECKDBVERSION_DELEGATE));
                        break;
                    case IdeType.GETCONNECTIONINFOEX:
                        _GETCONNECTIONINFOEX = (GETCONNECTIONINFOEX_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCONNECTIONINFOEX_DELEGATE));
                        break;
                    case IdeType.FINDCONNECTION:
                        _FINDCONNECTION = (FINDCONNECTION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FINDCONNECTION_DELEGATE));
                        break;
                    case IdeType.ADDCONNECTION:
                        _ADDCONNECTION = (ADDCONNECTION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(ADDCONNECTION_DELEGATE));
                        break;
                    case IdeType.CONNECTCONNECTION:
                        _CONNECTCONNECTION = (CONNECTCONNECTION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CONNECTCONNECTION_DELEGATE));
                        break;
                    case IdeType.SETMAINCONNECTION:
                        _SETMAINCONNECTION = (SETMAINCONNECTION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETMAINCONNECTION_DELEGATE));
                        break;
                    case IdeType.GETWINDOWCONNECTION:
                        _GETWINDOWCONNECTION = (GETWINDOWCONNECTION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETWINDOWCONNECTION_DELEGATE));
                        break;
                    case IdeType.SETWINDOWCONNECTION:
                        _SETWINDOWCONNECTION = (SETWINDOWCONNECTION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETWINDOWCONNECTION_DELEGATE));
                        break;
                    case IdeType.GETCONNECTIONTREE:
                        _GETCONNECTIONTREE = (GETCONNECTIONTREE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCONNECTIONTREE_DELEGATE));
                        break;
                    case IdeType.GETCONNECTIONINFOEX10:
                        _GETCONNECTIONINFOEX10 = (GETCONNECTIONINFOEX10_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCONNECTIONINFOEX10_DELEGATE));
                        break;
                    case IdeType.FINDCONNECTIONEX10:
                        _FINDCONNECTIONEX10 = (FINDCONNECTIONEX10_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FINDCONNECTIONEX10_DELEGATE));
                        break;
                    case IdeType.ADDCONNECTIONEX10:
                        _ADDCONNECTIONEX10 = (ADDCONNECTIONEX10_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(ADDCONNECTIONEX10_DELEGATE));
                        break;
                    case IdeType.GETCONNECTIONTREEEX10:
                        _GETCONNECTIONTREEEX10 = (GETCONNECTIONTREEEX10_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETCONNECTIONTREEEX10_DELEGATE));
                        break;
                }
            }
        }

        public class SQL
        {
            public enum SqlType
            {
                EXECUTE = 40,
                FIELDCOUNT = 41,
                EOF = 42,
                NEXT = 43,
                FIELD = 44,
                FIELDNAME = 45,
                FIELDINDEX = 46,
                FIELDTYPE = 47,
                ERRORMESSAGE = 48,
                USEPLUGINSESSION = 50,
                USEDEFAULTSESSION = 51,
                CHECKCONNECTION = 52,
                GETDBMSGETOUTPUT = 53,
                SETVARIABLE = 54,
                GETVARIABLE = 55,
                CLEARVARIABLES = 56,
                SETPLUGINSESSION = 57
            }

            #region Delegates
            private delegate int EXECUTE_DELEGATE(string SQL);
            private delegate int FIELDCOUNT_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool EOF_DELEGATE();
            private delegate int NEXT_DELEGATE();
            private delegate IntPtr FIELD_DELEGATE(int Field);
            private delegate IntPtr FIELDNAME_DELEGATE(int Field);
            private delegate int FIELDINDEX_DELEGATE(string Name);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool USEPLUGINSESSION_DELEGATE(int PlugInID);
            private delegate void USEDEFAULTSESSION_DELEGATE(int PlugInID);
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool CHECKCONNECTION_DELEGATE();
            private delegate IntPtr GETDBMSGETOUTPUT_DELEGATE();
            private delegate void SETVARIABLE_DELEGATE(string Name, string Value);
            private delegate IntPtr GETVARIABLE_DELEGATE(string Name);
            private delegate void CLEARVARIABLES_DELEGATE();
            [return: MarshalAs(UnmanagedType.Bool)]
            private delegate bool SETPLUGINSESSION_DELEGATE(int PlugInID, string Username, string Password, string Database, string ConnectAs);
            private delegate int FIELDTYPE_DELEGATE(int Field);
            private delegate IntPtr ERRORMESSAGE_DELEGATE();
            #endregion

            #region vars
            private static EXECUTE_DELEGATE _EXECUTE;
            private static FIELDCOUNT_DELEGATE _FIELDCOUNT;
            private static EOF_DELEGATE _EOF;
            private static NEXT_DELEGATE _NEXT;
            private static FIELD_DELEGATE _FIELD;
            private static FIELDNAME_DELEGATE _FIELDNAME;
            private static FIELDINDEX_DELEGATE _FIELDINDEX;
            private static USEPLUGINSESSION_DELEGATE _USEPLUGINSESSION;
            private static USEDEFAULTSESSION_DELEGATE _USEDEFAULTSESSION;
            private static CHECKCONNECTION_DELEGATE _CHECKCONNECTION;
            private static GETDBMSGETOUTPUT_DELEGATE _GETDBMSGETOUTPUT;
            private static SETVARIABLE_DELEGATE _SETVARIABLE;
            private static GETVARIABLE_DELEGATE _GETVARIABLE;
            private static CLEARVARIABLES_DELEGATE _CLEARVARIABLES;
            private static SETPLUGINSESSION_DELEGATE _SETPLUGINSESSION;
            private static FIELDTYPE_DELEGATE _FIELDTYPE;
            private static ERRORMESSAGE_DELEGATE _ERRORMESSAGE;
            #endregion

            #region methods
            /// <summary>
            /// Executes the statement defined in the SQL parameter. The function returns 0 if successful, else the Oracle error number. 
            /// </summary> 
            public int Execute(string SQL)
            {
                return _EXECUTE(SQL);
            }

            /// <summary>
            /// Returns the number of fields after a Execute.
            /// </summary>
            public int FieldCount()
            {
                return _FIELDCOUNT();
            }

            /// <summary>
            /// Returns if there are any more rows to fetch.
            /// </summary>
            public bool Eof()
            {
                return _EOF();
            }

            /// <summary>
            /// Returns the next row after a Execute. The function returns 0 if successful, else the Oracle Error number
            /// </summary>
            public int Next()
            {
                return _NEXT();
            }

            /// <summary>
            /// Returns the field specified by the Field parameter.
            /// </summary>
            public string Field(int Field)
            {
                return Marshal.PtrToStringAnsi(_FIELD(Field));
            }

            /// <summary>
            /// Returns the fieldname specified by the Field parameter.
            /// </summary>
            public string FieldName(int Field)
            {
                return Marshal.PtrToStringAnsi(_FIELDNAME(Field));
            }

            /// <summary>
            /// Converts a fieldname into an index, which can be used in the Field, FieldName and FieldType functions. If the field does not exist, the return value is -1.
            /// </summary>
            public int FieldIndex(string Name)
            {
                return _FIELDINDEX(Name);
            }

            /// <summary>
            /// Normally, the SQL functions will use the main PL/SQL Developer Oracle session. If you want to make sure you don’t interfere with other transactions, and you want the PlugIn to use a private session, call this function.
            /// </summary>
            public bool UsePlugInSession(int PlugInID)
            {
                return _USEPLUGINSESSION(PlugInID);
            }

            /// <summary>
            /// This function will cancel the previous function and set the Oracle session back to default. 
            /// </summary>
            public void UseDefaultSession(int PlugInID)
            {
                _USEDEFAULTSESSION(PlugInID);
            }

            /// <summary>
            /// Forces PL/SQL Developer to check if the current connection to the database is still open (and tries a re-connect if necessary). The return value indicates if there is a connection.
            /// </summary>
            public bool CheckConnection()
            {
                return _CHECKCONNECTION();
            }

            /// <summary>
            /// Returns sys.dbms_output for the current (PlugIn specific) session.
            /// </summary>
            public string GetDBMSGetOutput()
            {
                return Marshal.PtrToStringAnsi(_GETDBMSGETOUTPUT());
            }

            /// <summary>
            /// This function declares a variable. Call this for al variables you use in the statement you pass in Execute. 
            /// </summary>
            public void SetVariable(string Name, string Value)
            {
                _SETVARIABLE(Name, Value);
            }

            /// <summary>
            /// This function will return the value of a variable. Available in version 700 
            /// </summary>
            public string GetVariable(string Name)
            {
                return Marshal.PtrToStringAnsi(_GETVARIABLE(Name));
            }

            /// <summary>
            /// Clear all declared variables. If you are finished doing a query it is a good      idea to call this function to prevent errors for the next execute.      Available in version 700      
            /// </summary>
            public void ClearVariables()
            {
                _CLEARVARIABLES();
            }

            /// <summary>
            /// This function allows you to specify the connection details used for the SQL functions for the PlugIn. If your Plug-In has a specific task for the current window, you can get the connection details with the GetWindowConnection() and GetConnectionInfoEx() functions. The return value indicates if the function succeeded.   
            /// </summary>
            public bool SetPlugInSession(int PlugInID, string Username, string Password, string Database, string ConnectAs)
            {
                return _SETPLUGINSESSION(PlugInID, Username, Password, Database, ConnectAs);
            }

            /// <summary>
            /// Return the fieldtype of a field.      3 = otInteger      4 = otFloat      5 = otString      8 = otLong      12 = otDate      24 = otLongRaw 
            /// </summary>
            public int FieldType(int Field)
            {
                return _FIELDTYPE(Field);
            }

            /// <summary>
            /// This function will return the error message for any error that occurred during: Execute, Eof, Next, SetConnection, Available in version 301
            /// </summary>
            public string ErrorMessage()
            {
                return Marshal.PtrToStringAnsi(_ERRORMESSAGE());
            }
            #endregion

            /// <summary>
            ///  Call Back Funktionen für SQL  
            /// </summary>  <param name="index"></param>  <param name="function"></param>
            public static void RegisterCallback(SqlType index, IntPtr function)
            {
                switch (index)
                {
                    case SqlType.EXECUTE:
                        _EXECUTE = (EXECUTE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(EXECUTE_DELEGATE));
                        break;
                    case SqlType.FIELDCOUNT:
                        _FIELDCOUNT = (FIELDCOUNT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FIELDCOUNT_DELEGATE));
                        break;
                    case SqlType.EOF:
                        _EOF = (EOF_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(EOF_DELEGATE));
                        break;
                    case SqlType.NEXT:
                        _NEXT = (NEXT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(NEXT_DELEGATE));
                        break;
                    case SqlType.FIELD:
                        _FIELD = (FIELD_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FIELD_DELEGATE));
                        break;
                    case SqlType.FIELDNAME:
                        _FIELDNAME = (FIELDNAME_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FIELDNAME_DELEGATE));
                        break;
                    case SqlType.FIELDINDEX:
                        _FIELDINDEX = (FIELDINDEX_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FIELDINDEX_DELEGATE));
                        break;
                    case SqlType.FIELDTYPE:
                        _FIELDTYPE = (FIELDTYPE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(FIELDTYPE_DELEGATE));
                        break;
                    case SqlType.ERRORMESSAGE:
                        _ERRORMESSAGE = (ERRORMESSAGE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(ERRORMESSAGE_DELEGATE));
                        break;
                    case SqlType.USEPLUGINSESSION:
                        _USEPLUGINSESSION = (USEPLUGINSESSION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(USEPLUGINSESSION_DELEGATE));
                        break;
                    case SqlType.USEDEFAULTSESSION:
                        _USEDEFAULTSESSION = (USEDEFAULTSESSION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(USEDEFAULTSESSION_DELEGATE));
                        break;
                    case SqlType.CHECKCONNECTION:
                        _CHECKCONNECTION = (CHECKCONNECTION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CHECKCONNECTION_DELEGATE));
                        break;
                    case SqlType.GETDBMSGETOUTPUT:
                        _GETDBMSGETOUTPUT = (GETDBMSGETOUTPUT_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETDBMSGETOUTPUT_DELEGATE));
                        break;
                    case SqlType.SETVARIABLE:
                        _SETVARIABLE = (SETVARIABLE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETVARIABLE_DELEGATE));
                        break;
                    case SqlType.GETVARIABLE:
                        _GETVARIABLE = (GETVARIABLE_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(GETVARIABLE_DELEGATE));
                        break;
                    case SqlType.CLEARVARIABLES:
                        _CLEARVARIABLES = (CLEARVARIABLES_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(CLEARVARIABLES_DELEGATE));
                        break;
                    case SqlType.SETPLUGINSESSION:
                        _SETPLUGINSESSION = (SETPLUGINSESSION_DELEGATE)Marshal.GetDelegateForFunctionPointer(function, typeof(SETPLUGINSESSION_DELEGATE));
                        break;

                }
            }

        }
    }
}