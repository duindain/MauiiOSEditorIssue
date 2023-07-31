#nullable enable
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
#if __IOS__ || MACCATALYST
using System;
using System.Runtime.InteropServices;
using ObjCRuntime;
using PlatformView = MauiBrokenEditoriOS.iOSTextPlatformView;
#elif MONOANDROID
using PlatformView = AndroidX.AppCompat.Widget.AppCompatEditText;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.TextBox;
#elif TIZEN
using PlatformView = Tizen.UIExtensions.NUI.Editor;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformView = System.Object;
#endif

namespace MauiBrokenEditoriOS
{
    public class MyTextViewHandler : EditorHandler
    {
        new MauiTextView PlatformView => PlatformView;
    }
}

