using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Syncfusion.Windows.Tools.Controls;
using Core.Interface;
using Core.Model;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF.Interface;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media;
using Core.Implement;
using morfologik.stemming;
using morfologik.stemming.polish;
using WPF.Enum;
using WPF.View;

namespace WPF.ViewModel
{




    public class MainViewModel : ViewModelBase
    {
        private readonly ICoreOcr _coreOcr;
        private readonly IDataService _dataService;
        private readonly IDataExchangeViewModel _dataExchangeViewModel;

        public MainViewModel(ICoreOcr coreOcr,IDataService dataService, IDataExchangeViewModel dataExchangeViewModel)
        {
            _coreOcr = coreOcr;
            _dataService = dataService;
            _dataExchangeViewModel = dataExchangeViewModel;
        }

        private void ExecuteSearchCommand()
        {
            var speller = new PolishStemmer();

            var text = speller.lookup(_searchText).toArray().FirstOrDefault() as WordData;

            IDslDictionaries DslDictionaries = new DslDictionaries();

            var textFromDsl = DslDictionaries.SearchWordInDslDictionaries(text.getStem().toString());

            _dataExchangeViewModel.Add(EnumExchangeViewmodel.Search,textFromDsl);

            new SearchView().Show();

        }

        private void ExecuteExitCommand()
        {
            Messenger.Default.Send(new NotificationMessage(this, "CloseMain"));
        }


        private async void ExecuteOcrCommand()
        {
            await _coreOcr.LoadImage(_bitmapImage.UriSource.AbsolutePath);

            var text = await _coreOcr.OcrPages("pol", 2);

            var page = await _coreOcr.DecodeHocr(text);

            _documentAdv = LoadDocumentAdv(page);

            
            RaisePropertyChanged(DocumentADVPropertyName);
        }


        private void ExecuteOpenCommand()
        {
            _bitmapImage = _dataService.LoadImage();

            RaisePropertyChanged(BitmapImagePropertyName);
        }

        private void ExecuteShowParagraphCommand()
        {
            foreach (var sectionAdv in _documentAdv.Sections)
            {
                foreach (var blockAdv in sectionAdv.Blocks)
                {
                    foreach (var inline1 in blockAdv.Inlines)
                    {
                        var inline = (SpanAdv)inline1;
                        inline.Foreground = Colors.Red;
                    }
                }
            }

            RaisePropertyChanged(DocumentADVPropertyName);
        }

        private DocumentAdv LoadDocumentAdv(List<TextPage> pages)
        {
            var DocumentAdv = new DocumentAdv();
            SectionAdv sectionAdv = new SectionAdv();
            DocumentAdv.Sections.Add(sectionAdv);

            foreach (var textPage in pages)
            {
                foreach (var paragraph in textPage.Paragraphs)
                {
                    var paragraphAdv = new ParagraphAdv();
                    sectionAdv.Blocks.Add(paragraphAdv);

                    foreach (var line in paragraph.Lines)
                    {
                        foreach (var word in line.Words)
                        {

                            SpanAdv spanAdv = new SpanAdv { Text = word.Word + " " };
                            if (word.Bold) spanAdv.FontWeight = FontWeights.Bold;
                            paragraphAdv.Inlines.Add(spanAdv);


                        }
                    }


                }
            }
            return DocumentAdv;
        }

        #region Command

        private RelayCommand _exitRelayCommand;

        public RelayCommand ExitCommand
        {
            get
            {
                return _exitRelayCommand
                    ?? (_exitRelayCommand = new RelayCommand(ExecuteExitCommand));
            }
        }

        private RelayCommand _ocrRelayCommand;

        public RelayCommand OcrCommand
        {
            get
            {
                return _ocrRelayCommand
                    ?? (_ocrRelayCommand = new RelayCommand(ExecuteOcrCommand));
            }
        }

        private RelayCommand _openRelayCommand;

        public RelayCommand OpenCommand
        {
            get
            {
                return _openRelayCommand
                       ?? (_openRelayCommand = new RelayCommand(ExecuteOpenCommand));
            }
        }

        private RelayCommand _showParagraphCommand;

        public RelayCommand ShowParagraphCommand => _showParagraphCommand
                                                    ?? (_showParagraphCommand = new RelayCommand(ExecuteShowParagraphCommand));

        private RelayCommand _searchRelayCommand;
        
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchRelayCommand
                    ?? (_searchRelayCommand = new RelayCommand(ExecuteSearchCommand));
            }
        }

       
        #endregion

        #region DocumentAdv
        /// <summary>
        /// The <see cref="DocumentAdv" /> property's name.
        /// </summary>
        public const string DocumentADVPropertyName = "DocumentAdv";

        private DocumentAdv _documentAdv;

        /// <summary>
        /// Sets and gets the DocumentAdv property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DocumentAdv DocumentAdv
        {
            get => _documentAdv;

            set
            {
                if (_documentAdv == value)
                {
                    return;
                }

                _documentAdv = value;
                RaisePropertyChanged(DocumentADVPropertyName);
            }
        }
        #endregion

        #region BitmapImage
        /// <summary>
        /// The <see cref="BitmapImage" /> property's name.
        /// </summary>
        public const string BitmapImagePropertyName = "BitmapImage";

        private BitmapImage _bitmapImage;

        /// <summary>
        /// Sets and gets the BitmapImage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public BitmapImage BitmapImage
        {
            get => _bitmapImage;

            set
            {
                if (_bitmapImage == value)
                {
                    return;
                }

                _bitmapImage = value;
                RaisePropertyChanged(BitmapImagePropertyName);
            }
        }
        #endregion

        #region SearchText

        /// <summary>
        /// The <see cref="SearchText" /> property's name.
        /// </summary>
        public const string SearchTextPropertyName = "SearchText";

        private string _searchText ;

        /// <summary>
        /// Sets and gets the SearchText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (_searchText == value)
                {
                    return;
                }

                _searchText = value;
                RaisePropertyChanged(SearchTextPropertyName);
            }
        }

        #endregion
        
    }
}