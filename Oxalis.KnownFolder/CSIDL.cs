using System;

namespace Oxalis.Windows.Shell
{
	/// <summary>
	/// Constant special item ID list
	/// </summary>
	[Flags]
	public enum CSIDL : int
	{
		FlagCreate = 0x8000,
		AdminTools = 0x0030,
		AltStartup = 0x001d,
		AppData = 0x001a,
		BitBucket = 0x000a,
		CDBurnArea = 0x003b,
		CommonAdminTools = 0x002f,
		CommonAltStartup = 0x001e,
		CommonAppData = 0x0023,
		CommonDesktopDirectory = 0x0019,
		CommonDocuments = 0x002e,
		CommonFavorites = 0x001f,
		CommonMusic = 0x0035,
		CommonPictures = 0x0036,
		CommonPrograms = 0x0017,
		CommonStartMenu = 0x0016,
		CommonStartup = 0x0018,
		CommonTemplates = 0x002d,
		CommonVideo = 0x0037,
		Controls = 0x0003,
		Cookies = 0x0021,
		Desktop = 0x0000,
		DesktopDirectory = 0x0010,
		Drives = 0x0011,
		Favorites = 0x0006,
		Fonts = 0x0014,
		History = 0x0022,
		Internet = 0x0001,
		InternetCache = 0x0020,
		LocalAppData = 0x001c,
		MyDocuments = 0x000c,
		MyMusic = 0x000d,
		MyPictures = 0x0027,
		MyVideo = 0x000e,
		NetHood = 0x0013,
		Network = 0x0012,
		Personal = 0x0005,
		Printers = 0x0004,
		PrintHood = 0x001b,
		Profile = 0x0028,
		Profiles = 0x003e,
		ProgramFiles = 0x0026,
		ProgramFilesCommon = 0x002b,
		Programs = 0x0002,
		Recent = 0x0008,
		SendTo = 0x0009,
		StartMenu = 0x000b,
		Startup = 0x0007,
		System = 0x0025,
		Templates = 0x0015,
		Windows = 0x0024
	}
}
