using System.Collections.Generic;
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

            ShowResult();
        }

        private void ShowResult()
        {
            if (!_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.Search)) return;
            var text = (List<string>) _dataExchangeViewModel.Item(EnumExchangeViewmodel.Search);
            var temp = new StringBuilder();

            foreach (string s in text)
            {
                temp.AppendLine(s);

            }

            Result = temp.ToString();


            RaisePropertyChanged(ResultPropertyName);
        }

        private void ExitCommand()
        {
            Messenger.Default.Send(new NotificationMessage(this, "CloseSearch"));
        }


        #region Result

        /// <summary>
        /// The <see cref="Result" /> property's name.
        /// </summary>
        public const string ResultPropertyName = "Result";

        private string _result  ;

        /// <summary>
        /// Sets and gets the Result property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Result
        {
            get => _result;

            set
            {
                if (_result == value)
                {
                    return;
                }

                _result = value;
                RaisePropertyChanged(ResultPropertyName);
            }
        }

        #endregion

        #region ExitCommand

        private RelayCommand _exitRelayCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand MyCommand => _exitRelayCommand
                                         ?? (_exitRelayCommand = new RelayCommand(ExitCommand));

        
        #endregion



    }
}