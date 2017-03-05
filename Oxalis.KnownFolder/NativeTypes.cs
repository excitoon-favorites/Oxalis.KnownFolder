using System;
using System.Runtime.InteropServices;
using Oxalis.Windows.Memory;

namespace Oxalis.Windows.Shell
{
	[ComImport]
	[Guid("4df0c730-df9d-4ae3-9153-aa6b82e9795a")]
	internal class KnownFolderManagerNative
	{

	}

	[ComImport]
	[Guid("8BE2D872-86AA-4d47-B776-32CCA40C7018")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IKnownFolderManagerNative
	{
		Guid FolderIdFromCsidl(int nCsidl);
		int FolderIdToCsidl(ref Guid rfid);
		uint GetFolderIds(out SafeCoTaskMemHandle ppKFId);
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetFolder(ref Guid rfid);
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetFolderByName([MarshalAs(UnmanagedType.LPWStr)] string pszCanonicalName);
		void RegisterFolder(ref Guid rfid, ref KnownFolderDefinitionNative pKFD);
		void UnregisterFolder(ref Guid rfid);
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object FindFolderFromPath(
			[MarshalAs(UnmanagedType.LPWStr)] string pszPath,
			int mode);
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object FindFolderFromIDList(IntPtr pidl);
		void Redirect(
			[In] ref Guid rfid,
			IntPtr hwnd,
			uint Flags,
			[MarshalAs(UnmanagedType.LPWStr)] string pszTargetPath,
			[In] uint cFolders,
			[In, MarshalAs(UnmanagedType.LPArray)] Guid[] pExclusion,
			[Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszError);
	}

	[ComImport]
	[Guid("3AA7AF7E-9B36-420c-A8E3-F77D4674A488")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IKnownFolderNative
	{
		Guid GetId();
		KnownFolderCategory GetCategory();
		[return: MarshalAs(UnmanagedType.IUnknown)] object GetShellItem(uint dwFlags, ref Guid riid);
		SafeCoTaskMemHandle GetPath([In] uint dwFlags);
		void SetPath([In] uint dwFlags, [MarshalAs(UnmanagedType.LPWStr)] string pszPath);
		IntPtr GetIDList([In] uint dwFlags);
		Guid GetFolderType();
		uint GetRedirectionCapabilities();
		KnownFolderDefinitionNative GetFolderDefinition();
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct KnownFolderDefinitionNative
	{
		public KnownFolderCategory category;
		public IntPtr pszName;
		public IntPtr pszDescription;
		public Guid fidParent;
		public IntPtr pszRelativePath;
		public IntPtr pszParsingName;
		public IntPtr pszToolTip;
		public IntPtr pszLocalizedName;
		public IntPtr pszIcon;
		public IntPtr pszSecurity;
		public uint dwAttributes;
		public KnownFolderDefinitionFlags kfdFlags;
		public Guid ftidType;

		public void Free()
		{
			Marshal.FreeCoTaskMem(pszName);
			Marshal.FreeCoTaskMem(pszDescription);
			Marshal.FreeCoTaskMem(pszRelativePath);
			Marshal.FreeCoTaskMem(pszParsingName);
			Marshal.FreeCoTaskMem(pszToolTip);
			Marshal.FreeCoTaskMem(pszLocalizedName);
			Marshal.FreeCoTaskMem(pszIcon);
			Marshal.FreeCoTaskMem(pszSecurity);
		}
	}
}