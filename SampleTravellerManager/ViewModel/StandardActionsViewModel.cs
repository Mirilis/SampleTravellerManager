using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NWCSampleManager;
using SampleTravellerManager.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SampleTravellerManager.ViewModel
{
    public class StandardActionsViewModel : ViewModelBase
    {
        #region Private Fields

        private RelayCommand command_Copy;
        private RelayCommand command_Delete;
        private RelayCommand command_Load;
        private RelayCommand command_New;
        private RelayCommand command_Save;
        private RelayCommand command_Settings;
        public RelayCommand Command_Settings
        {
            get
            {
                return command_Settings
                    ?? (command_Settings = new RelayCommand(
                                          () =>
                                          {
                                              OnSettingsCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        #endregion Private Fields

        #region Public Properties

        public event EventHandler OnCopyCommand;
        public event EventHandler OnLoadCommand;
        public event EventHandler OnDeleteCommand;
        public event EventHandler OnNewCommand;
        public event EventHandler OnSaveCommand;
        public event EventHandler OnCloseCommand;
        public event EventHandler OnSettingsCommand;

        private RelayCommand command_Close;
        public RelayCommand Command_Close
        {
            get
            {
                return command_Close
                    ?? (command_Close = new RelayCommand(
                                          () =>
                                          {
                                              OnCloseCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        public RelayCommand Command_Copy
        {
            get
            {
                return command_Copy
                    ?? (command_Copy = new RelayCommand(
                                          () =>
                                          {
                                              OnCopyCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        public RelayCommand Command_Delete
        {
            get
            {
                return command_Delete
                    ?? (command_Delete = new RelayCommand(
                                          () =>
                                          {
                                              OnDeleteCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }

        public RelayCommand Command_Load
        {
            get
            {
                return command_Load
                    ?? (command_Load = new RelayCommand(
                                          () =>
                                          {
                                              OnLoadCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }
        public RelayCommand Command_New
        {
            get
            {
                return command_New
                    ?? (command_New = new RelayCommand(
                                          () =>
                                          {
                                              OnNewCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }
        public RelayCommand Command_Save
        {
            get
            {
                return command_Save
                    ?? (command_Save = new RelayCommand(
                                          () =>
                                          {
                                              OnSaveCommand(this, new EventArgs());
                                          },
                                          () => true));
            }
        }


        
        #endregion Public Properties
    }
}
