using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using StudyAppThing.Misc;

namespace StudyAppThing.Views.UserControls
{
    public partial class LevelsView : UserControl
    {
        public LevelsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the scroll has changed.
        /// </summary>
        /// <remarks>
        /// This has to be done in code-behind and not a View Model because it is an event on a control.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollChanged(object? sender, ScrollChangedEventArgs e)
        {
            ScrollViewer viewer = (ScrollViewer)sender;
            ItemsControl items = (ItemsControl)viewer.Content;
            // the seperators hidden
            List<int> hidden = new List<int>();
            int index = 0;
            foreach (var item in items.GetLogicalChildren())
            {
                // the control's content presenter
                ContentPresenter p = (ContentPresenter)item;
                if (p.TransformedBounds != null)
                {
                    // check the seperator is hidden behind header
                    if (p.TransformedBounds.Value.Transform.M32 <= 120 
                        && p.Content.GetType().Name.Contains("Seperator"))
                    {
                        hidden.Add(index);
                        index++;
                    }
                }
            }
            // if there are hidden headers.
            if (hidden.Count > 0)
            {
                // set the unit
                CurrentUser.User.CurrentUnit = CurrentUser.User.CurrentCourse.Units[hidden.Last()];
            }
            // there are no hidden headers, set to 0 (first unit).
            else
            {
                // set the unit
                CurrentUser.User.CurrentUnit = CurrentUser.User.CurrentCourse.Units[0];
            }
        }
    }
}
