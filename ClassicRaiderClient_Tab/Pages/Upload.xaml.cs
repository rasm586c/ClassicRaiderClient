﻿using System;
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

namespace ClassicRaiderClient_Tab.Pages
{
    /// <summary>
    /// Interaction logic for Upload.xaml
    /// </summary>
    public partial class Upload : UserControl
    {
        enum UploadVisuals
        {
            Uploading,
            Idle
        }
        
        public Upload()
        {
            InitializeComponent();

            if (Properties.Settings.Default.Uploading)
            {
                BeginUpload();   
            }

            start_upload.Click += (s, e) => 
            {
                if (Properties.Settings.Default.Uploading) {

                    SetUploadVisuals(UploadVisuals.Idle);
                    GlobalVariables.Environment.AllowHandling = false;
                    Properties.Settings.Default.Uploading = false;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                }
                else
                {
                    BeginUpload(); 
                }
            };
        }

        private void BeginUpload()
        {
            GlobalVariables.Environment.AllowHandling = true;
            GlobalVariables.Environment.CreateFileWatcher(Properties.Settings.Default.Path);
            Properties.Settings.Default.Uploading = true;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            SetUploadVisuals(UploadVisuals.Uploading);
        }

        private void SetUploadVisuals(UploadVisuals visuals)
        {
            switch (visuals)
            {
                case UploadVisuals.Idle:
                    start_upload.Content = "Start Synchronisation";
                    uploading_progress.Text = "Click the button below to begin automatically synchronising with Classic Raider.";
                    progressring.IsActive = false;
                    break;

                case UploadVisuals.Uploading:
                    start_upload.Content = "Stop Synchronisation";
                    uploading_progress.Text = "ClassicRaiderProfile is now automatically synchronising. You may close the window.";
                    progressring.IsActive = true;
                    break;
            }
        }

    }
}
