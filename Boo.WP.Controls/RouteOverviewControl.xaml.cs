// -----------------------------------------------------------------------
//  <copyright file="RouteOverviewControl.xaml.cs" company="Ketto">
//      Copyright (c) Ketto. All rights reserved.
//  </copyright>
//  <author>Nicolas Boonaert</author>
// -----------------------------------------------------------------------
namespace Boo.WP.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using Boo.WP.Controls.Entities.Logic;
    using Boo.WP.Controls.Entities.Presentation.Enums;

    /// <summary>
    ///     The route overview control.
    /// </summary>
    public partial class RouteOverviewControl : UserControl
    {
        #region Constants

        /// <summary>
        ///     The default height active.
        /// </summary>
        private const int c_DefaultHeightActive = 24;

        /// <summary>
        ///     The default height inactive.
        /// </summary>
        private const int c_DefaultHeightInactive = 12;

        #endregion

        #region Static Fields

        /// <summary>
        ///     The route overview items property.
        /// </summary>
        public static readonly DependencyProperty RouteOverviewItemsProperty =
            DependencyProperty.Register(
                "RouteOverviewItems", 
                typeof(ObservableCollection<RouteOverviewItem>), 
                typeof(RouteOverviewControl), 
                new PropertyMetadata(null, RouteOverviewItemsCallback));

        /// <summary>
        ///     The control reference.
        /// </summary>
        private static RouteOverviewControl controlReference = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteOverviewControl" /> class.
        /// </summary>
        public RouteOverviewControl()
        {
            InitializeComponent();

            // Initialize items
            this.RouteOverviewItems = new ObservableCollection<RouteOverviewItem>();
            this.Items = new List<RouteOverviewItem>();

            // Initialize the unit
            this.Unit = RouteOverviewBaseUnit.Time;

            // Set default value for height
            this.HeightActive = c_DefaultHeightActive;
            this.HeightInactive = c_DefaultHeightInactive;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the height active.
        /// </summary>
        /// <value>
        ///     The height active.
        /// </value>
        public int HeightActive { get; set; }

        /// <summary>
        ///     Gets or sets the height inactive.
        /// </summary>
        /// <value>
        ///     The height inactive.
        /// </value>
        public int HeightInactive { get; set; }

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        public List<RouteOverviewItem> Items { get; set; }

        /// <summary>
        ///     Gets or sets the route overview items.
        /// </summary>
        public ObservableCollection<RouteOverviewItem> RouteOverviewItems
        {
            get
            {
                return (ObservableCollection<RouteOverviewItem>)GetValue(RouteOverviewItemsProperty);
            }

            set
            {
                SetValue(RouteOverviewItemsProperty, value);
            }
        }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public RouteOverviewBaseUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The route overview items callback.
        /// </summary>
        /// <param name="dependencyObject">
        /// The dependency object.
        /// </param>
        /// <param name="dependencyPropertyChangedEventArgs">
        /// The dependency property changed event args.
        /// </param>
        public static void RouteOverviewItemsCallback(
            DependencyObject dependencyObject, 
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((ObservableCollection<RouteOverviewItem>)dependencyPropertyChangedEventArgs.NewValue).CollectionChanged +=
                (sender, args) =>
                    {
                        controlReference = (RouteOverviewControl)dependencyObject;

                        if (args.NewItems != null)
                        {
                            foreach (RouteOverviewItem newItem in args.NewItems)
                            {
                                newItem.PropertyChanged += NewItemOnPropertyChanged;
                            }
                        }

                        if (args.OldItems != null)
                        {
                            foreach (RouteOverviewItem newItem in args.OldItems)
                            {
                                newItem.PropertyChanged -= NewItemOnPropertyChanged;
                            }
                        }

                        controlReference.Refresh();
                    };
        }

        /// <summary>
        ///     The refresh.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public void Refresh()
        {
            // Clear stackpanel
            this.pnlContainer.Children.Clear();

            // Calculate total duration
            double totalDuration = (double)this.RouteOverviewItems.Sum(r => r.Duration.Ticks);
            double totalDistance = this.RouteOverviewItems.Sum(r => r.Distance);

            var p = VisualTreeHelper.GetParent(this) as UIElement;
            var panel = (Panel)p;
            if (panel != null)
            {
                double parentWidth = panel.ActualWidth;

                // Loop through each items
                foreach (var currentItem in this.RouteOverviewItems)
                {
                    double ratioWidth;
                    switch (this.Unit)
                    {
                        case RouteOverviewBaseUnit.Distance:
                            ratioWidth = currentItem.Distance / totalDistance;
                            break;
                        case RouteOverviewBaseUnit.Time:
                            ratioWidth = currentItem.Duration.Ticks / totalDuration;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    // Create a rectangle associate to the current item
                    Rectangle rectangle = new Rectangle();
                    rectangle.VerticalAlignment = VerticalAlignment.Bottom;
                    rectangle.Width = ratioWidth * parentWidth;
                    rectangle.Height = currentItem.Active ? this.HeightActive : this.HeightInactive;
                    rectangle.Fill = new SolidColorBrush(currentItem.Color);

                    this.pnlContainer.Children.Add(rectangle);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The new item on property changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="propertyChangedEventArgs">
        /// The property changed event args.
        /// </param>
        private static void NewItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Active")
            {
                controlReference.Refresh();
            }
        }

        #endregion
    }
}