#nullable enable
using System;
using System.Threading.Tasks;
using Gdk;
using Microsoft.Maui.Platform;

namespace Microsoft.Maui.Handlers
{

	public partial class ImageHandler : ViewHandler<IImage, ImageView>
	{

		protected override ImageView CreatePlatformView()
		{
			var img = new ImageView();

			return img;
		}

		[MissingMapper]
		public static void MapAspect(IImageHandler handler, IImage image) { }

		[MissingMapper]
		public static void MapIsAnimationPlaying(IImageHandler handler, IImage image) { }

		public static void MapSource(IImageHandler handler, IImage image) =>
			MapSourceAsync(handler, image).FireAndForget(handler);

		public static async Task MapSourceAsync(IImageHandler handler, IImage image)
		{
			if (handler.PlatformView == null)
				return;

			await handler.SourceLoader.UpdateImageSourceAsync();

		}

		
		static int Request(double viewSize) => viewSize >= 0 ? (int)viewSize : -1;
		private void UpdateWidth(IImage image)
		{
			var widthRequest = Request(image.Width);

			if (widthRequest != -1 && widthRequest != PlatformView.WidthRequest && widthRequest != PlatformView.AllocatedWidth)
			{
				PlatformView.WidthRequest = widthRequest;
				PlatformView.QueueResize();
			}
		}
		
		private void UpdateHeight(IImage image)
		{
			var heightRequest = Request(image.Height);

			if (heightRequest != -1 && heightRequest != PlatformView.WidthRequest && heightRequest != PlatformView.AllocatedWidth)
			{
				PlatformView.WidthRequest = heightRequest;
				PlatformView.QueueResize();
			}
		}
		
		public static void MapWidth(IImageHandler handler, IImage view)
		{
			((ImageHandler)handler).UpdateWidth(view);
		}

		public static void MapHeight(IImageHandler handler, IImage view)
		{
			((ImageHandler)handler).UpdateHeight(view);
		}

		void OnSetImageSource(Pixbuf? obj) => PlatformView.Image = obj;

	}

}