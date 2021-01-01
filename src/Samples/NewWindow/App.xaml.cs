﻿using System;
using System.Windows;

namespace Elmish.Uno.Samples.NewWindow
{
  public partial class App : Application
  {
    public App()
    {
      this.Activated += StartElmish;
    }

    private void StartElmish(object sender, EventArgs e)
    {
      this.Activated -= StartElmish;
      Program.main(MainWindow, () => new Window1(), () => new Window2());
    }

  }
}
