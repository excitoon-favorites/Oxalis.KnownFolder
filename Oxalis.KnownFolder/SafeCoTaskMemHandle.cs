using System;
using System.Runtime.InteropServices;

namespace Oxalis.Windows.Memory
{
	internal sealed class SafeCoTaskMemHandle : SafeHandle
	{
		public override bool IsInvalid => this.handle == IntPtr.Zero;

		private SafeCoTaskMemHandle()
			: base(IntPtr.Zero, true)
		{

		}

		public SafeCoTaskMemHandle(IntPtr handle, bool ownsHandle)
			: base(IntPtr.Zero, ownsHandle)
		{
			this.handle = handle;
		}

		protected override bool ReleaseHandle()
		{
			Marshal.FreeCoTaskMem(this.handle);
			return true;
		}
	}
}
