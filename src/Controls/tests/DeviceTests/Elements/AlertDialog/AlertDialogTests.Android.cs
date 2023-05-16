using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Platform;
using Xunit;
using AndroidX.AppCompat.App;
using Android.Graphics;

namespace Microsoft.Maui.DeviceTests
{
	public partial class AlertDialogTests: ControlsHandlerTestBase
	{

		async Task<Color> ReturnTextColor()
		{
			var mauiContextStub1 = new ContextStub(ApplicationServices);
			var activity = mauiContextStub1.GetActivity();
			mauiContextStub1.Context = new Android.Views.ContextThemeWrapper(activity, Resource.Style.Maui_MainTheme_NoActionBar);
			Color color = Color.Transparent;
			await InvokeOnMainThreadAsync(() =>
			{
				var builder = new AlertDialog.Builder(activity);
				var alertDialog = builder.Create();
				alertDialog.Show();
				var button = alertDialog.GetButton((int)Android.Content.DialogButtonType.Negative);
				var textColor = button.CurrentTextColor;
				color = new Color(textColor);
			});

			return color;
		}

		[Fact]
		public void AlertDialogButtonColorLightTheme()
		{
			AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
			var textColor = ReturnTextColor().Result;
			Assert.True(textColor.GetBrightness() < 0.5);
		}

		[Fact]
		public void AlertDialogButtonColorDarkTheme()
		{
			AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
			var textColor = ReturnTextColor().Result;
			Assert.True(textColor.GetBrightness() > 0.5);
		}
	}
}
