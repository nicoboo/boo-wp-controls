using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Boo.WP.Controls.ClientSample.Resources;

namespace Boo.WP.Controls.ClientSample
{
    using System.Windows.Input;
    using System.Windows.Media;

    using Boo.WP.Controls.Entities.Logic;

    public partial class MainPage : PhoneApplicationPage
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += (sender, args) => this.RouteOverviewControl.Refresh();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The btn active_ tap.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnActive_Tap(object sender, GestureEventArgs e)
        {
            var rand = new Random().Next(this.RouteOverviewControl.RouteOverviewItems.Count - 1);

            foreach (var routeOverviewItem in this.RouteOverviewControl.RouteOverviewItems)
            {
                routeOverviewItem.Active = false;
            }

            this.RouteOverviewControl.RouteOverviewItems[rand].Active = true;
        }

        /// <summary>
        /// The btn random_ tap.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnRandom_Tap(object sender, GestureEventArgs e)
        {
            var color = Colors.Blue;
            var rand = new Random().Next(1000);

            if (rand % 2 == 1)
            {
                color = Colors.Green;
            }

            this.RouteOverviewControl.RouteOverviewItems.Add(
                new RouteOverviewItem() { Active = false, Color = color, Distance = rand });
        }

        #endregion
    }
}