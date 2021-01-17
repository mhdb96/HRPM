using HRPMonitor.ICallers;
using HRPMSharedLibrary.Models;
using System;
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
using System.Windows.Shapes;

namespace HRPMonitor.Views
{
    /// <summary>
    /// Interaction logic for TasksWindow.xaml
    /// </summary>
    public partial class TasksWindow : Window
    {

        WorkTask _curentTask;
        List<WorkTask> _workTasks;
        ITasksWindowCaller caller;
        public TasksWindow(WorkTask curentTask, List<WorkTask> workTasks, ITasksWindowCaller callindWindow)
        {
            caller = callindWindow;
            _curentTask = curentTask;
            _workTasks = workTasks;
            InitializeComponent();
            tasksList.ItemsSource = workTasks;       
            if(curentTask == null)
            {
                newTaskBtn.Visibility = Visibility.Visible;
                SaveTaskBtn.Visibility = Visibility.Hidden;
                newTaskDetails.Visibility = Visibility.Hidden;
            }     
            else
            {
                descriptionText.Text = curentTask.Description;
                titleText.Text = curentTask.Title;
                newTaskBtn.Visibility = Visibility.Hidden;
                SaveTaskBtn.Visibility = Visibility.Visible;
                newTaskDetails.Visibility = Visibility.Visible;
                createTaskBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void createTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            _curentTask = new WorkTask();
            _curentTask.Title = titleText.Text;
            _curentTask.Description = descriptionText.Text;
            _curentTask.StartTime = DateTime.Now;           
            caller.TaskCreated(_curentTask);
            Close();
        }

        private void newTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            newTaskBtn.Visibility = Visibility.Hidden;
            newTaskDetails.Visibility = Visibility.Visible;
            createTaskBtn.Visibility = Visibility.Visible;
            descriptionText.Text = null;
            titleText.Text = null;
        } 

        private void SaveTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            _curentTask.EndTime = DateTime.Now;
            _workTasks.Add(_curentTask);
            caller.TaskSaved(_curentTask);
            tasksList.ItemsSource = null;
            tasksList.Items.Clear();
            tasksList.ItemsSource = _workTasks;
            newTaskBtn.Visibility = Visibility.Visible;
            SaveTaskBtn.Visibility = Visibility.Hidden;
            newTaskDetails.Visibility = Visibility.Hidden;            
        }
    }
}
