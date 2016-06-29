using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace SecKeyTest_Folomeev.ViewModels
{
    class MainViewModel : INotifyPropertyChanged    
    {
        
        public Models.CodeGenerator cg = new Models.CodeGenerator(); // Экземляр генератора записей
        Models.IOxml XmlSaver = new Models.IOxml(); // экземпляр ввода-вывода XML

        public Command GenerateRecords { get; set; } // Команда для кнопки генерации
        public Command SaveXML { get; set; } // команда для сохранения записей
        public Command OpenXML { get; set; } // команда для открытия записей

        int recordsCount { get; set; } // Переменная требуемого количества записей

        /// <summary>
        /// Список записей
        /// </summary>
        private ObservableCollection<RecordViewModel> _RecordList;
        public ObservableCollection<RecordViewModel> RecordList 
        {
            get { return _RecordList; }
            set
            {
                if (value != _RecordList)
                {
                    _RecordList = value;                    
                    NotifyPropertyChanged("RecordList");
                }
            }
        }


        /// <summary>
        /// Контент для общего количества записей
        /// </summary>
        private string _count;
        public string count
        {
            get { return _count; }
            set
            {
                if (value != _count)
                {
                    _count = value;                    
                    NotifyPropertyChanged("count");
                }
            }
        }

        /// <summary>
        /// Переменная процесса генерации
        /// </summary>
        private bool _generationProcess;
        public bool generationProcess
        {
            get { return _generationProcess; }
            set
            {
                if (value != _generationProcess)
                {
                    _generationProcess = value;
                    cg.generatorStatus = _generationProcess;                    
                    NotifyPropertyChanged("generationProcess");
                }
            }
        }

        /// <summary>
        /// Проверка на ввод символа - допускается только цифра
        /// </summary>
        private string _recordsInput;
        public string recordsInput
        {
            get { return _recordsInput; }
            set
            {
                if (value != _recordsInput && Models.DigitChecker.IsDigitAllowed(value))
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        if (Convert.ToUInt32(value) < 16777217 && Convert.ToUInt32(value) > 0)
                        {
                            _recordsInput = value;
                            recordsCount = Convert.ToInt32(_recordsInput);
                            NotifyPropertyChanged("recordsInput");
                        }
                    }
                    else { _recordsInput = ""; }
                }                
            }
        }

        /// <summary>
        /// Строка статуса
        /// </summary>
        private string _status;
        public string status
        {
            get { return _status; }
            set
            {
                if (value != _status)
                {
                    _status = value;                    
                    NotifyPropertyChanged("status");
                }
            }
        }

        /// <summary>
        /// Текст для кнопки генерации
        /// </summary>
        private string _buttonText;
        public string buttonText
        {
            get { return _buttonText; }
            set
            {
                if (value != _buttonText)
                {
                    _buttonText = value;
                    NotifyPropertyChanged("buttonText");
                }
            }
        }

        /// <summary>
        /// Текст для выбранной строки
        /// </summary>
        private string _selectedRow;
        public string selectedRow
        {
            get { return _selectedRow; }
            set
            {
                if (value != _selectedRow)
                {
                    _selectedRow = value;
                    NotifyPropertyChanged("selectedRow");
                }
            }
        }

        /// <summary>
        /// Выбранная запись в датагриде
        /// </summary>
        private RecordViewModel _selectedRecord;   
        public RecordViewModel selectedRecord       
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;
                try { selectedRow = "Cтрока № " + _selectedRecord.Id; }
                catch { }
                
                NotifyPropertyChanged("selectedRecord");
            }
        }        

        /// <summary>
        /// Метод генерации записей в отдельном потоке
        /// </summary>
        public void StartGeneration()
        {


            if (generationProcess)
            {
                generationProcess = false;
            }
            else
            {                            
                buttonText = "Остановить";
                status = "Идет генерация записей";
                generationProcess = true;

                if (RecordList != null) { RecordList.Clear(); }

                cg.DoGenerationAsync(recordsCount);

                RecordList = cg.localRecordList;

                generationProcess = false;
                status = "Ожидание начала работы";
                buttonText = "Генерировать";
                count = "Всего: " + RecordList.Count;
                GC.Collect();
            }            
        }

        /// <summary>
        /// Метод сохранения записей
        /// </summary>
        private void SaveToXml()
        {            
            XmlSaver.SaveXML(RecordList);
        }

        /// <summary>
        /// Метод загрузки записей из файла
        /// </summary>
        private void OpenXml()
        {
            RecordList = XmlSaver.OpenXml();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainViewModel()
        {
            GenerateRecords = new Command(StartGeneration) { IsExecutable = true };
            SaveXML = new Command(SaveToXml) { IsExecutable = true };
            OpenXML = new Command(OpenXml) { IsExecutable = true };
            status = "Ожидание начала работы";
            buttonText = "Генерировать";
        }

        protected virtual void NotifyPropertyChanged(string propertyName)
        {

            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
