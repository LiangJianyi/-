using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace 风骚哥签到日期计算器 {
	using Debug = System.Diagnostics.Debug;
	public sealed partial class MainPage : Page {
		public MainPage() {
			this.InitializeComponent();
		}

		private void CalendarDatePicker_Previous_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args) {
			var calendar = sender as CalendarDatePicker;
			Debug.WriteLine($"Previous: {calendar.Date.Value.Month}/{calendar.Date.Value.Day}/{calendar.Date.Value.Year}");
		}

		private void CalendarDatePicker_Today_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args) {
			var calendar = sender as CalendarDatePicker;
			var offset = calendar.Date - CalendarDatePicker_Previous.Date;
			if (CalendarDatePicker_Previous.Date != null) {
				Debug.WriteLine($"风骚哥本次签到距离上次签到已过了: {offset.Value.Days}天");
			}
			else {
				Debug.WriteLine("error");
			}
		}
	}
}
