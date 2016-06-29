using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows;
using System.Threading;

namespace SecKeyTest_Folomeev.Models
{
    class CodeGenerator
    {
        Views.ProgressViewWindow progressWindow;    
        public bool generatorStatus;        
        public ObservableCollection<ViewModels.RecordViewModel> localRecordList { get; set; }
        private int generated;
        Random rnd;

        public CodeGenerator()
        {                        
            rnd = new Random();
        }

        private ViewModels.RecordViewModel SingleRecord()
        {            
             ViewModels.RecordViewModel singleRecord = new ViewModels.RecordViewModel(generated.ToString("D6"), RandomString(9), rnd.Next(10000,100000), rnd.Next(-1000,1000));
             return singleRecord;
        }

        private void OtherWindowShow()
        {
            
            
            
        }

        public void DoGenerationAsync(int count)
        {
            progressWindow = new Views.ProgressViewWindow();
            var generationThread = new Task(() => GenerateRecords(count));
            generationThread.Start();
                 
            progressWindow.ShowDialog();
        }


        private async void GenerateRecords(int count)
        {                                   
            localRecordList = new ObservableCollection<ViewModels.RecordViewModel>();            

            generated = 0;

            while (generated < count && generatorStatus)
            {
                lock (localRecordList)
                { 
                localRecordList.Add(SingleRecord());
                generated++;
                }

                // Опрашиваем состояние прогресса через каждую 50-ю запись, чтобы не задерживать процесс генерации
                if (generated % 50 == 0)
                {

                    if (!Application.Current.Dispatcher.CheckAccess())
                    {
                        await Application.Current.Dispatcher.BeginInvoke(
                        new Action(() =>
                        {                            
                            progressWindow.label1.Content = generated * 100 / count + "%";
                            progressWindow.prgBar.Value = generated * 100 / count;
                        }));
                    }
                }
            }

            if (!Application.Current.Dispatcher.CheckAccess())
            {
                await Application.Current.Dispatcher.BeginInvoke(
                new Action(() => {
                    progressWindow.Close();
                    GC.Collect();
                }));
            }

        }

        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <returns>Random string</returns>
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();            
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65)));
                builder.Append(ch);
            }            
            return builder.ToString();
        }
    }
}
