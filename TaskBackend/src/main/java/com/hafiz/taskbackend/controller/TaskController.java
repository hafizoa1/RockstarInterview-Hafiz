package com.hafiz.taskbackend.controller;

import com.hafiz.taskbackend.entity.Task;
import com.hafiz.taskbackend.service.TaskService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("api/v1/taskController")
public class TaskController {

    @Autowired
    TaskService taskService;

    @GetMapping("/{id}")
    public Task getTask(@PathVariable Long id) {
        return taskService.getTaskById(id);
    }

    @PostMapping()
    public void createTask(@RequestBody Task task) {
        taskService.createTask(task);
    }

    @PutMapping("/{id}")
    public void updateTask(@RequestBody Task task, @PathVariable Long id) {
        taskService.updateTask(task, id);
    }

    @DeleteMapping("/{id}")
    public void deleteTask(@PathVariable Long id) {
        taskService.deleteTask(id);
    }
}

//todo: return response entities for these tasks so testing can be better.
//todo: gracefully handle errors.
