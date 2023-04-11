using System;
using GLib;
using Gtk;

namespace Microsoft.Maui.Handlers
{

	public partial class WindowHandler : ElementHandler<IWindow, Gtk.Window>
	{
		MauiToolbar? ToolBar { get; set; }
		Widget? Content { get; set; }

		Box vbox = new Box(Orientation.Vertical, 0);

		public static void MapTitle(IWindowHandler handler, IWindow window) =>
			handler.PlatformView.UpdateTitle(window);

		public static void MapContent(IWindowHandler handler, IWindow window)
		{
			_ = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			var nativeContent = window.Content.ToPlatform(handler.MauiContext);

			if (handler is WindowHandler windowHandler)
			{
				windowHandler.UpdateContents(nativeContent, windowHandler.ToolBar);
				windowHandler.Content = nativeContent;
			}
			else
			{
				handler.PlatformView.Child = nativeContent;
			}
		}

		[MissingMapper]
		public static void MapRequestDisplayDensity(IWindowHandler handler, IWindow window, object? args) { }
		
		public static void MapToolbar(IWindowHandler handler, IWindow view)
		{
			if (view is IToolbarElement tb)
			{
				if (handler.MauiContext is null)
					return;
				if (handler is WindowHandler windowHandler)
				{
					if (windowHandler.ToolBar is not null)
					{
						windowHandler.ToolBar.BackButtonClicked -= windowHandler.OnBackButtonClicked;
					}
					windowHandler.UpdateContents(windowHandler.Content, tb.Toolbar?.ToPlatform(handler.MauiContext) as MauiToolbar);
					if (windowHandler.ToolBar is not null)
					{
						windowHandler.ToolBar.BackButtonClicked += windowHandler.OnBackButtonClicked;
					}
				}
			}
		}

		void UpdateContents(Widget? content, MauiToolbar? newToolbar)
		{
			if(content is null)
				return;
			ToolBar = newToolbar;
			foreach (var child in (vbox.Children.Clone() as Widget[])!)
			{
				vbox.Remove(child);	
			}
			if (ToolBar is null)
			{
				vbox.PackStart(content, true, true, 0);
			}
			else
			{
				vbox.PackStart(ToolBar, false, true, 0);
				vbox.Add(content);
				vbox.SetChildPacking(content, true,true, 0, PackType.End);
			}
			if (PlatformView.Child is null)
				PlatformView.Child = vbox;
			else
				PlatformView.ReplaceChild(PlatformView.Child, vbox);
		}

		void OnBackButtonClicked(object? sender)
		{
			
		}
	}

}