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
			this.LeftMenu.IsPaneOpen = false;
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
					HomeGrid.RowDefinitions[2].Height = new GridLength(100);
					TextBlock textBlock = new TextBlock {
						Name = "ResultTextBolck",
						Text = $"风骚哥本次签到距离上次签到已过了: {(dateTimeToday - datetimePrevious).Days}天{(dateTimeToday - datetimePrevious).Hours}小时{(dateTimeToday - datetimePrevious).Minutes}分",
						FontSize = 50
					};
					HomeGrid.Children.Add(textBlock);
					Grid.SetColumn(textBlock, 0);
					Grid.SetRow(textBlock, 2);
					Grid.SetColumnSpan(textBlock, 2);
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
			var stackPanel = e.ClickedItem as StackPanel;
			var clickedItemTextBlock = stackPanel.Children[1] as TextBlock;
			switch (clickedItemTextBlock.Text) {
				case "主页":
					this.LeftMenu.Content = CreateHomeGrid();
					break;
				default:
					break;
			}
		}

		private Grid CreateHomeGrid() {
			var homeGrid = new Grid() { Name = "HomeGrid" };
			homeGrid.ColumnDefinitions.Add(new ColumnDefinition());
			homeGrid.ColumnDefinitions.Add(new ColumnDefinition());
			homeGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(120) });
			homeGrid.RowDefinitions.Add(new RowDefinition());
			homeGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0) });

			var titleTextBlock = new TextBlock() {
				Name = "TitleTextBlock",
				Text = "风骚哥今天签到了吗？",
				FontSize = 50,
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(0, 35, 0, 0)
			};
			homeGrid.Children.Add(titleTextBlock);
			Grid.SetColumn(titleTextBlock, 0);
			Grid.SetColumnSpan(titleTextBlock, 2);
			Grid.SetRow(titleTextBlock, 0);

			// 封装“上次签到时间”的表格
			var leftGrid = new Grid();
			leftGrid.ColumnDefinitions.Add(new ColumnDefinition());
			leftGrid.RowDefinitions.Add(new RowDefinition());
			var leftCalendarDatePicker = new CalendarDatePicker() {
				Name = "CalendarDatePicker_Previous",
				Header = "上次签到时间",
				PlaceholderText = "上次签到时间",
				HorizontalAlignment = HorizontalAlignment.Center,
			};
			leftCalendarDatePicker.DateChanged += CalendarDatePicker_Previous_DateChanged;
			var leftTimePicker = new TimePicker() {
				Name = "TimePicker_Previous",
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(0, 120, 0, 0)
			};
			leftTimePicker.TimeChanged += TimePicker_Previous_TimeChanged;
			leftGrid.Children.Add(leftCalendarDatePicker);
			leftGrid.Children.Add(leftTimePicker);
			Grid.SetColumn(leftCalendarDatePicker, 0);
			Grid.SetRow(leftTimePicker, 0);
			homeGrid.Children.Add(leftGrid);
			Grid.SetColumn(leftGrid, 0);
			Grid.SetRow(leftGrid, 1);

			// 封装“今天签到时间”的表格
			var rightGrid = new Grid();
			rightGrid.ColumnDefinitions.Add(new ColumnDefinition());
			rightGrid.RowDefinitions.Add(new RowDefinition());
			var rightCalendarDatePicker = new CalendarDatePicker() {
				Name = "CalendarDatePicker_Today",
				Header = "今天签到时间",
				PlaceholderText = "今天签到时间",
				HorizontalAlignment = HorizontalAlignment.Center,
			};
			rightCalendarDatePicker.DateChanged += CalendarDatePicker_Today_DateChanged;
			var rightTimePicker = new TimePicker() {
				Name = "TimePicker_Today",
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(0, 120, 0, 0)
			};
			rightTimePicker.TimeChanged += TimePicker_Today_TimeChanged;
			rightGrid.Children.Add(rightCalendarDatePicker);
			rightGrid.Children.Add(rightTimePicker);
			Grid.SetColumn(rightCalendarDatePicker, 0);
			Grid.SetRow(rightTimePicker, 0);
			homeGrid.Children.Add(rightGrid);
			Grid.SetColumn(rightGrid, 1);
			Grid.SetRow(rightGrid, 1);

			return homeGrid;
		}

		private void SplitView_PointerPressed(object sender, PointerRoutedEventArgs e) {
			var splitView = sender as SplitView;
			if (!splitView.IsPaneOpen) {
				splitView.IsPaneOpen = true;
			}
		}
	}
}
