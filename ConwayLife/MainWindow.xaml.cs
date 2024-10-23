using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Xml.Linq;

namespace ConwayLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RunState = new LifeState(40, 40);
            LoadInitialBoardState("InitialBoardState.xml");
            InitializeGrid(RunState);

            LifeGridUpdateTimer = new Timer(500);
            LifeGridUpdateTimer.Elapsed += LifeGridUpdate;
            LifeGridUpdateTimer.Start();
        }

        public LifeState RunState
        {
            get;
            private set;
        }
        public bool IsUpdating
        {
            get;
            set;
        }

        public void InitializeGrid(LifeState state)
        {
            LifeGrid.RowDefinitions.Clear();
            LifeGrid.ColumnDefinitions.Clear();

            for(int RowCount = 0; RowCount < state.Rows; RowCount++)
            {
                LifeGrid.RowDefinitions.Add(new RowDefinition());
            }
            for(int ColumnCount = 0; ColumnCount < state.Columns; ColumnCount++)
            {
                LifeGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for(int RowCount = 0; RowCount < state.Rows; RowCount++)
            {
                for(int ColumnCount = 0; ColumnCount < state.Columns; ColumnCount++)
                {
                    Canvas LifeElement = new Canvas();
                    Grid.SetRow(LifeElement, RowCount);
                    Grid.SetColumn(LifeElement, ColumnCount);
                    LifeGrid.Children.Add(LifeElement);
                }
            }
        }

        void LifeGridUpdate(object sender, ElapsedEventArgs e)
        {
            if (IsUpdating == false)
            {
                IsUpdating = true;

                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    foreach (Canvas LifeElement in LifeGrid.Children)
                    {
                        if (RunState.Values[Grid.GetRow(LifeElement), Grid.GetColumn(LifeElement)] == true)
                        {
                            LifeElement.Background = Brushes.Black;
                        }
                        else
                        {
                            LifeElement.Background = Brushes.AntiqueWhite;
                        }
                    }
                });

                RunState.Advance();

                IsUpdating = false;
            }
        }

        private void LoadInitialBoardState(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            var rows = doc.Root.Elements("Row").ToList();

            for (int row = 0; row < rows.Count; row++)
            {
                var cells = rows[row].Elements("Cell").ToList();
                for (int col = 0; col < cells.Count; col++)
                {
                    RunState.Values[row, col] = bool.Parse(cells[col].Value);
                }
            }
        }

        private Timer LifeGridUpdateTimer;
    }
}
