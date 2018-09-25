using System;
using System.Configuration;
using System.Windows.Forms;
using Triage.Mortician.IntegrationTest;
using static System.IO.Path;

namespace Triage.TestApplications.WinForms
{
    public partial class MyForm : Form
    {
        public MyForm()
        {
            InitializeComponent();
            var button = new Button
            {
                Text = "Hello World"
            };
            button.Click += (sender, args) => button.Text = "Clicked!";
            Controls.Add(button);
            DumpHelper.CreateDump(Combine(
                ConfigurationManager.AppSettings[IntPtr.Size == 4 ? "DumpLocationX86" : "DumpLocationX64"],
                "helloworld.dmp"));
        }
    }
}