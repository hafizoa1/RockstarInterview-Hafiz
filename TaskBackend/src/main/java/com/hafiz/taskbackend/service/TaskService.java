package com.hafiz.taskbackend.service;

import com.hafiz.taskbackend.entity.Task;
import com.hafiz.taskbackend.exceptions.ResourceNotFoundException;
import com.hafiz.taskbackend.repository.TaskRepository;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class TaskService {

    @Autowired
    TaskRepository taskRepository;

    public List<Task> getAllTasks() {
        return taskRepository.findAll();
    }

    public Task getTaskById(Long id) {
        return taskRepository.findById(id).orElse(null);
    }

    public void createTask(Task task) {
        taskRepository.save(task);
    }

    public void deleteTask(Long id) {
        taskRepository.deleteById(id);
    }

    public Task updateTask(Task updatedTask, Long id) {
        return taskRepository.findById(id)
            .map(task -> {
                task.setTaskName(updatedTask.getTaskName());
                task.setPriority(updatedTask.getPriority());
                task.setDescription(updatedTask.getDescription());
                task.setIsCompleted(updatedTask.getIsCompleted());
                return taskRepository.save(task);
            })
            .orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + id));
    }

}
