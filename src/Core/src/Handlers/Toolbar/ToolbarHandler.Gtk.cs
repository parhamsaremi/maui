using System;

namespace Microsoft.Maui.Handlers
{
	public partial class ToolbarHandler : ElementHandler<IToolbar, MauiToolbar>
	{
		protected override MauiToolbar CreatePlatformElement() => new();
		
		protected override void ConnectHandler(MauiToolbar platformView)
		{
			platformView.BackButton.Clicked += OnBackButtonPressed;
			base.ConnectHandler(platformView);
		}

		protected override void DisconnectHandler(MauiToolbar platformView)
		{
			platformView.BackButton.Clicked -= OnBackButtonPressed;
			base.DisconnectHandler(platformView);
		}

		public static void MapTitle(IToolbarHandler arg1, IToolbar arg2)
		{
		}
		
		void OnBackButtonPressed(object? sender, EventArgs args)
		{
			if (VirtualView is { BackButtonVisible: true, IsVisible: true })
			{
				//TODO: implement this behavior
			}
		}
	}
}