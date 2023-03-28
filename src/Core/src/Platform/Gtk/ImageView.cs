using Gdk;

namespace Microsoft.Maui.Platform
{

	// https://docs.gtk.org/gtk3/class.Imgage.html 

	// GtkImage has nothing like Aspect; maybe an ownerdrawn class is needed 
	// could be: https://docs.gtk.org/gtk3/class.DrawingArea.html
	// or Microsoft.Maui.Graphics.Platform.Gtk.GtkGraphicsView

	public class ImageView : Gtk.Image
	{
		Pixbuf? OriginalSizeBuf;

		public Pixbuf? Image
		{
			get => Pixbuf;
			set
			{
				Pixbuf = value;
				OriginalSizeBuf = value;
			}
		}

		public void Resize(int width, int height)
		{
			var newBuf = OriginalSizeBuf?.ScaleSimple(width, height, InterpType.Bilinear);
			Pixbuf = newBuf;
		}

	}

}