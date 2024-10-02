using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using TaskTrackerFrontend.Models;

namespace TaskTrackerFrontend.ViewModels
{
    public class TaskViewModel : ReactiveObject
    {
        private string _taskName = string.Empty;
        private TaskModel? _selectedTask;

        public TaskViewModel()
        {
            Tasks = new ObservableCollection<TaskModel>();

            var canAddTask = this.WhenAnyValue(
                x => x.TaskName,
                (string name) => !string.IsNullOrWhiteSpace(name)
            );

            AddTaskCommand = ReactiveCommand.Create(AddTask, canAddTask);

            var canDeleteTask = this.WhenAnyValue(
                x => x.SelectedTask,
                (TaskModel? task) => task != null
            );

            DeleteTaskCommand = ReactiveCommand.Create(DeleteTask, canDeleteTask);
        }

        public ObservableCollection<TaskModel> Tasks { get; }

        public string TaskName
        {
            get => _taskName;
            set => this.RaiseAndSetIfChanged(ref _taskName, value);
        }

        public TaskModel? SelectedTask
        {
            get => _selectedTask;
            set => this.RaiseAndSetIfChanged(ref _selectedTask, value);
        }

        public ReactiveCommand<Unit, Unit> AddTaskCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteTaskCommand { get; }

        private void AddTask()
        {
            Tasks.Add(new TaskModel { TaskName = TaskName });
            TaskName = string.Empty;
        }

        private void DeleteTask()
        {
            if (SelectedTask != null)
            {
                Tasks.Remove(SelectedTask);
            }
        }
    }
}