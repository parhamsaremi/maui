using Gtk;

namespace Microsoft.Maui.Platform
{
	public class MauiToolbar: Box
	{
		internal Button BackButton { get; }
		public MauiToolbar(): base(Orientation.Horizontal, 0)
		{
			BackButton = new Button();
			BackButton.Image = new Image(Stock.GoBack, IconSize.Button);
			BackButton.Relief = ReliefStyle.None; // Remove button border

			PackStart(BackButton, false, false, 0);
		}
	}	
}