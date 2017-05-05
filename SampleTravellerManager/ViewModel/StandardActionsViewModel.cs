using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NWCSampleManager;
using SampleTravelerManager.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SampleTravelerManager.ViewModel
{
    public class StandardActionsViewModel : ViewModelBase
    {
        #region Private Fields

        private RelayCommand commandClose;
        private RelayCommand commandCopy;
        private RelayCommand commandDelete;
        private RelayCommand commandLoad;
        private RelayCommand commandMinimize;
        private RelayCommand commandNew;
        private RelayCommand commandSave;
        private RelayCommand commandSettings;

        #endregion Private Fields

        #region Public Events

        public event EventHandler OnCloseCommand;

        public event EventHandler OnCopyCommand;

        public event EventHandler OnDeleteCommand;

        public event EventHandler OnLoadCommand;

        public event EventHandler OnNewCommand;

        public event EventHandler OnSaveCommand;

        public event EventHandler OnSettingsCommand;

        #endregion Public Events

        #region Public Properties

        public RelayCommand CommandClose
        {
            get
            {
                return commandClose
                    ?? (commandClose = new RelayCommand(
                                          () =>
                                          {
                                              OnCloseCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        public RelayCommand CommandCopy
        {
            get
            {
                return commandCopy
                    ?? (commandCopy = new RelayCommand(
                                          () =>
                                          {
                                              OnCopyCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        public RelayCommand CommandDelete
        {
            get
            {
                return commandDelete
                    ?? (commandDelete = new RelayCommand(
                                          () =>
                                          {
                                              OnDeleteCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        public RelayCommand CommandLoad
        {
            get
            {
                return commandLoad
                    ?? (commandLoad = new RelayCommand(
                                          () =>
                                          {
                                              OnLoadCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        public RelayCommand CommandMinimize
        {
            get
            {
                return commandMinimize
                    ?? (commandMinimize = new RelayCommand(
                                          () =>
                                          {
                                              Messenger.Default.Send<MinimizeWindows>(new MinimizeWindows());
                                          },
                                          () => true));
            }
        }

        public RelayCommand CommandNew
        {
            get
            {
                return commandNew
                    ?? (commandNew = new RelayCommand(
                                          () =>
                                          {
                                              OnNewCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        public RelayCommand CommandSave
        {
            get
            {
                return commandSave
                    ?? (commandSave = new RelayCommand(
                                          () =>
                                          {
                                              OnSaveCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        public RelayCommand CommandSettings
        {
            get
            {
                return commandSettings
                    ?? (commandSettings = new RelayCommand(
                                          () =>
                                          {
                                              // OnSettingsCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        #endregion Public Properties
    }
}