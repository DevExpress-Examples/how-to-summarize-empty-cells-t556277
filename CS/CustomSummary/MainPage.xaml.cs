using System.Collections.Generic;
using DevExpress.Data;
using DevExpress.UI.Xaml.Grid;
using Windows.UI.Xaml.Controls;

namespace CustomSummary {
    public sealed partial class MainPage : Page {
        List<TestData> list;
        Dictionary<int, bool> selectedValues = new Dictionary<int, bool>();

        public MainPage() {
            this.InitializeComponent();
            list = new List<TestData>();
            for (int i = 0; i < 100; i++) {
                list.Add(new TestData() { Text = "item " + i, Number = i });
                if (i % 3 == 0)
                    list[i].Number = null;
            }
            grid.ItemsSource = list;
        }

        int emptyCellsTotalCount = 0;



        public class TestData {
            public string Text { get; set; }
            public int? Number { get; set; }
        }

        private void grid_CustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e) {
            if (((GridSummaryItem)e.Item).FieldName != "Number")
                return;
            if (e.IsTotalSummary) {
                if (e.SummaryProcess == CustomSummaryProcess.Start) {
                    emptyCellsTotalCount = 0;
                }
                if (e.SummaryProcess == CustomSummaryProcess.Calculate) {
                    int? val = (int?)e.FieldValue;
                    if (!val.HasValue)
                        emptyCellsTotalCount++;
                    e.TotalValue = emptyCellsTotalCount;
                }
            }

        }
    }
}
