using System;
using System.Runtime.InteropServices;

namespace Oxalis.Windows.Shell
{
	public sealed class KnownFolderManager
	{
		[Flags]
		public enum RedirectFlags
		{
			UserExclusive = 0x00000001,
			CopySourceDACL = 0x00000002,
			OwnerUser = 0x00000004,
			SetOwnerExplicit = 0x00000008,
			CheckOnly = 0x00000010,
			WithUI = 0x00000020,
			UnPIN = 0x00000040,
			PIN = 0x00000080,
			CopyContents = 0x00000200,
			DelSourceContents = 0x00000400,
			ExcludeAllKnownSubFolders = 0x00000800
		}

		[Flags]
		public enum FindMode : int
		{
			ExactMatch = 0,
			NearestParentMatch = 1
		}

		private IKnownFolderManagerNative manager;

		public KnownFolderManager()
		{
			manager = (IKnownFolderManagerNative)new KnownFolderManagerNative();
		}

		public Guid GetFolderIdFromCsidl(CSIDL csidl)
		{
			var folderId = manager.FolderIdFromCsidl((int)csidl);
			return folderId;
		}

		public CSIDL GetCsidlFromFolderId(Guid folderId)
		{
			var csidl = (CSIDL)manager.FolderIdToCsidl(ref folderId);
			return csidl;
		}

		public Guid[] GetFolderIds()
		{
			var count = manager.GetFolderIds(out var idsAddrHandle);
			using (idsAddrHandle)
			{
				var addr = idsAddrHandle.DangerousGetHandle();
				var ids = new Guid[count];
				for (var i = 0; i < count; i++)
				{
					ids[i] = Marshal.PtrToStructure<Guid>(addr);
					addr += Marshal.SizeOf<Guid>();
				}
				return ids;
			}
		}

		public KnownFolder GetFolder(Guid folderId)
		{
			var folder = manager.GetFolder(folderId);
			return new KnownFolder(folder);
		}

		public KnownFolder GetFolderByName(string canonicalName)
		{
			return new KnownFolder(manager.GetFolderByName(canonicalName));
		}

		public void RegisterFolder(Guid folderId, KnownFolderDefinition definition)
		{
			var native = definition.CreateNativeStructure();
			try
			{
				manager.RegisterFolder(folderId, ref native);
			}
			finally
			{
				native.Free();
			}
		}

		public void UnregisterFolder(Guid folderId)
		{
			manager.UnregisterFolder(ref folderId);
		}

		public KnownFolder FindFolderFromPath(
				string path,
				FindMode mode)
		{
			return new KnownFolder(manager.FindFolderFromPath(
				path, (int)mode));
		}

		public KnownFolder FindFolderFromIDList(
				IntPtr pidl)
		{
			return new KnownFolder(manager.FindFolderFromIDList(pidl));
		}

		public string Redirect(
			Guid folderId,
			IntPtr windowHandle,
			RedirectFlags flags,
			string targetPath = null,
			Guid[] exclusion = null)
		{
			manager.Redirect(ref folderId, windowHandle,
				(uint)flags, targetPath,
				exclusion != null ? (uint)exclusion.Length : 0,
				exclusion,
				out var error);
			return error;
		}
	}
}
