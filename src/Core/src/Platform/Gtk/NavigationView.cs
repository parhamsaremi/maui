using System;
using System.Linq;
using Gtk;

namespace Microsoft.Maui.Platform
{
	public interface IToolbarContainer
	{
		public void SetToolbar(MauiToolbar? toolbar);
	}
	public class NavigationView : Gtk.Box, IToolbarContainer
	{
		MauiToolbar? _toolbar;
		Gtk.Widget? pageWidget;
		IMauiContext? MauiContext;

		public NavigationView() : base(Gtk.Orientation.Vertical, 0)
		{

		}

		public void Connect(IStackNavigationView virtualView)
		{
			MauiContext = virtualView.Handler?.MauiContext;
		}

		public void Disconnect(IStackNavigationView virtualView)
		{
			MauiContext = null;
		}

		public void SetToolbar(MauiToolbar? toolbar)
		{
			if(toolbar is null)
				return;
			if (_toolbar is not null)
			{
				Remove(_toolbar);
			}

			_toolbar = toolbar;
			PackStart(_toolbar, false, true, 0);
		}
		
		public void RequestNavigation(NavigationRequest request)
		{
			// stack top is last
			var page = request.NavigationStack.Last();
			var newPageWidget = page.ToPlatform(MauiContext!);
			if (pageWidget is null)
			{
				PackEnd(newPageWidget, true, true, 0);
			}
			else
			{
				Remove(pageWidget);
				Add(newPageWidget);
				SetChildPacking(newPageWidget, true, true, 0, PackType.End);
			}
			pageWidget = newPageWidget;
		}

	}

}