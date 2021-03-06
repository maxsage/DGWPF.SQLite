﻿using DG.WPF.SQLite.Model;
//using DG.WPF.QuizCreator.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AdditionalInfoViewModel : ViewModelBase
    {
        public static readonly string AdditionalInfoPreHtml = new ViewModelLocator().ParameterViewModel.PreHtml;
        public static readonly string AdditionalInfoPostHtml = new ViewModelLocator().ParameterViewModel.PostHtml;


        /// <summary>
        /// The <see cref="IsDirty" /> property's name.
        /// </summary>
        public const string IsDirtyPropertyName = "IsDirty";

        private bool _isDirty;

        /// <summary>
        /// Gets the IsDirty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }

            set
            {
                if (_isDirty == value)
                {
                    return;
                }

                _isDirty = value;
                RaisePropertyChanged(IsDirtyPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the AdditionalInfoFullHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AdditionalInfoFullHtml
        {
            get
            {
                return
                    AdditionalInfoPreHtml +
                    AdditionalInfoHtml +
                    AdditionalInfoPostHtml;
            }
        }

        /// <summary>
        /// Sets and gets the DisplayInFileSystem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool DisplayInReader
        {
            get
            {
                return
                    !DisplayInBrowser;
            }
        }

        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";
        private int _id;

        /// <summary>
        /// Sets and gets the Id property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                Set(IdPropertyName, ref _id, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="SubCategoryId" /> property's name.
        /// </summary>
        public const string SubCategoryIdPropertyName = "SubCategoryId";

        private int _subCategoryId;

        /// <summary>
        /// Sets and gets the SubCategoryId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SubCategoryId
        {
            get
            {
                return _subCategoryId;
            }
            set
            {
                Set(SubCategoryIdPropertyName, ref _subCategoryId, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="SequenceNumber" /> property's name.
        /// </summary>
        public const string SequenceNumberPropertyName = "SequenceNumber";

        private int _sequenceNumber;

        /// <summary>
        /// Sets and gets the SequenceNumber property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SequenceNumber
        {
            get
            {
                return _sequenceNumber;
            }
            set
            {
                Set(SequenceNumberPropertyName, ref _sequenceNumber, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="SectionDescription" /> property's name.
        /// </summary>
        public const string SectionDescriptionPropertyName = "SectionDescription";

        private string _sectionDescription;

        /// <summary>
        /// Sets and gets the SequenceNumber property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SectionDescription
        {
            get
            {
                return _sectionDescription;
            }
            set
            {
                Set(SectionDescriptionPropertyName, ref _sectionDescription, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="AdditionalInfoHtml" /> property's name.
        /// </summary>
        public const string AdditionalInfoHtmlPropertyName = "AdditionalInfoHtml";

        private string _additionalInfoHtml;

        /// <summary>
        /// Sets and gets the AdditionalInfoHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AdditionalInfoHtml
        {
            get
            {
                return _additionalInfoHtml;
            }
            set
            {
                Set(AdditionalInfoHtmlPropertyName, ref _additionalInfoHtml, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="AdditionalInfoUri" /> property's name.
        /// </summary>
        public const string AdditionalInfoUriPropertyName = "AdditionalInfoUri";

        private string _additionalInfoUri;

        /// <summary>
        /// Sets and gets the AdditionalInfoUri property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AdditionalInfoUri
        {
            get
            {
                return _additionalInfoUri;
            }
            set
            {
                Set(AdditionalInfoUriPropertyName, ref _additionalInfoUri, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="DisplayInBrowser" /> property's name.
        /// </summary>
        public const string DisplayInBrowserPropertyName = "DisplayInBrowser";

        private bool _displayInBrowser;

        /// <summary>
        /// Sets and gets the DisplayInBrowser property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool DisplayInBrowser
        {
            get
            {
                return _displayInBrowser;
            }
            set
            {
                Set(DisplayInBrowserPropertyName, ref _displayInBrowser, value);
                IsDirty = true;
            }
        }

    }
}