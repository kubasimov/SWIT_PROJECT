using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WPF.Enum;
using WPF.Interface;

namespace WPF.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly IDataExchangeViewModel _dataExchangeViewModel;

        public SearchViewModel(IDataExchangeViewModel dataExchangeViewModel)
        {
            _dataExchangeViewModel = dataExchangeViewModel;
            ShowTextResult();
        }

        private void ShowTextResult()
        {
            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.Search))
            {
                var temp = (Dictionary<string,string>) _dataExchangeViewModel.Item(EnumExchangeViewmodel.Search);
                var text = new StringBuilder();
                if (temp.Count>0)
                {
                    

                    foreach (var tempKey in temp)
                    {
                        text.AppendLine(tempKey.Key);
                        text.AppendLine("\t" + tempKey.Value);
                        text.AppendLine(Environment.NewLine);
                    }
                }
                else
                {
                    text.AppendLine("Nie znaleziono hasła w słownikach");
                }
                

                

                ShowText = text.ToString();

                RaisePropertyChanged(ShowTextPropertyName);
            }
        }
        private void ExecuteExitCommand()
        {
            Messenger.Default.Send(new NotificationMessage(this,"CloseSearch"));
        }


        #region ShowText
        /// <summary>
        /// The <see cref="ShowText" /> property's name.
        /// </summary>
        public const string ShowTextPropertyName = "ShowText";

        private string _showText ;

        /// <summary>
        /// Sets and gets the ShowText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ShowText
        {
            get
            {
                return _showText;
            }

            set
            {
                if (_showText == value)
                {
                    return;
                }

                _showText = value;
                RaisePropertyChanged(ShowTextPropertyName);
            }
        }
        #endregion

        #region ExitCommand

        private RelayCommand _exitRelayCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand ExitCommand => _exitRelayCommand
                                           ?? (_exitRelayCommand = new RelayCommand(ExecuteExitCommand));

        

        #endregion
    }
}