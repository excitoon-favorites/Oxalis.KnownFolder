using System;
using System.Runtime.InteropServices;

namespace Oxalis.Windows.Shell
{
	[Flags]
	public enum KnownFolderFlag : uint
	{
		Default = 0x0,
		SimpleIDList = 0x100,
		NotParentRelative = 0x200,
		DefaultPath = 0x400,
		Init = 0x800,
		NoAlias = 0x1000,
		DontUnexpand = 0x2000,
		DontVerify = 0x4000,
		Create = 0x8000,
		NoAppContainerRedirection = 0x10000,
		AliasOnly = 0x80000000
	}

	public enum KnownFolderCategory : uint
	{
		Virtual = 1,
		Fixed = 2,
		Common = 3,
		PerUser = 4
	}

	public sealed class KnownFolder
	{
		private IKnownFolderNative folder;

		public KnownFolder(IntPtr pUnk)
		{
			folder = (IKnownFolderNative)Marshal.GetObjectForIUnknown(pUnk);
		}

		public KnownFolder(object unk)
		{
			folder = (IKnownFolderNative)unk;
		}

		public string GetPath(KnownFolderFlag flags)
		{
			try
			{
				using (var handle = folder.GetPath((uint)flags))
				{
					return Marshal.PtrToStringUni(handle.DangerousGetHandle());
				}
			}
			catch
			{
				return null;
			}
		}

		public void SetPath(string path, KnownFolderFlag flags)
		{
			folder.SetPath((uint)flags, path);
		}

		public IntPtr GetIDList(KnownFolderFlag flags)
		{
			return folder.GetIDList((uint)flags);
		}

		public Guid Id => folder.GetId();

		public KnownFolderCategory Category => folder.GetCategory();

		public string Path
		{
			get
			{
				return GetPath(KnownFolderFlag.Default);
			}
			set
			{
				SetPath(value, KnownFolderFlag.Default);
			}
		}

		public Guid FolderType => folder.GetFolderType();
		public uint RedirectionCapabilities => folder.GetRedirectionCapabilities();

		public KnownFolderDefinition Definition
		{
			get
			{
				var native = folder.GetFolderDefinition();
				var definition = new KnownFolderDefinition(native);
				native.Free();
				return definition;
			}
		}

		public object GetShellItem(KnownFolderFlag flag)
		{
			var IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");
			return folder.GetShellItem((uint)flag, ref IID_IUnknown);
		}
	}
}