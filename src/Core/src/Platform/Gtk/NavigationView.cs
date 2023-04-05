using System;
using System.Linq;
using Gtk;

namespace Microsoft.Maui.Platform
{

	public class NavigationView : Gtk.Box
	{
		Gtk.Button backButton = new(){Label = "back"};
		Gtk.Widget? pageWidget = null;
		IMauiContext? mauiContext = null;

		public NavigationView() : base(Gtk.Orientation.Vertical, 0) { }

		public void Connect(IStackNavigationView virtualView)
		{
			mauiContext = virtualView.Handler?.MauiContext;
		}

		public void Disconnect(IStackNavigationView virtualView)
		{
		}

		public void RequestNavigation(NavigationRequest request)
		{
			// stack top is last
			var page = request.NavigationStack.Last();
			var newPageWidget = page.ToPlatform(mauiContext!);
			var oldPageWidget = pageWidget;
			if (pageWidget is null)
			{
				this.PackStart(backButton, true, false, 0);
				this.Add(newPageWidget);
			}
			else
			{
				backButton.ButtonPressEvent += (o, args) =>
				{
					this.Remove(newPageWidget);
					this.Add(oldPageWidget);
					pageWidget = oldPageWidget;
				};
				this.Remove(pageWidget);
				this.Add(newPageWidget);
				// this.SetChildPacking(newPageWidget, true, true, 0, PackType.Start);
			}
			pageWidget = newPageWidget;
		}

	}

}