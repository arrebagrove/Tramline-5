﻿using TramlineFive.Common;
using TramlineFive.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Popups;
using TramlineFive.DataAccess.DomainLogic;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TramlineFive.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public SettingsViewModel SettingsViewModel { get; private set; }
        public Settings()
        {
            this.InitializeComponent();

            SettingsViewModel = new SettingsViewModel();
            this.DataContext = SettingsViewModel;
            this.Transitions = AnimationManager.SetUpPageAnimation();
        }

        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            FolderPicker picker = new FolderPicker();
            StorageFolder folder = await picker.PickSingleFolderAsync();

            if (folder != null)
            {
                string serialized = JsonConvert.SerializeObject(await FavouriteDO.AllAsync());
                StorageFile file = await folder.CreateFileAsync($"{Strings.AppName}_{DateTime.Now.ToString(Formats.Timestamp)}.t5d");
                await FileIO.WriteTextAsync(file, serialized);

                await new MessageDialog($"{Formats.ExportSuccess} - {file.Name}").ShowAsync();
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
        }

        private async void btnClearHistory_Click(object sender, RoutedEventArgs e)
        {
            await SettingsViewModel.ClearHistoryAsync();
            await new MessageDialog("Историята е изчистена успешно.").ShowAsync();
        }
    }
}
