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
#if DEBUG
			Debug.WriteLine($"Previous: {calendar.Date.Value.Month}/{calendar.Date.Value.Day}/{calendar.Date.Value.Year}"); 
#endif
		}

		private void CalendarDatePicker_Today_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args) {
			var calendar = sender as CalendarDatePicker;
			var offset = calendar.Date - CalendarDatePicker_Previous.Date;
#if DEBUG
			if (CalendarDatePicker_Previous.Date != null) {
				Debug.WriteLine($"风骚哥本次签到距离上次签到已过了: {offset.Value.Days}天");
			}
			else {
				Debug.WriteLine("error");
			} 
#endif
		}

		private void TimePicker_Previous_TimeChanged(object sender, TimePickerValueChangedEventArgs e) {

		}

		private void TimePicker_Today_TimeChanged(object sender, TimePickerValueChangedEventArgs e) {
			if (CalendarDatePicker_Previous.Date != null && TimePicker_Previous.Time != null && CalendarDatePicker_Today.Date != null) {
				var datePrevious = CalendarDatePicker_Previous.Date;
				var dateToday = CalendarDatePicker_Today.Date;
				var timePrevious = TimePicker_Previous.Time;
				var timeToday = TimePicker_Today.Time;
				var datetimePrevious = new DateTime(
					datePrevious.Value.Year,
					datePrevious.Value.Month,
					datePrevious.Value.Day,
					timePrevious.Hours,
					timePrevious.Minutes,
					timePrevious.Seconds
				);
				Debug.WriteLine($"datetimePrevious: {datetimePrevious.ToString()}");
				var dateTimeToday = new DateTime(
					dateToday.Value.Year,
					dateToday.Value.Month,
					dateToday.Value.Day,
					timeToday.Hours,
					timeToday.Minutes,
					timeToday.Seconds
				);
				Debug.WriteLine($"datetimeToday: {dateTimeToday.ToString()}");
				Debug.WriteLine($"风骚哥本次签到距离上次签到已过了: {dateTimeToday - datetimePrevious}");

				if (FindName("ResultTextBolck") != null) {
					(FindName("ResultTextBolck") as TextBlock).Text = $"风骚哥本次签到距离上次签到已过了: {(dateTimeToday - datetimePrevious).Days}天{(dateTimeToday - datetimePrevious).Hours}小时{(dateTimeToday - datetimePrevious).Minutes}分";
				}
				else {
					Root.RowDefinitions[2].Height = new GridLength(100);
					TextBlock textBlock = new TextBlock();
					textBlock.Name = "ResultTextBolck";
					textBlock.Text = $"风骚哥本次签到距离上次签到已过了: {(dateTimeToday - datetimePrevious).Days}天{(dateTimeToday - datetimePrevious).Hours}小时{(dateTimeToday - datetimePrevious).Minutes}分";
					textBlock.FontSize = 50;
					Root.Children.Add(textBlock);
					Grid.SetColumn((FrameworkElement)textBlock, 0);
					Grid.SetRow((FrameworkElement)textBlock, 2);
					Grid.SetColumnSpan((FrameworkElement)textBlock, 2);
				}

				Debug.WriteLine($"风骚哥本次签到距离上次签到已过了: {(dateTimeToday - datetimePrevious).ToString("c")}");

				Debug.WriteLine((dateTimeToday - datetimePrevious).Days);
				Debug.WriteLine((dateTimeToday - datetimePrevious).Hours);
				Debug.WriteLine((dateTimeToday - datetimePrevious).Minutes);
			}
			else {
				Debug.WriteLine("error");
			}
		}

		private void LeftListView_ItemClick(object sender, ItemClickEventArgs e) {

		}
	}
}
