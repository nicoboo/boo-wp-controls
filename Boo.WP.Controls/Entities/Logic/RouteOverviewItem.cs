// -----------------------------------------------------------------------
//  <copyright file="RouteOverviewItem.cs" company="Ketto">
//      Copyright (c) Ketto. All rights reserved.
//  </copyright>
//  <author>Nicolas Boonaert</author>
// -----------------------------------------------------------------------
namespace Boo.WP.Controls.Entities.Logic
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    using Boo.WP.Controls.Properties;

    /// <summary>
    ///     The route overview item.
    /// </summary>
    public class RouteOverviewItem : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        ///     The active.
        /// </summary>
        private bool active = false;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteOverviewItem" /> class.
        /// </summary>
        public RouteOverviewItem()
        {
            this.Active = false;
            this.Color = new Color() { A = 255, R = 255, G = 0, B = 0 };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteOverviewItem"/> class.
        /// </summary>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <param name="distance">
        /// The distance.
        /// </param>
        public RouteOverviewItem(TimeSpan duration, double distance)
        {
            this.Duration = duration;
            this.Distance = distance;
            this.Active = false;
            this.Color = new Color() { A = 255, R = 255, G = 0, B = 0 };
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether [active].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [active]; otherwise, <c>false</c>.
        /// </value>
        public bool Active
        {
            get
            {
                return this.active;
            }

            set
            {
                if (this.Active != value)
                {
                    this.active = value;

                    // Fire the nofity property changed
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the distance.
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        public TimeSpan Duration { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}