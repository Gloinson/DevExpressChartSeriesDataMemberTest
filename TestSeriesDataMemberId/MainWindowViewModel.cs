using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using TestSeriesDataMemberId.ViewModels;


namespace TestSeriesDataMemberId
{
    public class ComplexId : IEquatable<ComplexId>
    {
        public string Key1 { get; set; }
        public string Key2 { get; set; }

        public override string ToString()
        {
            return Key1;
        }

        public bool Equals(ComplexId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Key1, other.Key1) && string.Equals(Key2, other.Key2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ComplexId)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Key1 != null ? Key1.GetHashCode() : 0) * 397) ^ (Key2 != null ? Key2.GetHashCode() : 0);
            }
        }
    }

    public class ValuePoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public ComplexId Id { get; set; }
    }


    public class MainWindowViewModel : NotificationBase
    {
        private readonly object sync = new object();

        private ObservableCollection<ValuePoint> dataCollection = new ObservableCollection<ValuePoint>();
        public ObservableCollection<ValuePoint> DataCollection
        {
            get { return dataCollection; }
            set { SetProperty(ref dataCollection, value); }
        }

        public UICommand SecondSeriesAtOnceCommand { get; }
        public UICommand SecondSeriesValueByValueCommand { get; }
        public UICommand ThirdSeriesAtOnceCommand { get; }
        public UICommand ThirdSeriesValueByValueCommand { get; }

        private void SecondSeriesAtOnce(object obj)
        {
            AddSecondSeriesFirstValue();
            AddSecondSeriesSecondValue();
        }

        private void ThirdSeriesAtOnce(object obj)
        {
            AddThirdSeriesFirstValue();
            AddThirdSeriesSecondValue();
        }

        private void SecondSeriesValueByValue(object obj)
        {
            AddSecondSeriesFirstValue();

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) => { AddSecondSeriesSecondValue(); };
            timer.Start();
        }


        private void ThirdSeriesValueByValue(object obj)
        {
            AddThirdSeriesFirstValue();

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) => { AddThirdSeriesSecondValue(); };
            timer.Start();
        }

        public MainWindowViewModel()
        {
            BindingOperations.EnableCollectionSynchronization(dataCollection, sync);
            InitialData();

            SecondSeriesAtOnceCommand = new UICommand(SecondSeriesAtOnce);
            SecondSeriesValueByValueCommand = new UICommand(SecondSeriesValueByValue);
            ThirdSeriesAtOnceCommand = new UICommand(ThirdSeriesAtOnce);
            ThirdSeriesValueByValueCommand = new UICommand(ThirdSeriesValueByValue);

        }

        private void InitialData()
        {
            // 1st Series, 1st/2nd Value: Series-M_1, flat line
            dataCollection.Add(
                new ValuePoint()
                {
                    X = 0,
                    Y = 1,
                    Id = new ComplexId() { Key1 = "Series", Key2 = "M_1" }
                });
            dataCollection.Add(
                new ValuePoint()
                {
                    X = 10,
                    Y = 1,
                    Id = new ComplexId() { Key1 = "Series", Key2 = "M_1" }
                });
        }

        private void AddSecondSeriesFirstValue()
        {
            dataCollection.Add(
                new ValuePoint()
                {
                    X = 0,
                    Y = 10,
                    Id = new ComplexId() { Key1 = "Series", Key2 = "M_2" }
                });
        }

        private void AddSecondSeriesSecondValue()
        {
            dataCollection.Add(
                new ValuePoint()
                {
                    X = 10,
                    Y = 10,
                    Id = new ComplexId() { Key1 = "Series", Key2 = "M_2" }
                });
        }

        private void AddThirdSeriesFirstValue()
        {
            dataCollection.Add(
                new ValuePoint()
                {
                    X = 0,
                    Y = 5,
                    Id = new ComplexId() { Key1 = "Extra", Key2 = "M_3" }
                });
        }

        private void AddThirdSeriesSecondValue()
        {
            dataCollection.Add(
                new ValuePoint()
                {
                    X = 10,
                    Y = 5,
                    Id = new ComplexId() { Key1 = "Extra", Key2 = "M_3" }
                });
        }
    }
}
