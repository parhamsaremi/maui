using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;

namespace Microsoft.Maui.Platform
{

	public class NavigationView : Gtk.Box
	{
		Gtk.Widget? toolbar;
		Gtk.Widget? pageWidget = null;
		IMauiContext? MauiContext;
		Stack<Widget?> stack = new();

		public NavigationView() : base(Gtk.Orientation.Vertical, 0)
		{
			var hbox = new Box(Gtk.Orientation.Horizontal, 0);
			var backBtn = new Button();
			backBtn.Image = new Image(Stock.GoBack, IconSize.Button);
			backBtn.Relief = ReliefStyle.None; // Remove button border

			hbox.PackStart(backBtn, false, false, 0);
			
			backBtn.Clicked += (o, args) =>
			{
				if (stack.Count <= 1) return;
				var currentPage = stack.Pop();
				var previousPage = stack.Peek();
				Remove(currentPage);
				Add(previousPage);
				SetChildPacking(previousPage, true, true, 0, PackType.End);
				pageWidget = previousPage;
			};

			toolbar = hbox;
		}

		public void Connect(IStackNavigationView virtualView)
		{
			MauiContext = virtualView.Handler?.MauiContext;
		}

		public void Disconnect(IStackNavigationView virtualView)
		{
			MauiContext = null;
		}

		public void RequestNavigation(NavigationRequest request)
		{
			// stack top is last
			var page = request.NavigationStack.Last();
			var newPageWidget = page.ToPlatform(MauiContext!);
			stack.Push(newPageWidget);
			if (pageWidget is null)
			{
				Add(toolbar);
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