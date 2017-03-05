using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Oxalis.Windows.Shell
{
	[Flags]
	public enum KnownFolderDefinitionFlags
	{
		LocalRedirectOnly = 0x2,
		Roamable = 0x4,
		PreCreate = 0x8,
		Stream = 0x10,
		PublishExpandeddPath = 0x20,
		NoRedirectUI = 0x40,
	}

	public sealed class KnownFolderDefinition
	{
		public KnownFolderCategory Category;
		public string Name;
		public string Description;
		public Guid ParentFolderId;
		public string RelativePath;
		public string ParsingName;
		public string ToolTip;
		public string LocalizedName;
		public string Icon;
		public string Security;
		public FileAttributes Attributes;
		public KnownFolderDefinitionFlags Flags;
		public Guid FolderTypeId;

		internal KnownFolderDefinition(KnownFolderDefinitionNative definition)
		{
			Category = definition.category;
			Name = Marshal.PtrToStringUni(definition.pszName);
			Description = Marshal.PtrToStringUni(definition.pszDescription);
			ParentFolderId = definition.fidParent;
			RelativePath = Marshal.PtrToStringUni(definition.pszRelativePath);
			ParsingName = Marshal.PtrToStringUni(definition.pszParsingName);
			ToolTip = Marshal.PtrToStringUni(definition.pszToolTip);
			LocalizedName = Marshal.PtrToStringUni(definition.pszLocalizedName);
			Icon = Marshal.PtrToStringUni(definition.pszIcon);
			Security = Marshal.PtrToStringUni(definition.pszSecurity);
			Attributes = (FileAttributes)definition.dwAttributes;
			Flags = definition.kfdFlags;
			FolderTypeId = definition.ftidType;
		}

		internal KnownFolderDefinitionNative CreateNativeStructure()
		{
			var definition = new KnownFolderDefinitionNative();
			definition.category = Category;
			definition.pszDescription = Marshal.StringToCoTaskMemUni(Description);
			definition.fidParent = ParentFolderId;
			definition.pszRelativePath = Marshal.StringToCoTaskMemUni(RelativePath);
			definition.pszParsingName = Marshal.StringToCoTaskMemUni(ParsingName);
			definition.pszToolTip = Marshal.StringToCoTaskMemUni(ToolTip);
			definition.pszLocalizedName = Marshal.StringToCoTaskMemUni(LocalizedName);
			definition.pszIcon = Marshal.StringToCoTaskMemUni(Icon);
			definition.pszSecurity = Marshal.StringToCoTaskMemUni(Security);
			definition.dwAttributes = (uint)Attributes;
			definition.kfdFlags = Flags;
			definition.ftidType = FolderTypeId;
			return definition;
		}
	}
}
